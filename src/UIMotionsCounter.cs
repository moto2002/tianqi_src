using System;
using System.Collections.Generic;

public class UIMotionsCounter
{
	public List<string> listMotions = new List<string>();

	private static UIMotionsCounter m_Instance;

	public static UIMotionsCounter Instance
	{
		get
		{
			if (UIMotionsCounter.m_Instance == null)
			{
				UIMotionsCounter.m_Instance = new UIMotionsCounter();
			}
			return UIMotionsCounter.m_Instance;
		}
	}
}
