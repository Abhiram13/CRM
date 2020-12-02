using System;
using MongoDB;

namespace CRM {
   class ICustomer {
      public MongoDB.Bson.ObjectId? _id { get; set; }
      public int ID { get; set; }
      public string TITLE { get; set; }
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; } 
      public string EMAIL { get; set; }
      public string PASSWORD { get; set; }
      public int MOBILE { get; set; }
      public string GENDER { get; set; }
      public string DESIGNATION { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
      public string STATE { get; set; }
      public string REGION { get; set; }
      public string ROLE { get; set; }
      public int? __v { get; set; }
   }
}