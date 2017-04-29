using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TownCountdownUI : UIBase
{
	private Slider mSlider;

	private Text mTxTitle;

	private bool mIsCountdown;

	private float mDelay;

	private float mDeltaTime;

	private Action mOnFinish;

	private Action<bool> mOnStop;

	private int mOpenByTaskId;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = false;
		this.isEndNav = false;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mTxTitle = base.FindTransform("Text").GetComponent<Text>();
		this.mSlider = UIHelper.Get<Slider>(base.get_transform(), "Slider");
	}

	private void Update()
	{
		if (this.mIsCountdown)
		{
			this.mDeltaTime += Time.get_deltaTime();
			if (this.mDeltaTime >= this.mDelay)
			{
				this.StopCountdown(true);
				if (this.mOnFinish != null)
				{
					this.mOnFinish.Invoke();
				}
			}
			else
			{
				this.mSlider.set_value(this.mDeltaTime / this.mDelay);
			}
		}
	}

	protected override void AddListeners()
	{
		EventDispatcher.AddListener(EventNames.EndNav, new Callback(this.OnStopCountdown));
		EventDispatcher.AddListener(EventNames.StopSwitchCity, new Callback(this.OnStopCountdown));
		EventDispatcher.AddListener(AIManagerEvent.SelfAIDeactive, new Callback(this.OnStopCountdown));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.EndNav, new Callback(this.OnStopCountdown));
		EventDispatcher.RemoveListener(EventNames.StopSwitchCity, new Callback(this.OnStopCountdown));
		EventDispatcher.RemoveListener(AIManagerEvent.SelfAIDeactive, new Callback(this.OnStopCountdown));
	}

	private void OnStopCountdown()
	{
		if (this.mIsCountdown)
		{
			this.StopCountdown(false);
		}
	}

	private void StopCountdown(bool isAuto = false)
	{
		this.mIsCountdown = false;
		this.mSlider.set_value(0f);
		UIManagerControl.Instance.HideUI("TownCountdownUI");
		if (this.mOnStop != null)
		{
			this.mOnStop.Invoke(isAuto);
		}
	}

	public void StartCountdown(float time, string tips, int taskId, Action onFinish = null, Action<bool> onStop = null)
	{
		if (this.mIsCountdown && taskId == this.mOpenByTaskId)
		{
			return;
		}
		this.mDelay = time;
		this.mOnFinish = onFinish;
		this.mOnStop = onStop;
		this.mDeltaTime = 0f;
		this.mIsCountdown = true;
		this.mSlider.set_value(0f);
		this.mTxTitle.set_text(tips);
		this.mOpenByTaskId = taskId;
	}

	public void StartTransmit(int taskId, int sceneId)
	{
		float time = 3f;
		PaoHuanRenWuPeiZhi paoHuanRenWuPeiZhi = DataReader<PaoHuanRenWuPeiZhi>.Get("transferTime");
		if (paoHuanRenWuPeiZhi != null)
		{
			time = float.Parse(paoHuanRenWuPeiZhi.value) / 1000f;
		}
		this.StartCountdown(time, GameDataUtils.GetChineseContent(310027, false), taskId, delegate
		{
			MainTaskManager.Instance.SwitchCityLock = true;
			MainTaskManager.Instance.IsSwitchCityByTask = true;
			EventDispatcher.Broadcast<int>(CityManagerEvent.ChangeCityByIntegrationHearth, sceneId);
		}, null);
	}
}
