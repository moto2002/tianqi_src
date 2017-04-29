using System;

public class Singleton<T> where T : new()
{
	private static T Instance = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);

	public static T S
	{
		get
		{
			return Singleton<T>.Instance;
		}
	}
}
