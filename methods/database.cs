using System;
using MongoDB.Driver;

namespace CRM {
   public class Document<collectionType, documentType> {
      private IMongoCollection<collectionType> db;
      public Document(string collectionName) {
         this.db = Mongo.database.GetCollection<collectionType>(collectionName);
      }

      public void Insert(collectionType doc) {
         this.db.InsertOne(doc);
      }
   }
}

// new Document("employee").Insert(new IEmployee());