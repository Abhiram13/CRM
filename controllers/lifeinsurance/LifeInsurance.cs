using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;
using CRM;
using Models.TransactionsRequestBody;
using CustomerManagement;
using EmployeeManagement;

namespace TransactionManagement {
   public class LifeInsuranceTransactionController : Controller {      
      private Task<LifeInsuranceBody> transaction;

      public LifeInsuranceTransactionController(HttpContext context) {
         this.transaction = JSONObject.Deserilise<LifeInsuranceBody>(context);
      }

      // public async Task<string> Add() {
      //    LifeInsuranceBody trans = await this.transaction;
      //    DocumentVerification<LifeInsuranceBody> details = new DocumentVerification<LifeInsuranceBody>() {
      //       document = trans,
      //       boolean = !(await CustomerController.IsCustomerExist(trans.mobile)) && !(await EmployeeController.IsEmployeeExist(trans.manager)),
      //       table = Table.lifeInsurance,
      //    };

      //    return Add<LifeInsuranceBody>(details);
      // }
   }
}