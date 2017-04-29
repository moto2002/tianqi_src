using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NcInterpolate
{
	public enum EaseType
	{
		Linear,
		EaseInQuad,
		EaseOutQuad,
		EaseInOutQuad,
		EaseInCubic,
		EaseOutCubic,
		EaseInOutCubic,
		EaseInQuart,
		EaseOutQuart,
		EaseInOutQuart,
		EaseInQuint,
		EaseOutQuint,
		EaseInOutQuint,
		EaseInSine,
		EaseOutSine,
		EaseInOutSine,
		EaseInExpo,
		EaseOutExpo,
		EaseInOutExpo,
		EaseInCirc,
		EaseOutCirc,
		EaseInOutCirc
	}

	public delegate Vector3 ToVector3<T>(T v);

	public delegate float Function(float a, float b, float c, float d);

	private static Vector3 Identity(Vector3 v)
	{
		return v;
	}

	private static Vector3 TransformDotPosition(Transform t)
	{
		return t.get_position();
	}

	[DebuggerHidden]
	private static IEnumerable<float> NewTimer(float duration)
	{
		NcInterpolate.<NewTimer>c__Iterator0 <NewTimer>c__Iterator = new NcInterpolate.<NewTimer>c__Iterator0();
		<NewTimer>c__Iterator.duration = duration;
		<NewTimer>c__Iterator.<$>duration = duration;
		NcInterpolate.<NewTimer>c__Iterator0 expr_15 = <NewTimer>c__Iterator;
		expr_15.$PC = -2;
		return expr_15;
	}

	[DebuggerHidden]
	private static IEnumerable<float> NewCounter(int start, int end, int step)
	{
		NcInterpolate.<NewCounter>c__Iterator1 <NewCounter>c__Iterator = new NcInterpolate.<NewCounter>c__Iterator1();
		<NewCounter>c__Iterator.start = start;
		<NewCounter>c__Iterator.end = end;
		<NewCounter>c__Iterator.step = step;
		<NewCounter>c__Iterator.<$>start = start;
		<NewCounter>c__Iterator.<$>end = end;
		<NewCounter>c__Iterator.<$>step = step;
		NcInterpolate.<NewCounter>c__Iterator1 expr_31 = <NewCounter>c__Iterator;
		expr_31.$PC = -2;
		return expr_31;
	}

	public static IEnumerator NewEase(NcInterpolate.Function ease, Vector3 start, Vector3 end, float duration)
	{
		IEnumerable<float> driver = NcInterpolate.NewTimer(duration);
		return NcInterpolate.NewEase(ease, start, end, duration, driver);
	}

	public static IEnumerator NewEase(NcInterpolate.Function ease, Vector3 start, Vector3 end, int slices)
	{
		IEnumerable<float> driver = NcInterpolate.NewCounter(0, slices + 1, 1);
		return NcInterpolate.NewEase(ease, start, end, (float)(slices + 1), driver);
	}

	[DebuggerHidden]
	private static IEnumerator NewEase(NcInterpolate.Function ease, Vector3 start, Vector3 end, float total, IEnumerable<float> driver)
	{
		NcInterpolate.<NewEase>c__Iterator2 <NewEase>c__Iterator = new NcInterpolate.<NewEase>c__Iterator2();
		<NewEase>c__Iterator.end = end;
		<NewEase>c__Iterator.start = start;
		<NewEase>c__Iterator.driver = driver;
		<NewEase>c__Iterator.ease = ease;
		<NewEase>c__Iterator.total = total;
		<NewEase>c__Iterator.<$>end = end;
		<NewEase>c__Iterator.<$>start = start;
		<NewEase>c__Iterator.<$>driver = driver;
		<NewEase>c__Iterator.<$>ease = ease;
		<NewEase>c__Iterator.<$>total = total;
		return <NewEase>c__Iterator;
	}

	private static Vector3 Ease(NcInterpolate.Function ease, Vector3 start, Vector3 distance, float elapsedTime, float duration)
	{
		start.x = ease(start.x, distance.x, elapsedTime, duration);
		start.y = ease(start.y, distance.y, elapsedTime, duration);
		start.z = ease(start.z, distance.z, elapsedTime, duration);
		return start;
	}

	public static NcInterpolate.Function Ease(NcInterpolate.EaseType type)
	{
		NcInterpolate.Function result = null;
		switch (type)
		{
		case NcInterpolate.EaseType.Linear:
			result = new NcInterpolate.Function(NcInterpolate.Linear);
			break;
		case NcInterpolate.EaseType.EaseInQuad:
			result = new NcInterpolate.Function(NcInterpolate.EaseInQuad);
			break;
		case NcInterpolate.EaseType.EaseOutQuad:
			result = new NcInterpolate.Function(NcInterpolate.EaseOutQuad);
			break;
		case NcInterpolate.EaseType.EaseInOutQuad:
			result = new NcInterpolate.Function(NcInterpolate.EaseInOutQuad);
			break;
		case NcInterpolate.EaseType.EaseInCubic:
			result = new NcInterpolate.Function(NcInterpolate.EaseInCubic);
			break;
		case NcInterpolate.EaseType.EaseOutCubic:
			result = new NcInterpolate.Function(NcInterpolate.EaseOutCubic);
			break;
		case NcInterpolate.EaseType.EaseInOutCubic:
			result = new NcInterpolate.Function(NcInterpolate.EaseInOutCubic);
			break;
		case NcInterpolate.EaseType.EaseInQuart:
			result = new NcInterpolate.Function(NcInterpolate.EaseInQuart);
			break;
		case NcInterpolate.EaseType.EaseOutQuart:
			result = new NcInterpolate.Function(NcInterpolate.EaseOutQuart);
			break;
		case NcInterpolate.EaseType.EaseInOutQuart:
			result = new NcInterpolate.Function(NcInterpolate.EaseInOutQuart);
			break;
		case NcInterpolate.EaseType.EaseInQuint:
			result = new NcInterpolate.Function(NcInterpolate.EaseInQuint);
			break;
		case NcInterpolate.EaseType.EaseOutQuint:
			result = new NcInterpolate.Function(NcInterpolate.EaseOutQuint);
			break;
		case NcInterpolate.EaseType.EaseInOutQuint:
			result = new NcInterpolate.Function(NcInterpolate.EaseInOutQuint);
			break;
		case NcInterpolate.EaseType.EaseInSine:
			result = new NcInterpolate.Function(NcInterpolate.EaseInSine);
			break;
		case NcInterpolate.EaseType.EaseOutSine:
			result = new NcInterpolate.Function(NcInterpolate.EaseOutSine);
			break;
		case NcInterpolate.EaseType.EaseInOutSine:
			result = new NcInterpolate.Function(NcInterpolate.EaseInOutSine);
			break;
		case NcInterpolate.EaseType.EaseInExpo:
			result = new NcInterpolate.Function(NcInterpolate.EaseInExpo);
			break;
		case NcInterpolate.EaseType.EaseOutExpo:
			result = new NcInterpolate.Function(NcInterpolate.EaseOutExpo);
			break;
		case NcInterpolate.EaseType.EaseInOutExpo:
			result = new NcInterpolate.Function(NcInterpolate.EaseInOutExpo);
			break;
		case NcInterpolate.EaseType.EaseInCirc:
			result = new NcInterpolate.Function(NcInterpolate.EaseInCirc);
			break;
		case NcInterpolate.EaseType.EaseOutCirc:
			result = new NcInterpolate.Function(NcInterpolate.EaseOutCirc);
			break;
		case NcInterpolate.EaseType.EaseInOutCirc:
			result = new NcInterpolate.Function(NcInterpolate.EaseInOutCirc);
			break;
		}
		return result;
	}

	public static IEnumerable<Vector3> NewBezier(NcInterpolate.Function ease, Transform[] nodes, float duration)
	{
		IEnumerable<float> steps = NcInterpolate.NewTimer(duration);
		return NcInterpolate.NewBezier<Transform>(ease, nodes, new NcInterpolate.ToVector3<Transform>(NcInterpolate.TransformDotPosition), duration, steps);
	}

	public static IEnumerable<Vector3> NewBezier(NcInterpolate.Function ease, Transform[] nodes, int slices)
	{
		IEnumerable<float> steps = NcInterpolate.NewCounter(0, slices + 1, 1);
		return NcInterpolate.NewBezier<Transform>(ease, nodes, new NcInterpolate.ToVector3<Transform>(NcInterpolate.TransformDotPosition), (float)(slices + 1), steps);
	}

	public static IEnumerable<Vector3> NewBezier(NcInterpolate.Function ease, Vector3[] points, float duration)
	{
		IEnumerable<float> steps = NcInterpolate.NewTimer(duration);
		return NcInterpolate.NewBezier<Vector3>(ease, points, new NcInterpolate.ToVector3<Vector3>(NcInterpolate.Identity), duration, steps);
	}

	public static IEnumerable<Vector3> NewBezier(NcInterpolate.Function ease, Vector3[] points, int slices)
	{
		IEnumerable<float> steps = NcInterpolate.NewCounter(0, slices + 1, 1);
		return NcInterpolate.NewBezier<Vector3>(ease, points, new NcInterpolate.ToVector3<Vector3>(NcInterpolate.Identity), (float)(slices + 1), steps);
	}

	[DebuggerHidden]
	private static IEnumerable<Vector3> NewBezier<T>(NcInterpolate.Function ease, IList nodes, NcInterpolate.ToVector3<T> toVector3, float maxStep, IEnumerable<float> steps)
	{
		NcInterpolate.<NewBezier>c__Iterator3<T> <NewBezier>c__Iterator = new NcInterpolate.<NewBezier>c__Iterator3<T>();
		<NewBezier>c__Iterator.nodes = nodes;
		<NewBezier>c__Iterator.steps = steps;
		<NewBezier>c__Iterator.toVector3 = toVector3;
		<NewBezier>c__Iterator.ease = ease;
		<NewBezier>c__Iterator.maxStep = maxStep;
		<NewBezier>c__Iterator.<$>nodes = nodes;
		<NewBezier>c__Iterator.<$>steps = steps;
		<NewBezier>c__Iterator.<$>toVector3 = toVector3;
		<NewBezier>c__Iterator.<$>ease = ease;
		<NewBezier>c__Iterator.<$>maxStep = maxStep;
		NcInterpolate.<NewBezier>c__Iterator3<T> expr_4F = <NewBezier>c__Iterator;
		expr_4F.$PC = -2;
		return expr_4F;
	}

	private static Vector3 Bezier(NcInterpolate.Function ease, Vector3[] points, float elapsedTime, float duration)
	{
		for (int i = points.Length - 1; i > 0; i--)
		{
			for (int j = 0; j < i; j++)
			{
				points[j].x = ease(points[j].x, points[j + 1].x - points[j].x, elapsedTime, duration);
				points[j].y = ease(points[j].y, points[j + 1].y - points[j].y, elapsedTime, duration);
				points[j].z = ease(points[j].z, points[j + 1].z - points[j].z, elapsedTime, duration);
			}
		}
		return points[0];
	}

	public static IEnumerable<Vector3> NewCatmullRom(Transform[] nodes, int slices, bool loop)
	{
		return NcInterpolate.NewCatmullRom<Transform>(nodes, new NcInterpolate.ToVector3<Transform>(NcInterpolate.TransformDotPosition), slices, loop);
	}

	public static IEnumerable<Vector3> NewCatmullRom(Vector3[] points, int slices, bool loop)
	{
		return NcInterpolate.NewCatmullRom<Vector3>(points, new NcInterpolate.ToVector3<Vector3>(NcInterpolate.Identity), slices, loop);
	}

	[DebuggerHidden]
	private static IEnumerable<Vector3> NewCatmullRom<T>(IList nodes, NcInterpolate.ToVector3<T> toVector3, int slices, bool loop)
	{
		NcInterpolate.<NewCatmullRom>c__Iterator4<T> <NewCatmullRom>c__Iterator = new NcInterpolate.<NewCatmullRom>c__Iterator4<T>();
		<NewCatmullRom>c__Iterator.nodes = nodes;
		<NewCatmullRom>c__Iterator.toVector3 = toVector3;
		<NewCatmullRom>c__Iterator.loop = loop;
		<NewCatmullRom>c__Iterator.slices = slices;
		<NewCatmullRom>c__Iterator.<$>nodes = nodes;
		<NewCatmullRom>c__Iterator.<$>toVector3 = toVector3;
		<NewCatmullRom>c__Iterator.<$>loop = loop;
		<NewCatmullRom>c__Iterator.<$>slices = slices;
		NcInterpolate.<NewCatmullRom>c__Iterator4<T> expr_3F = <NewCatmullRom>c__Iterator;
		expr_3F.$PC = -2;
		return expr_3F;
	}

	private static Vector3 CatmullRom(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, float elapsedTime, float duration)
	{
		float num = elapsedTime / duration;
		float num2 = num * num;
		float num3 = num2 * num;
		return previous * (-0.5f * num3 + num2 - 0.5f * num) + start * (1.5f * num3 + -2.5f * num2 + 1f) + end * (-1.5f * num3 + 2f * num2 + 0.5f * num) + next * (0.5f * num3 - 0.5f * num2);
	}

	private static float Linear(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return distance * (elapsedTime / duration) + start;
	}

	private static float EaseInQuad(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return distance * elapsedTime * elapsedTime + start;
	}

	private static float EaseOutQuad(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return -distance * elapsedTime * (elapsedTime - 2f) + start;
	}

	private static float EaseInOutQuad(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return distance / 2f * elapsedTime * elapsedTime + start;
		}
		elapsedTime -= 1f;
		return -distance / 2f * (elapsedTime * (elapsedTime - 2f) - 1f) + start;
	}

	private static float EaseInCubic(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return distance * elapsedTime * elapsedTime * elapsedTime + start;
	}

	private static float EaseOutCubic(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		elapsedTime -= 1f;
		return distance * (elapsedTime * elapsedTime * elapsedTime + 1f) + start;
	}

	private static float EaseInOutCubic(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return distance / 2f * elapsedTime * elapsedTime * elapsedTime + start;
		}
		elapsedTime -= 2f;
		return distance / 2f * (elapsedTime * elapsedTime * elapsedTime + 2f) + start;
	}

	private static float EaseInQuart(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return distance * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
	}

	private static float EaseOutQuart(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		elapsedTime -= 1f;
		return -distance * (elapsedTime * elapsedTime * elapsedTime * elapsedTime - 1f) + start;
	}

	private static float EaseInOutQuart(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return distance / 2f * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
		}
		elapsedTime -= 2f;
		return -distance / 2f * (elapsedTime * elapsedTime * elapsedTime * elapsedTime - 2f) + start;
	}

	private static float EaseInQuint(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return distance * elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
	}

	private static float EaseOutQuint(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		elapsedTime -= 1f;
		return distance * (elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + 1f) + start;
	}

	private static float EaseInOutQuint(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return distance / 2f * elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + start;
		}
		elapsedTime -= 2f;
		return distance / 2f * (elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + 2f) + start;
	}

	private static float EaseInSine(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return -distance * Mathf.Cos(elapsedTime / duration * 1.57079637f) + distance + start;
	}

	private static float EaseOutSine(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return distance * Mathf.Sin(elapsedTime / duration * 1.57079637f) + start;
	}

	private static float EaseInOutSine(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return -distance / 2f * (Mathf.Cos(3.14159274f * elapsedTime / duration) - 1f) + start;
	}

	private static float EaseInExpo(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return distance * Mathf.Pow(2f, 10f * (elapsedTime / duration - 1f)) + start;
	}

	private static float EaseOutExpo(float start, float distance, float elapsedTime, float duration)
	{
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return distance * (-Mathf.Pow(2f, -10f * elapsedTime / duration) + 1f) + start;
	}

	private static float EaseInOutExpo(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return distance / 2f * Mathf.Pow(2f, 10f * (elapsedTime - 1f)) + start;
		}
		elapsedTime -= 1f;
		return distance / 2f * (-Mathf.Pow(2f, -10f * elapsedTime) + 2f) + start;
	}

	private static float EaseInCirc(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		return -distance * (Mathf.Sqrt(1f - elapsedTime * elapsedTime) - 1f) + start;
	}

	private static float EaseOutCirc(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / duration) : 1f);
		elapsedTime -= 1f;
		return distance * Mathf.Sqrt(1f - elapsedTime * elapsedTime) + start;
	}

	private static float EaseInOutCirc(float start, float distance, float elapsedTime, float duration)
	{
		elapsedTime = ((elapsedTime <= duration) ? (elapsedTime / (duration / 2f)) : 2f);
		if (elapsedTime < 1f)
		{
			return -distance / 2f * (Mathf.Sqrt(1f - elapsedTime * elapsedTime) - 1f) + start;
		}
		elapsedTime -= 2f;
		return distance / 2f * (Mathf.Sqrt(1f - elapsedTime * elapsedTime) + 1f) + start;
	}
}
