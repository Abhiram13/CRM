namespace CRM {
   public class String : JSON {
      public static string Encode(string str) {
         var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
         return System.Convert.ToBase64String(plainTextBytes);
      }

      public static string Decode(string str) {
         var base64EncodedBytes = System.Convert.FromBase64String(str);
         return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
      }
   }

   public sealed class Text {
      public static string Encode(string str) {
         var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
         return System.Convert.ToBase64String(plainTextBytes);
      }

      public static string Decode(string str) {
         var base64EncodedBytes = System.Convert.FromBase64String(str);
         return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
      }
   }
}