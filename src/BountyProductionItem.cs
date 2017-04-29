using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BountyProductionItem : BaseUIBehaviour
{
	private const string countDownText = "{0}:{1}:{2}";

	public Text Num;

	public Image consumeIcon;

	public Image Icon;

	public Image Quality;

	public Text Name;

	public Text countdown;

	public GameObject GetRewardTips;

	public GameObject consume;

	private ulong uid;

	private DateTime time;

	private int quickenCost;

	private int quickenTime;

	public ButtonCustom ButtonSpeedUp;

	private ProductionInfo Info;

	private void Start()
	{
		this.ButtonSpeedUp.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSpeedup);
	}

	private void OnEnable()
	{
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.RefreshCountdown));
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.RefreshCountdown));
	}

	public void UpdateData(ProductionInfo info)
	{
		this.Info = info;
		this.uid = info.uId;
		this.time = BountyManager.Instance.ProductionTimeMap.get_Item(this.uid);
		this.quickenCost = DataReader<XuanShangSheZhi>.Get("quickenCost").num;
		this.quickenTime = DataReader<XuanShangSheZhi>.Get("quickenTime").num;
		ShengChanJiDi shengChanJiDi = DataReader<ShengChanJiDi>.Get(info.typeId);
		ResourceManager.SetSprite(this.consumeIcon, GameDataUtils.GetIcon(DataReader<Items>.Get(DataReader<XuanShangSheZhi>.Get("quickenCoinType").num).littleIcon));
		ResourceManager.SetSprite(this.Icon, GameDataUtils.GetIcon(shengChanJiDi.baseicon));
		ResourceManager.SetSprite(this.Quality, GameDataUtils.GetIcon(5050 + shengChanJiDi.baseQuality));
		this.Name.set_text(shengChanJiDi.baseName);
		Debug.LogError(string.Concat(new object[]
		{
			"地唯一id:",
			info.uId,
			",配置id:",
			info.typeId,
			"==========配置的名字：",
			shengChanJiDi.baseName,
			"，配置图片:",
			5050 + shengChanJiDi.baseQuality,
			"===",
			GameDataUtils.GetIcon(5050 + shengChanJiDi.baseQuality)
		}));
		this.RefreshCountdown();
	}

	public void RefreshCountdown()
	{
		TimeSpan timeSpan = this.time - TimeManager.Instance.PreciseServerTime;
		if (this.time >= TimeManager.Instance.PreciseServerTime)
		{
			this.countdown.set_text(Convert.ToDateTime(timeSpan.ToString()).ToString("HH:mm:ss"));
			int num = (int)timeSpan.get_TotalMinutes();
			if (timeSpan.get_Seconds() > 0)
			{
				num++;
			}
			this.Num.set_text(((int)(1.0 * (double)num * (double)this.quickenCost / (double)this.quickenTime + 0.99999)).ToString());
		}
		else
		{
			this.countdown.set_text(string.Format("{0}:{1}:{2}", "00", "00", "00"));
		}
		bool flag = this.time <= TimeManager.Instance.PreciseServerTime;
		this.GetRewardTips.SetActive(flag);
		this.consume.SetActive(!flag);
		this.countdown.get_transform().get_parent().get_gameObject().SetActive(!flag);
	}

	private void OnClickSpeedup(GameObject go)
	{
		if (this.time > TimeManager.Instance.PreciseServerTime)
		{
			(UIManagerControl.Instance.OpenUI("BountySpeedupDialog", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as BountySpeedupDialog).UpdateData(this.Info, this.time);
		}
		else if (!BackpackManager.Instance.ShowBackpackFull())
		{
			NetworkManager.Send(new BountyTaskOpenBoxReq
			{
				uId = this.uid
			}, ServerType.Data);
		}
	}
}
