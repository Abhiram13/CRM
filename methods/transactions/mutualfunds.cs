using System;

namespace CRM {
   public class IMutualFunds : IMongoObject {
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
