using System;
using MongoDB.Driver;
using CRM;
using Models;
using System.Collections.Generic;
using MongoDB.Driver.Linq;

namespace DataBase {
   #nullable enable
   public abstract class DatabaseOperations<DataType> {
		private DocumentStructure<DataType>? _document;
		public IMongoCollection<DataType>? _collection;
		public DatabaseOperations(DocumentStructure<DataType> document) {
			_document = document;
         _collection = Mongo.database.GetCollection<DataType>(document.Collection);
		}

      public DatabaseOperations() {}

      public List<DataType> Test() {
			// ProjectionDefinition<DataType> x = Builders<DataType>.Projection.Exclude(emp => emp["firstname"]);
			List<DataType> list = _collection.Find(_document.filter).Project<DataType>(_document.project).ToList();
			return list;
		}

      public List<DataType> FetchOne() {
         return _collection.Find(_document?.filter).ToList();
      }

		private bool _isDocumentExist() {
			return FetchOne().Count > 0;
		}

      public ResponseModel Insert() {
         if (_document == null || _document.RequestBody == null) {
				return new ResponseModel(StatusCode.BadRequest);
			} else if (!_isDocumentExist()) {
				_collection?.InsertOne(_document.RequestBody);
				return new ResponseModel(StatusCode.Inserted);
			}

			return new ResponseModel(StatusCode.DocumentFound);
		}

      public ResponseModel UpdateOne() { 
         if (_collection != null && _document != null) {
				UpdateResult result = _collection.UpdateOne(_document.filter, _document.update);     
            // _collection.AsQueryable().Where()
            if (result.MatchedCount > 0) {
					return new ResponseModel(StatusCode.OK);
				}

				return new ResponseModel(StatusCode.NotModified);
			}

			return new ResponseModel(StatusCode.BadRequest);
		}
   }
}