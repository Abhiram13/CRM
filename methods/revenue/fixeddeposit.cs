using System;

namespace CRM {
   public class IFixedDepositRevenue : IMongoObject {
      public string SCHEMA { get; set; }
      public string COMPANY { get; set; }
      public int TENOUR { get; set; }
      public float REVENUE { get; set; }
   }
}