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
         this.Filter();
         collection.InsertOne(doc);         
      }

      public async Task<string> FetchAll() {
         List<DocumentType> list = await collection.Find<DocumentType>(new BsonDocument()).ToListAsync<DocumentType>();
         string str = JsonSerializer.Serialize<List<DocumentType>>(list);

         return str;
      }

      public void Filter() {
         // var filterBuilder1 = Builders<Student>.Filter;
         // var filter1 = filterBuilder1.Eq(x => x.CreatedOn, today);
         // List<Student> searchResult1 = collection.Find(filter1).ToList();
         DateTime now = new DateTime(2020, 10, 11);
         var filter = Builders<ILifeTransaction>.Filter.Gte(x => x.ENTRY_DATE, now);
         List<ILifeTransaction> list = Mongo.database.GetCollection<ILifeTransaction>("life_insurance").Find(filter).ToList();

         foreach (ILifeTransaction life in list) {
            Console.WriteLine(life.ENTRY_DATE);
         }
      }
   }
}