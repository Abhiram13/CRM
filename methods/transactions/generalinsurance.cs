using System;

namespace CRM {
   public class IGeneralInsurance : IMongoObject {
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
   }
}
