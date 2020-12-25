using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM {
   public class Database<DocumentType> {
      private IMongoCollection<DocumentType> collection;

      public Database(string CollectionName) {
         this.collection = Mongo.database.GetCollection<DocumentType>(CollectionName);
      }

      public void Insert(DocumentType doc) {
         foreach (var key in doc.GetType().GetProperties()) {
            if (key.GetValue(doc) is DateTime) {
               this.Filter(key.GetValue(doc).ToString());
            }
         }         
         collection.InsertOne(doc);
      }

      private void FetchDate() {
         //4/28/2020 12:00:00 AM
      }

      public async Task<string> FetchAll() {
         List<DocumentType> list = await collection.Find<DocumentType>(new BsonDocument()).ToListAsync<DocumentType>();
         string str = JsonSerializer.Serialize<List<DocumentType>>(list);

         return str;
      }

      public void Filter(string date) {
         DateTime now = new DateTime(2020, 10, 11);
         var filter = Builders<ILifeTransaction>.Filter.Gte(x => x.ENTRY_DATE, DateTime.Parse(date));
         List<ILifeTransaction> list = Mongo.database.GetCollection<ILifeTransaction>("life_insurance").Find(filter).ToList();

         foreach (ILifeTransaction life in list) {
            Console.WriteLine(life.ENTRY_DATE);
         }
      }
   }
}