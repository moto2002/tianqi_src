using Spine;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[ExecuteInEditMode]
public class SkeletonAnimation : SkeletonRenderer, ISkeletonAnimation
{
	private const float GLOBAL_RATE = 1.1f;

	public int templateId;

	[SerializeField]
	private string animationName;

	[Tooltip("Whether or not an animation should loop. This only applies to the initial animation specified in the inspector, or any subsequent Animations played through .AnimationName. Animations set through state.SetAnimation are unaffected.")]
	public bool loop;

	[Tooltip("The rate at which animations progress over time. 1 means 100%. 0.5 means 50%.")]
	public float timeScale = 1f;

	[HideInInspector]
	public Action callbackComplete;

	public AnimationState state;

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

	public string AnimationName
	{
		get
		{
			if (!this.valid)
			{
				return null;
			}
			TrackEntry current = this.state.GetCurrent(0);
			return (current != null) ? current.Animation.Name : null;
		}
		set
		{
			if (this.animationName == value)
			{
				return;
			}
			this.animationName = value;
			if (!this.check_valid())
			{
				return;
			}
			if (value == null || value.get_Length() == 0)
			{
				this.state.ClearTrack(0);
			}
			else
			{
				this.state.SetAnimation(0, value, this.loop, this.callbackComplete, this.templateId);
			}
		}
	}

	public Skeleton Skeleton
	{
		get
		{
			return this.skeleton;
		}
	}

	private bool check_valid()
	{
		if (!this.valid)
		{
			this.Awake();
			if (!this.valid)
			{
				Debug.LogWarning("You tried to change AnimationName but the SkeletonAnimation was not valid. Try checking your Skeleton Data for errors.");
				return false;
			}
		}
		return true;
	}

	public void Clear()
	{
		base.Reset();
		if (this.state != null)
		{
			this.state.ClearTracks();
		}
		this.loop = true;
		this.callbackComplete = null;
		this.animationName = string.Empty;
	}

	public override void Reset()
	{
		if (!Application.get_isPlaying() || true)
		{
			base.Reset();
			if (!this.valid)
			{
				return;
			}
			this.state = new AnimationState(this.skeletonDataAsset.GetAnimationStateData());
			if (this.animationName != null && this.animationName.get_Length() > 0)
			{
				this.state.SetAnimation(0, this.animationName, this.loop, this.callbackComplete, this.templateId);
				this.Update(0f);
			}
		}
		else
		{
			XTaskManager.instance.StartTask(base.ResetAsync(delegate
			{
				if (this == null)
				{
					return;
				}
				if (!this.valid)
				{
					return;
				}
				this.state = new AnimationState(this.skeletonDataAsset.GetAnimationStateData());
				if (this.animationName != null && this.animationName.get_Length() > 0)
				{
					this.state.SetAnimation(0, this.animationName, this.loop, this.callbackComplete, this.templateId);
					this.Update(0f);
				}
			}), null);
		}
	}

	public virtual void Update()
	{
		this.Update(Time.get_deltaTime());
	}

	public virtual void Update(float deltaTime)
	{
		if (!this.valid)
		{
			return;
		}
		deltaTime *= 1.1f * this.timeScale;
		this.skeleton.Update(deltaTime);
		this.state.Update(deltaTime);
		this.state.Apply(this.skeleton);
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
	}
}
