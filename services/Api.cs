using Models;
using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using DataBase;

namespace Services {
   namespace ApiManagement {
      public class States : DatabaseOperations<StatesModels>, IFetchAll<string> {
         public States(DocumentStructure<StatesModels> document) : base(document) {}

         public List<string> FetchAll() {
				List<StatesModels> list = _collection.Find(new BsonDocument()).ToList();
				return new List<string>(list[0].states);
			}
      }

		public class Locations : DatabaseOperations<Location>, IFetchAll<string> {
			public Locations(DocumentStructure<Location> document) : base(document) { }

			public List<string> FetchAll() {
				List<Location> list = _collection.Find(new BsonDocument()).ToList();
				List<string> locations = new List<string>();

            foreach (Location location in list) {
					locations.Add(location.location);
				}

				return locations;
			}
		}

		public class Roles : DatabaseOperations<RolesModels>, IFetchAll<string> {
			public Roles(DocumentStructure<RolesModels> document) : base(document) { }

			public List<string> FetchAll() {
				List<RolesModels> list = _collection.Find(new BsonDocument()).ToList();
				return new List<string>(list[0].roles);
			}
		}
   }
}