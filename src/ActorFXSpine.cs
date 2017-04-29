using GameData;
using System;
using UnityEngine;
using XEngineActor;

public class ActorFXSpine : Actor
{
	public int _uid;

	public FXSpine _dataSpine;

	public string _uibase;

	public int _depth;

	public bool _stencilMask;

	private Action m_finishCallback;

	private SkeletonAnimation m_skeletonAnimation;

	private bool _isEnableRender = true;

	private Renderer _myRenderer;

	private int _animIndex;

	private bool IsFXFinished;

	public Action _finishCallback
	{
		get
		{
			return this.m_finishCallback;
		}
		set
		{
			this.m_finishCallback = value;
		}
	}

	public SkeletonAnimation _skeletonAnimation
	{
		get
		{
			if (this.m_skeletonAnimation == null)
			{
				this.m_skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			}
			return this.m_skeletonAnimation;
		}
	}

	public Renderer myRenderer
	{
		get
		{
			if (this._myRenderer == null && this != null && base.get_transform() != null)
			{
				this._myRenderer = base.GetComponent<Renderer>();
			}
			return this._myRenderer;
		}
		set
		{
			this._myRenderer = value;
		}
	}

	public void AwakeSelf()
	{
		this.IsFXFinished = false;
		this.AddListener();
	}

	protected override void Awake()
	{
		base.Awake();
		base.get_gameObject().AddComponent<IgnoreCanvasRaycast>();
	}

	private void AddListener()
	{
		EventDispatcher.AddListener("UIManagerControl.ResetSpineRendering", new Callback(this.OnResetSpineRendering));
		EventDispatcher.AddListener("UIManagerControl.FakeHideAllUI", new Callback(this.OnFakeHideAllUI));
	}

	private void RemoveListener()
	{
		EventDispatcher.RemoveListener("UIManagerControl.ResetSpineRendering", new Callback(this.OnResetSpineRendering));
		EventDispatcher.RemoveListener("UIManagerControl.FakeHideAllUI", new Callback(this.OnFakeHideAllUI));
	}

	private void OnResetSpineRendering()
	{
		if (string.IsNullOrEmpty(this._uibase))
		{
			return;
		}
		if (this.IsRendererAlways())
		{
			return;
		}
		this.SetEnableRenderer(UIManagerControl.Instance.IsShowSpine(this._uibase));
	}

	private void OnFakeHideAllUI()
	{
		if (this.IsRendererAlways())
		{
			return;
		}
		this.JustSetEnableRenderer(this._isEnableRender);
	}

	private void HandleComplete()
	{
		this._animIndex++;
		this.PlayAnimation();
	}

	public void FXFinished(bool isCallback = true)
	{
		if (this.IsFXFinished)
		{
			return;
		}
		this.IsFXFinished = true;
		if (this._finishCallback != null && isCallback)
		{
			this._finishCallback.Invoke();
			this._finishCallback = null;
		}
		if (this._dataSpine == null || this._dataSpine.stayLastFrame == 0)
		{
			FXSpineManager.Instance.DeleteSpine(this._uid, true);
		}
	}

	public void PoolRecycle()
	{
		this.ResetAll();
		base.get_gameObject().SetActive(false);
		TimerHeap.AddTimer(1u, 0, delegate
		{
			if (this != null && base.get_gameObject() != null)
			{
				if (!base.get_enabled())
				{
					Object.Destroy(base.get_gameObject());
					return;
				}
				if (ClientApp.Instance != null)
				{
					base.Reuse();
				}
			}
		});
	}

	private void ResetAll()
	{
		this.RemoveListener();
		this.JustSetEnableRenderer(true);
		if (this._skeletonAnimation != null)
		{
			this._skeletonAnimation.Clear();
		}
		this._uid = 0;
		this._dataSpine = null;
		this._uibase = string.Empty;
		this._depth = 0;
		this._finishCallback = null;
		this._isEnableRender = true;
	}

	public void PlaySpine()
	{
		if (this._skeletonAnimation == null)
		{
			Debug.LogError("_skeletonAnimation is null");
			this.FXFinished(true);
			return;
		}
		if (this._dataSpine == null)
		{
			Debug.LogError("_dataSpine is null");
			this.FXFinished(true);
			return;
		}
		this.OnResetSpineRendering();
		this._animIndex = 0;
		this.PlayAnimation();
	}

	private void PlayAnimation()
	{
		string[] array = this._dataSpine.anim.Split(new char[]
		{
			';'
		});
		if (array == null || this._animIndex >= array.Length)
		{
			this.FXFinished(true);
			return;
		}
		string anim = array[this._animIndex];
		this.PlayAnimationWithName(anim);
	}

	private void PlayAnimationWithName(string anim)
	{
		if (this._skeletonAnimation == null)
		{
			Debug.LogError("_skeletonAnimation is null");
			return;
		}
		this._skeletonAnimation.loop = this.Isloop(anim);
		this._skeletonAnimation.callbackComplete = new Action(this.HandleComplete);
		if (this._dataSpine != null)
		{
			this._skeletonAnimation.templateId = this._dataSpine.id;
		}
		this._skeletonAnimation.AnimationName = anim;
	}

	private bool Isloop(string anim)
	{
		return anim.Contains("idle");
	}

	private bool IsRendererAlways()
	{
		return this._depth >= 14000;
	}

	private void SetEnableRenderer(bool isEnableRenderer)
	{
		if (this._dataSpine == null)
		{
			return;
		}
		this._isEnableRender = isEnableRenderer;
		if (this.IsRendererAlways())
		{
			this.JustSetEnableRenderer(true);
			return;
		}
		if (UIManagerControl.Instance.IsFakeHideAllUI)
		{
			this.JustSetEnableRenderer(false);
		}
		else
		{
			this.JustSetEnableRenderer(this._isEnableRender);
		}
	}

	private void JustSetEnableRenderer(bool enableRenderer)
	{
		if (this.myRenderer == null)
		{
			return;
		}
		this.myRenderer.set_enabled(enableRenderer);
	}
}
