using System;
using System.Reflection;
using System.Collections.Generic;

public static class OBJECT {
	public static List<string> GetKeys<ObjectType>(ObjectType obj) {
		List<string> keys = new List<string>();
		foreach (PropertyInfo key in typeof(ObjectType).GetProperties()) {
			keys.Add(key.Name);
		}
		return keys;
	}

	public static List<string> GetValues<ObjectType>(ObjectType obj) {
		List<string> keys = new List<string>();
		foreach (PropertyInfo value in typeof(ObjectType).GetProperties()) {
			keys.Add((string)value.GetValue(obj));
		}
		return keys;
	}
}

public class Date {
   public static string Currentdate() {
		DateTime date = DateTime.Now;
		return $"{date.Year}/{date.Month}/{date.Day}/{date.Hour}/{date.Minute}/{date.Second}/{date.Millisecond}";
	}
}