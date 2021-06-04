using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;
using CRM;
using Models.TransactionsRequestBody;
using CustomerManagement;
using EmployeeManagement;

namespace TransactionManagement {
   public class FixedDepositTransactionController : TransactionController {
      private Task<FixedDepositBody> transaction;

      public FixedDepositTransactionController(HttpContext context) {
         this.transaction = JSONObject.Deserilise<FixedDepositBody>(context);
      }

      public async Task<string> Add() {
         FixedDepositBody trans = await this.transaction;
         TransactionVerification<FixedDepositBody> details = new TransactionVerification<FixedDepositBody>() {
            document = trans,
            isCustomerExist = await CustomerController.IsCustomerExist(trans.MOBILE),
            isEmployeeExist = await EmployeeController.IsEmployeeExist(trans.MANAGER),
            table = Table.fixedDeposit,
         };

         return AddTransaction<FixedDepositBody>(details);
      }
   }
}