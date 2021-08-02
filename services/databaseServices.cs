using System;
using MongoDB.Driver;
using CRM;
using Models;

namespace DatabaseManagement {
   /// <summary>
   /// Gives properties collection and builders for filtering Database documents
   /// </summary>
   /// <typeparam name="T">Type of Document</typeparam>
   public class Database<T> {
      public IMongoCollection<T> collection;
      public FilterDefinitionBuilder<T> builders;
      public ProjectionDefinitionBuilder<T> projection;      

      public Database(string collectionName) {
         collection = Mongo.database.GetCollection<T>(collectionName);
         builders = Builders<T>.Filter;
         projection = Builders<T>.Projection;         
      }
   }
}