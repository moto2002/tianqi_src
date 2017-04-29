using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;

public class CompatibilityIP
{
	[DllImport("__Internal")]
	private static extern string getIPv6(string host, string port);

	private static string GetIPv6(string host, string port)
	{
		return host + "&&ipv4";
	}

	public static void GetIpType(string serverIp, string serverPort, out string newServerIp, out AddressFamily newServerAddressFamily)
	{
		newServerAddressFamily = 2;
		newServerIp = serverIp;
		try
		{
			string iPv = CompatibilityIP.GetIPv6(serverIp, serverPort);
			if (!string.IsNullOrEmpty(iPv))
			{
				string[] array = Regex.Split(iPv, "&&");
				if (array.Length >= 2)
				{
					string text = array[1];
					if (text == "ipv6")
					{
						newServerIp = array[0];
						newServerAddressFamily = 23;
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.get_Message());
		}
	}
}
