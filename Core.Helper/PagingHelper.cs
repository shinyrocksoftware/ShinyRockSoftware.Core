using Core.Model;
using Core.Model.Interface;

namespace Core.Helper;

public static class PagingHelper
{
	public static IEnumerable<T> GetPlainPage<T>(IEnumerable<T> collection, int pageNumber, int pageSize)
	{
		return pageSize == 0
			? collection
			: collection.Skip((pageNumber - 1) * pageSize)
			            .Take(pageSize);
	}

	public static IEnumerablePage<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int pageSize)
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

	public static IEnumerablePage<TM> CreatePage<T, TM>(IEnumerablePage<T> dataPage, Func<T, TM> transformMethod)
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

	public static IEnumerablePage<T> CreatePage<T>(IEnumerable<T> dataPage, int pageNumber, int pageSize, int totalCount)
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
			PageData = Enumerable.Empty<T>()
		};

	public static IEnumerablePage<T> FromInterface<T>(EnumerablePage<T> page) =>
		new EnumerablePage<T>
		{
			PageData = page.PageData
			, PageNumber = page.PageNumber
			, PageList = page.PageList
			, PageSize = page.PageSize
			, TotalCount = page.TotalCount
		};
}