using System;

// Fetch employee details based on manager id
// Fetch all transactions between dates
// Get all dates from the fetched transactions and push them into array
// loop through every date and fetch from all transactions revenue

namespace CRM {
   public class ZonalRevenueReport : ZonalReportBody {
      public long MANAGER { get; set; }
   }
}
