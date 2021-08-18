using System.Collections.Generic;

namespace System {
	public interface IFetchAll<T> {
		List<T> FetchAll();
	}
}