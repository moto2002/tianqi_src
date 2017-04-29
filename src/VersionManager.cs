using System;
using System.Linq;

public static class VersionManager
{
	private static string m_clientVersion;

	private static int[] m_clientVersions;

	private static string m_serverVersion;

	private static int[] m_serverVersions;

	public static string CurrentVersion
	{
		get
		{
			return VersionManager.m_clientVersion;
		}
		set
		{
			VersionManager.m_clientVersion = value;
			VersionManager.m_clientVersions = Enumerable.ToArray<int>(Enumerable.Select<string, int>(VersionManager.m_clientVersion.Split(new char[]
			{
				'.'
			}), (string x) => int.Parse(x)));
		}
	}

	public static string ServerVersion
	{
		get
		{
			return VersionManager.m_serverVersion;
		}
		set
		{
			VersionManager.m_serverVersion = value;
			VersionManager.m_serverVersions = Enumerable.ToArray<int>(Enumerable.Select<string, int>(VersionManager.m_serverVersion.Split(new char[]
			{
				'.'
			}), (string x) => int.Parse(x)));
		}
	}

	public static bool IsForceUpdateNeeded()
	{
		return VersionManager.m_serverVersions[0] > VersionManager.m_clientVersions[0];
	}

	public static bool IsCodeUpdateNeeded()
	{
		return VersionManager.m_serverVersions[1] > VersionManager.m_clientVersions[1];
	}

	public static bool IsSplitPackageUpdateNeeded()
	{
		return VersionManager.m_serverVersions[2] > VersionManager.m_clientVersions[2];
	}

	public static bool IsPatchUpdateNeeded()
	{
		return VersionManager.m_serverVersions[3] > VersionManager.m_clientVersions[3];
	}
}
