using GameData;
using Package;
using System;
using XNetwork;

public class EnergyManager : BaseSubSystemManager
{
	public int maxBuyTimes;

	public int currentBuyTimes;

	protected Action m_buySuccessCallback;

	private static EnergyManager m_Instance;

	public static EnergyManager Instance
	{
		get
		{
			if (EnergyManager.m_Instance == null)
			{
				EnergyManager.m_Instance = new EnergyManager();
			}
			return EnergyManager.m_Instance;
		}
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		NetworkManager.AddListenEvent<EnergyLoginPush>(new NetCallBackMethod<EnergyLoginPush>(this.OnGetEnergyLoginPush));
		NetworkManager.AddListenEvent<EnergyChangedNty>(new NetCallBackMethod<EnergyChangedNty>(this.OnGetEnergyChangedNty));
		NetworkManager.AddListenEvent<BuyEnergyRes>(new NetCallBackMethod<BuyEnergyRes>(this.OnGetBuyEnergyRes));
	}

	public void SendBuyEnergyReq()
	{
		NetworkManager.Send(typeof(BuyEnergyReq), null, ServerType.Data);
	}

	private void OnGetEnergyLoginPush(short state, EnergyLoginPush down)
	{
		if (state == 0)
		{
			if (down != null)
			{
				this.maxBuyTimes = down.buyInfo.maxBuyTimes;
				this.currentBuyTimes = down.buyInfo.buyTimes;
				Debuger.Error(string.Concat(new object[]
				{
					"OnGetEnergyLoginPush  down.buyInfo.maxBuyTimes  ",
					down.buyInfo.maxBuyTimes,
					"  down.buyInfo.buyTimes  ",
					down.buyInfo.buyTimes
				}), new object[0]);
			}
			else
			{
				Debuger.Error("down == null OnGetEnergyLoginPush", new object[0]);
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetEnergyChangedNty(short state, EnergyChangedNty down)
	{
		if (state == 0)
		{
			if (down != null)
			{
				this.maxBuyTimes = down.buyInfo.maxBuyTimes;
				this.currentBuyTimes = down.buyInfo.buyTimes;
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	private void OnGetBuyEnergyRes(short state, BuyEnergyRes down)
	{
		if (state == 0)
		{
			if (this.m_buySuccessCallback != null)
			{
				this.m_buySuccessCallback.Invoke();
				this.m_buySuccessCallback = null;
			}
		}
		else
		{
			StateManager.Instance.StateShow(state, 0);
		}
	}

	public void BuyEnergy(Action buySuccessCallback)
	{
		this.m_buySuccessCallback = buySuccessCallback;
		int vipTimesByType = VIPPrivilegeManager.Instance.GetVipTimesByType(1);
		int num = EnergyManager.Instance.currentBuyTimes + 1;
		if (num >= vipTimesByType)
		{
			num = vipTimesByType;
		}
		Energy energy = DataReader<Energy>.DataList.get_Item(0);
		int count = DataReader<TiLiGouMai>.DataList.get_Count();
		TiLiGouMai tiLiGouMai;
		if (num >= count)
		{
			tiLiGouMai = DataReader<TiLiGouMai>.DataList.get_Item(DataReader<TiLiGouMai>.DataList.get_Count() - 1);
		}
		else
		{
			tiLiGouMai = DataReader<TiLiGouMai>.Get(num);
		}
		string text = GameDataUtils.GetChineseContent(500028, false);
		text = text.Replace("x{0}", tiLiGouMai.needDiamond.ToString());
		text = text.Replace("x{1}", energy.buyEnergyVal.ToString());
		text = text.Replace("x{2}", (EnergyManager.Instance.maxBuyTimes - EnergyManager.Instance.currentBuyTimes).ToString());
		text = text.Replace("x{3}", EnergyManager.Instance.maxBuyTimes.ToString());
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(500027, false), text, delegate
		{
		}, delegate
		{
			EnergyManager.Instance.SendBuyEnergyReq();
		}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
	}
}
