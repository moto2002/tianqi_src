using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLoseUI : UIBase
{
	private Text countDownText;

	private ButtonCustom BtnExit;

	private ButtonCustom BtnAgain;

	private Transform AnimationObjs;

	private GameObject UpBtns;

	private ButtonCustom BtnDamageCal;

	private ButtonCustom BtnDamageCal2;

	private Text PassTimeNum;

	private Text MyPowerValue;

	private Text RecommendPowerValue;

	private Text MyPowerValueDesc;

	private Text RecommendPowerValueDesc;

	private List<StrongerInfoData> UpWays;

	private int UpType;

	public Action BtnExitAction;

	public Action BtnAgainAction;

	public Action BtnEquipQuaAction;

	public Action BtnEquipLvAction;

	public Action BtnGemLvAction;

	public Action BtnSkillAction;

	public Action BtnPetLvAction;

	public Action BtnPetStarAction;

	public Action BtnPetSkillAction;

	public Action BtnGodSoldierAction;

	public Action BtnWingAction;

	protected int recommendPower;

	private Transform fxTrans;

	protected uint timer;

	private TimeCountDown timeCountDown;

	private int fx_LoseStart;

	private int fx_LoseIdle;

	private int fx_LoseExplode;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.75f;
		this.isIgnoreCollider = true;
		this.isClick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.BtnExit = base.FindTransform("BtnExit").GetComponent<ButtonCustom>();
		this.BtnAgain = base.FindTransform("BtnAgain").GetComponent<ButtonCustom>();
		this.UpBtns = base.FindTransform("UpBtns").get_gameObject();
		this.BtnDamageCal = base.FindTransform("BtnDamageCal").GetComponent<ButtonCustom>();
		this.BtnDamageCal2 = base.FindTransform("BtnDamageCal2").GetComponent<ButtonCustom>();
		this.PassTimeNum = base.FindTransform("PassTimeNum").GetComponent<Text>();
		this.MyPowerValue = base.FindTransform("MyPowerValue").GetComponent<Text>();
		this.RecommendPowerValue = base.FindTransform("RecommendPowerValue").GetComponent<Text>();
		this.MyPowerValueDesc = base.FindTransform("MyPowerValueDesc").GetComponent<Text>();
		this.RecommendPowerValueDesc = base.FindTransform("RecommendPowerValueDesc").GetComponent<Text>();
		this.countDownText = base.FindTransform("CountDownText").GetComponent<Text>();
		this.AnimationObjs = base.FindTransform("AnimationObjs");
		this.BtnExit.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnExit);
		this.BtnAgain.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnAgain);
		this.BtnDamageCal.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnDamageCal);
		this.BtnDamageCal2.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnDamageCal2);
		this.fxTrans = base.FindTransform("fxRoot");
	}

	protected override void OnEnable()
	{
		SoundManager.PlayUI(10041, false);
		EventDispatcher.Broadcast<bool, float, float>(ShaderEffectEvent.ENABLE_BG_BLUR, true, 30f, 0f);
		this.PassTimeNum.set_text(string.Empty);
		this.countDownText.set_text(string.Empty);
		this.MyPowerValueDesc.set_text("我的战力：");
		this.MyPowerValue.set_text(EntityWorld.Instance.EntSelf.Fighting.ToString());
		this.AddUpBtns();
		this.AnimationObjs.GetComponent<CanvasGroup>().set_alpha(0f);
		this.timer = TimerHeap.AddTimer(1000u, 0, delegate
		{
			if (this != null && base.get_gameObject() != null && this.AnimationObjs != null)
			{
				BaseTweenAlphaBaseTime component = this.AnimationObjs.GetComponent<BaseTweenAlphaBaseTime>();
				if (component != null)
				{
					component.TweenAlpha(0f, 1f, 0f, 0.5f);
				}
			}
		});
		EventDispatcher.Broadcast<bool>(ShaderEffectEvent.PLAYER_DEAD, true);
		this.PlayAnimation();
		this.BtnDamageCal.get_gameObject().SetActive(true);
		this.BtnDamageCal2.get_gameObject().SetActive(false);
		this.BtnAgain.get_gameObject().SetActive(true);
		GuideManager.Instance.StopGuideOfFinish();
	}

	protected override void OnDisable()
	{
		EventDispatcher.Broadcast<bool, float, float>(ShaderEffectEvent.ENABLE_BG_BLUR, false, 0f, 0f);
		EventDispatcher.Broadcast<bool>(ShaderEffectEvent.PLAYER_DEAD, false);
		TimerHeap.DelTimer(this.timer);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected void OnClickBtnExit(GameObject sender)
	{
		this.Show(false);
		LoadingUIView.Open(false);
		if (this.BtnExitAction != null)
		{
			this.BtnExitAction.Invoke();
		}
	}

	protected void OnClickBtnAgain(GameObject sender)
	{
		DungeonInfo dungeonInfo = DungeonManager.Instance.GetDungeonInfo(InstanceManager.CurrentInstanceDataID);
		if (dungeonInfo != null && dungeonInfo.remainingChallengeTimes == 0)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505067, false));
			return;
		}
		this.Show(false);
		if (this.BtnAgainAction != null)
		{
			this.BtnAgainAction.Invoke();
		}
	}

	protected void OnClickBtnDamageCal(GameObject sender)
	{
		InstanceDamageCal instanceDamageCal = UIManagerControl.Instance.OpenUI("InstanceDamageCal", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as InstanceDamageCal;
		instanceDamageCal.ResetUI();
	}

	private void OnClickBtnDamageCal2(GameObject sender)
	{
		this.OnClickBtnDamageCal(null);
	}

	protected void OnClickBtnEquipQua(GameObject sender)
	{
		this.Show(false);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		if (this.BtnEquipQuaAction != null)
		{
			this.BtnEquipQuaAction.Invoke();
		}
	}

	protected void OnClickBtnEquipLv(GameObject sender)
	{
		this.Show(false);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		if (this.BtnEquipLvAction != null)
		{
			this.BtnEquipLvAction.Invoke();
		}
	}

	protected void OnClickBtnGemLv(GameObject sender)
	{
		this.Show(false);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		if (this.BtnGemLvAction != null)
		{
			this.BtnGemLvAction.Invoke();
		}
	}

	private void OnClickBtnSkill(GameObject sender)
	{
		this.Show(false);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		if (this.BtnSkillAction != null)
		{
			this.BtnSkillAction.Invoke();
		}
	}

	protected void OnClickBtnPetLv(GameObject sender)
	{
		this.Show(false);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		if (this.BtnPetLvAction != null)
		{
			this.BtnPetLvAction.Invoke();
		}
	}

	protected void OnClickBtnPetStar(GameObject sender)
	{
		this.Show(false);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		if (this.BtnPetStarAction != null)
		{
			this.BtnPetStarAction.Invoke();
		}
	}

	protected void OnClickBtnPetSkill(GameObject sender)
	{
		this.Show(false);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		if (this.BtnPetSkillAction != null)
		{
			this.BtnPetSkillAction.Invoke();
		}
	}

	protected void OnClickBtnGodSoldier(GameObject go)
	{
		this.Show(false);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		if (this.BtnGodSoldierAction != null)
		{
			this.BtnGodSoldierAction.Invoke();
		}
	}

	protected void OnClickBtnWing(GameObject go)
	{
		this.Show(false);
		SceneLoadedUIManager.Instance.IsFromBattleClick = true;
		if (this.BtnWingAction != null)
		{
			this.BtnWingAction.Invoke();
		}
	}

	public void ShowBtnAgainBtn(bool isShow)
	{
		this.BtnAgain.get_gameObject().SetActive(isShow);
		RectTransform rectTransform = this.BtnExit.get_gameObject().get_transform() as RectTransform;
		rectTransform.set_localPosition((!isShow) ? new Vector3(0f, rectTransform.get_localPosition().y, rectTransform.get_localPosition().z) : new Vector3(200f, rectTransform.get_localPosition().y, rectTransform.get_localPosition().z));
	}

	public void ShowBtnDamageCal(bool isShow)
	{
		this.BtnDamageCal.get_gameObject().SetActive(isShow);
	}

	protected void AddUpBtns()
	{
		this.UpWays = StrongerManager.Instance.GetLowest3StrongerData();
		for (int i = 0; i < this.UpWays.get_Count(); i++)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("UpBtn");
			instantiate2Prefab.get_transform().SetParent(this.UpBtns.get_transform(), true);
			Transform transform = instantiate2Prefab.get_transform().FindChild("BtnBg");
			StrongerType type = this.UpWays.get_Item(i).Type;
			BianQiangJieMianPeiZhi bianQiangJieMianPeiZhi = DataReader<BianQiangJieMianPeiZhi>.Get((int)type);
			Image component = transform.GetComponent<Image>();
			if (bianQiangJieMianPeiZhi != null && bianQiangJieMianPeiZhi.icon2 > 0)
			{
				ResourceManager.SetSprite(component, GameDataUtils.GetIcon(bianQiangJieMianPeiZhi.icon2));
			}
			component.SetNativeSize();
			if (transform is RectTransform)
			{
				(transform as RectTransform).set_pivot(ConstVector2.ML);
			}
			switch (type)
			{
			case StrongerType.Equip:
				instantiate2Prefab.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEquipLv);
				break;
			case StrongerType.EquipStrength:
			case StrongerType.EquipStarUp:
			case StrongerType.EquipEnchantment:
				instantiate2Prefab.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnEquipQua);
				break;
			case StrongerType.Gem:
				instantiate2Prefab.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnGemLv);
				break;
			case StrongerType.Wing:
				instantiate2Prefab.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnWing);
				break;
			case StrongerType.PetLevel:
				instantiate2Prefab.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnPetLv);
				break;
			case StrongerType.PetUpgrade:
				instantiate2Prefab.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnPetStar);
				break;
			case StrongerType.GodSoldier:
				instantiate2Prefab.get_transform().GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnGodSoldier);
				break;
			default:
				Debug.Log("There is no exist Upway, ID = " + this.UpType);
				break;
			}
		}
	}

	public void ShowRecommendPower(bool isShow, int theRecommendPower = 0)
	{
		if (isShow)
		{
			this.recommendPower = theRecommendPower;
			this.RecommendPowerValueDesc.get_gameObject().SetActive(true);
			this.RecommendPowerValue.get_gameObject().SetActive(true);
			this.RecommendPowerValue.set_text(this.recommendPower.ToString());
			this.MyPowerValueDesc.get_rectTransform().set_localPosition(new Vector3(-175f, 65f, 0f));
			this.MyPowerValue.get_rectTransform().set_localPosition(new Vector3(30f, 65f, 0f));
			this.RecommendPowerValueDesc.get_rectTransform().set_localPosition(new Vector3(145f, 65f, 0f));
			this.RecommendPowerValue.get_rectTransform().set_localPosition(new Vector3(350f, 65f, 0f));
		}
		else
		{
			this.RecommendPowerValueDesc.get_gameObject().SetActive(false);
			this.RecommendPowerValue.get_gameObject().SetActive(false);
			this.MyPowerValueDesc.get_rectTransform().set_localPosition(new Vector3(-25f, 65f, 0f));
			this.MyPowerValue.get_rectTransform().set_localPosition(new Vector3(180f, 65f, 0f));
		}
	}

	public void OnCountDown(int countTime, Action countDownEndAction)
	{
		this.StopCountDown();
		this.timeCountDown = new TimeCountDown(countTime, TimeFormat.SECOND, delegate
		{
			if (this.countDownText != null)
			{
				this.countDownText.set_text(string.Format("<color=green>" + this.timeCountDown.GetSeconds() + "秒</color>", new object[0]) + "后自动离开");
			}
		}, delegate
		{
			if (countDownEndAction != null)
			{
				countDownEndAction.Invoke();
			}
			this.Show(false);
		}, true);
	}

	public void StopCountDown()
	{
		if (this.timeCountDown != null)
		{
			this.timeCountDown.Dispose();
			this.timeCountDown = null;
		}
	}

	private void PlayAnimation()
	{
		this.fx_LoseStart = FXSpineManager.Instance.PlaySpine(424, this.fxTrans, "CommonBattlePassUI", 3152, delegate
		{
			FXSpineManager.Instance.DeleteSpine(this.fx_LoseStart, true);
			this.fx_LoseIdle = FXSpineManager.Instance.ReplaySpine(this.fx_LoseIdle, 421, this.fxTrans, "CommonBattlePassUI", 3151, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fx_LoseExplode = FXSpineManager.Instance.PlaySpine(427, this.fxTrans, "CommonBattlePassUI", 2153, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}
}
