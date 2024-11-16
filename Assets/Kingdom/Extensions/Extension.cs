using System.Collections.Generic;
using System.Linq;

namespace Kingdom
{
	public static class Extension {
		public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> o) where T:class
			=> o.Where(x => x is not null)!;
	}
}