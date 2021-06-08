using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;
using CRM;
using Models.TransactionsRequestBody;
using CustomerManagement;
using EmployeeManagement;

namespace TransactionManagement {
   public class LifeInsuranceTransactionController : TransactionController {      
      private Task<LifeInsuranceBody> transaction;

      public LifeInsuranceTransactionController(HttpContext context) {
         this.transaction = JSONObject.Deserilise<LifeInsuranceBody>(context);
      }

      public async Task<string> Add() {
         LifeInsuranceBody trans = await this.transaction;
         TransactionVerification<LifeInsuranceBody> details = new TransactionVerification<LifeInsuranceBody>() {
            document = trans,
            // isCustomerExist = await CustomerController.IsCustomerExist(trans.MOBILE),
            // isEmployeeExist = await EmployeeController.IsEmployeeExist(trans.MANAGER),
            boolean = !(await CustomerController.IsCustomerExist(trans.MOBILE)) && !(await EmployeeController.IsEmployeeExist(trans.MANAGER)),
            table = Table.lifeInsurance,
         };

         return AddTransaction<LifeInsuranceBody>(details);
      }
   }
}