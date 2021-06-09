using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;
using CRM;
using Models.TransactionsRequestBody;
using CustomerManagement;
using EmployeeManagement;

namespace TransactionManagement {
   public class MutualFundsTransactionController : Controller {
      private Task<MutualFundsBody> transaction;

      public MutualFundsTransactionController(HttpContext context) : base(context) {
         this.transaction = JSONObject.Deserilise<MutualFundsBody>(context);
      }

      public async Task<string> Add() {
         MutualFundsBody trans = await this.transaction;
         DocumentVerification<MutualFundsBody> details = new DocumentVerification<MutualFundsBody>() {
            document = trans,
            boolean = !(await CustomerController.IsCustomerExist(trans.MOBILE)) && !(await EmployeeController.IsEmployeeExist(trans.MANAGER)),
            table = Table.mutualFunds,
         };

         return Add<MutualFundsBody>(details);
      }
   }
}