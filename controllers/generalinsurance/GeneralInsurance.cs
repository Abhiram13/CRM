using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;
using CRM;
using Models.TransactionsRequestBody;
using CustomerManagement;
using EmployeeManagement;

namespace TransactionManagement {
   public class GeneralInsuranceTransactionController : TransactionController {
      private Task<GeneralInsuranceBody> transaction;

      public GeneralInsuranceTransactionController(HttpContext context) {
         this.transaction = JSONObject.Deserilise<GeneralInsuranceBody>(context);
      }

      public async Task<string> Add() {
         GeneralInsuranceBody trans = await this.transaction;
         TransactionVerification<GeneralInsuranceBody> details = new TransactionVerification<GeneralInsuranceBody>() {
            document = trans,
            // isCustomerExist = await CustomerController.IsCustomerExist(trans.MOBILE),
            // isEmployeeExist = await EmployeeController.IsEmployeeExist(trans.MANAGER),
            boolean = !(await CustomerController.IsCustomerExist(trans.MOBILE)) && !(await EmployeeController.IsEmployeeExist(trans.MANAGER)),
            table = Table.generalInsurance,
         };

         return AddTransaction<GeneralInsuranceBody>(details);
      }
   }
}