using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models {
   public abstract class IMongoObject {
      [BsonIgnoreIfNull]
      public ObjectId _id { get; set; } = new ObjectId();
      public byte? __v { get; } = 1;
   }

   public class ResponseModel<T> {
      public ResponseModel(int status, T data) {
			Status = status;
			Response = data;
		}
      public int Status { get; private set; }
		public T Response { get; private set; }
	}

	public class ResponseModel {
      public ResponseModel(int status, string data) {
			Status = status;
			Response = data;
		}

		public ResponseModel(int status) {
			Status = status;
			Response = StatusMessage.Send(Status);
		}
		public int Status { get; private set; }
		public string Response { get; private set; }
	}

   public struct HashDetails {
      public string salt { get; set; }
      public string password { get; set; }
   }

   public class StatesModels : IMongoObject {
      public string[] states { get; set; } = new string[] { };
   }

   public class RolesModels : IMongoObject {
      public string[] roles { get; set; } = new string[] { };
   }

   public class Employee : IMongoObject {
      public int empid { get; set; } = 0;
      public string title { get; set; } = "";
      public string firstname { get; set; } = "";
      public string lastname { get; set; } = "";
      public string email { get; set; } = "";
      public string password { get; set; } = "";
      public long mobile { get; set; } = 0;
      public string location { get; set; } = "";
      public string branch { get; set; } = "";
      public string state { get; set; } = "";
      public string role { get; set; } = "";
      public string salt { get; set; } = "";
   }

   public class EmployeeResponseBody {
		private Employee Emp;
		public EmployeeResponseBody(Employee emp) {
			Emp = emp;
		}
      public int empid { get { return Emp.empid; } }
      public string title { get { return Emp.title; } }
      public string fullname { get { return $"{Emp.firstname} {Emp.lastname}"; } }
      public string email { get { return Emp.email; } }
      public long mobile { get { return Emp.mobile; } }
      public string location { get { return Emp.location; } }
      public string branch { get { return Emp.branch; } }
      public string state { get { return Emp.state; } }
      public string role { get { return Emp.role; } }
   }

   public class Customer : IMongoObject {
      public string title { get; set; }
      public string firstname { get; set; }
      public string lastname { get; set; }
      public long? mobile { get; set; }
      public string email { get; set; }
      public DateTime? birthdate { get; set; }
      public string gender { get; set; }
      public string pan { get; set; }
      public long? aadhaar { get; set; }
      public string location { get; set; }
      public string branch { get; set; }
      public string line_1 { get; set; }
      public string line_2 { get; set; }
      public string city { get; set; }
      public string district { get; set; }
      public string state { get; set; }
      public string country { get; set; }
      public long pincode { get; set; }
   }

   public class Branch : Location {
      public string branch { get; set; }
   }

   public struct BranchResponseModel {
      public BranchResponseModel(Branch b) {
			branch = b.branch;
			location = b.location;
		}

		public string location { get; private set; }
		public string branch { get; private set; }
   }

   public class Location : IMongoObject {
      public string location { get; set; }
   }

   public struct LoginRequest {
      public int id { get; set; }
      public string password { get; set; }
   }

   public struct ResponseBody<T> {
      public short statusCode { get; set; }
      public T body { get; set; }
   }
}