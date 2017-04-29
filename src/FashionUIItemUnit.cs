using Foundation.Core.Databinding;
using GameData;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FashionUIItemUnit : BaseUIBehaviour
{
	protected Image FashionUIItemUnitItemFrame;

	protected Image FashionUIItemUnitItemIcon;

	protected Text FashionUIItemUnitName;

	protected Text FashionUIItemUnitLevel;

	protected Text FashionUIItemUnitTimeLimit;

	protected GameObject FashionUIItemUnitItemStateOwn;

	protected GameObject FashionUIItemUnitItemStateExpired;

	protected GameObject FashionUIItemUnitItemStateWearing;

	protected GameObject FashionUIItemUnitMask;

	protected int highLightFxID;

	protected string fashionDataID;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.FashionUIItemUnitItemFrame = base.FindTransform("FashionUIItemUnitItemFrame").GetComponent<Image>();
		this.FashionUIItemUnitItemIcon = base.FindTransform("FashionUIItemUnitItemIcon").GetComponent<Image>();
		this.FashionUIItemUnitName = base.FindTransform("FashionUIItemUnitName").GetComponent<Text>();
		this.FashionUIItemUnitLevel = base.FindTransform("FashionUIItemUnitLevel").GetComponent<Text>();
		this.FashionUIItemUnitTimeLimit = base.FindTransform("FashionUIItemUnitTimeLimit").GetComponent<Text>();
		this.FashionUIItemUnitItemStateOwn = base.FindTransform("FashionUIItemUnitItemStateOwn").get_gameObject();
		this.FashionUIItemUnitItemStateExpired = base.FindTransform("FashionUIItemUnitItemStateExpired").get_gameObject();
		this.FashionUIItemUnitItemStateWearing = base.FindTransform("FashionUIItemUnitItemStateWearing").get_gameObject();
		this.FashionUIItemUnitMask = base.FindTransform("FashionUIItemUnitMask").get_gameObject();
		base.GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickUnit));
	}

	public void SetData(FashionData fashionInformation, bool isShowHighLight)
	{
		this.fashionDataID = fashionInformation.dataID;
		ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(fashionInformation.dataID);
		if (DataReader<Items>.Contains(shiZhuangXiTong.itemsID))
		{
			Items items = DataReader<Items>.Get(shiZhuangXiTong.itemsID);
			this.SetIcon(items);
			this.SetName(GameDataUtils.GetChineseContent(items.name, false));
			this.SetLevel(GameDataUtils.GetChineseContent(items.describeId2, false));
			this.SetTimeLimit(this.GetTimeStringByTimeout(fashionInformation.state, fashionInformation.time));
			this.SetState(fashionInformation.state);
			this.SetMask(this.GetIsMaskOn(fashionInformation.state));
		}
		else
		{
			this.SetName(string.Empty);
			this.SetLevel(string.Empty);
			this.SetTimeLimit(string.Empty);
			this.SetState(FashionData.FashionDataState.None);
			this.SetMask(true);
		}
		if (isShowHighLight)
		{
			if (this.highLightFxID == 0)
			{
				this.highLightFxID = FXSpineManager.Instance.PlaySpine(603, base.get_transform(), "FashionUI", 3010, null, "UI", 0f, -3f, 1.1f, 1.2f, true, FXMaskLayer.MaskState.None);
			}
		}
		else
		{
			FXSpineManager.Instance.DeleteSpine(this.highLightFxID, true);
			this.highLightFxID = 0;
		}
	}

	protected string GetFashionAttr(int attrID)
	{
		if (!DataReader<Attrs>.Contains(attrID))
		{
			return string.Empty;
		}
		Attrs attrs = DataReader<Attrs>.Get(attrID);
		int num = (attrs.attrs.get_Count() >= attrs.values.get_Count()) ? attrs.values.get_Count() : attrs.attrs.get_Count();
		if (num == 0)
		{
			return string.Empty;
		}
		XDict<int, long> xDict = new XDict<int, long>();
		for (int i = 0; i < num; i++)
		{
			if (xDict.ContainsKey(attrs.attrs.get_Item(i)))
			{
				XDict<int, long> xDict2;
				XDict<int, long> expr_80 = xDict2 = xDict;
				int key;
				int expr_8F = key = attrs.attrs.get_Item(i);
				long num2 = xDict2[key];
				expr_80[expr_8F] = num2 + (long)attrs.values.get_Item(i);
			}
			else
			{
				xDict.Add(attrs.attrs.get_Item(i), (long)attrs.values.get_Item(i));
			}
		}
		if (xDict.Count == 0)
		{
			return string.Empty;
		}
		return AttrUtility.GetStandardAddDesc(xDict.Keys.get_Item(0), xDict.Values.get_Item(0));
	}

	protected void SetIcon(Items fashionItemData)
	{
		ResourceManager.SetSprite(this.FashionUIItemUnitItemFrame, GameDataUtils.GetItemFrame(fashionItemData.id));
		ResourceManager.SetSprite(this.FashionUIItemUnitItemIcon, GameDataUtils.GetIcon(fashionItemData.icon));
	}

	protected void SetName(string name)
	{
		this.FashionUIItemUnitName.set_text(name);
	}

	protected void SetLevel(string text)
	{
		this.FashionUIItemUnitLevel.set_text(text);
	}

	protected void SetTimeLimit(string text)
	{
		this.FashionUIItemUnitTimeLimit.set_text(text);
	}

	protected string GetTimeStringByTimeout(FashionData.FashionDataState fashionDataState, int time)
	{
		if (fashionDataState == FashionData.FashionDataState.Own && time == -1)
		{
			return GameDataUtils.GetChineseContent(1005016, false);
		}
		if (fashionDataState == FashionData.FashionDataState.Expired)
		{
			return string.Empty;
		}
		if (time > 0)
		{
			int num = time - TimeManager.Instance.PreciseServerSecond;
			if (num < 0)
			{
				num = 0;
			}
			return this.GetTimeStringByTime(num);
		}
		return string.Empty;
	}

	protected string GetTimeStringByTime(int deltaTime)
	{
		TimeSpan timeSpan = new TimeSpan(0, 0, deltaTime);
		if (timeSpan.get_Days() > 0)
		{
			return string.Format(GameDataUtils.GetChineseContent(1005005, false), timeSpan.get_Days(), timeSpan.get_Hours());
		}
		if (timeSpan.get_Hours() > 0)
		{
			return string.Format(GameDataUtils.GetChineseContent(1005006, false), timeSpan.get_Hours());
		}
		if (timeSpan.get_Minutes() > 0)
		{
			return string.Format(GameDataUtils.GetChineseContent(1005017, false), timeSpan.get_Minutes());
		}
		return string.Format(GameDataUtils.GetChineseContent(1005017, false), 0);
	}

	protected void SetState(FashionData.FashionDataState state)
	{
		switch (state)
		{
		case FashionData.FashionDataState.Own:
			this.FashionUIItemUnitItemStateOwn.SetActive(false);
			this.FashionUIItemUnitItemStateExpired.SetActive(false);
			this.FashionUIItemUnitItemStateWearing.SetActive(false);
			break;
		case FashionData.FashionDataState.Expired:
			this.FashionUIItemUnitItemStateOwn.SetActive(false);
			this.FashionUIItemUnitItemStateExpired.SetActive(true);
			this.FashionUIItemUnitItemStateWearing.SetActive(false);
			break;
		case FashionData.FashionDataState.Dressing:
			this.FashionUIItemUnitItemStateOwn.SetActive(false);
			this.FashionUIItemUnitItemStateExpired.SetActive(false);
			this.FashionUIItemUnitItemStateWearing.SetActive(true);
			break;
		default:
			this.FashionUIItemUnitItemStateOwn.SetActive(false);
			this.FashionUIItemUnitItemStateExpired.SetActive(false);
			this.FashionUIItemUnitItemStateWearing.SetActive(false);
			break;
		}
	}

	protected bool GetIsMaskOn(FashionData.FashionDataState fashionDataState)
	{
		return fashionDataState == FashionData.FashionDataState.None || fashionDataState == FashionData.FashionDataState.Expired;
	}

	protected void SetMask(bool isShow)
	{
		this.FashionUIItemUnitMask.SetActive(isShow);
	}

	protected void OnClickUnit()
	{
		FashionManager.Instance.OpenFashionPreviewUI(this.fashionDataID);
	}
}
