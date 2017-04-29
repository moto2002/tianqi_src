using Spine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SkeletonAnimator : SkeletonRenderer, ISkeletonAnimation
{
	public enum MixMode
	{
		AlwaysMix,
		MixNext,
		SpineStyle
	}

	public SkeletonAnimator.MixMode[] layerMixModes = new SkeletonAnimator.MixMode[0];

	private Dictionary<int, Animation> animationTable = new Dictionary<int, Animation>();

	private Dictionary<AnimationClip, int> clipNameHashCodeTable = new Dictionary<AnimationClip, int>();

	private Animator animator;

	private float lastTime;

	protected event UpdateBonesDelegate _UpdateLocal
	{
		[MethodImpl(32)]
		add
		{
			this._UpdateLocal = (UpdateBonesDelegate)Delegate.Combine(this._UpdateLocal, value);
		}
		[MethodImpl(32)]
		remove
		{
			this._UpdateLocal = (UpdateBonesDelegate)Delegate.Remove(this._UpdateLocal, value);
		}
	}

	public event UpdateBonesDelegate UpdateLocal
	{
		add
		{
			this._UpdateLocal = (UpdateBonesDelegate)Delegate.Combine(this._UpdateLocal, value);
		}
		remove
		{
			this._UpdateLocal = (UpdateBonesDelegate)Delegate.Remove(this._UpdateLocal, value);
		}
	}

	protected event UpdateBonesDelegate _UpdateWorld
	{
		[MethodImpl(32)]
		add
		{
			this._UpdateWorld = (UpdateBonesDelegate)Delegate.Combine(this._UpdateWorld, value);
		}
		[MethodImpl(32)]
		remove
		{
			this._UpdateWorld = (UpdateBonesDelegate)Delegate.Remove(this._UpdateWorld, value);
		}
	}

	public event UpdateBonesDelegate UpdateWorld
	{
		add
		{
			this._UpdateWorld = (UpdateBonesDelegate)Delegate.Combine(this._UpdateWorld, value);
		}
		remove
		{
			this._UpdateWorld = (UpdateBonesDelegate)Delegate.Remove(this._UpdateWorld, value);
		}
	}

	protected event UpdateBonesDelegate _UpdateComplete
	{
		[MethodImpl(32)]
		add
		{
			this._UpdateComplete = (UpdateBonesDelegate)Delegate.Combine(this._UpdateComplete, value);
		}
		[MethodImpl(32)]
		remove
		{
			this._UpdateComplete = (UpdateBonesDelegate)Delegate.Remove(this._UpdateComplete, value);
		}
	}

	public event UpdateBonesDelegate UpdateComplete
	{
		add
		{
			this._UpdateComplete = (UpdateBonesDelegate)Delegate.Combine(this._UpdateComplete, value);
		}
		remove
		{
			this._UpdateComplete = (UpdateBonesDelegate)Delegate.Remove(this._UpdateComplete, value);
		}
	}

	public Skeleton Skeleton
	{
		get
		{
			return this.skeleton;
		}
	}

	public Skeleton GetSkeleton()
	{
		return this.skeleton;
	}

	public override void Reset()
	{
		base.Reset();
		if (!this.valid)
		{
			return;
		}
		this.animationTable.Clear();
		this.clipNameHashCodeTable.Clear();
		SkeletonData skeletonData = this.skeletonDataAsset.GetSkeletonData(true);
		foreach (Animation current in skeletonData.Animations)
		{
			this.animationTable.Add(current.Name.GetHashCode(), current);
		}
		this.animator = base.GetComponent<Animator>();
		this.lastTime = Time.get_time();
	}

	private void Update()
	{
		if (!this.valid)
		{
			return;
		}
		if (this.layerMixModes.Length != this.animator.get_layerCount())
		{
			Array.Resize<SkeletonAnimator.MixMode>(ref this.layerMixModes, this.animator.get_layerCount());
		}
		float num = Time.get_time() - this.lastTime;
		this.skeleton.Update(Time.get_deltaTime());
		int layerCount = this.animator.get_layerCount();
		for (int i = 0; i < layerCount; i++)
		{
			float num2 = this.animator.GetLayerWeight(i);
			if (i == 0)
			{
				num2 = 1f;
			}
			AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(i);
			AnimatorStateInfo nextAnimatorStateInfo = this.animator.GetNextAnimatorStateInfo(i);
			AnimatorClipInfo[] currentAnimatorClipInfo = this.animator.GetCurrentAnimatorClipInfo(i);
			AnimatorClipInfo[] nextAnimatorClipInfo = this.animator.GetNextAnimatorClipInfo(i);
			SkeletonAnimator.MixMode mixMode = this.layerMixModes[i];
			if (mixMode == SkeletonAnimator.MixMode.AlwaysMix)
			{
				for (int j = 0; j < currentAnimatorClipInfo.Length; j++)
				{
					AnimatorClipInfo animatorClipInfo = currentAnimatorClipInfo[j];
					float num3 = animatorClipInfo.get_weight() * num2;
					if (num3 != 0f)
					{
						float num4 = currentAnimatorStateInfo.get_normalizedTime() * animatorClipInfo.get_clip().get_length();
						this.animationTable.get_Item(this.GetAnimationClipNameHashCode(animatorClipInfo.get_clip())).Mix(this.skeleton, Mathf.Max(0f, num4 - num), num4, currentAnimatorStateInfo.get_loop(), null, num3);
					}
				}
				if (nextAnimatorStateInfo.get_fullPathHash() != 0)
				{
					for (int k = 0; k < nextAnimatorClipInfo.Length; k++)
					{
						AnimatorClipInfo animatorClipInfo2 = nextAnimatorClipInfo[k];
						float num5 = animatorClipInfo2.get_weight() * num2;
						if (num5 != 0f)
						{
							float num6 = nextAnimatorStateInfo.get_normalizedTime() * animatorClipInfo2.get_clip().get_length();
							this.animationTable.get_Item(this.GetAnimationClipNameHashCode(animatorClipInfo2.get_clip())).Mix(this.skeleton, Mathf.Max(0f, num6 - num), num6, nextAnimatorStateInfo.get_loop(), null, num5);
						}
					}
				}
			}
			else if (mixMode >= SkeletonAnimator.MixMode.MixNext)
			{
				int l;
				for (l = 0; l < currentAnimatorClipInfo.Length; l++)
				{
					AnimatorClipInfo animatorClipInfo3 = currentAnimatorClipInfo[l];
					float num7 = animatorClipInfo3.get_weight() * num2;
					if (num7 != 0f)
					{
						float num8 = currentAnimatorStateInfo.get_normalizedTime() * animatorClipInfo3.get_clip().get_length();
						this.animationTable.get_Item(this.GetAnimationClipNameHashCode(animatorClipInfo3.get_clip())).Apply(this.skeleton, Mathf.Max(0f, num8 - num), num8, currentAnimatorStateInfo.get_loop(), null);
						break;
					}
				}
				while (l < currentAnimatorClipInfo.Length)
				{
					AnimatorClipInfo animatorClipInfo4 = currentAnimatorClipInfo[l];
					float num9 = animatorClipInfo4.get_weight() * num2;
					if (num9 != 0f)
					{
						float num10 = currentAnimatorStateInfo.get_normalizedTime() * animatorClipInfo4.get_clip().get_length();
						this.animationTable.get_Item(this.GetAnimationClipNameHashCode(animatorClipInfo4.get_clip())).Mix(this.skeleton, Mathf.Max(0f, num10 - num), num10, currentAnimatorStateInfo.get_loop(), null, num9);
					}
					l++;
				}
				l = 0;
				if (nextAnimatorStateInfo.get_fullPathHash() != 0)
				{
					if (mixMode == SkeletonAnimator.MixMode.SpineStyle)
					{
						while (l < nextAnimatorClipInfo.Length)
						{
							AnimatorClipInfo animatorClipInfo5 = nextAnimatorClipInfo[l];
							float num11 = animatorClipInfo5.get_weight() * num2;
							if (num11 != 0f)
							{
								float num12 = nextAnimatorStateInfo.get_normalizedTime() * animatorClipInfo5.get_clip().get_length();
								this.animationTable.get_Item(this.GetAnimationClipNameHashCode(animatorClipInfo5.get_clip())).Apply(this.skeleton, Mathf.Max(0f, num12 - num), num12, nextAnimatorStateInfo.get_loop(), null);
								break;
							}
							l++;
						}
					}
					while (l < nextAnimatorClipInfo.Length)
					{
						AnimatorClipInfo animatorClipInfo6 = nextAnimatorClipInfo[l];
						float num13 = animatorClipInfo6.get_weight() * num2;
						if (num13 != 0f)
						{
							float num14 = nextAnimatorStateInfo.get_normalizedTime() * animatorClipInfo6.get_clip().get_length();
							this.animationTable.get_Item(this.GetAnimationClipNameHashCode(animatorClipInfo6.get_clip())).Mix(this.skeleton, Mathf.Max(0f, num14 - num), num14, nextAnimatorStateInfo.get_loop(), null, num13);
						}
						l++;
					}
				}
			}
		}
		if (this._UpdateLocal != null)
		{
			this._UpdateLocal(this);
		}
		this.skeleton.UpdateWorldTransform();
		if (this._UpdateWorld != null)
		{
			this._UpdateWorld(this);
			this.skeleton.UpdateWorldTransform();
		}
		if (this._UpdateComplete != null)
		{
			this._UpdateComplete(this);
		}
		this.lastTime = Time.get_time();
	}

	private int GetAnimationClipNameHashCode(AnimationClip clip)
	{
		int hashCode;
		if (!this.clipNameHashCodeTable.TryGetValue(clip, ref hashCode))
		{
			hashCode = clip.get_name().GetHashCode();
			this.clipNameHashCodeTable.Add(clip, hashCode);
		}
		return hashCode;
	}
}
