using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;
using CRM;
using Models.TransactionsRequestBody;
using CustomerManagement;
using EmployeeManagement;

namespace TransactionManagement {
   public class GeneralInsuranceTransactionController : Controller {
      private Task<GeneralInsuranceBody> transaction;

      public GeneralInsuranceTransactionController(HttpContext context) {
         this.transaction = JSONObject.Deserilise<GeneralInsuranceBody>(context);
      }

      // public async Task<string> Add() {
      //    GeneralInsuranceBody trans = await this.transaction;
      //    DocumentVerification<GeneralInsuranceBody> details = new DocumentVerification<GeneralInsuranceBody>() {
      //       document = trans,
      //       boolean = !(await CustomerController.IsCustomerExist(trans.mobile)) && !(await EmployeeController.IsEmployeeExist(trans.manager)),
      //       table = Table.generalInsurance,
      //    };

      //    return Add<GeneralInsuranceBody>(details);
      // }
   }
}