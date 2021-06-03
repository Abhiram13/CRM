using Models;
using CRM;

namespace TransactionManagement {
   public partial class TransactionController {
      public string AddTransaction<T>(TransactionVerification<T> transaction) {
         if (!transaction.isEmployeeExist && !transaction.isCustomerExist) {
            new Database<T>(transaction.table).Insert(transaction.document);
            return "Transaction successfully Added";
         }

         return "Transaction not added";
      }
   }
}