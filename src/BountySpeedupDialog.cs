using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BountySpeedupDialog : UIBase
{
	private const string countDownText = "{0}:{1}:{2}";

	public Text Num;

	public Image CoinIcon;

	public Image Background;

	public Image Quality;

	public Text Name;

	public Text countdown;

	public ButtonCustom SpeedupButton;

	private ulong uid;

	private DateTime time;

	private int quickenCost;

	private int quickenTime;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	private void Start()
	{
		this.SpeedupButton.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSpeedup);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener("TimeManagerEvent.UnscaleSecondPast", new Callback(this.OnSecondsPast));
	}

	public void UpdateData(ProductionInfo info, DateTime time)
	{
		this.time = time;
		this.uid = info.uId;
		this.quickenCost = DataReader<XuanShangSheZhi>.Get("quickenCost").num;
		this.quickenTime = DataReader<XuanShangSheZhi>.Get("quickenTime").num;
		ShengChanJiDi shengChanJiDi = DataReader<ShengChanJiDi>.Get(info.typeId);
		ResourceManager.SetSprite(this.CoinIcon, GameDataUtils.GetIcon(DataReader<Items>.Get(DataReader<XuanShangSheZhi>.Get("quickenCoinType").num).littleIcon));
		ResourceManager.SetSprite(this.Background, GameDataUtils.GetIcon(shengChanJiDi.baseicon));
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
		this.OnSecondsPast();
	}

	public void OnSecondsPast()
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
			this.Num.set_text("x" + ((int)(1.0 * (double)num * (double)this.quickenCost / (double)this.quickenTime + 0.99999)).ToString());
		}
		else
		{
			this.countdown.set_text(string.Format("{0}:{1}:{2}", "00", "00", "00"));
		}
	}

	private void OnClickSpeedup(GameObject go)
	{
		DialogBoxUIViewModel.Instance.ShowAsOKCancel_as(GameDataUtils.GetChineseContent(513638, false), string.Format(GameDataUtils.GetChineseContent(513639, false), this.Num.get_text(), this.Name.get_text()), null, null, delegate
		{
			NetworkManager.Send(new BountyAccelerateBoxOpenReq
			{
				uId = this.uid
			}, ServerType.Data);
		}, GameDataUtils.GetChineseContent(500012, false), GameDataUtils.GetChineseContent(500011, false), "button_orange_1", "button_orange_1", UINodesManager.MiddleUIRoot);
		this.Show(false);
	}
}
