using Base.Model;
using Base.Model.Interface;

namespace Base.Extension;

public static class EnumerableExtensions
{
	public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
	{
		var result = enumerable == null;

		if (!result && enumerable != null)
		{
			result = !enumerable.Any()
			         || (typeof(T) == typeof(string)
				         ? enumerable.Select(e => e == null || string.IsNullOrWhiteSpace(e.ToString())).All(b => b)
				         : enumerable.All(e => e == null));
		}

		return result;
	}

	public static bool IsNotNullNorEmpty<T>(this IEnumerable<T> enumerable)
	{
		return !IsNullOrEmpty(enumerable);
	}

	public static async Task Loop<T>(this IEnumerable<T> enumerable, Func<T, Task> eachFunction)
	{
		if (enumerable.IsNotNullNorEmpty())
		{
			foreach (var item in enumerable)
			{
				await eachFunction(item);
			}
		}
	}

	public static void Loop<T>(this IEnumerable<T> enumerable, Action<int, T> action)
	{
		if (enumerable.IsNotNullNorEmpty())
		{
			var index = 0;

			foreach (var item in enumerable)
			{
				action(index++, item);
			}
		}
	}

	public static async Task LoopParallel<T>(this IEnumerable<T> enumerable, Func<T, Task> eachFunction)
	{
		if (enumerable.IsNotNullNorEmpty())
		{
			var tasks = enumerable.Select(eachFunction)
			                      .ToList();

			await Task.WhenAll(tasks);
		}
	}

	public static async Task<IEnumerable<TK>> LoopParallelTasks<T, TK>(this IEnumerable<T> enumerable, Func<T, Task<TK>> eachFunction)
	{
		IEnumerable<TK> result;

		if (enumerable.IsNullOrEmpty())
		{
			result = await Task.FromResult(Enumerable.Empty<TK>());
		}
		else
		{
			var tasks = enumerable.Select(eachFunction).ToList();
			await Task.WhenAll(tasks);

			result = tasks.Select(t => t.Result);
		}

		return result;
	}

	public static bool ContainsCI(this IEnumerable<string> enumerable, string compareString)
	{
		return enumerable.Any(c => c.IsNotNullNorEmpty() && c.EqualsCI(compareString));
	}

	public static async Task<IReadOnlyCollection<T>> WhenAll<T>(this IEnumerable<ValueTask<T>> tasks)
	{
		var results = new List<T>();
		var toAwait = new List<Task<T>>();

		foreach (var valueTask in tasks)
		{
			if (valueTask.IsCompletedSuccessfully)
			{
				results.Add(valueTask.Result);
			}
			else
			{
				toAwait.Add(valueTask.AsTask());
			}
		}

		results.AddRange(await Task.WhenAll(toAwait));

		return results;
	}

	/// <summary>
	/// LINQ in .NET 6, shortcut for TryGetNonEnumeratedCount
	/// </summary>
	/// <param name="enumerable"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static int Count<T>(this IEnumerable<T> enumerable)
	{
		var success = enumerable.TryGetNonEnumeratedCount(out var result);
		return success ? result : 0;
	}

	public static IEnumerable<T> GetPlainPage<T>(this IEnumerable<T> collection, int pageNumber, int pageSize)
	{
		return pageSize == 0
			? collection
			: collection.Skip((pageNumber - 1) * pageSize)
			            .Take(pageSize);
	}

	public static IEnumerablePage<T> GetPage<T>(this IEnumerable<T> collection, int pageNumber, int pageSize)
	{
		var input = collection.ToList();
		var pageCount = GetPageCount(pageSize, input.Count);

		return new EnumerablePage<T>
		{
			PageData = GetPlainPage(input, pageNumber, pageSize)
			, PageNumber  = pageNumber
			, PageSize = pageSize
			, TotalCount = input.Count
			, PageList = GetPageList(pageNumber, pageCount, 5)
		};
	}

	public static IEnumerablePage<TM> CreatePage<T, TM>(this IEnumerablePage<T> dataPage, Func<T, TM> transformMethod)
	{
		return new EnumerablePage<TM>
		{
			PageData = dataPage.PageData.Select(transformMethod)
			, PageNumber = dataPage.PageNumber
			, PageSize = dataPage.PageSize
			, TotalCount = dataPage.TotalCount
			, PageList = dataPage.PageList
		};
	}

	public static IEnumerablePage<T> CreatePage<T>(this IEnumerable<T> dataPage, int pageNumber, int pageSize, int totalCount)
	{
		var pageCount = GetPageCount(pageSize, totalCount);

		return new EnumerablePage<T>
		{
			PageData = dataPage
			, PageNumber = pageNumber
			, PageSize = pageSize
			, TotalCount = totalCount
			, PageList = GetPageList(pageNumber, pageCount, 5)
		};
	}

	public static int GetPageCount(int pageSize, int collectionSize)
	{
		var pageCount = collectionSize / Math.Max(1, pageSize) + (collectionSize % pageSize == 0
			? 0
			: 1);
		return pageCount == 0
			? 1
			: pageCount;
	}

	private static IEnumerable<int> GetPageList(int pageNumber, int pageCount, int pageToShow)
	{
		var halfPageToShow = pageToShow / 2;
		var ret = new List<int>();

		if (pageNumber == 1 || pageNumber <= halfPageToShow)
		{
			for (var j = 1; j <= Math.Min(pageCount, pageToShow); j++)
			{
				ret.Add(j);
			}

			return ret;
		}

		if (pageNumber == pageCount || pageNumber + halfPageToShow >= pageCount)
		{
			for (var k = pageCount - Math.Min(pageCount, pageToShow) + 1; k <= pageCount; k++)
			{
				ret.Add(k);
			}

			return ret;
		}

		for (var i = 1; i <= pageCount; i++)
		{
			if (i == pageNumber ||
			    (i < pageNumber && i >= Math.Max(0, pageNumber - halfPageToShow)) ||
			    (i > pageNumber && i <= Math.Min(pageCount, pageNumber + halfPageToShow)))
			{
				ret.Add(i);
			}
		}

		return ret;
	}

	public static IEnumerablePage<T> Empty<T>() =>
		new EnumerablePage<T>
		{
			PageData = []
		};

	public static IEnumerablePage<T> FromInterface<T>(this EnumerablePage<T> page) =>
		new EnumerablePage<T>
		{
			PageData = page.PageData
			, PageNumber = page.PageNumber
			, PageList = page.PageList
			, PageSize = page.PageSize
			, TotalCount = page.TotalCount
		};
}