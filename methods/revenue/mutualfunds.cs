using System;

namespace CRM {
   public class IMutualFundsRevenue : IMongoObject {
      public string AMC { get; set; }
      public string FUNDS { get; set; }
      public string PLAN { get; set; }
      public string OPTION { get; set; }
      public string SUB_OPTION { get; set; } = "";
      public string MODE { get; set; }
      public int REVENUE { get; set; }
   }
}