using MongoDB.Driver;

namespace CRM {
	public static class Mongo {
		public static string url = "";
		public static MongoClient client = new MongoClient($"mongodb+srv://abhiramdb:abhiram@13@crm-cluster.i47fm.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
		public static IMongoDatabase database = client.GetDatabase("CRM");
	}
}