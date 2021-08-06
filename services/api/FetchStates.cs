using Models;
using System;
using Services.DatabaseManagement;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Services {
   namespace ApiManagement {
      public sealed class States : Document<StatesModels> {
			private States() : base(Table.states) { }
			public static string[] fetchStates() {
				List<StatesModels> states = new States().FetchAll();
				return states[0].states;
         }
      }
   }
}