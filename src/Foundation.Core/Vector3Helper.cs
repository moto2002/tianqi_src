using System;
using System.Linq;
using UnityEngine;

namespace Foundation.Core
{
	public static class Vector3Helper
	{
		public static Vector3 Parse(string text)
		{
			string[] array = text.Split(new char[]
			{
				','
			});
			if (Enumerable.Count<string>(array) != 3)
			{
				Debug.Log("Error:Vector3.Parse takes an input of float,float,float");
				return Vector3.get_zero();
			}
			return new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}

		public static Vector2 To2D(this Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		public static Vector3 To3D(this Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}
	}
}
