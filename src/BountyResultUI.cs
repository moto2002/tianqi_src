using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BountyResultUI : UIBase
{
	public Image ResultImage;

	public Image ResultImageFx;

	public Transform Items;

	public GameObject[] StarImages;

	public Text scoreText;

	public Image ProductionBaseIcon;

	public Image ProductionBaseQuality;

	public Text ProductionBaseName;

	public Text ExpNum;

	public Text MoneyNum;

	public ButtonCustom ButtonConfirmWin;

	public ButtonCustom ButtonConfirmFail;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		this.ButtonConfirmWin.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickConfirmOfWin);
		this.ButtonConfirmFail.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickConfirmOfFail);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		Utils.WinSetting(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		Utils.WinSetting(false);
	}

	private void OnClickConfirmOfWin(GameObject go)
	{
		EventDispatcher.Broadcast("GuideManager.InstanceExit");
		EventDispatcher.Broadcast("GuideManager.InstanceWin");
		NetworkManager.Send(new BountyTaskExitBtlReq(), ServerType.Data);
	}

	private void OnClickConfirmOfFail(GameObject go)
	{
		EventDispatcher.Broadcast("GuideManager.InstanceExit");
		NetworkManager.Send(new BountyTaskExitBtlReq(), ServerType.Data);
	}

	public void UpdateData(BountyTaskResultNty info)
	{
		Debug.LogError(info.win + "=====UpdateData======" + this.ProductionBaseName.get_transform().get_parent().get_parent().get_gameObject());
		if (info.win == 1)
		{
			ResourceManager.SetSprite(this.ResultImage, ResourceManager.GetIconSprite("win_01"));
			ResourceManager.SetSprite(this.ResultImageFx, ResourceManager.GetIconSprite("win_Light_01"));
			if (info.newProductionUId != 0uL)
			{
				ShengChanJiDi shengChanJiDi = DataReader<ShengChanJiDi>.Get(BountyManager.Instance.Info.productions.Find((ProductionInfo a) => a.uId == info.newProductionUId).typeId);
				ResourceManager.SetSprite(this.ProductionBaseIcon, GameDataUtils.GetIcon(shengChanJiDi.baseicon));
				ResourceManager.SetSprite(this.ProductionBaseQuality, GameDataUtils.GetIcon(5050 + shengChanJiDi.baseQuality));
				this.ProductionBaseName.set_text(shengChanJiDi.baseName);
				Debug.LogError(string.Concat(new object[]
				{
					"新基地唯一id:",
					info.newProductionUId,
					",配置id:",
					shengChanJiDi.id,
					"==========配置的名字：",
					shengChanJiDi.baseName,
					"，配置图片:",
					5050 + shengChanJiDi.baseQuality,
					"===",
					GameDataUtils.GetIcon(5050 + shengChanJiDi.baseQuality)
				}));
				this.ProductionBaseName.get_transform().get_parent().get_parent().get_gameObject().SetActive(true);
			}
			else
			{
				this.ProductionBaseName.get_transform().get_parent().get_parent().get_gameObject().SetActive(false);
			}
			this.ButtonConfirmFail.get_gameObject().SetActive(false);
			this.ButtonConfirmWin.get_gameObject().SetActive(true);
		}
		else if (info.win == 0)
		{
			ResourceManager.SetSprite(this.ResultImage, ResourceManager.GetIconSprite("lcxs_pingju"));
			ResourceManager.SetSprite(this.ResultImageFx, ResourceManager.GetIconSprite("win_Light_03"));
			this.ProductionBaseName.get_transform().get_parent().get_parent().get_gameObject().SetActive(false);
			this.ButtonConfirmFail.get_gameObject().SetActive(true);
			this.ButtonConfirmWin.get_gameObject().SetActive(false);
		}
		else if (info.win == -1)
		{
			ResourceManager.SetSprite(this.ResultImage, ResourceManager.GetIconSprite("failure_bg_zi01"));
			ResourceManager.SetSprite(this.ResultImageFx, ResourceManager.GetIconSprite("win_Light_02"));
			this.ProductionBaseName.get_transform().get_parent().get_parent().get_gameObject().SetActive(false);
			this.ButtonConfirmFail.get_gameObject().SetActive(true);
			this.ButtonConfirmWin.get_gameObject().SetActive(false);
		}
		using (List<BountyTaskResultNty.DropItemInfo>.Enumerator enumerator = info.items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				BountyTaskResultNty.DropItemInfo current = enumerator.get_Current();
				if (current.cfgId == 2)
				{
					this.MoneyNum.set_text(current.count.ToString());
				}
				else if (current.cfgId == 1)
				{
					this.ExpNum.set_text(current.count.ToString());
				}
			}
		}
		for (int i = 0; i < 3; i++)
		{
			this.StarImages[i].SetActive(info.gotStarCondition.get_Item(i));
		}
		if (info.gotScore > 0)
		{
			this.scoreText.set_text("+" + info.gotScore.ToString());
		}
		else
		{
			this.scoreText.set_text(info.gotScore.ToString());
		}
	}
}
