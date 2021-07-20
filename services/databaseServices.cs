using System;
using MongoDB.Driver;
using CRM;
using Models;

namespace Database {
   /// <summary>
   /// Gives properties collection and builders for filtering Database documents
   /// </summary>
   /// <typeparam name="T">Type of Document</typeparam>
   public class DatabaseService<T> {
      public IMongoCollection<T> collection;
      public FilterDefinitionBuilder<T> builders;
      public ProjectionDefinitionBuilder<T> projection;      

      public DatabaseService(string collectionName) {
         collection = Mongo.database.GetCollection<T>(collectionName);
         builders = Builders<T>.Filter;
         projection = Builders<T>.Projection;         
      }
   }
}