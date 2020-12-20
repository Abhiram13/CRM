using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM
{
   public class Database<DocumentType>
   {
      private IMongoCollection<DocumentType> collection;

      public Database(string CollectionName)
      {
         this.collection = Mongo.database.GetCollection<DocumentType>(CollectionName);
      }

      public void Insert(DocumentType doc)
      {
         collection.InsertOne(doc);
      }

      public async Task<string> FetchAll()
      {
         List<DocumentType> list = await collection.Find<DocumentType>(new BsonDocument()).ToListAsync<DocumentType>();
         string str = JsonSerializer.Serialize<List<DocumentType>>(list);

         return str;
      }
   }
}