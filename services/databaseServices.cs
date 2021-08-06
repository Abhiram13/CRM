using System;
using MongoDB.Driver;
using MongoDB.Bson;
using CRM;
using Models;
using System.Collections.Generic;

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