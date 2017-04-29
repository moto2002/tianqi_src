using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : UIBase
{
	public static TitleUI Instance;

	private ListView2 TitlesList;

	protected override void Preprocessing()
	{
		this.isMask = false;
		this.alpha = 0.7f;
		this.isClick = true;
		base.hideMainCamera = true;
	}

	private void Awake()
	{
		TitleUI.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.TitlesList = base.FindTransform("TitlesList").GetComponent<ListView2>();
	}

	private void Start()
	{
	}

	protected override void OnEnable()
	{
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
		this.RefreshUI();
		base.SetAsLastSibling();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			TitleUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.RefreshTitleInfo, new Callback(this.RefreshUI));
		EventDispatcher.AddListener<int>(EventNames.UpdateWearInfo, new Callback<int>(this.OnUpdateWearInfo));
	}

	protected override void RemoveListeners()
	{
		EventDispatcher.RemoveListener(EventNames.RefreshTitleInfo, new Callback(this.RefreshUI));
		EventDispatcher.RemoveListener<int>(EventNames.UpdateWearInfo, new Callback<int>(this.OnUpdateWearInfo));
		base.RemoveListeners();
	}

	public void RefreshUI()
	{
		int num = TitleManager.Instance.TitleListOwn.get_Count() + TitleManager.Instance.TitleList.get_Count();
		this.TitlesList.CreateRow(num, 0);
		int num2 = 0;
		using (List<TitleInfo>.Enumerator enumerator = TitleManager.Instance.TitleListOwn.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				TitleInfo current = enumerator.get_Current();
				this.TitlesList.Items.get_Item(num2).GetComponent<TitleUiItem>().UpdateItem(true, current);
				this.TitlesList.Items.get_Item(num2).GetComponent<ButtonCustom>().set_enabled(true);
				num2++;
			}
		}
		using (List<TitleInfo>.Enumerator enumerator2 = TitleManager.Instance.TitleList.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				TitleInfo current2 = enumerator2.get_Current();
				this.TitlesList.Items.get_Item(num2).GetComponent<TitleUiItem>().UpdateItem(false, current2);
				this.TitlesList.Items.get_Item(num2).GetComponent<ButtonCustom>().set_enabled(false);
				num2++;
			}
		}
		NetworkManager.Send(new LookTitleReq(), ServerType.Data);
	}

	public void OnUpdateWearInfo(int id)
	{
		using (List<GameObject>.Enumerator enumerator = this.TitlesList.Items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GameObject current = enumerator.get_Current();
				Transform transform = current.get_transform().Find("Wear");
				TitleUiItem component = current.GetComponent<TitleUiItem>();
				transform.get_gameObject().SetActive(component.id == id);
			}
		}
	}

	private void OnSecondsPast()
	{
		using (List<GameObject>.Enumerator enumerator = this.TitlesList.Items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GameObject current = enumerator.get_Current();
				current.GetComponent<TitleUiItem>().RefreshTime();
			}
		}
	}

	private void setDetail(int id)
	{
		ChengHao chengHao = DataReader<ChengHao>.Get(id);
		Transform transform = base.FindTransform("DetailTitleText");
		Transform transform2 = base.FindTransform("DetailTitleIcon");
		if (DataReader<ChengHao>.Get(id).displayWay == 1)
		{
			transform.get_gameObject().SetActive(true);
			transform2.get_gameObject().SetActive(false);
			transform.GetComponent<Text>().set_text(GameDataUtils.GetChineseContent(DataReader<ChengHao>.Get(id).icon, false));
		}
		else
		{
			transform.get_gameObject().SetActive(false);
			transform2.get_gameObject().SetActive(true);
			ResourceManager.SetSprite(transform2.GetComponent<Image>(), GameDataUtils.GetIcon(DataReader<ChengHao>.Get(id).icon));
		}
		Text component = base.FindTransform("ConditionDesc").GetComponent<Text>();
		string text = GameDataUtils.GetChineseContent(chengHao.introduction, false);
		string text2 = null;
		int condition = chengHao.condition;
		if (condition != 2)
		{
			if (condition != 10)
			{
				text2 = chengHao.size.ToString();
			}
			else
			{
				int chapterOrder = DataReader<ZhuXianZhangJiePeiZhi>.Get(DataReader<ZhuXianPeiZhi>.Get(chengHao.size).chapterId).chapterOrder;
				int instance = DataReader<ZhuXianPeiZhi>.Get(chengHao.size).instance;
				string chineseContent = GameDataUtils.GetChineseContent(DataReader<ZhuXianPeiZhi>.Get(chengHao.size).name, false);
				text = string.Format(text, chapterOrder, instance, chineseContent);
			}
		}
		else
		{
			text2 = GameDataUtils.GetChineseContent(DataReader<JingJiChangFenDuan>.Get(chengHao.size).name, false);
		}
		if (text2 != null)
		{
			text = string.Format(text, text2);
		}
		if (chengHao.schedule == -1)
		{
			component.set_text(text);
		}
		else
		{
			int num = 0;
			if (TitleManager.Instance.idProcessMap.ContainsKey(id))
			{
				num = TitleManager.Instance.idProcessMap.get_Item(id);
			}
			if (num > chengHao.schedule)
			{
				num = chengHao.schedule;
			}
			component.set_text(string.Concat(new object[]
			{
				text,
				"(",
				num,
				"/",
				chengHao.schedule,
				")"
			}));
		}
		GameObject[] array = new GameObject[4];
		for (int i = 0; i < 4; i++)
		{
			array[i] = base.FindTransform("BonusesDesc" + i).get_gameObject();
			array[i].SetActive(false);
		}
		List<int> attrs = DataReader<Attrs>.Get(chengHao.gainProperty).attrs;
		List<int> values = DataReader<Attrs>.Get(chengHao.gainProperty).values;
		for (int j = 0; j < attrs.get_Count(); j++)
		{
			array[j].SetActive(true);
			array[j].get_transform().GetComponent<Text>().set_text(AttrUtility.GetStandardAddDesc(attrs.get_Item(j), values.get_Item(j)));
		}
		Text component2 = base.FindTransform("Desc").GetComponent<Text>();
		component2.set_text(GameDataUtils.GetChineseContent(DataReader<ChengHao>.Get(id).gainIntroduction, false));
		component2.get_gameObject().SetActive(TitleManager.Instance.Contain(id));
		GameObject gameObject = base.FindTransform("LimitTime").get_gameObject();
		if (DataReader<ChengHao>.Get(id).duration > 0 && TitleManager.Instance.OwnTitleMap.ContainsKey(id))
		{
			int remainTime = TitleManager.Instance.GetTitleInfoById(id).remainTime;
			if (remainTime > 0)
			{
				string text3 = string.Empty;
				if (remainTime > 86400)
				{
					text3 = remainTime / 86400 + "天";
				}
				else if (remainTime > 3600)
				{
					text3 = remainTime / 3600 + "小时";
				}
				else if (remainTime > 60)
				{
					text3 = remainTime / 60 + "分钟";
				}
				else if (remainTime > 0)
				{
					text3 = 1 + "分\u3000钟";
				}
				if (!text3.Equals(string.Empty))
				{
					gameObject.SetActive(true);
					gameObject.get_transform().Find("LimitTimeDesc").GetComponent<Text>().set_text(text3);
				}
				else
				{
					gameObject.SetActive(false);
				}
			}
		}
		else
		{
			gameObject.SetActive(false);
		}
		GameObject btn = base.FindTransform("BtnComfirm").get_gameObject();
		if (TitleManager.Instance.Contain(id) && id != TitleManager.Instance.OwnCurrId)
		{
			btn.SetActive(true);
			btn.GetComponent<ButtonCustom>().onClickCustom = delegate(GameObject v)
			{
				btn.SetActive(false);
			};
		}
		else
		{
			btn.SetActive(false);
		}
	}
}
