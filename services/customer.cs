
namespace CRM {
   public class CustomerSearchQuery {
      public long QUERY { get; set; }
   }

   // public sealed class Customer : JSON {
   //    private Task<Customer> customer;

   //    public Customer(HttpContext context) {
   //       customer = body(context);
   //    }

   //    private async Task<Customer> body(HttpContext context) {
   //       return await Deserilise<Customer>(context);
   //    }

   //    private async Task<string> check() {
   //       Customer emp = await this.customer;

   //       foreach (var prop in emp.GetType().GetProperties()) {
   //          if (prop.GetValue(emp) == null) {
   //             return $"Please provide {prop.Name.ToLower()}";
   //          }
   //       }

   //       return "OK";
   //    }

   //    public static async Task<Customer[]> fetchAllCustomers() {
   //       string customer = await new Database<Customer>("customer").FetchAll();
   //       return JSONObject.DeserializeObject<Customer[]>(customer);
   //    }

   //    public static async Task<Customer> isCustomerExist(long? mobile, long? aadhaar) {
   //       Customer[] listOfCustomers = await Customer.fetchAllCustomers();
   //       return Array.Find<Customer>(listOfCustomers, c => c.MOBILE == mobile && c.AADHAAR == aadhaar);
   //    }

   //    public async Task<string> Add() {
   //       string check = await this.check();
   //       Customer customer = await this.customer;

   //       if (check == "OK") {
   //          if (await Customer.isCustomerExist(customer.MOBILE, customer.AADHAAR) == null) {
   //             new Database<Customer>("customer").Insert(customer);
   //             return "Customer Successfully Added";
   //          }
   //          return "Customer already Existed";
   //       }

   //       return check;
   //    }

   //    public async Task<string> search(HttpContext context) {
   //       Customer[] listOfCustomers = await Customer.fetchAllCustomers();
   //       CustomerSearchQuery query = await Deserilise<CustomerSearchQuery>(context);
   //       Customer customer = Array.Find<Customer>(listOfCustomers, customer => customer.MOBILE == query.QUERY || customer.AADHAAR == query.QUERY);

   //       if (customer == null) {
   //          return "Customer Not Found";
   //       }

   //       return Serialize<Customer>(customer);
   //    }
   // }
}