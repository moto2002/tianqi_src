using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Foundation.Core
{
	public static class StringHelper
	{
		public static byte[] GetBytes(this string str)
		{
			byte[] array = new byte[str.get_Length() * 2];
			Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
			return array;
		}

		public static string GetString(this byte[] bytes)
		{
			char[] array = new char[bytes.Length / 2];
			Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
			return new string(array);
		}

		public static string[] SplitByNewline(this string s)
		{
			return s.Split(new string[]
			{
				"\r\n",
				"\n"
			}, 0);
		}

		public static string RemoveNewline(this string s)
		{
			return s.Replace("\r\n", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
		}

		public static bool IsEmail(this string email)
		{
			Regex regex = new Regex("^(?!\\.)(\"([^\"\\r\\\\]|\\\\[\"\\r\\\\])*\"|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\\.)\\.)*)(?<!\\.)@[a-z0-9][\\w\\.-]*[a-z0-9]\\.[a-z][a-z\\.]*[a-z]$", 1);
			return regex.IsMatch(email);
		}

		public static int RandomNumber(int min, int max)
		{
			return Random.Range(min, max);
		}

		public static string RandomString(int size)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < size; i++)
			{
				char c = Convert.ToChar(Convert.ToInt32(Math.Floor((double)(26f * Random.get_value() + 65f))));
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		public static string GenerateId(int size)
		{
			string text = Guid.NewGuid().ToString().Replace("-", string.Empty);
			return text.Substring(0, size);
		}
	}
}
