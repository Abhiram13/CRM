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

		public static string Serialize<ObjectType>(ObjectType obj) {
			string objJson = JSON.Serializer<ObjectType>(obj);
			string dateJson = Date.Currentdate();
			return Text.Encode(objJson + dateJson);
		}
	}
}