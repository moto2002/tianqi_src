using System;
using System.Collections;
using UnityEngine;

public class NgAnimation
{
	public static AnimationClip SetAnimation(Animation tarAnimation, int tarIndex, AnimationClip srcClip)
	{
		int num = 0;
		AnimationClip[] array = new AnimationClip[tarAnimation.GetClipCount() - tarIndex + 1];
		IEnumerator enumerator = tarAnimation.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				AnimationState animationState = (AnimationState)enumerator.get_Current();
				if (tarIndex == num)
				{
					tarAnimation.RemoveClip(animationState.get_clip());
				}
				if (tarIndex < num)
				{
					array[num - tarIndex - 1] = animationState.get_clip();
					tarAnimation.RemoveClip(animationState.get_clip());
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		tarAnimation.AddClip(srcClip, srcClip.get_name());
		for (int i = 0; i < array.Length; i++)
		{
			tarAnimation.AddClip(array[i], array[i].get_name());
		}
		return srcClip;
	}

	public static AnimationState GetAnimationByIndex(Animation anim, int nIndex)
	{
		int num = 0;
		IEnumerator enumerator = anim.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				AnimationState result = (AnimationState)enumerator.get_Current();
				if (num == nIndex)
				{
					return result;
				}
				num++;
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}
}
