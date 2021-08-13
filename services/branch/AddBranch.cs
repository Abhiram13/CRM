using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;
using MongoDB.Driver;
using Services.DatabaseManagement;

namespace BranchManagement {
   public sealed partial class BranchServices : Services<Branch> {
      public BranchServices(HttpRequest request) : base(request, Table.branch) {}

      public short Insert() {
         FilterDefinition<Branch> locationFilter = document.builders.Eq("location", requestBody.location);
         FilterDefinition<Branch> branchFilter = document.builders.Eq("branch", requestBody.branch) & locationFilter;
         return document.Insert(requestBody, branchFilter);
      }
   }
}