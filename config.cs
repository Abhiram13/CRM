using MongoDB.Driver;

namespace CRM {
   public static class Mongo {
      public static string url = "";
      public static MongoClient client = new MongoClient($"mongodb+srv://abhiramdb:abhiram13@crm-cluster.i47fm.mongodb.net/CRM-Cluster?retryWrites=true&w=majority");
      public static IMongoDatabase database = client.GetDatabase("CRM");
   }
}