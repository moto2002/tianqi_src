using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GetGodWeaponUI : UIBase
{
	private const int DRIFT_SPEED = 850;

	private RectTransform mRectIcon;

	private RectTransform JumpDriftUIPos;

	private Text mTxName;

	private Text mTxTitle;

	private Text mTxContent;

	private Image mMaskScreen;

	private GameObject mDescPanel;

	private int mFxBgId;

	private int mFxIconId;

	private uint mDelayGodWeaponId;

	private bool mStartMove;

	private bool mCanClick;

	private Action mNextAction;

	private Vector3 m_targetDriftPosition;

	private Vector2 m_targetDriftOffset;

	private uint mDelayId;

	private float mTargetDist;

	private Artifact mGodData;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mRectIcon = base.FindTransform("ContinueIcon").GetComponent<RectTransform>();
		this.mTxName = base.FindTransform("txName").GetComponent<Text>();
		this.mTxTitle = base.FindTransform("txTitle").GetComponent<Text>();
		this.mTxContent = base.FindTransform("txContent").GetComponent<Text>();
		this.JumpDriftUIPos = base.FindTransform("JumpDriftUIPos").GetComponent<RectTransform>();
		this.mMaskScreen = base.FindTransform("MaskScreen").GetComponent<Image>();
		this.mMaskScreen.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickMask));
		this.mDescPanel = base.FindTransform("DescPanel").get_gameObject();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (MainTaskManager.Instance.AutoTaskId > 0 && EntityWorld.Instance != null && EntityWorld.Instance.EntSelf != null)
		{
			MainTaskManager.Instance.StopToNPC(true);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		TimerHeap.DelTimer(this.mDelayId);
		GuideManager.Instance.out_system_lock = false;
		GodWeaponManager.Instance.WeaponLock = false;
		this.ResetWeapon();
	}

	private void Update()
	{
		this.DriftToTheTarget();
	}

	public void SetData(int godId, Action nextAction)
	{
		this.mNextAction = nextAction;
		this.mGodData = DataReader<Artifact>.Get(godId);
		if (this.mGodData != null)
		{
			if (UIManagerControl.Instance.IsOpen("TownUI") && this.mGodData.areaIndex == 5)
			{
				Transform transform = WidgetSystem.FindWidgetOnUI(this.mGodData.widgetId, true);
				if (transform != null)
				{
					transform.get_gameObject().SetActive(true);
				}
				TownUI.Instance.ForceOpenRightBottom();
			}
			this.RefreshDesc(this.mGodData);
			this.mFxIconId = FXSpineManager.Instance.PlaySpine(3907, this.mRectIcon.get_transform(), "TownUI", 14001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			TimerHeap.AddTimer(500u, 0, delegate
			{
				this.mCanClick = true;
			});
			Debug.Log("=====神器[" + godId + "]开放指导!!!=====");
		}
		else
		{
			this.ExecuteNext();
		}
	}

	private void OnClickMask()
	{
		if (this.mCanClick)
		{
			this.mCanClick = false;
			this.SetDriftTarget(this.mGodData.areaIndex, this.mGodData.widgetId);
			this.ShowFightPower(this.mGodData);
			this.mStartMove = true;
			this.mDescPanel.SetActive(false);
			this.mMaskScreen.set_color(new Color(0f, 0f, 0f, 0f));
		}
	}

	private void ResetWeapon()
	{
		this.mDescPanel.SetActive(true);
		this.mMaskScreen.set_color(new Color(0f, 0f, 0f, 0.5f));
		this.mRectIcon.set_anchoredPosition(new Vector2(-195f, -10f));
		this.mRectIcon.set_localScale(Vector3.get_one());
		this.mRectIcon.get_gameObject().SetActive(true);
	}

	private void SetDriftTarget(int area, int wightId)
	{
		if (!SystemOpenManager.GetTargetPosition(area, wightId, ref this.m_targetDriftPosition, ref this.m_targetDriftOffset))
		{
			this.DriftFinished();
			return;
		}
		UIBase uIIfExist = UIManagerControl.Instance.GetUIIfExist("TownUI");
		if (uIIfExist == null)
		{
			Debug.LogError("TownUI is null");
			return;
		}
		List<Vector2> offsets;
		(uIIfExist as TownUI).MoveAreaButtons(area, SystemOpenManager.GetBehinds(area, wightId, out offsets), offsets);
		this.JumpDriftUIPos.set_position(this.m_targetDriftPosition);
		this.JumpDriftUIPos.set_localPosition(new Vector3(this.JumpDriftUIPos.get_localPosition().x, this.JumpDriftUIPos.get_localPosition().y, 0f));
		RectTransform expr_AE = this.JumpDriftUIPos;
		expr_AE.set_anchoredPosition(expr_AE.get_anchoredPosition() + this.m_targetDriftOffset);
		this.mTargetDist = Vector2.Distance(this.mRectIcon.get_anchoredPosition(), this.JumpDriftUIPos.get_anchoredPosition());
	}

	private void DriftToTheTarget()
	{
		if (!this.mStartMove)
		{
			return;
		}
		if (this.mRectIcon == null || !this.mRectIcon.get_gameObject().get_activeSelf())
		{
			return;
		}
		float num = Vector2.Distance(this.mRectIcon.get_anchoredPosition(), this.JumpDriftUIPos.get_anchoredPosition());
		if (num <= 1f)
		{
			this.mStartMove = false;
			FXSpineManager.Instance.PlaySpine(3305, this.mRectIcon.get_transform(), string.Empty, 10006, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			FXSpineManager.Instance.DeleteSpine(this.mFxIconId, true);
			TimerHeap.AddTimer(300u, 0, new Action(this.DriftFinished));
			return;
		}
		this.mRectIcon.set_anchoredPosition(Vector2.MoveTowards(this.mRectIcon.get_anchoredPosition(), this.JumpDriftUIPos.get_anchoredPosition(), 850f * Time.get_deltaTime()));
		this.mRectIcon.set_localScale(Vector3.get_one() * Mathf.Lerp(0.5f, 1f, num / this.mTargetDist));
	}

	private void DriftFinished()
	{
		this.mRectIcon.get_gameObject().SetActive(false);
		this.ExecuteNext();
	}

	private void ExecuteNext()
	{
		GuideManager.Instance.out_system_lock = false;
		GodWeaponManager.Instance.WeaponLock = false;
		MainTaskManager.Instance.ExecuteTask(0, false);
		if (this.mNextAction != null)
		{
			this.mNextAction.Invoke();
		}
	}

	private void ShowFightPower(Artifact data)
	{
		if (data.system == 4)
		{
			Artifact.SystemparameterPair systemparameterPair = data.systemParameter.Find((Artifact.SystemparameterPair e) => e.key == EntityWorld.Instance.EntSelf.TypeID);
			if (systemparameterPair != null)
			{
				ArtifactSkill artifactSkill = DataReader<ArtifactSkill>.Get(systemparameterPair.value);
				if (artifactSkill != null)
				{
					this.ShowFightPower(artifactSkill.effect.get_Item(0));
				}
			}
		}
		else if (data.system == 5)
		{
			Artifact.SystemparameterPair systemparameterPair2 = data.systemParameter.Find((Artifact.SystemparameterPair e) => e.key == EntityWorld.Instance.EntSelf.TypeID);
			if (systemparameterPair2 != null)
			{
				this.ShowFightPower(systemparameterPair2.value);
			}
		}
	}

	private void ShowFightPower(int attrId)
	{
		Attrs attrs = DataReader<Attrs>.Get(attrId);
		if (attrs != null)
		{
			long num = EquipGlobal.CalculateFightingByIDAndValue(attrs.attrs, attrs.values);
			if (num > 0L)
			{
				FightingUpUI fightingUpUI = UIManagerControl.Instance.OpenUI("FightingUpUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush) as FightingUpUI;
				fightingUpUI.SetPowerUp(num, null);
			}
		}
	}

	private void RefreshDesc(Artifact data)
	{
		this.mTxName.set_text(GameDataUtils.GetChineseContent(data.name, false));
		ResourceManager.SetSprite(this.mRectIcon.GetComponent<Image>(), GameDataUtils.GetIcon(this.mGodData.model));
		string text = string.Empty;
		string text2 = string.Empty;
		Artifact.SystemparameterPair systemparameterPair = data.systemParameter.Find((Artifact.SystemparameterPair e) => e.key == EntityWorld.Instance.EntSelf.TypeID);
		if (systemparameterPair != null || data.system == 5)
		{
			switch (data.system)
			{
			case 1:
			{
				Skill skill = DataReader<Skill>.Get(systemparameterPair.value);
				if (skill != null)
				{
					text = "激活技能：" + GameDataUtils.GetChineseContent(skill.name, false);
					text2 = GameDataUtils.GetChineseContent(skill.describeId, false);
				}
				break;
			}
			case 2:
			{
				Runes runes = DataReader<Runes>.Get(systemparameterPair.value);
				Runes_basic runes_basic = DataReader<Runes_basic>.Get(systemparameterPair.value);
				if (runes != null && runes_basic != null)
				{
					text = "激活符文：" + GameDataUtils.GetChineseContent(runes_basic.name, false);
					text2 = GameDataUtils.GetChineseContent(runes.desc, false);
				}
				break;
			}
			case 3:
			{
				SystemOpen systemOpen = DataReader<SystemOpen>.Get(systemparameterPair.value);
				if (systemOpen != null)
				{
					text2 = GameDataUtils.GetChineseContent(systemOpen.bewrite, false);
				}
				break;
			}
			case 4:
			{
				ArtifactSkill artifactSkill = DataReader<ArtifactSkill>.Get(systemparameterPair.value);
				if (artifactSkill != null)
				{
					text = "激活被动：" + GameDataUtils.GetChineseContent(artifactSkill.name, false);
					text2 = GameDataUtils.GetChineseContent(artifactSkill.desc, false);
				}
				break;
			}
			case 5:
				text = "激活属性：" + GameDataUtils.GetChineseContent(data.skillName, false);
				text2 = GameDataUtils.GetChineseContent(data.skillExplain, false);
				break;
			}
		}
		this.mTxTitle.set_text(text);
		this.mTxContent.set_text(text2);
	}
}
