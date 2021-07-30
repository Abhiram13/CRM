using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models {
   public abstract class IMongoObject {
      [BsonIgnoreIfNull]
      public MongoDB.Bson.ObjectId _id { get; set; } = new ObjectId();
      public int? __v { get; } = 1;
   }

   public struct CustomerDetails {
      public long mobile { get; set; }
      public long aadhaar { get; set; }
   }

   public struct HashDetails {
      public string salt { get; set; }
      public string password { get; set; }
   }

   public struct DocumentVerification<T> {
      public bool boolean { get; set; }
      public string table { get; set; }
      public T document { get; set; }
   }

   public class States : IMongoObject {
      public string[] states { get; set; } = new string[] { };
   }

   public class Roles : IMongoObject {
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

   public class BranchModel : IMongoObject {
      public string location { get; set; }
      public string branch { get; set; }
   }

   public class ProductModel : IMongoObject {
      public string product { get; set; }
      public string company { get; set; }
      public string type { get; set; }
   }

   namespace ProductReportsRequestBody {
      public class BranchProduct : ZonalProduct {
         public string branch { get; set; }
      }

      public class ZonalProduct : RequestBody {
         public string location { get; set; }
      }

      public class RequestBody : IMongoObject {
         public DateTime end_date { get; set; }
         public DateTime start_date { get; set; }
      }

      public class Rmproduct : RequestBody {
         public long manager { get; set; }
      }
   }

   namespace RevenuesRequestBody {
      public class FixedDepositRevenue : IMongoObject {
         public string schema { get; set; }
         public string company { get; set; }
         public int tenour { get; set; }
         public float revenue { get; set; }
      }

      public class GeneralInsuranceRevenue : IMongoObject {
         public string product { get; set; }
         public string company { get; set; }
         public int revenue { get; set; }
      }

      public class LifeInsuranceRevenue : IMongoObject {
         public string product { get; set; }
         public string company { get; set; }
         public string plan { get; set; }
         public string payment_term { get; set; }
         public int revenue { get; set; }
      }

      public class MutualFundsRevenue : IMongoObject {
         public string amc { get; set; }
         public string funds { get; set; }
         public string plan { get; set; }
         public string option { get; set; }
         public string sub_option { get; set; } = "";
         public string mode { get; set; }
         public int revenue { get; set; }
      }
   }

   namespace TransactionsRequestBody {
      public class FixedDepositBody : IMongoObject {
         public string company { get; set; }
         public string product { get; set; }
         public string schema { get; set; }
         public int tenour { get; set; }
         public long mobile { get; set; }
         public long aadhaar { get; set; }
         public string account { get; set; }
         public string bank { get; set; }
         public long amount { get; set; }
         public long revenue { get; set; }
         public DateTime entry_date { get; set; }
         public DateTime issuance_date { get; set; }
         public int manager { get; set; }
      }

      public class GeneralInsuranceBody : IMongoObject {
         public string company { get; set; }
         public string product { get; set; }
         public long gross { get; set; }
         public long net { get; set; }
         public string policy_number { get; set; }
         public int policy_tenour { get; set; }
         public string policy_type { get; set; }
         public DateTime policy_login_date { get; set; }
         public string insurance_type { get; set; }
         public string bank { get; set; }
         public int payment_term { get; set; }
         public DateTime entry_date { get; set; }
         public long revenue { get; set; }
         public long mobile { get; set; }
         public long aadhaar { get; set; }
         public int manager { get; set; }
      }

      public class LifeInsuranceBody : IMongoObject {
         public long mobile { get; set; }
         public long aadhaar { get; set; }
         public string account { get; set; }
         public string bank { get; set; }
         public string plan { get; set; }
         public int term { get; set; }
         public string company { get; set; }
         public int payment_term { get; set; }
         public long gross { get; set; }
         public long net { get; set; }
         public long revenue { get; set; }
         public DateTime entry_date { get; set; }
         public int manager { get; set; }
      }

      public class MutualFundsBody : IMongoObject {
         public string amc { get; set; }
         public string product { get; set; }
         public string fund { get; set; }
         public string plan { get; set; }
         public string option { get; set; }
         public string sub_option { get; set; }
         public string mode { get; set; }
         public string account { get; set; }
         public long amount { get; set; }
         public string bank { get; set; }
         public int payment_term { get; set; }
         public DateTime entry_date { get; set; }
         public DateTime issuance_date { get; set; }
         public long revenue { get; set; }
         public long mobile { get; set; }
         public long aadhaar { get; set; }
         public int manager { get; set; }
      }
   }

   namespace ZonalReportsResponseBody {
      public class FixedDepositZ : TransactionsRequestBody.FixedDepositBody {
         public string firstname { get; set; }
         public string lastname { get; set; }
         public string email { get; set; }
         public DateTime birthdate { get; set; }
         public string location { get; set; }
         public string branch { get; set; }
      }

      public class GeneralInsuranceZ : TransactionsRequestBody.GeneralInsuranceBody {
         public string firstname { get; set; }
         public string lastname { get; set; }
         public string email { get; set; }
         public DateTime birthdate { get; set; }
         public string location { get; set; }
         public string branch { get; set; }
      }

      public class MutualFundsZ : TransactionsRequestBody.MutualFundsBody {
         public string firstname { get; set; }
         public string lastname { get; set; }
         public string email { get; set; }
         public DateTime birthdate { get; set; }
         public string location { get; set; }
         public string branch { get; set; }
      }

      public class LifeInsuranceZ : TransactionsRequestBody.LifeInsuranceBody {
         public string firstname { get; set; }
         public string lastname { get; set; }
         public string email { get; set; }
         public DateTime birthdate { get; set; }
         public string location { get; set; }
         public string branch { get; set; }
      }
   }

   namespace RevenueReport {
      namespace Zonal {
         public class ReportRequestBody : ProductReportsRequestBody.ZonalProduct {
            public long manager { get; set; }
         }
      }

      namespace Reports {
         public class DataModel {
            public long life { get; set; }
            public long general { get; set; }
            public long mutual { get; set; }
            public long fixedD { get; set; }
            public long total { get; set; }
         }

         public class ReportModel {
            public DateTime entry_date { get; set; }
            public DataModel data { get; set; }
         }

         public class TotalModel {
            public ReportModel[] revenue { get; set; }
            public DataModel total { get; set; }
         }
      } 
   }

   public struct LoginRequest {
      public int id { get; set; }
      public string password { get; set; }
   }

   public struct ResponseBody<T> {
      public int statusCode { get; set; }
      public T body { get; set; }
   }
}