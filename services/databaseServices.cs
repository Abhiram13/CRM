using System;
using MongoDB.Driver;
using CRM;
using Models;
using System.Collections.Generic;

namespace DataBase {
   #nullable enable
   public abstract class DatabaseOperations<DataType> {
		private DocumentStructure<DataType>? _document;
		public IMongoCollection<DataType>? _collection;
		public DatabaseOperations(DocumentStructure<DataType> document) {
			_document = document;
         _collection = Mongo.database.GetCollection<DataType>(document.Collection);
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
   }
}