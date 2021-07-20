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

   public class Employee : IMongoObject {
      public int empid { get; set; } = 0;
      public string title { get; set; } = "";
      public string firstname { get; set; } = "";
      public string lastname { get; set; } = "";
      public string email { get; set; } = "";
      public string password { get; set; } = "";
      public long mobile { get; set; } = 0;
      public string gender { get; set; } = "";
      public string designation { get; set; } = "";
      public string location { get; set; } = "";
      public string branch { get; set; } = "";
      public string state { get; set; } = "";
      public string region { get; set; } = "";
      public string role { get; set; } = "";
      public string salt { get; set; } = "";
   }

   public class Customer : IMongoObject {
      public string TITLE { get; set; }
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public long? MOBILE { get; set; }
      public string EMAIL { get; set; }
      public DateTime? BIRTHDATE { get; set; }
      public string GENDER { get; set; }
      public string PAN { get; set; }
      public long? AADHAAR { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
      public string PRESENT_LINE_1 { get; set; }
      public string PRESENT_LINE_2 { get; set; }
      public string PRESENT_CITY { get; set; }
      public string PRESENT_DISTRICT { get; set; }
      public string PRESENT_STATE { get; set; }
      public string PRESENT_COUNTRY { get; set; }
      public long PRESENT_PINCODE { get; set; }
      public bool IS_PERMANENT { get; set; } = true;
      public string PERMANENT_LINE_1 { get; set; } = null;
      public string PERMANENT_LINE_2 { get; set; } = null;
      public string PERMANENT_CITY { get; set; } = null;
      public string PERMANENT_DISTRICT { get; set; } = null;
      public string PERMANENT_STATE { get; set; } = null;
      public string PERMANENT_COUNTRY { get; set; } = null;
      public long PERMANENT_PINCODE { get; set; } = 0;
   }

   public class BranchModel : IMongoObject {
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
   }

   public class DesignationModel : IMongoObject {
      public string DESIGNATION { get; set; }
   }

   public class ProductModel : IMongoObject {
      public string PRODUCT { get; set; }
      public string COMPANY { get; set; }
      public string TYPE { get; set; }
   }

   namespace ProductReportsRequestBody {
      public abstract class IMongoObject {
         [BsonIgnoreIfNull]
         public MongoDB.Bson.ObjectId _id { get; set; } = new ObjectId();
         public int? __v { get; } = 1;
      }

      // BranchRequestBody
      public class BranchProduct : ZonalProduct {
         public string BRANCH { get; set; }
      }

      //ZonalRequestBody
      public class ZonalProduct : RequestBody {
         public string LOCATION { get; set; }
      }

      //RequestBody
      public class RequestBody : IMongoObject {
         public DateTime END_DATE { get; set; }
         public DateTime START_DATE { get; set; }
      }

      //RMRequestBody
      public class RMProduct : RequestBody {
         public long MANAGER { get; set; }
      }
   }

   namespace RevenuesRequestBody {
      public abstract class IMongoObject {
         [BsonIgnoreIfNull]
         public MongoDB.Bson.ObjectId _id { get; set; } = new ObjectId();
         public int? __v { get; } = 1;
      }

      public class FixedDepositRevenue : IMongoObject {
         public string SCHEMA { get; set; }
         public string COMPANY { get; set; }
         public int TENOUR { get; set; }
         public float REVENUE { get; set; }
      }

      public class GeneralInsuranceRevenue : IMongoObject {
         public string PRODUCT { get; set; }
         public string COMPANY { get; set; }
         public int REVENUE { get; set; }
      }

      public class LifeInsuranceRevenue : IMongoObject {
         public string PRODUCT { get; set; }
         public string COMPANY { get; set; }
         public string PLAN { get; set; }
         public string PREMIUM_PAYMENT_TERM { get; set; }
         public int REVENUE { get; set; }
      }

      public class MutualFundsRevenue : IMongoObject {
         public string AMC { get; set; }
         public string FUNDS { get; set; }
         public string PLAN { get; set; }
         public string OPTION { get; set; }
         public string SUB_OPTION { get; set; } = "";
         public string MODE { get; set; }
         public int REVENUE { get; set; }
      }
   }

   namespace TransactionsRequestBody {
      public abstract class IMongoObject {
         [BsonIgnoreIfNull]
         public MongoDB.Bson.ObjectId _id { get; set; } = new ObjectId();
         public int? __v { get; } = 1;
      }

      public class FixedDepositBody : IMongoObject {
         public string COMPANY { get; set; }
         public string PRODUCT { get; set; }
         public string SCHEMA { get; set; }
         public int TENOUR { get; set; }
         public long MOBILE { get; set; }
         public long AADHAAR { get; set; }
         public string ACCOUNT { get; set; }
         public string BANK { get; set; }
         public long AMOUNT { get; set; }
         public long REVENUE { get; set; }
         public DateTime ENTRY_DATE { get; set; }
         public DateTime ISSUANCE_DATE { get; set; }
         public int MANAGER { get; set; }
      }

      public class GeneralInsuranceBody : IMongoObject {
         public string COMPANY { get; set; }
         public string PRODUCT { get; set; }
         public long GROSS { get; set; }
         public long NET { get; set; }
         public string POLICY_NUMBER { get; set; }
         public int POLICY_TENOUR { get; set; }
         public string POLICY_TYPE { get; set; }
         public DateTime POLICY_LOGIN_DATE { get; set; }
         public string INSURANCE_TYPE { get; set; }
         public string BANK { get; set; }
         public int PAYMENT_TERM { get; set; }
         public DateTime ENTRY_DATE { get; set; }
         public long REVENUE { get; set; }
         public long MOBILE { get; set; }
         public long AADHAAR { get; set; }
         public int MANAGER { get; set; }
      }

      public class LifeInsuranceBody : IMongoObject {
         public long MOBILE { get; set; }
         public long AADHAAR { get; set; }
         public string ACCOUNT { get; set; }
         public string BANK { get; set; }
         public string PLAN { get; set; }
         public int TERM { get; set; }
         public string COMPANY { get; set; }
         public int PREMIUM_PAYMENT_TERM { get; set; }
         public long GROSS { get; set; }
         public long NET { get; set; }
         public long REVENUE { get; set; }
         public DateTime ENTRY_DATE { get; set; }
         public int MANAGER { get; set; }
      }

      public class MutualFundsBody : IMongoObject {
         public string AMC { get; set; }
         public string PRODUCT { get; set; }
         public string FUND { get; set; }
         public string PLAN { get; set; }
         public string OPTION { get; set; }
         public string SUB_OPTION { get; set; }
         public string MODE { get; set; }
         public string ACCOUNT { get; set; }
         public long AMOUNT { get; set; }
         public string BANK { get; set; }
         public int PAYMENT_TERM { get; set; }
         public DateTime ENTRY_DATE { get; set; }
         public DateTime ISSUANCE_DATE { get; set; }
         public long REVENUE { get; set; }
         public long MOBILE { get; set; }
         public long AADHAAR { get; set; }
         public int MANAGER { get; set; }
      }
   }

   namespace ZonalReportsResponseBody {
      public class FixedDepositZ : TransactionsRequestBody.FixedDepositBody {
         public string FIRSTNAME { get; set; }
         public string LASTNAME { get; set; }
         public string EMAIL { get; set; }
         public DateTime BIRTHDATE { get; set; }
         public string LOCATION { get; set; }
         public string BRANCH { get; set; }
      }

      public class GeneralInsuranceZ : TransactionsRequestBody.GeneralInsuranceBody {
         public string FIRSTNAME { get; set; }
         public string LASTNAME { get; set; }
         public string EMAIL { get; set; }
         public DateTime BIRTHDATE { get; set; }
         public string LOCATION { get; set; }
         public string BRANCH { get; set; }
      }

      public class MutualFundsZ : TransactionsRequestBody.MutualFundsBody {
         public string FIRSTNAME { get; set; }
         public string LASTNAME { get; set; }
         public string EMAIL { get; set; }
         public DateTime BIRTHDATE { get; set; }
         public string LOCATION { get; set; }
         public string BRANCH { get; set; }
      }

      public class LifeInsuranceZ : TransactionsRequestBody.LifeInsuranceBody {
         public string FIRSTNAME { get; set; }
         public string LASTNAME { get; set; }
         public string EMAIL { get; set; }
         public DateTime BIRTHDATE { get; set; }
         public string LOCATION { get; set; }
         public string BRANCH { get; set; }
      }
   }

   namespace RevenueReport {
      namespace Zonal {
         public class ReportRequestBody : ProductReportsRequestBody.ZonalProduct {
            public long MANAGER { get; set; }
         }
      }

      namespace Reports {
         public class DataModel {
            public long LIFE { get; set; }
            public long GENERAL { get; set; }
            public long MUTUAL { get; set; }
            public long FIXED { get; set; }
            public long TOTAL { get; set; }
         }

         public class ReportModel {
            public DateTime ENTRY_DATE { get; set; }
            public DataModel DATA { get; set; }
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