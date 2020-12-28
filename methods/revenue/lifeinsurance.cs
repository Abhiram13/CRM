using System;

namespace CRM {
   public class ILifeRevenue : IMongoObject {
      public string PRODUCT { get; set; }
      public string COMPANY { get; set; }
      public string PLAN { get; set; }
      public string PREMIUM_PAYMENT_TERM { get; set; }
      public int REVENUE { get; set; }
   }
}