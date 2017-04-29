using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public static class Auto
{
	[DebuggerHidden]
	public static IEnumerator Wait(float duration)
	{
		Auto.<Wait>c__Iterator4B <Wait>c__Iterator4B = new Auto.<Wait>c__Iterator4B();
		<Wait>c__Iterator4B.duration = duration;
		<Wait>c__Iterator4B.<$>duration = duration;
		return <Wait>c__Iterator4B;
	}

	[DebuggerHidden]
	public static IEnumerator WaitUntil(Predicate predicate)
	{
		Auto.<WaitUntil>c__Iterator4C <WaitUntil>c__Iterator4C = new Auto.<WaitUntil>c__Iterator4C();
		<WaitUntil>c__Iterator4C.predicate = predicate;
		<WaitUntil>c__Iterator4C.<$>predicate = predicate;
		return <WaitUntil>c__Iterator4C;
	}

	public static IEnumerator MoveTo(this RectTransform trans, Vector3 target, float duration, EaseType ease)
	{
		return trans.MoveTo(target, duration, Ease.FromType(ease));
	}

	[DebuggerHidden]
	public static IEnumerator MoveTo(this RectTransform trans, Vector3 target, float duration, Easer ease)
	{
		Auto.<MoveTo>c__Iterator4D <MoveTo>c__Iterator4D = new Auto.<MoveTo>c__Iterator4D();
		<MoveTo>c__Iterator4D.trans = trans;
		<MoveTo>c__Iterator4D.target = target;
		<MoveTo>c__Iterator4D.duration = duration;
		<MoveTo>c__Iterator4D.ease = ease;
		<MoveTo>c__Iterator4D.<$>trans = trans;
		<MoveTo>c__Iterator4D.<$>target = target;
		<MoveTo>c__Iterator4D.<$>duration = duration;
		<MoveTo>c__Iterator4D.<$>ease = ease;
		return <MoveTo>c__Iterator4D;
	}

	[DebuggerHidden]
	public static IEnumerator MoveTo(this RectTransform trans, Vector3 target, float duration, EaseType ease, Action callback)
	{
		Auto.<MoveTo>c__Iterator4E <MoveTo>c__Iterator4E = new Auto.<MoveTo>c__Iterator4E();
		<MoveTo>c__Iterator4E.trans = trans;
		<MoveTo>c__Iterator4E.target = target;
		<MoveTo>c__Iterator4E.duration = duration;
		<MoveTo>c__Iterator4E.ease = ease;
		<MoveTo>c__Iterator4E.callback = callback;
		<MoveTo>c__Iterator4E.<$>trans = trans;
		<MoveTo>c__Iterator4E.<$>target = target;
		<MoveTo>c__Iterator4E.<$>duration = duration;
		<MoveTo>c__Iterator4E.<$>ease = ease;
		<MoveTo>c__Iterator4E.<$>callback = callback;
		return <MoveTo>c__Iterator4E;
	}

	public static IEnumerator MoveToAnchoredPosition(this RectTransform trans, Vector2 target, float duration, EaseType ease, Action callback = null)
	{
		return trans.MoveToAnchoredPosition(target, duration, Ease.FromType(ease), callback);
	}

	[DebuggerHidden]
	public static IEnumerator MoveToAnchoredPosition(this RectTransform trans, Vector2 target, float duration, Easer ease, Action callback = null)
	{
		Auto.<MoveToAnchoredPosition>c__Iterator4F <MoveToAnchoredPosition>c__Iterator4F = new Auto.<MoveToAnchoredPosition>c__Iterator4F();
		<MoveToAnchoredPosition>c__Iterator4F.trans = trans;
		<MoveToAnchoredPosition>c__Iterator4F.target = target;
		<MoveToAnchoredPosition>c__Iterator4F.duration = duration;
		<MoveToAnchoredPosition>c__Iterator4F.ease = ease;
		<MoveToAnchoredPosition>c__Iterator4F.callback = callback;
		<MoveToAnchoredPosition>c__Iterator4F.<$>trans = trans;
		<MoveToAnchoredPosition>c__Iterator4F.<$>target = target;
		<MoveToAnchoredPosition>c__Iterator4F.<$>duration = duration;
		<MoveToAnchoredPosition>c__Iterator4F.<$>ease = ease;
		<MoveToAnchoredPosition>c__Iterator4F.<$>callback = callback;
		return <MoveToAnchoredPosition>c__Iterator4F;
	}

	[DebuggerHidden]
	public static IEnumerator ScaleTo(this RectTransform trans, Vector3 target, float duration, Easer ease)
	{
		Auto.<ScaleTo>c__Iterator50 <ScaleTo>c__Iterator = new Auto.<ScaleTo>c__Iterator50();
		<ScaleTo>c__Iterator.trans = trans;
		<ScaleTo>c__Iterator.target = target;
		<ScaleTo>c__Iterator.duration = duration;
		<ScaleTo>c__Iterator.ease = ease;
		<ScaleTo>c__Iterator.<$>trans = trans;
		<ScaleTo>c__Iterator.<$>target = target;
		<ScaleTo>c__Iterator.<$>duration = duration;
		<ScaleTo>c__Iterator.<$>ease = ease;
		return <ScaleTo>c__Iterator;
	}

	[DebuggerHidden]
	public static IEnumerator ColorTo(this Image img, Color target, float duration, Easer ease)
	{
		Auto.<ColorTo>c__Iterator51 <ColorTo>c__Iterator = new Auto.<ColorTo>c__Iterator51();
		<ColorTo>c__Iterator.img = img;
		<ColorTo>c__Iterator.target = target;
		<ColorTo>c__Iterator.duration = duration;
		<ColorTo>c__Iterator.ease = ease;
		<ColorTo>c__Iterator.<$>img = img;
		<ColorTo>c__Iterator.<$>target = target;
		<ColorTo>c__Iterator.<$>duration = duration;
		<ColorTo>c__Iterator.<$>ease = ease;
		return <ColorTo>c__Iterator;
	}

	public static Quaternion Loop(float duration, Quaternion from, Quaternion to, float offsetPercent)
	{
		return Quaternion.Lerp(from, to, Auto.Loop(duration, 0f, 1f, offsetPercent));
	}

	public static Vector3 Loop(float duration, Vector3 from, Vector3 to, float offsetPercent)
	{
		return Vector3.Lerp(from, to, Auto.Loop(duration, 0f, 1f, offsetPercent));
	}

	public static float SoundInOut(float t, float b, float c, float d)
	{
		t /= d / 2f;
		if (t < 1f)
		{
			return c / 2f * t * t * t + b;
		}
		t -= 2f;
		return c / 2f * (t * t * t + 2f) + b;
	}

	public static float Loop(float duration, float from, float to, float offsetPercent)
	{
		float num = to - from;
		float num2 = (Time.get_time() + duration * offsetPercent) * (Mathf.Abs(num) / duration);
		if (num > 0f)
		{
			return from + Time.get_time() - num * (float)Mathf.FloorToInt(Time.get_time() / num);
		}
		return from - (Time.get_time() - Mathf.Abs(num) * (float)Mathf.FloorToInt(num2 / Mathf.Abs(num)));
	}

	public static Quaternion Wave(float duration, Quaternion from, Quaternion to, float offsetPercent)
	{
		return Quaternion.Lerp(from, to, Auto.Wave(duration, 0f, 1f, offsetPercent));
	}

	public static Vector3 Wave(float duration, Vector3 from, Vector3 to, float offsetPercent)
	{
		return Vector3.Lerp(from, to, Auto.Wave(duration, 0f, 1f, offsetPercent));
	}

	public static float Wave(float duration, float from, float to, float offsetPercent)
	{
		float num = (to - from) / 2f;
		return from + num + Mathf.Sin((Time.get_time() + duration * offsetPercent) / duration * 6.28318548f) * num;
	}
}
