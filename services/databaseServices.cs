using System;
using MongoDB.Driver;
using MongoDB.Bson;
using CRM;
using Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DataBase {
   #nullable enable
	public class Docu<DocumentType> {
		private DocumentType? _requestObject { get; set; }
      private IMongoCollection<DocumentType> _collection { get; set; }
		public FilterDefinitionBuilder<DocumentType> Builder { get { return Builders<DocumentType>.Filter; } }
		public Docu(HttpRequest request, string collectionName) {
			_requestObject = RequestBody.Decode<DocumentType>(request);
			_collection = Mongo.database.GetCollection<DocumentType>(collectionName);
		}

      public Docu(string collectionName) {
         _collection = Mongo.database.GetCollection<DocumentType>(collectionName);         
      }

      public List<DocumentType> FetchOne(FilterDefinition<DocumentType> filter) {
			return _collection.Find(filter).ToList();
      }

      private bool _isDocumentExist(FilterDefinition<DocumentType> filter) {
			return FetchOne(filter).Count > 0 ? true : false;
		}

      public ResponseModel Insert(FilterDefinition<DocumentType> filter) {
			if (_requestObject == null) {
				return new ResponseModel(StatusCode.BadRequest);
			} else if (!_isDocumentExist(filter)) {
				_collection.InsertOne(_requestObject);
				return new ResponseModel(StatusCode.Inserted);
			}

			return new ResponseModel(StatusCode.DocumentFound);
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