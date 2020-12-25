using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class ILifeTransaction : IMongoObject {
      public string MOBILE { get; set; }
      public string AADHAAR { get; set; }
      public string ACCOUNT { get; set; }
      public string BANK { get; set; }
      public string PLAN { get; set; }
      public int TERM { get; set; }
      public string COMPANY { get; set; }
      public int PREMIUM_PAYMENT_TERM { get; set; }
      public string GROSS { get; set; }
      public string NET { get; set; }
      public string REVENUE { get; set; }
      public DateTime ENTRY_DATE { get; set; }
   }
}

public static class DateEdit {
   public static DateTime Edit(string date) {
      int day = Int32.Parse(date.Split("/")[0]);
      int month = Int32.Parse(date.Split("/")[1]);
      int year = Int32.Parse(date.Split("/")[2]);

      return new DateTime(year, month, day);
   }
}   
