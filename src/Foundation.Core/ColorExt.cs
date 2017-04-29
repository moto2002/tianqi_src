using System;
using UnityEngine;

namespace Foundation.Core
{
	public static class ColorExt
	{
		public static Color SetAlpha(this Color c, float a)
		{
			return new Color(c.r, c.g, c.b, a);
		}

		public static string Serialize(this Color c)
		{
			return string.Format("{0},{1},{2},{3}", new object[]
			{
				c.r,
				c.g,
				c.b,
				c.a
			});
		}

		public static Color Deserialize(string color)
		{
			string[] array = color.Split(new char[]
			{
				','
			});
			return new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
		}

		public static string ColorToHex(Color32 color)
		{
			return color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		}

		public static Color HexToColor(string hex)
		{
			byte b = byte.Parse(hex.Substring(0, 2), 515);
			byte b2 = byte.Parse(hex.Substring(2, 2), 515);
			byte b3 = byte.Parse(hex.Substring(4, 2), 515);
			return new Color32(b, b2, b3, 255);
		}
	}
}
