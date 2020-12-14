using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM
{
   public class Document<collectionType>
   {
      private IMongoCollection<collectionType> db;
      public Document(string collectionName)
      {
         this.db = Mongo.database.GetCollection<collectionType>(collectionName);
      }

      public void Insert(collectionType doc)
      {
         this.db.InsertOne(doc);
      }

      public string FetchAll()
      {
         List<collectionType> list = this.db.Find<collectionType>(new BsonDocument()).ToList();
         string json = JsonSerializer.Serialize<List<collectionType>>(list);
         return json;
      }
   }
}

// new Document("employee").Insert(new IEmployee());