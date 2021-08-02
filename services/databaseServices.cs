using System;
using MongoDB.Driver;
using MongoDB.Bson;
using CRM;
using Models;
using System.Collections.Generic;

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

   public class Document<T> {
      private IMongoCollection<T> collection;
      public FilterDefinitionBuilder<T> builders;

      public Document(string collectionName) {
         collection = Mongo.database.GetCollection<T>(collectionName);
         builders = Builders<T>.Filter;
      }

      private bool isDocumentExist(FilterDefinition<T> filter) {
         T document = FetchOne(filter);
         return document == null;
      }

      public void Insert(T document) {
         collection.InsertOne(document);
      }

      public void Insert(T document, FilterDefinition<T> filter) {
         if (isDocumentExist(filter)) {
            collection.InsertOne(document);
            Console.WriteLine("Document inserted");
         }

         Console.WriteLine("Document already Exists");
      }

      public List<T> FetchAll() {
         return collection.Find(new BsonDocument()).ToList();
      }

      public T FetchOne(FilterDefinition<T> filter) {
         List<T> doc = collection.Find(filter).ToList();
         return doc.Count > 0 ? doc[0] : default(T);
      }
   }
}