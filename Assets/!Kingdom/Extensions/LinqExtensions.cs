using System.Collections.Generic;
using System.Linq;

public static class LinqExtensions
{
	public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> o) where T : class
		=> o.Where(x => x is not null)!;
}
