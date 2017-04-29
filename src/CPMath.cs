using System;
using UnityEngine;

public class CPMath
{
	public static Vector3 CalculateBezier(float t, Vector3 p, Vector3 a, Vector3 b, Vector3 q)
	{
		float num = t * t;
		float num2 = num * t;
		float num3 = 1f - t;
		float num4 = num3 * num3;
		float num5 = num4 * num3;
		return num5 * p + 3f * num4 * t * a + 3f * num3 * num * b + num2 * q;
	}

	public static Vector3 CalculateHermite(Vector3 p, Vector3 a, Vector3 b, Vector3 q, float t, float tension, float bias)
	{
		float num = t * t;
		float num2 = num * t;
		Vector3 vector = (a - p) * (1f + bias) * (1f - tension) / 2f;
		vector += (b - a) * (1f - bias) * (1f - tension) / 2f;
		Vector3 vector2 = (b - a) * (1f + bias) * (1f - tension) / 2f;
		vector2 += (q - b) * (1f - bias) * (1f - tension) / 2f;
		float num3 = 2f * num2 - 3f * num + 1f;
		float num4 = num2 - 2f * num + t;
		float num5 = num2 - num;
		float num6 = -2f * num2 + 3f * num;
		return num3 * a + num4 * vector + num5 * vector2 + num6 * b;
	}

	public static Vector3 CalculateCatmullRom(Vector3 p, Vector3 a, Vector3 b, Vector3 q, float t)
	{
		float num = t * t;
		Vector3 vector = -0.5f * p + 1.5f * a - 1.5f * b + 0.5f * q;
		Vector3 vector2 = p - 2.5f * a + 2f * b - 0.5f * q;
		Vector3 vector3 = -0.5f * p + 0.5f * b;
		return vector * t * num + vector2 * num + vector3 * t + a;
	}

	public static Vector2 CalculateBezier(float t, Vector2 p, Vector2 a, Vector2 b, Vector2 q)
	{
		float num = t * t;
		float num2 = num * t;
		float num3 = 1f - t;
		float num4 = num3 * num3;
		float num5 = num4 * num3;
		return num5 * p + 3f * num4 * t * a + 3f * num3 * num * b + num2 * q;
	}

	public static Vector2 CalculateHermite(Vector2 p, Vector2 a, Vector2 b, Vector2 q, float t, float tension, float bias)
	{
		float num = t * t;
		float num2 = num * t;
		Vector2 vector = (a - p) * (1f + bias) * (1f - tension) / 2f;
		vector += (b - a) * (1f - bias) * (1f - tension) / 2f;
		Vector2 vector2 = (b - a) * (1f + bias) * (1f - tension) / 2f;
		vector2 += (q - b) * (1f - bias) * (1f - tension) / 2f;
		float num3 = 2f * num2 - 3f * num + 1f;
		float num4 = num2 - 2f * num + t;
		float num5 = num2 - num;
		float num6 = -2f * num2 + 3f * num;
		return num3 * a + num4 * vector + num5 * vector2 + num6 * b;
	}

	public static Vector2 CalculateCatmullRom(Vector2 p, Vector2 a, Vector2 b, Vector2 q, float t)
	{
		float num = t * t;
		Vector2 vector = -0.5f * p + 1.5f * a - 1.5f * b + 0.5f * q;
		Vector2 vector2 = p - 2.5f * a + 2f * b - 0.5f * q;
		Vector2 vector3 = -0.5f * p + 0.5f * b;
		return vector * t * num + vector2 * num + vector3 * t + a;
	}

	public static Quaternion CalculateCubic(Quaternion p, Quaternion a, Quaternion b, Quaternion q, float t)
	{
		if (Quaternion.Dot(p, q) < 0f)
		{
			q = new Quaternion(-q.x, -q.y, -q.z, -q.w);
		}
		if (Quaternion.Dot(p, a) < 0f)
		{
			a = new Quaternion(-a.x, -a.y, -a.z, -a.w);
		}
		if (Quaternion.Dot(p, b) < 0f)
		{
			b = new Quaternion(-b.x, -b.y, -b.z, -b.w);
		}
		Quaternion p2 = CPMath.SquadTangent(a, p, q);
		Quaternion q2 = CPMath.SquadTangent(p, q, b);
		float t2 = 2f * t * (1f - t);
		return CPMath.Slerp(CPMath.Slerp(p, q, t), CPMath.Slerp(p2, q2, t), t2);
	}

	public static float CalculateCubic(float p, float a, float b, float q, float t)
	{
		float num = t * t;
		float num2 = num * t;
		float num3 = 1f - t;
		float num4 = num3 * num3;
		float num5 = num4 * num3;
		return num5 * p + 3f * num4 * t * q + 3f * num3 * num * a + num2 * b;
	}

	public static float CalculateHermite(float p, float a, float b, float q, float t, float tension, float bias)
	{
		float num = t * t;
		float num2 = num * t;
		float num3 = (a - p) * (1f + bias) * (1f - tension) / 2f;
		num3 += (b - a) * (1f - bias) * (1f - tension) / 2f;
		float num4 = (b - a) * (1f + bias) * (1f - tension) / 2f;
		num4 += (q - b) * (1f - bias) * (1f - tension) / 2f;
		float num5 = 2f * num2 - 3f * num + 1f;
		float num6 = num2 - 2f * num + t;
		float num7 = num2 - num;
		float num8 = -2f * num2 + 3f * num;
		return num5 * a + num6 * num3 + num7 * num4 + num8 * b;
	}

	public static float CalculateCatmullRom(float p, float a, float b, float q, float t)
	{
		float num = t * t;
		float num2 = -0.5f * p + 1.5f * a - 1.5f * b + 0.5f * q;
		float num3 = p - 2.5f * a + 2f * b - 0.5f * q;
		float num4 = -0.5f * p + 0.5f * b;
		return num2 * t * num + num3 * num + num4 * t + a;
	}

	public static float SmoothStep(float val)
	{
		return val * val * (3f - 2f * val);
	}

	public static Quaternion SquadTangent(Quaternion before, Quaternion center, Quaternion after)
	{
		Quaternion quaternion = CPMath.LnDif(center, before);
		Quaternion quaternion2 = CPMath.LnDif(center, after);
		Quaternion identity = Quaternion.get_identity();
		for (int i = 0; i < 4; i++)
		{
			identity.set_Item(i, -0.25f * (quaternion.get_Item(i) + quaternion2.get_Item(i)));
		}
		return center * CPMath.Exp(identity);
	}

	public static Quaternion LnDif(Quaternion a, Quaternion b)
	{
		Quaternion q = Quaternion.Inverse(a) * b;
		CPMath.Normalize(q);
		return CPMath.Log(q);
	}

	public static Quaternion Normalize(Quaternion q)
	{
		float num = Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
		if (num > 0f)
		{
			q.x /= num;
			q.y /= num;
			q.z /= num;
			q.w /= num;
		}
		else
		{
			q.x = 0f;
			q.y = 0f;
			q.z = 0f;
			q.w = 1f;
		}
		return q;
	}

	public static Quaternion Exp(Quaternion q)
	{
		float num = Mathf.Sqrt(q.get_Item(0) * q.get_Item(0) + q.get_Item(1) * q.get_Item(1) + q.get_Item(2) * q.get_Item(2));
		if ((double)num < 1E-06)
		{
			return new Quaternion(q.get_Item(0), q.get_Item(1), q.get_Item(2), Mathf.Cos(num));
		}
		float num2 = Mathf.Sin(num) / num;
		return new Quaternion(q.get_Item(0) * num2, q.get_Item(1) * num2, q.get_Item(2) * num2, Mathf.Cos(num));
	}

	public static Quaternion Log(Quaternion q)
	{
		float num = Mathf.Sqrt(q.get_Item(0) * q.get_Item(0) + q.get_Item(1) * q.get_Item(1) + q.get_Item(2) * q.get_Item(2));
		if ((double)num < 1E-06)
		{
			return new Quaternion(q.get_Item(0), q.get_Item(1), q.get_Item(2), 0f);
		}
		float num2 = Mathf.Acos(q.get_Item(3)) / num;
		return new Quaternion(q.get_Item(0) * num2, q.get_Item(1) * num2, q.get_Item(2) * num2, 0f);
	}

	public static Quaternion Slerp(Quaternion p, Quaternion q, float t)
	{
		float num = Quaternion.Dot(p, q);
		Quaternion result;
		if (1f + num > 1E-05f)
		{
			float num5;
			float num6;
			if (1f - num > 1E-05f)
			{
				float num2 = Mathf.Acos(num);
				float num3 = Mathf.Sin(num2);
				float num4 = Mathf.Sign(num3) * 1f / num3;
				num5 = Mathf.Sin((1f - t) * num2) * num4;
				num6 = Mathf.Sin(t * num2) * num4;
			}
			else
			{
				num5 = 1f - t;
				num6 = t;
			}
			result.x = num5 * p.x + num6 * q.x;
			result.y = num5 * p.y + num6 * q.y;
			result.z = num5 * p.z + num6 * q.z;
			result.w = num5 * p.w + num6 * q.w;
		}
		else
		{
			float num5 = Mathf.Sin((1f - t) * 3.14159274f * 0.5f);
			float num6 = Mathf.Sin(t * 3.14159274f * 0.5f);
			result.x = num5 * p.x - num6 * p.y;
			result.y = num5 * p.y + num6 * p.x;
			result.z = num5 * p.z - num6 * p.w;
			result.w = p.z;
		}
		return result;
	}

	public static Quaternion Nlerp(Quaternion p, Quaternion q, float t)
	{
		float num = 1f - t;
		Quaternion quaternion;
		quaternion.x = num * p.x + t * q.x;
		quaternion.y = num * p.y + t * q.y;
		quaternion.z = num * p.z + t * q.z;
		quaternion.w = num * p.w + t * q.w;
		CPMath.Normalize(quaternion);
		return quaternion;
	}

	public static Quaternion GetQuatConjugate(Quaternion q)
	{
		return new Quaternion(-q.x, -q.y, -q.z, q.w);
	}

	public static float SignedAngle(Vector3 from, Vector3 to, Vector3 up)
	{
		Vector3 normalized = (to - from).get_normalized();
		Vector3 vector = Vector3.Cross(up, normalized);
		float num = Vector3.Dot(from, vector);
		return Vector3.Angle(from, to) * Mathf.Sign(num);
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, -max, -min);
	}
}
