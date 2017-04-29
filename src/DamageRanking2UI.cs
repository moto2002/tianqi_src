using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageRanking2UI : BaseUIBehaviour
{
	protected GameObject BellwetherInfo;

	protected Text BellwetherInfoRank;

	protected Text BellwetherInfoName;

	protected Image BellwetherInfoPercentage;

	protected RectTransform BellwetherInfoDamageImage;

	protected Text BellwetherInfoDamageText;

	protected GameObject SelfInfo;

	protected Image SelfInfoPercentage;

	protected Text SelfInfoRank;

	protected Text SelfInfoName;

	protected RectTransform SelfInfoDamageImage;

	protected Text SelfInfoDamageText;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.BellwetherInfo = base.FindTransform("BellwetherInfo").get_gameObject();
		this.BellwetherInfoPercentage = base.FindTransform("BellwetherInfoPercentage").GetComponent<Image>();
		this.BellwetherInfoRank = base.FindTransform("BellwetherInfoRank").GetComponent<Text>();
		this.BellwetherInfoName = base.FindTransform("BellwetherInfoName").GetComponent<Text>();
		this.BellwetherInfoDamageImage = (base.FindTransform("BellwetherInfoDamageImage") as RectTransform);
		this.BellwetherInfoDamageText = base.FindTransform("BellwetherInfoDamageText").GetComponent<Text>();
		this.SelfInfo = base.FindTransform("SelfInfo").get_gameObject();
		this.SelfInfoPercentage = base.FindTransform("SelfInfoPercentage").GetComponent<Image>();
		this.SelfInfoRank = base.FindTransform("SelfInfoRank").GetComponent<Text>();
		this.SelfInfoName = base.FindTransform("SelfInfoName").GetComponent<Text>();
		this.SelfInfoDamageImage = (base.FindTransform("SelfInfoDamageImage") as RectTransform);
		this.SelfInfoDamageText = base.FindTransform("SelfInfoDamageText").GetComponent<Text>();
	}

	private void OnEnable()
	{
		this.BellwetherInfo.SetActive(false);
		this.SelfInfo.SetActive(false);
		this.AddListeners();
	}

	private void OnDisable()
	{
		this.RemoveListeners();
	}

	protected override void AddListeners()
	{
		if (!this.IsAddListenersSuccess)
		{
			this.IsAddListenersSuccess = true;
			EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
		}
	}

	protected override void RemoveListeners()
	{
		if (this.IsAddListenersSuccess)
		{
			this.IsAddListenersSuccess = false;
			EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
		}
	}

	protected void OnSecondsPast()
	{
		InstanceManager.QueryBattleSituation(BattleHurtInfoType.RoleMakeBossHurt, delegate(XDict<long, BattleSituationInfo> battleSituationInfoDic)
		{
			BattleSituationInfo battleSituationInfo = null;
			if (battleSituationInfoDic == null || battleSituationInfoDic.Count == 0)
			{
				BattleSituationInfo battleSituationInfo2 = new BattleSituationInfo
				{
					id = EntityWorld.Instance.EntSelf.ID,
					name = EntityWorld.Instance.EntSelf.Name,
					num = 0L
				};
				battleSituationInfo = new BattleSituationInfo
				{
					id = EntityWorld.Instance.EntSelf.ID,
					name = EntityWorld.Instance.EntSelf.Name,
					num = 0L
				};
				this.RefreshUI(battleSituationInfo2, 1, 0f, battleSituationInfo, 1, 0f);
			}
			else
			{
				List<BattleSituationInfo> list = new List<BattleSituationInfo>();
				long num = 0L;
				int selfRank = 0;
				for (int i = 0; i < battleSituationInfoDic.Count; i++)
				{
					num += battleSituationInfoDic.Values.get_Item(i).num;
					list.Add(battleSituationInfoDic.Values.get_Item(i));
				}
				list.Sort(new Comparison<BattleSituationInfo>(BattleSituationInfo.Comparition));
				BattleSituationInfo battleSituationInfo2 = list.get_Item(0);
				float bellwetherPercentage = (num != 0L) ? ((battleSituationInfoDic.Count != 1) ? ((float)((double)battleSituationInfo2.num / (double)num)) : 1f) : 0f;
				for (int j = 0; j < list.get_Count(); j++)
				{
					if (list.get_Item(j).id == EntityWorld.Instance.EntSelf.ID)
					{
						selfRank = j + 1;
						battleSituationInfo = list.get_Item(j);
						break;
					}
				}
				float selfPercentage;
				if (battleSituationInfo == null)
				{
					selfRank = battleSituationInfoDic.Count + 1;
					battleSituationInfo = new BattleSituationInfo
					{
						id = EntityWorld.Instance.EntSelf.ID,
						name = EntityWorld.Instance.EntSelf.Name,
						num = 0L
					};
					selfPercentage = 0f;
				}
				else
				{
					selfPercentage = ((num != 0L) ? ((battleSituationInfoDic.Count != 1) ? ((float)((double)battleSituationInfo.num / (double)num)) : 1f) : 0f);
				}
				this.RefreshUI(battleSituationInfo2, 1, bellwetherPercentage, battleSituationInfo, selfRank, selfPercentage);
				list.Clear();
			}
		});
	}

	protected void RefreshUI(BattleSituationInfo bellwetherInfo, int bellwetherRank, float bellwetherPercentage, BattleSituationInfo selfInfo, int selfRank, float selfPercentage)
	{
		if (!this.BellwetherInfo.get_activeSelf())
		{
			this.BellwetherInfo.SetActive(true);
		}
		this.BellwetherInfoRank.set_text(bellwetherRank + ".");
		this.BellwetherInfoName.set_text(bellwetherInfo.name + "：");
		this.BellwetherInfoPercentage.set_fillAmount(bellwetherPercentage);
		this.BellwetherInfoDamageText.set_text(bellwetherInfo.num.ToString());
		if (!this.SelfInfo.get_activeSelf())
		{
			this.SelfInfo.SetActive(true);
		}
		this.SelfInfoRank.set_text(selfRank + ".");
		this.SelfInfoName.set_text(GameDataUtils.GetChineseContent(505180, false) + "：");
		this.SelfInfoPercentage.set_fillAmount(selfPercentage);
		this.SelfInfoDamageText.set_text(selfInfo.num.ToString());
	}
}
