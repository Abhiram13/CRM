using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;
using MongoDB.Driver;
using Services.DatabaseManagement;
using DataBase;

namespace Services {
	namespace BranchManagement {
		public sealed partial class BranchServices : Services<Branch> {
			public BranchServices(HttpRequest request) : base(request, Table.branch) { }

			public short Insert() {
				FilterDefinition<Branch> locationFilter = document.builders.Eq("location", requestBody.location);
				FilterDefinition<Branch> branchFilter = document.builders.Eq("branch", requestBody.branch) & locationFilter;
				return document.Insert(requestBody, branchFilter);
			}
		}

      public sealed class BranchSer {
         public static ResponseModel Insert(HttpRequest request) {
				Branch branch = new Branch();
				Console.WriteLine(branch.branch);
				Docu<Branch> document = new Docu<Branch>(request, Table.branch);			
				FilterDefinition<Branch> branchFilter = document.Builder.Eq("branch", branch.branch) & document.Builder.Eq("location", branch.location);
				return document.Insert(branchFilter);
			}
      }
	}
}