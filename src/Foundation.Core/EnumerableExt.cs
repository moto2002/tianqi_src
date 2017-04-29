using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Foundation.Core
{
	public static class EnumerableExt
	{
		public static T Random<T>(this IEnumerable<T> list)
		{
			int num = Enumerable.Count<T>(list);
			if (num == 0)
			{
				return default(T);
			}
			return Enumerable.ElementAt<T>(list, UnityEngine.Random.Range(0, num));
		}

		public static T Next<T>(this T[] list, T current)
		{
			if (list == null || list.Length == 0)
			{
				return current;
			}
			if (current == null)
			{
				return list[0];
			}
			int num = Array.IndexOf<T>(list, current);
			num++;
			if (num >= list.Length)
			{
				num = 0;
			}
			return list[num];
		}

		public static T Back<T>(this T[] list, T current)
		{
			if (list == null || list.Length == 0)
			{
				return current;
			}
			if (current == null)
			{
				return list[0];
			}
			int num = Array.IndexOf<T>(list, current);
			num--;
			if (num < 0)
			{
				num = list.Length - 1;
			}
			return list[num];
		}

		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			using (IEnumerator<T> enumerator = collection.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T current = enumerator.get_Current();
					action.Invoke(current);
				}
			}
			return collection;
		}
	}
}
