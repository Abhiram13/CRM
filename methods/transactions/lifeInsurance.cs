using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class ILifeTransaction : IMongoObject {
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
   }
}
