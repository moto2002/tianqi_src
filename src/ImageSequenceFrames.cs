using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSequenceFrames : Image
{
	private const float NO_MATTER_SCALE_FLAG = -1f;

	[SetProperty("IsAnimating"), SerializeField]
	private bool _IsAnimating;

	private float _Scale = -1f;

	private readonly int LOOP_PLAY_TIMES_FLAG = -1;

	[SetProperty("PlayTimes"), SerializeField]
	private int _PlayTimes;

	private List<SpriteRenderer> listSpriteAnimationFrames;

	private float animationPerFrameTime;

	private Action callBack;

	private int currentAnimateIndex;

	private float animateTimeCal;

	public bool IsAnimating
	{
		get
		{
			return this._IsAnimating;
		}
		set
		{
			this._IsAnimating = value;
		}
	}

	public float Scale
	{
		get
		{
			return this._Scale;
		}
		set
		{
			this._Scale = value;
		}
	}

	private int PlayTimes
	{
		get
		{
			return this._PlayTimes;
		}
		set
		{
			this._PlayTimes = value;
		}
	}

	private bool IsLoop()
	{
		return this.PlayTimes == this.LOOP_PLAY_TIMES_FLAG;
	}

	protected override void Start()
	{
		base.Start();
	}

	public void PlayAnimation(List<string> listFrames, float durationPerFrame, Action actionFinishCallBack, int toPlayTimes)
	{
		if (this.IsAnimating)
		{
			return;
		}
		if (listFrames.get_Count() == 0)
		{
			if (actionFinishCallBack != null)
			{
				actionFinishCallBack.Invoke();
			}
			base.set_enabled(false);
			return;
		}
		this.SelfReset();
		this.SetSprites(listFrames);
		this.animationPerFrameTime = durationPerFrame;
		this.callBack = actionFinishCallBack;
		this.PlayTimes = Mathf.Max(1, toPlayTimes);
	}

	public void PlayAnimation2Loop(List<string> listFrames, float durationPerFrame)
	{
		if (this.IsAnimating)
		{
			return;
		}
		this.SelfReset();
		this.SetSprites(listFrames);
		this.animationPerFrameTime = durationPerFrame;
		this.PlayTimes = this.LOOP_PLAY_TIMES_FLAG;
	}

	public void Replay(Action actionFinishCallBack)
	{
		if (this.listSpriteAnimationFrames.get_Count() == 0)
		{
			if (actionFinishCallBack != null)
			{
				actionFinishCallBack.Invoke();
			}
			base.set_enabled(false);
			return;
		}
		if (!this.IsLoop())
		{
			this.SelfReset();
			this.callBack = actionFinishCallBack;
		}
	}

	private void Update()
	{
		if (!this.IsAnimating)
		{
			return;
		}
		if (this.listSpriteAnimationFrames == null || this.listSpriteAnimationFrames.get_Count() == 0)
		{
			return;
		}
		this.animateTimeCal += Time.get_deltaTime();
		if (this.animateTimeCal > this.animationPerFrameTime)
		{
			this.animateTimeCal = 0f;
			this.SetSprite(this.listSpriteAnimationFrames.get_Item(this.currentAnimateIndex % this.listSpriteAnimationFrames.get_Count()));
			this.currentAnimateIndex++;
			if (!this.IsLoop() && this.currentAnimateIndex >= this.listSpriteAnimationFrames.get_Count() * this.PlayTimes)
			{
				this.IsAnimating = false;
				if (this.callBack != null)
				{
					this.callBack.Invoke();
				}
			}
		}
	}

	private void SetSprites(List<string> listFrames)
	{
		if (this.listSpriteAnimationFrames == null)
		{
			this.listSpriteAnimationFrames = new List<SpriteRenderer>();
		}
		this.listSpriteAnimationFrames.Clear();
		for (int i = 0; i < listFrames.get_Count(); i++)
		{
			this.listSpriteAnimationFrames.Add(this.GetSprite(listFrames.get_Item(i)));
		}
	}

	private SpriteRenderer GetSprite(string spriteName)
	{
		if (!SystemConfig.IsReadUIImageOn)
		{
			return ResourceManagerBase.GetNullSprite();
		}
		return ResourceManager.GetIconSprite(spriteName);
	}

	private void SetSprite(SpriteRenderer spr)
	{
		ResourceManager.SetSprite(this, spr);
		if (this.Scale != -1f)
		{
			this.SetNativeSize();
			base.get_transform().set_localScale(new Vector3(this.Scale, this.Scale, 1f));
		}
	}

	protected void SelfReset()
	{
		this.currentAnimateIndex = 0;
		this.animateTimeCal = 0f;
		this.IsAnimating = true;
		this.callBack = null;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}
}
