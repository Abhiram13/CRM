using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   class ICustomer : IMongoObject {
      public string TITLE { get; set; }
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public long MOBILE { get; set; }
      public string EMAIL { get; set; }
      public DateTime BIRTHDATE { get; set; }
      public string GENDER { get; set; }
      public string PAN { get; set; }
      public long AADHAAR { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
      public string PRESENT_LINE_1 { get; set; } = "";
      public string PRESENT_LINE_2 { get; set; } = "";
      public string PRESENT_CITY { get; set; } = "";
      public string PRESENT_DISTRICT { get; set; } = "";
      public string PRESENT_STATE { get; set; } = "";
      public string PRESENT_COUNTRY { get; set; } = "";
      public long PRESENT_PINCODE { get; set; } = 0;
      public bool IS_PERMANENT { get; set; } = true;
      public string PERMANENT_LINE_1 { get; set; } = "";
      public string PERMANENT_LINE_2 { get; set; } = "";
      public string PERMANENT_CITY { get; set; } = "";
      public string PERMANENT_DISTRICT { get; set; } = "";
      public string PERMANENT_STATE { get; set; } = "";
      public string PERMANENT_COUNTRY { get; set; } = "";
      public long PERMANENT_PINCODE { get; set; } = 0;
   }
}