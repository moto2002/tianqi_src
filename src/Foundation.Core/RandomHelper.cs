using System;
using UnityEngine;

namespace Foundation.Core
{
	public static class RandomHelper
	{
		public static float Range01()
		{
			return Random.Range(0f, 1f);
		}

		public static Vector3 Inside2DCircle(float radius)
		{
			Vector2 vector = Random.get_insideUnitCircle() * radius;
			Vector3 result = new Vector3(vector.x, 0f, vector.y);
			return result;
		}
	}
}
