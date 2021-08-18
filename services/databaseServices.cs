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

   public sealed class Status {
		public const int OK = 200;
		public const int Inserted = 201;
		public const int NoContent = 204;
      public const int DocumentFound = 302;
		public const int NotModified = 304;         
      public const int BadRequest = 400;
      public const int Unauthorised = 401;
      public const int Forbidden = 403;
      public const int NotFound = 404;
      public const int ServerError = 500;
   }

   #nullable enable
	public class Document<DocumentType> {
		private DocumentType? _requestObject { get; set; }
      private IMongoCollection<DocumentType> _collection { get; set; }
		public FilterDefinitionBuilder<DocumentType> Builder { get { return Builders<DocumentType>.Filter; } }
		public Document(HttpRequest request, string collectionName) {
			_requestObject = Http.Decode<DocumentType>(request);
			_collection = Mongo.database.GetCollection<DocumentType>(collectionName);
		}

      public Document(string collectionName) {
         _collection = Mongo.database.GetCollection<DocumentType>(collectionName);         
      }

      public List<DocumentType> FetchOne(FilterDefinition<DocumentType> filter) {
			return _collection.Find(filter).ToList();
      }

      private bool _isDocumentExist(FilterDefinition<DocumentType> filter) {
			return FetchOne(filter).Count > 0 ? true : false;
		}

      public int Insert(DocumentType document, FilterDefinition<DocumentType> filter) {
         if (!_isDocumentExist(filter)) {
				_collection.InsertOne(document);
				return Status.Inserted;
			}

			return Status.DocumentFound;
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
				T? document = FetchOne(filter);
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

			public T? FetchOne(FilterDefinition<T> filter) {
				List<T> doc = collection.Find(filter).ToList();
				return doc.Count > 0 ? doc[0] : default(T);
			}
		}
	}
}