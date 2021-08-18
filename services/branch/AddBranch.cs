using Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using DataBase;
using System;

namespace Services {
	namespace BranchManagement {
      public class BranchService : DatabaseOperations<Branch>, IFetchAll<BranchResponseModel> {
         public BranchService(DocumentStructure<Branch> document) : base(document) { } 

         public List<BranchResponseModel> FetchAll() {
				List<Branch> list = _collection.Find(new BsonDocument()).ToList();
				List<BranchResponseModel> listOfBranches = new List<BranchResponseModel>();
				foreach (Branch branch in list) {
					listOfBranches.Add(new BranchResponseModel(branch));
				}

				return listOfBranches;
			}
		}
	}
}