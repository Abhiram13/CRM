namespace CRM {
	public sealed class Text {
		public static string Encode(string str) {
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(str);
			return System.Convert.ToBase64String(plainTextBytes);
		}

		public static string Decode(string str) {
			var base64EncodedBytes = System.Convert.FromBase64String(str);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}

		public static string Tokenize() {
			string dateJson = Date.Currentdate();
			return Text.Encode(dateJson);
		}

		public static string Tokenize<T>(T obj) {
			string objJson = JSON.Serializer<T>(obj);
			string dateJson = Date.Currentdate();
			return Text.Encode(objJson + dateJson);
		}

		public static string Tokenize<T, U>(T obj, U ob) {
			string objJson = JSON.Serializer<T>(obj);
			string obJson = JSON.Serializer<U>(ob);
			string dateJson = Date.Currentdate();
			return Text.Encode(objJson + dateJson);
		}
	}
}