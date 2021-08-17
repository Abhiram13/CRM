using Models;
using CRM;
using System.Collections.Generic;

namespace Services {
	namespace BranchManagement {
		public sealed partial class BranchServices : Services<Branch> {
         public BranchServices(string table) : base(table) {}
			public List<BranchResponseModel> FetchAll() {
				List<Branch> list = document.FetchAll();
				List<BranchResponseModel> listOfBranches = new List<BranchResponseModel>();
            foreach (Branch branch in list) {
					listOfBranches.Add(new BranchResponseModel(branch));
				}
				return listOfBranches;
			}
		}
	}
}