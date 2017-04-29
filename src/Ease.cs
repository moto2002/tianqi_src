using System;
using UnityEngine;

public static class Ease
{
	public static readonly Easer Linear = (float t) => t;

	public static readonly Easer QuadIn = (float t) => t * t;

	public static readonly Easer QuadOut = (float t) => 1f - Ease.QuadIn(1f - t);

	public static readonly Easer QuadInOut = (float t) => (t > 0.5f) ? (Ease.QuadOut(t * 2f - 1f) / 2f + 0.5f) : (Ease.QuadIn(t * 2f) / 2f);

	public static readonly Easer CubeIn = (float t) => t * t * t;

	public static readonly Easer CubeOut = (float t) => 1f - Ease.CubeIn(1f - t);

	public static readonly Easer CubeInOut = (float t) => (t > 0.5f) ? (Ease.CubeOut(t * 2f - 1f) / 2f + 0.5f) : (Ease.CubeIn(t * 2f) / 2f);

	public static readonly Easer BackIn = (float t) => t * t * (2.70158f * t - 1.70158f);

	public static readonly Easer BackOut = (float t) => 1f - Ease.BackIn(1f - t);

	public static readonly Easer BackInOut = (float t) => (t > 0.5f) ? (Ease.BackOut(t * 2f - 1f) / 2f + 0.5f) : (Ease.BackIn(t * 2f) / 2f);

	public static readonly Easer ExpoIn = (float t) => Mathf.Pow(2f, 10f * (t - 1f));

	public static readonly Easer ExpoOut = (float t) => 1f - Ease.ExpoIn(t);

	public static readonly Easer ExpoInOut = (float t) => (t >= 0.5f) ? (Ease.ExpoOut(t * 2f) / 2f) : (Ease.ExpoIn(t * 2f) / 2f);

	public static readonly Easer SineIn = (float t) => -Mathf.Cos(1.57079637f * t) + 1f;

	public static readonly Easer SineOut = (float t) => Mathf.Sin(1.57079637f * t);

	public static readonly Easer SineInOut = (float t) => -Mathf.Cos(3.14159274f * t) / 2f + 0.5f;

	public static readonly Easer ElasticIn = (float t) => 1f - Ease.ElasticOut(1f - t);

	public static readonly Easer ElasticOut = (float t) => Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - 0.075f) * 6.28318548f / 0.3f) + 1f;

	public static readonly Easer ElasticInOut = (float t) => (t > 0.5f) ? (Ease.ElasticOut(t * 2f - 1f) / 2f + 0.5f) : (Ease.ElasticIn(t * 2f) / 2f);

	public static Easer FromType(EaseType type)
	{
		switch (type)
		{
		case EaseType.Linear:
			return Ease.Linear;
		case EaseType.QuadIn:
			return Ease.QuadIn;
		case EaseType.QuadOut:
			return Ease.QuadOut;
		case EaseType.QuadInOut:
			return Ease.QuadInOut;
		case EaseType.CubeIn:
			return Ease.CubeIn;
		case EaseType.CubeOut:
			return Ease.CubeOut;
		case EaseType.CubeInOut:
			return Ease.CubeInOut;
		case EaseType.BackIn:
			return Ease.BackIn;
		case EaseType.BackOut:
			return Ease.BackOut;
		case EaseType.BackInOut:
			return Ease.BackInOut;
		case EaseType.ExpoIn:
			return Ease.ExpoIn;
		case EaseType.ExpoOut:
			return Ease.ExpoOut;
		case EaseType.ExpoInOut:
			return Ease.ExpoInOut;
		case EaseType.SineIn:
			return Ease.SineIn;
		case EaseType.SineOut:
			return Ease.SineOut;
		case EaseType.SineInOut:
			return Ease.SineInOut;
		case EaseType.ElasticIn:
			return Ease.ElasticIn;
		case EaseType.ElasticOut:
			return Ease.ElasticOut;
		case EaseType.ElasticInOut:
			return Ease.ElasticInOut;
		default:
			return Ease.Linear;
		}
	}
}
