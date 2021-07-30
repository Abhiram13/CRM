using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;
using CRM;
using Models.TransactionsRequestBody;
using CustomerManagement;
using EmployeeManagement;

namespace TransactionManagement {
   public class FixedDepositTransactionController : Controller {
      private Task<FixedDepositBody> transaction;

      public FixedDepositTransactionController(HttpContext context) {
         this.transaction = JSONObject.Deserilise<FixedDepositBody>(context);
      }

      // public async Task<string> Add() {
      //    FixedDepositBody trans = await this.transaction;
      //    DocumentVerification<FixedDepositBody> details = new DocumentVerification<FixedDepositBody>() {
      //       document = trans,
      //       boolean = !(await CustomerController.IsCustomerExist(trans.mobile)) && !(await EmployeeController.IsEmployeeExist(trans.manager)),
      //       table = Table.fixedDeposit,
      //    };

      //    return Add<FixedDepositBody>(details);
      // }
   }
}