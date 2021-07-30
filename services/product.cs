using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;

namespace CRM {
   public class Product : JSON {
      HttpContext context;
      Task<ProductModel> product;
      public Product(HttpContext Context) {
         context = Context;
         product = Deserilise<ProductModel>(Context);
      }

      public async Task<ProductModel[]> fetchAllProducts() {
         string listOfProducts = await new Database<ProductModel>("product").FetchAll();
         return DeserializeObject<ProductModel[]>(listOfProducts);
      }

      private async Task<bool> isProductExist() {
         ProductModel[] products = await this.fetchAllProducts();
         ProductModel product = await this.product;

         foreach (ProductModel prod in products) {
            if (prod.company.ToString() == product.company.ToString() && prod.product.ToString() == product.product.ToString()) {
               return true;
            }
         }

         return false;
      }

      public async Task<string> Add() {
         if (!await this.isProductExist()) {
            new Database<ProductModel>("product").Insert(await this.product);
            return "Product has Added Successfully";
         }

         return "Product has already been added";
      }
   }
}