using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class RMReportBody : IMongoObject {
      public long MANAGER { get; set; }
      public DateTime START_DATE { get; set; }
      public DateTime END_DATE { get; set; }
   }
}