using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ActorPropertyUI : UIBase
{
	private const string AC = "ff7d4b";

	public static ActorPropertyUI Instance;

	private Text TextLV;

	private Text TextExp;

	private Image ImageProgress;

	private Text TextAtk;

	private Text TextDefence;

	private Text TextHpLmt;

	private Text TextPveAtk;

	private Text TextPvpAtk;

	private Text TextHitRatio;

	private Text TextDodgeRatio;

	private Text TextCritRatio;

	private Text TextDecritRatio;

	private Text TextCritHurtAddRatio;

	private Text TextParryRatio;

	private Text TextDeparryRatio;

	private Text TextParryHurtDeRatio;

	private Text TextSuckBloodScale;

	private Text TextHurtAddRatio;

	private Text TextHurtDeRatio;

	private Text TextPveHurtAddRatio;

	private Text TextPveHurtDeRatio;

	private Text TextPvpHurtAddRatio;

	private Text TextPvpHurtDeRatio;

	private Text TextMoveSpeed;

	private Text TextHpRestore;

	private void Awake()
	{
		ActorPropertyUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.TextLV = base.FindTransform("TextLV").GetComponent<Text>();
		this.TextExp = base.FindTransform("TextExp").GetComponent<Text>();
		this.ImageProgress = base.FindTransform("ImageProgress").GetComponent<Image>();
		this.TextAtk = base.FindTransform("TextAtk").GetComponent<Text>();
		this.TextDefence = base.FindTransform("TextDefence").GetComponent<Text>();
		this.TextHpLmt = base.FindTransform("TextHpLmt").GetComponent<Text>();
		this.TextPveAtk = base.FindTransform("TextPveAtk").GetComponent<Text>();
		this.TextPvpAtk = base.FindTransform("TextPvpAtk").GetComponent<Text>();
		this.TextHitRatio = base.FindTransform("TextHitRatio").GetComponent<Text>();
		this.TextDodgeRatio = base.FindTransform("TextDodgeRatio").GetComponent<Text>();
		this.TextCritRatio = base.FindTransform("TextCritRatio").GetComponent<Text>();
		this.TextDecritRatio = base.FindTransform("TextDecritRatio").GetComponent<Text>();
		this.TextCritHurtAddRatio = base.FindTransform("TextCritHurtAddRatio").GetComponent<Text>();
		this.TextParryRatio = base.FindTransform("TextParryRatio").GetComponent<Text>();
		this.TextDeparryRatio = base.FindTransform("TextDeparryRatio").GetComponent<Text>();
		this.TextParryHurtDeRatio = base.FindTransform("TextParryHurtDeRatio").GetComponent<Text>();
		this.TextSuckBloodScale = base.FindTransform("TextSuckBloodScale").GetComponent<Text>();
		this.TextHurtAddRatio = base.FindTransform("TextHurtAddRatio").GetComponent<Text>();
		this.TextHurtDeRatio = base.FindTransform("TextHurtDeRatio").GetComponent<Text>();
		this.TextPveHurtAddRatio = base.FindTransform("TextPveHurtAddRatio").GetComponent<Text>();
		this.TextPveHurtDeRatio = base.FindTransform("TextPveHurtDeRatio").GetComponent<Text>();
		this.TextPvpHurtAddRatio = base.FindTransform("TextPvpHurtAddRatio").GetComponent<Text>();
		this.TextPvpHurtDeRatio = base.FindTransform("TextPvpHurtDeRatio").GetComponent<Text>();
		this.TextMoveSpeed = base.FindTransform("TextMoveSpeed").GetComponent<Text>();
		this.TextHpRestore = base.FindTransform("TextHpRestore").GetComponent<Text>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			ActorPropertyUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.EquipEquipmentSucess, new Callback(this.OnEquipEquipmentSucess));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.EquipEquipmentSucess, new Callback(this.OnEquipEquipmentSucess));
	}

	private void OnEquipEquipmentSucess()
	{
		this.RefreshUI(EntityWorld.Instance.EntSelf);
	}

	public void RefreshUI(EntitySelf self)
	{
		this.Awake();
		this.TextAtk.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.Atk, RoleAttrTool.GetAtk(self), "ff7d4b"));
		this.TextDefence.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.Defence, RoleAttrTool.GetDefence(self), "ff7d4b"));
		this.TextHpLmt.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.HpLmt, RoleAttrTool.GetHpLmt(self), "ff7d4b"));
		this.TextHurtAddRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.HurtAddRatio, self.HurtAddRatio, "ff7d4b"));
		this.TextHurtDeRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.HurtDeRatio, self.HurtDeRatio, "ff7d4b"));
		this.TextHitRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.HitRatio, self.HitRatio, "ff7d4b"));
		this.TextDodgeRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.DodgeRatio, self.DodgeRatio, "ff7d4b"));
		this.TextCritRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.CritRatio, self.CritRatio, "ff7d4b"));
		this.TextCritHurtAddRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.CritAddValue, self.CritAddValue, "ff7d4b"));
		this.TextParryRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.ParryRatio, self.ParryRatio, "ff7d4b"));
		this.TextParryHurtDeRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.ParryHurtDeRatio, self.ParryHurtDeRatio, "ff7d4b"));
		this.TextDeparryRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.DeparryRatio, self.DeparryRatio, "ff7d4b"));
		this.TextDecritRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.DecritRatio, self.DecritRatio, "ff7d4b"));
		this.TextMoveSpeed.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.MoveSpeed, self.MoveSpeed, "ff7d4b"));
		this.TextHpRestore.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.HpRestore, self.HpRestore, "ff7d4b"));
		this.TextLV.set_text("Lv" + self.Lv);
		this.TextExp.set_text(Utils.SwitchChineseNumber(self.Exp, 0) + "/" + Utils.SwitchChineseNumber(self.ExpLmt, 0));
		float x;
		if (self.ExpLmt == 0L)
		{
			x = 0f;
		}
		else
		{
			x = 375f * ((float)self.Exp / (float)self.ExpLmt);
		}
		Vector2 sizeDelta = this.ImageProgress.GetComponent<RectTransform>().get_sizeDelta();
		sizeDelta.x = x;
		this.ImageProgress.GetComponent<RectTransform>().set_sizeDelta(sizeDelta);
	}

	public void RefreshUI(FindBuddyRes down)
	{
		this.Awake();
		if (down == null)
		{
			return;
		}
		this.TextAtk.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.Atk, down.infoExt.atk, "ff7d4b"));
		this.TextDefence.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.Defence, down.infoExt.defence, "ff7d4b"));
		this.TextHpLmt.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.HpLmt, down.infoExt.hp, "ff7d4b"));
		this.TextHurtAddRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.HurtAddRatio, down.infoExt.hurtAddRatio, "ff7d4b"));
		this.TextHurtDeRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.HurtDeRatio, down.infoExt.hurtDeRatio, "ff7d4b"));
		this.TextHitRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.HitRatio, down.infoExt.hitRatio, "ff7d4b"));
		this.TextDodgeRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.DodgeRatio, down.infoExt.dodgeRatio, "ff7d4b"));
		this.TextCritRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.CritRatio, down.infoExt.critRatio, "ff7d4b"));
		this.TextCritHurtAddRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.CritHurtAddRatio, down.infoExt.critHurtAddRatio, "ff7d4b"));
		this.TextParryRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.ParryRatio, down.infoExt.parryRatio, "ff7d4b"));
		this.TextParryHurtDeRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.ParryHurtDeRatio, down.infoExt.parryHurtDeRatio, "ff7d4b"));
		this.TextDeparryRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.DeparryRatio, down.infoExt.deparryRatio, "ff7d4b"));
		this.TextDecritRatio.set_text(AttrUtility.GetStandardDesc(GameData.AttrType.DecritRatio, down.infoExt.decritRatio, "ff7d4b"));
		this.TextMoveSpeed.set_text(string.Empty);
		this.TextHpRestore.set_text(string.Empty);
		this.TextLV.set_text("Lv" + down.info.lv);
		this.TextExp.set_text(down.infoExt.exp + "/" + down.infoExt.expLmt);
		float x;
		if (down.infoExt.expLmt == 0L)
		{
			x = 0f;
		}
		else
		{
			x = 375f * ((float)down.infoExt.exp / (float)down.infoExt.expLmt);
		}
		Vector2 sizeDelta = this.ImageProgress.GetComponent<RectTransform>().get_sizeDelta();
		sizeDelta.x = x;
		this.ImageProgress.GetComponent<RectTransform>().set_sizeDelta(sizeDelta);
	}
}
