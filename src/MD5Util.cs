using LuaFramework;
using System;
using System.Security.Cryptography;
using System.Text;

public class MD5Util
{
	private const int BufferSize = 16384;

	public static string Encrypt(byte[] data)
	{
		MD5 mD = new MD5CryptoServiceProvider();
		byte[] array = mD.ComputeHash(data);
		mD.Clear();
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += array[i].ToString("x").PadLeft(2, '0');
		}
		return text.ToLower();
	}

	public static string Encrypt(string strPwd)
	{
		byte[] bytes = Encoding.get_Default().GetBytes(strPwd);
		return MD5Util.Encrypt(bytes);
	}

	public static string EncryptFile(string filePath)
	{
		return Util.md5file(filePath);
	}
}
