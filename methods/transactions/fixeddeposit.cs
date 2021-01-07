using System;

namespace CRM {
   public class IFixedDeposit : IMongoObject {
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
}
