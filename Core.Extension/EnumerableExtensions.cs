namespace Core.Extension;

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
}