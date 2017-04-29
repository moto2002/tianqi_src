using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EliteDungeonUI : UIBase
{
	private ScrollRectCustom eliteSRC;

	private ButtonCustom nextPageBtn;

	private ButtonCustom prePageBtn;

	private List<Transform> arenaBtnList;

	private int CurrentIndex = -1;

	private Text canGetPrizeTimesTxt;

	private Text resetGetPrizeTimesTxt;

	private ListPool eliteDungeonItemRegionListPool;

	private int LastIndex = -1;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.eliteSRC = base.FindTransform("EliteSRC").GetComponent<ScrollRectCustom>();
		this.eliteSRC.movePage = false;
		this.eliteSRC.Arrow2First = base.FindTransform("PreButton");
		this.eliteSRC.Arrow2Last = base.FindTransform("NextButton");
		this.eliteDungeonItemRegionListPool = base.FindTransform("EliteDungeonItemRegion").GetComponent<ListPool>();
		this.arenaBtnList = new List<Transform>();
		for (int i = 1; i < 6; i++)
		{
			Transform transform = base.FindTransform("WorldMap" + i);
			if (transform != null)
			{
				transform.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickChangeArena);
				this.arenaBtnList.Add(transform);
			}
		}
		this.nextPageBtn = base.FindTransform("NextButton").GetComponent<ButtonCustom>();
		this.prePageBtn = base.FindTransform("PreButton").GetComponent<ButtonCustom>();
		this.nextPageBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickNextPage);
		this.prePageBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickPreviousPage);
		this.canGetPrizeTimesTxt = base.FindTransform("CanGetPrizeTimesTxt").FindChild("Des").GetComponent<Text>();
		this.resetGetPrizeTimesTxt = base.FindTransform("ResetPrizeTimesTxt").FindChild("Des").GetComponent<Text>();
		this.eliteDungeonItemRegionListPool.Clear();
	}

	protected override void OnEnable()
	{
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110036), string.Empty, delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		this.CurrentIndex = 0;
		this.OnClickChangeArena(this.arenaBtnList.get_Item(this.CurrentIndex).get_gameObject());
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdataEliteInfo, new Callback(this.RefreshUI));
		EventDispatcher.AddListener(EventNames.EliteCopyMiscChange, new Callback(this.RefreshUI));
		EventDispatcher.AddListener(EventNames.EliteMapChange, new Callback(this.RefreshArenaBtnState));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdataEliteInfo, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener(EventNames.EliteCopyMiscChange, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener(EventNames.EliteMapChange, new Callback(this.RefreshArenaBtnState));
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	private void RefreshArenaBtnState()
	{
		for (int i = 0; i < this.arenaBtnList.get_Count(); i++)
		{
			int num = i + 1;
			bool flag = true;
			JJingYingFuBenQuYu jJingYingFuBenQuYu = DataReader<JJingYingFuBenQuYu>.Get(num);
			if (jJingYingFuBenQuYu != null)
			{
				int level = jJingYingFuBenQuYu.level;
				if (EntityWorld.Instance.EntSelf.Lv < level)
				{
					flag = false;
				}
			}
			if (EliteDungeonManager.Instance.EliteMapInfoDic.ContainsKey(num) && EliteDungeonManager.Instance.EliteMapInfoDic.get_Item(num).openFlag && flag)
			{
				if (this.CurrentIndex < 0)
				{
					this.CurrentIndex = i;
				}
				this.arenaBtnList.get_Item(i).FindChild("LockImage").get_gameObject().SetActive(false);
				this.arenaBtnList.get_Item(i).FindChild("Image").GetComponent<Image>().set_color(new Color32(255, 255, 255, 255));
			}
			else
			{
				this.arenaBtnList.get_Item(i).FindChild("LockImage").get_gameObject().SetActive(true);
				this.arenaBtnList.get_Item(i).FindChild("Image").GetComponent<Image>().set_color(new Color32(24, 24, 22, 255));
			}
		}
	}

	protected void RefreshUI()
	{
		this.canGetPrizeTimesTxt.set_text(string.Format(GameDataUtils.GetChineseContent(505415, false), EliteDungeonManager.Instance.RestGetRewardTimes, EliteDungeonManager.Instance.GetVIPCanGetPrizeTimesMax()));
		this.resetGetPrizeTimesTxt.set_text(EliteDungeonManager.Instance.GetResetGetPrizeTimesStr());
		List<EliteDataInfo> eliteDataList = EliteDungeonManager.Instance.EliteDataList;
		List<EliteDataInfo> currentList = new List<EliteDataInfo>();
		for (int i = 0; i < eliteDataList.get_Count(); i++)
		{
			EliteDataInfo eliteDataInfo = eliteDataList.get_Item(i);
			if (eliteDataInfo.ArenaID == this.CurrentIndex + 1)
			{
				currentList.Add(eliteDataInfo);
			}
		}
		int count = currentList.get_Count();
		this.eliteDungeonItemRegionListPool.Create(count, delegate(int index)
		{
			if (index < count && index < this.eliteDungeonItemRegionListPool.Items.get_Count())
			{
				EliteDungeonItem component = this.eliteDungeonItemRegionListPool.Items.get_Item(index).GetComponent<EliteDungeonItem>();
				if (component != null)
				{
					component.RefreshUI(currentList.get_Item(index));
				}
			}
		});
		this.eliteSRC.OnHasBuilt = delegate
		{
			this.eliteSRC.Move2Index(EliteDungeonManager.Instance.GetCanStarFightBossIndex(1), true);
			this.eliteSRC.OnHasBuilt = null;
		};
	}

	private void OnClickChangeArena(GameObject go)
	{
		int num = this.arenaBtnList.FindIndex((Transform a) => a.get_gameObject() == go);
		if (num >= 0)
		{
			JJingYingFuBenQuYu jJingYingFuBenQuYu = DataReader<JJingYingFuBenQuYu>.Get(num + 1);
			if (jJingYingFuBenQuYu != null)
			{
				int level = jJingYingFuBenQuYu.level;
				if (EntityWorld.Instance.EntSelf.Lv < level)
				{
					string text = string.Format(GameDataUtils.GetChineseContent(505075, false), level);
					UIManagerControl.Instance.ShowToastText(text);
					return;
				}
			}
			if (!EliteDungeonManager.Instance.EliteMapInfoDic.ContainsKey(num + 1) || !EliteDungeonManager.Instance.EliteMapInfoDic.get_Item(num + 1).openFlag)
			{
				UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(505026, false));
				return;
			}
			this.CurrentIndex = num;
			if (this.LastIndex == this.CurrentIndex)
			{
				return;
			}
			if (this.LastIndex >= 0 && this.LastIndex < this.arenaBtnList.get_Count())
			{
				this.arenaBtnList.get_Item(this.LastIndex).get_transform().FindChild("ClickImage").get_gameObject().SetActive(false);
			}
			this.arenaBtnList.get_Item(num).get_transform().FindChild("ClickImage").get_gameObject().SetActive(true);
			this.LastIndex = this.CurrentIndex;
			this.OnClickMapBtn(this.CurrentIndex + 1);
		}
	}

	private void OnClickMapBtn(int _mapId)
	{
		EliteDungeonManager.Instance.SendEliteCopyInfoReq(_mapId);
	}

	private void OnClickNextPage(GameObject go)
	{
		if (this.eliteSRC != null)
		{
			this.eliteSRC.Move2Next();
		}
	}

	private void OnClickPreviousPage(GameObject go)
	{
		if (this.eliteSRC != null)
		{
			this.eliteSRC.Move2Previous();
		}
	}
}
