using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class IProduct : IMongoObject {
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

      private async Task<bool> isProductExist() {
         IProduct[] products = await this.fetchAllProducts();
         IProduct product = await this.product;

         foreach (IProduct prod in products) {
            if (prod.COMPANY.ToString() == product.COMPANY.ToString() && prod.PRODUCT.ToString() == product.PRODUCT.ToString()) {
               return true;
            }
         }

         return false;
      }

      public async Task<string> Add() {
         if (!await this.isProductExist()) {
            new Database<IProduct>("product").Insert(await this.product);
            return "Product has Added Successfully";
         }

         return "Product has already been added";
      }
   }
}