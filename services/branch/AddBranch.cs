using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;
using MongoDB.Driver;
using DatabaseManagement;

namespace BranchManagement {
   public sealed partial class BranchServices {
      private Task<Branch> branch;
      private Database<Branch> db = new Database<Branch>(Table.branch);

      public BranchServices(HttpRequest request) {
         branch = JSON.httpContextDeseriliser<Branch>(request);
      }

      private bool isBranchExist() {
         FilterDefinition<Branch> filter = db.builders.Eq("branch", branch.Result.branch) & db.builders.Eq("location", branch.Result.location);
         Branch b = db.collection.Find(filter).ToList()[0];
         return b == null;
      }

      public async void Add(HttpRequest request) {
         Branch branch = await JSON.httpContextDeseriliser<Branch>(request);
         Database<Branch> db = new Database<Branch>(Table.branch);
         db.collection.InsertOne(branch);
      }
   }
}