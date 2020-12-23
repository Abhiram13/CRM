using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class IProduct {
      public string PRODUCT { get; set; }
      public string COMPANY { get; set; }
      public string TYPE { get; set; }
   }

   public class Product : JSON {
      HttpContext context;
      Task<IProduct> product;
      public Product(HttpContext Context) {
         context = Context;
         product = Deserilise<IProduct>(Context);
      }

      public async Task<IProduct[]> fetchAllProducts() {
         string listOfProducts = await new Database<IProduct>("product").FetchAll();
         return DeserializeObject<IProduct[]>(listOfProducts);
      }

      private async void isProductExist() {
         IProduct product = await this.product;
      }

      public async void Add() {
         new Database<IProduct>("product").Insert(await this.product);
      }
   }
}