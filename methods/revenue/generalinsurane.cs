using System;

namespace CRM {
   public class IGeneralInsuranceRevenue : IMongoObject {
      public string PRODUCT { get; set; }
      public string COMPANY { get; set; }
      public int REVENUE { get; set; }
   }
}