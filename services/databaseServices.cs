using System;
using MongoDB.Driver;
using MongoDB.Bson;
using CRM;
using Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DataBase {
	public static class Http {
		/// <summary>
		/// Returns document object from decoding http request
		/// </summary>
		/// <typeparam name="DocumentType">Type of document that need to be decoded</typeparam>
		/// <param name="request">Http Request</param>
		public static DocumentType Decode<DocumentType>(HttpRequest request) {
			return JSON.httpContextDeseriliser<DocumentType>(request).Result;
		}
	}

	public class Document<DocumentType> {
		private DocumentType document;
		public Document(HttpRequest request) {
			document = Http.Decode<DocumentType>(request);
		}
	}
}

namespace Services {
	namespace DatabaseManagement {
		public class Document<T> {
			private IMongoCollection<T> collection;
			public FilterDefinitionBuilder<T> builders;

			public Document(string collectionName) {
				collection = Mongo.database.GetCollection<T>(collectionName);
				builders = Builders<T>.Filter;
			}

			public bool isDocumentExist(FilterDefinition<T> filter) {
				T document = FetchOne(filter);
				return document == null;
			}

			public short Insert(T document) {
				collection.InsertOne(document);
				return 200;
			}

			public short Insert(T document, FilterDefinition<T> filter) {
				try {
					if (isDocumentExist(filter)) {
						collection.InsertOne(document);
						return 200;
					}

					return 302;
				} catch (MongoException error) {
					Console.WriteLine(error);
					return 500;
				}
			}

			public List<T> FetchAll() {
				return collection.Find(new BsonDocument()).ToList();
			}

			public T FetchOne(FilterDefinition<T> filter) {
				List<T> doc = collection.Find(filter).ToList();
				return doc.Count > 0 ? doc[0] : default(T);
			}
		}
	}
}