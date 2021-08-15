using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;
using MongoDB.Driver;
using Services.DatabaseManagement;
using System.Collections.Generic;

namespace Services {
	namespace BranchManagement {
		public sealed partial class BranchServices : Services<Branch> {
         public BranchServices(string table) : base(table) {}
			public List<Branch> FetchAll() {
				return document.FetchAll();
			}
		}
	}
}