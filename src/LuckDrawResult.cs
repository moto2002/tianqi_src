using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckDrawResult : UIBase
{
	public ButtonCustom ButtonAgain;

	public ButtonCustom ButtonConfirm;

	public Image Icon;

	public Text Num;

	public GameObject Consume;

	public Transform ItemsContent;

	public Transform ItemsContent1;

	public Transform Fx;

	public Transform TitleFx;

	public Transform DofFx;

	public GameObject itemPos;

	public int DelayTime = 2000;

	public int PlayTime = 150;

	private Transform tmp;

	private List<AwardInfo> InfoList;

	private List<int> FxIdList = new List<int>();

	private bool IsClickAgain;

	protected override void Preprocessing()
	{
		this.isMask = false;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		this.ButtonAgain.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAgain);
		this.ButtonConfirm.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickConfirm);
	}

	protected override void OnEnable()
	{
		PetManager.Instance.IsLuckDrawing = true;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		PetManager.Instance.IsLuckDrawing = false;
		this.IsClickAgain = false;
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	public void OnClickAgain(GameObject go)
	{
		if (LuckDrawManager.Instance.CheckDrawType(LuckDrawManager.Instance.lastDrawType))
		{
			this.ButtonAgain.set_enabled(false);
		}
		this.IsClickAgain = true;
		LuckDrawManager.Instance.LuckDrawAgain(this);
	}

	public void OnClickConfirm(GameObject go)
	{
		this.Show(false);
		UIManagerControl.Instance.OpenUI("LuckDrawUI", null, true, UIType.FullScreen);
	}

	public void UpdateUI(List<AwardInfo> list)
	{
		PetManager.Instance.IsLuckDrawing = true;
		this.InfoList = list;
		this.ButtonAgain.get_gameObject().SetActive(false);
		this.ButtonConfirm.get_gameObject().SetActive(false);
		this.Consume.SetActive(false);
		using (List<int>.Enumerator enumerator = this.FxIdList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				FXSpineManager.Instance.DeleteSpine(current, true);
			}
		}
		this.FxIdList.Clear();
		if (list.get_Count() > 1)
		{
			this.tmp = this.ItemsContent;
		}
		else
		{
			this.tmp = this.ItemsContent1;
		}
		for (int i = 0; i < this.ItemsContent.get_childCount(); i++)
		{
			Object.Destroy(this.ItemsContent.GetChild(i).get_gameObject());
		}
		for (int j = 0; j < this.ItemsContent1.get_childCount(); j++)
		{
			Object.Destroy(this.ItemsContent1.GetChild(j).get_gameObject());
		}
		this.tmp.GetComponent<GridLayoutGroup>().set_constraintCount((int)Math.Ceiling(1.0 * (double)list.get_Count() / 2.0));
		int templateId = 1101;
		int templateId2 = 1103;
		if (this.IsClickAgain)
		{
			templateId = 1102;
			templateId2 = 1114;
		}
		int lastDrawType = LuckDrawManager.Instance.lastDrawType;
		IEnumerator enumerator2 = this.DofFx.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				Transform transform = (Transform)enumerator2.get_Current();
				Object.Destroy(transform.get_gameObject());
			}
		}
		finally
		{
			IDisposable disposable = enumerator2 as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		FXManager.Instance.PlayFXOfDisplay(1000, this.DofFx, Vector3.get_zero(), default(Quaternion), 1f, 1f, 0, false, null, null);
		FXSpineManager.Instance.PlaySpine(templateId2, this.Fx, "LuckDrawResult", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(templateId, this.Fx, "LuckDrawResult", 3002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(1104, this.TitleFx, "LuckDrawResult", 3001, delegate
		{
			this.FxIdList.Add(FXSpineManager.Instance.PlaySpine(1105, this.TitleFx, "LuckDrawResult", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		TimerHeap.AddTimer(1650u, 0, delegate
		{
			this.ShowIcon();
		});
		ChouJiangXiaoHao chouJiangXiaoHao = DataReader<ChouJiangXiaoHao>.Get(lastDrawType);
		if (chouJiangXiaoHao.lotteryId > 0 && (BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao.lotteryId) >= (long)chouJiangXiaoHao.lotteryAmount || lastDrawType == 1 || lastDrawType == 2))
		{
			this.Num.set_text(chouJiangXiaoHao.lotteryAmount + "/" + BackpackManager.Instance.OnGetGoodCount(chouJiangXiaoHao.lotteryId));
			ResourceManager.SetSprite(this.Icon, GameDataUtils.GetIcon(DataReader<Items>.Get(chouJiangXiaoHao.lotteryId).littleIcon));
		}
		else
		{
			this.Num.set_text(chouJiangXiaoHao.amount.ToString());
			ResourceManager.SetSprite(this.Icon, GameDataUtils.GetIcon(DataReader<Items>.Get(chouJiangXiaoHao.itemId).littleIcon));
		}
	}

	private void ShowIcon()
	{
		if (this.InfoList.get_Count() > 0)
		{
			Random random = new Random();
			int num = random.Next(0, this.InfoList.get_Count());
			AwardInfo awardInfo = this.InfoList.get_Item(num);
			this.InfoList.RemoveAt(num);
			if (awardInfo.itemFirstType == ItemFirstType.IFT.Pet)
			{
				this.ShowAsPet(awardInfo);
			}
			else
			{
				this.ShowAsNoPet(awardInfo);
			}
		}
		else
		{
			this.ButtonAgain.get_gameObject().SetActive(true);
			this.ButtonConfirm.get_gameObject().SetActive(true);
			this.Consume.SetActive(true);
			this.ButtonAgain.set_enabled(true);
		}
	}

	private void ShowAsPet(AwardInfo ai)
	{
		Items items = DataReader<Items>.Get(ai.itemId);
		int level = items.level;
		int decompose_star = items.level;
		bool replace = false;
		string petName = string.Empty;
		if (ai.hadFlag)
		{
			if (decompose_star > ai.petOldStar)
			{
				replace = true;
				petName = PetManager.GetPetName(ai.petCfgId, decompose_star);
				decompose_star = ai.petOldStar;
			}
			else
			{
				replace = false;
				petName = PetManager.GetPetName(ai.petCfgId, ai.petOldStar);
			}
		}
		PetManager.Instance.JustObtainPetNty(ai.petCfgId, level, decompose_star, ai.hadFlag, replace, petName, delegate
		{
			Pet pet = DataReader<Pet>.Get(ai.petCfgId);
			if (!ai.hadFlag)
			{
				Transform transform = ItemShow.ShowItem(this.tmp, ai.itemId, (long)ai.itemCount, false, null, 2001).get_transform();
				if (ai.petCfgId > 0)
				{
					TeShuChongWu teShuChongWu = DataReader<TeShuChongWu>.Get(ai.petCfgId);
					if (teShuChongWu != null)
					{
						this.FxIdList.Add(FXSpineManager.Instance.PlaySpine(teShuChongWu.effectId, transform, "LuckDrawResult", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None));
					}
				}
			}
			else
			{
				Transform transform = ItemShow.ShowItem(this.tmp, pet.fragmentId, (long)PetManager.GetReturnFragment(pet, decompose_star), false, null, 2001).get_transform();
			}
			TimerHeap.AddTimer(1000u, 0, delegate
			{
				this.ShowIcon();
			});
		});
	}

	private void ShowAsNoPet(AwardInfo ai)
	{
		Transform transform = ItemShow.ShowItem(this.tmp, ai.itemId, (long)ai.itemCount, false, null, 2001).get_transform();
		FXSpineManager.Instance.ReplaySpine(0, 1107, transform, "LuckDrawResult", 3001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		TimerHeap.AddTimer((uint)this.PlayTime, 0, delegate
		{
			this.ShowIcon();
		});
	}
}
