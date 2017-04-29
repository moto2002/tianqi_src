using System;
using UnityEngine;

public static class PlayerPrefsExt
{
	public static bool GetBool(string key)
	{
		return PlayerPrefs.GetInt(key, 0) == 1;
	}

	public static bool GetBool(string key, bool defaultValue)
	{
		return PlayerPrefs.GetInt(key, (!defaultValue) ? 0 : 1) == 1;
	}

	public static void SetBool(string key, bool value)
	{
		PlayerPrefs.SetInt(key, (!value) ? 0 : 1);
	}

	public static void SetStringPrefs(string key, string value)
	{
		PlayerPrefs.SetString(key, value);
	}

	public static string GetStringPrefs(string key)
	{
		return PlayerPrefs.GetString(key, string.Empty);
	}

	public static void SetIntPrefs(string key, int value)
	{
		PlayerPrefs.SetInt(key, value);
	}

	public static int GetIntPrefs(string key)
	{
		return PlayerPrefs.GetInt(key, 0);
	}
}
