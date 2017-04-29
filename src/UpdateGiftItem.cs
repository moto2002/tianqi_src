using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpdateGiftItem : BaseUIBehaviour
{
	public const string CLICK_REWARD = "ClickReward";

	public const string CLICK_BUTTON = "ClickButton";

	public Action<string, UpdateGiftItem> EventHandler;

	private GameObject mGoDone;

	private Image mProgress;

	private Text mTxProgress;

	private ButtonCustom mBtnGet;

	private ButtonCustom mBtnStart;

	private Text mTxDownloading;

	private GameObject mImgAlreadyGet;

	private float mAllSize;

	private float mCurSize;

	private ResourceSplit mLastPack;

	private UpdateAcInfo.AcStep.STEP mStatus = UpdateAcInfo.AcStep.STEP.Ready;

	public ButtonCustom BtnStart
	{
		get
		{
			return this.mBtnStart;
		}
	}

	public GengXinYouLi Data
	{
		get;
		protected set;
	}

	public ResourceSplit Pack
	{
		get;
		protected set;
	}

	public bool IsOnlyLocalDownload
	{
		get;
		protected set;
	}

	public float Size
	{
		get
		{
			return this.mAllSize;
		}
		set
		{
			this.mAllSize = value;
			if (this.mAllSize < 0f)
			{
				this.mBtnStart.set_interactable(false);
			}
			else if (this.mAllSize == 0f)
			{
				OperateActivityManager.Instance.SendDownLoadFinishReq(this.Data.Id);
			}
		}
	}

	public UpdateAcInfo.AcStep.STEP Status
	{
		get
		{
			return this.mStatus;
		}
		set
		{
			this.mStatus = value;
			switch (this.mStatus)
			{
			case UpdateAcInfo.AcStep.STEP.Ready:
				this.mGoDone.SetActive(false);
				this.mImgAlreadyGet.SetActive(false);
				this.mBtnGet.get_gameObject().SetActive(false);
				this.mBtnStart.get_gameObject().SetActive(true);
				if (this.mTxDownloading != null)
				{
					this.mTxDownloading.get_gameObject().SetActive(false);
				}
				this.mProgress.set_fillAmount(0f);
				this.mTxProgress.set_text("0.00MB/" + this.mAllSize.ToString("F") + "MB");
				break;
			case UpdateAcInfo.AcStep.STEP.Start:
				this.mGoDone.SetActive(false);
				this.mImgAlreadyGet.SetActive(false);
				this.mBtnGet.get_gameObject().SetActive(false);
				this.mBtnStart.get_gameObject().SetActive(false);
				if (this.mTxDownloading != null)
				{
					this.mTxDownloading.get_gameObject().SetActive(true);
				}
				this.mProgress.set_fillAmount(0f);
				break;
			case UpdateAcInfo.AcStep.STEP.Finish:
				if (this.IsOnlyLocalDownload)
				{
					using (List<UpdateAcInfo>.Enumerator enumerator = OperateActivityManager.Instance.ServerUpdateGiftInfos.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							UpdateAcInfo current = enumerator.get_Current();
							if (current.acId == this.Data.Id)
							{
								this.IsOnlyLocalDownload = false;
								this.Status = current.status;
								break;
							}
						}
					}
				}
				else
				{
					this.mGoDone.SetActive(true);
					this.mImgAlreadyGet.SetActive(false);
					this.mBtnGet.get_gameObject().SetActive(true);
					this.mBtnStart.get_gameObject().SetActive(false);
					if (this.mTxDownloading != null)
					{
						this.mTxDownloading.get_gameObject().SetActive(false);
					}
					this.mProgress.get_transform().get_parent().get_gameObject().SetActive(false);
				}
				break;
			case UpdateAcInfo.AcStep.STEP.Close:
				this.mGoDone.SetActive(true);
				this.mImgAlreadyGet.SetActive(true);
				this.mBtnGet.get_gameObject().SetActive(false);
				this.mBtnStart.get_gameObject().SetActive(false);
				if (this.mTxDownloading != null)
				{
					this.mTxDownloading.get_gameObject().SetActive(false);
				}
				this.mProgress.get_transform().get_parent().get_gameObject().SetActive(false);
				break;
			}
		}
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mGoDone = UIHelper.GetObject(base.get_transform(), "txDone");
		this.mProgress = UIHelper.GetImage(base.get_transform(), "Progress/Fill");
		this.mTxProgress = UIHelper.GetText(base.get_transform(), "Progress/txProgress");
		this.mBtnGet = UIHelper.GetCustomButton(base.get_transform(), "BtnGet");
		this.mBtnStart = UIHelper.GetCustomButton(base.get_transform(), "BtnStart");
		this.mTxDownloading = UIHelper.GetText(base.get_transform(), "txDownloading");
		this.mImgAlreadyGet = UIHelper.GetObject(base.get_transform(), "imgAlreadyGet");
		if (this.mTxDownloading != null)
		{
			this.mTxDownloading.set_text(GameDataUtils.GetChineseContent(513147, false));
		}
		UIHelper.GetText(base.get_transform(), "Progress/Text").set_text(GameDataUtils.GetChineseContent(513145, false));
		UIHelper.GetText(this.mBtnStart, "Text").set_text(GameDataUtils.GetChineseContent(513146, false));
		UIHelper.GetText(this.mBtnGet, "Text").set_text(GameDataUtils.GetChineseContent(513152, false));
		this.mBtnGet.get_onClick().AddListener(new UnityAction(this.OnClickButton));
		this.mBtnStart.get_onClick().AddListener(new UnityAction(this.OnClickButton));
		UIHelper.GetCustomButton(base.get_transform(), "btnGift").get_onClick().AddListener(delegate
		{
			if (this.EventHandler != null)
			{
				this.EventHandler.Invoke("ClickReward", this);
			}
		});
	}

	private void OnClickButton()
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Invoke("ClickButton", this);
		}
	}

	public void UpdateItem(GengXinYouLi data)
	{
		if (!this.m_bInited)
		{
			base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		}
		this.Data = data;
		if (this.Data == null || this.Data.ItemId.get_Count() != this.Data.ItemNum.get_Count())
		{
			Debug.Log("<color=red>Error:</color>[有礼更新]GengXinYouLi配置数据异常");
			return;
		}
		this.mLastPack = DataReader<ResourceSplit>.Get(this.Data.FinishPar - 1);
		if (this.mLastPack == null)
		{
			Debug.Log("<color=red>Error:</color>[有礼更新]ResourceSplit配置数据异常");
			return;
		}
		this.Pack = DataReader<ResourceSplit>.Get(this.Data.FinishPar);
		if (this.Pack == null)
		{
			Debug.Log("<color=red>Error:</color>[有礼更新]ResourceSplit配置数据异常");
			return;
		}
		UIHelper.GetText(base.get_transform(), "txTitle").set_text(string.Format(GameDataUtils.GetChineseContent(513143, false), OperateActivityManager.Instance.SwitchChineseNumber(this.mLastPack.ID)));
		UIHelper.GetText(base.get_transform(), "txMaxLv").set_text(string.Format(GameDataUtils.GetChineseContent(513144, false), this.mLastPack.LvLimit));
		using (List<UpdateAcInfo>.Enumerator enumerator = OperateActivityManager.Instance.LocalUpdateGiftInfos.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				UpdateAcInfo current = enumerator.get_Current();
				if (current.acId == this.Data.Id)
				{
					this.IsOnlyLocalDownload = (current.status != UpdateAcInfo.AcStep.STEP.Ready && this.Data.FinishPar > OperateActivityManager.Instance.CurrentVersion);
					break;
				}
			}
		}
		this.Status = UpdateAcInfo.AcStep.STEP.Ready;
		this.Size = this.Pack.SizeMbit;
	}

	public void SetProgressAndSpeed(float curSize, float allSize)
	{
		if (allSize <= 0f)
		{
			this.mProgress.set_fillAmount(1f);
			this.mTxProgress.set_text("100%");
		}
		else
		{
			this.mCurSize = curSize / 1024f;
			this.mProgress.set_fillAmount(this.mCurSize / this.mAllSize);
			this.mTxProgress.set_text(this.mCurSize.ToString("F") + "MB/" + this.mAllSize.ToString("F") + "MB");
		}
	}

	private GameObject RefreshReward(GameObject go, int id, int num)
	{
		ResourceManager.SetSprite(go.GetComponent<Image>(), GameDataUtils.GetItemFrame(id));
		ResourceManager.SetSprite(go.get_transform().FindChild("Image").GetComponent<Image>(), GameDataUtils.GetItemIcon(id));
		if (num > 1)
		{
			go.get_transform().FindChild("Text").GetComponent<Text>().set_text(num.ToString());
		}
		go.GetComponent<ButtonCustom>().onClickCustom = delegate(GameObject o)
		{
			ItemTipUIViewModel.ShowItem(id, null);
		};
		go.SetActive(true);
		return go;
	}
}
