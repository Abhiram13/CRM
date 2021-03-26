using System;
using System.Text.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public abstract class JSON {
      public async Task<DocumentType> Deserilise<DocumentType>(HttpContext context) {
         StreamReader reader = new StreamReader(context.Request.Body);
         Task<string> str = reader.ReadToEndAsync();
         return JsonSerializer.Deserialize<DocumentType>(await str);
      }      

      public Document DeserializeObject<Document>(string doc) {
         return JsonSerializer.Deserialize<Document>(doc);
      }

      public string Serialize<Document>(Document document) {
         return JsonSerializer.Serialize<Document>(document);
      }

      public static async Task<DocumentType> DeseriliseContext<DocumentType>(HttpContext context) {
         StreamReader reader = new StreamReader(context.Request.Body);
         Task<string> str = reader.ReadToEndAsync();
         return JsonSerializer.Deserialize<DocumentType>(await str);
      }

      public static Document DeserializeObjectStatic<Document>(string doc) {
         return JsonSerializer.Deserialize<Document>(doc);
      }

      public static string SerializeStatic<Document>(Document document) {
         return JsonSerializer.Serialize<Document>(document);
      }
   }
}