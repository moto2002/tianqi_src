using LitJson;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class SDKManager : MonoBehaviour
{
	private const string YSDK_ORDER_RECORD = "YSDK_ORDER_RECORD";

	private const string PAY_CALLBACK_ORDER_ID = "orderId";

	private const string PAY_CALLBACK_ORDER_AMOUNT = "orderAmount";

	private const string PAY_CALLBACK_PAY_WAY_ID = "payWayId";

	private const string PAY_CALLBACK_PAY_WAY_NAME = "payWayName";

	public static bool IsCanChangeSDKType;

	public static int TestSDKType;

	private static SDKManager instance;

	private Dictionary<string, OnSDKResultCallback> m_callbacks = new Dictionary<string, OnSDKResultCallback>();

	private bool IsFirstInited;

	public static string key_order_productId = "productId";

	public static string key_order_productName = "productName";

	public static string key_order_productUnit = "productUnit";

	public static string key_order_price = "price";

	public static string key_order_balance = "balance";

	public static string key_order_vip = "vip";

	public static string key_order_level = "level";

	public static string key_order_roleName = "roleName";

	public static string key_order_roleId = "roleId";

	public static string key_order_serverId = "serverId";

	public static string key_order_serverName = "serverName";

	public static string key_order_orderTime = "orderTime";

	public static string key_order_createRoleTime = "createRoleTime";

	public static string key_order_roleProfessionId = "professionid";

	public static string key_order_roleProfessionName = "profession";

	public static string key_order_roleGender = "gender";

	public static string key_order_rolePower = "power";

	public static string key_order_guildName = "guildName";

	public static string key_order_guildId = "partyid";

	public static string key_order_guildTitleId = "partyroleid";

	public static string key_order_guildTitleName = "partyrolename";

	private string m_productId = string.Empty;

	private string m_productName = string.Empty;

	private double m_price;

	public static string SubmitTypeLogin = "login_ret";

	public static string SubmitTypeRegister = "register_ret";

	public static string SubmitTypeEnterGameServer = "enterGameServer_ret";

	public static string SubmitTypeLevelup = "levelup_ret";

	public static string SubmitTypeCheckBalanceOnPay = "balanceOnPay_ret";

	public static string SubmitTypeOpenKF = "openkf_ret";

	public static string SubmitTypeCloseKF = "closekf_ret";

	public static string SubmitTypeUserCenter = "usercenter_rect";

	public static string SubmitTypeURLToPHP = "url_to_php";

	private static int mCreateTime;

	public static SDKManager Instance
	{
		get
		{
			if (SDKManager.instance == null)
			{
				SDKManager.instance = new GameObject("SDKManager").AddComponent<SDKManager>();
				Object.DontDestroyOnLoad(SDKManager.instance);
			}
			return SDKManager.instance;
		}
	}

	public void Init()
	{
		if (this.IsFirstInited)
		{
			return;
		}
		this.IsFirstInited = true;
		this.AddListener();
	}

	private void AddListener()
	{
		this.RegisterCallback("exitSDK", new OnSDKResultCallback(this.OnLogoutResp));
		this.RegisterCallback("applicationQuit", new OnSDKResultCallback(this.OnApplicationQuitResp));
		this.RegisterCallback("pay", new OnSDKResultCallback(this.OnPayToRechargeDiamondResp));
		NetworkManager.AddListenEvent<RechargeSuccessNty>(new NetCallBackMethod<RechargeSuccessNty>(this.OnRechargeSuccessNty));
	}

	private void OnRechargeSuccessNty(short state, RechargeSuccessNty down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			if (down.item.get_Count() > 0)
			{
				string text = string.Empty;
				for (int i = 0; i < down.item.get_Count(); i++)
				{
					if (i > 0)
					{
						text += ", ";
					}
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						GameDataUtils.GetItemName(down.item.get_Item(i).itemId, true, 0L),
						"x",
						down.item.get_Item(i).count
					});
				}
				DialogBoxUIViewModel.Instance.ShowAsConfirm("充值成功", string.Format(down.info, text), null, "确定", "button_orange_1", null);
			}
			else
			{
				UIManagerControl.Instance.ShowToastText("激活成功");
			}
		}
	}

	public int GetSDKType()
	{
		return NativeCallManager.GetSDKType();
	}

	public string GetSDKName(int sdkType)
	{
		string empty = string.Empty;
		if (SDKConstants.sdkNameMap.TryGetValue(sdkType, ref empty))
		{
			return empty;
		}
		Debug.LogError("获取渠道名[" + sdkType + "]失败!!!");
		return null;
	}

	public string GetSDKName()
	{
		if (SDKManager.IsCanChangeSDKType)
		{
			return this.GetSDKName(SDKManager.TestSDKType);
		}
		return this.GetSDKName(NativeCallManager.GetSDKType());
	}

	public bool HasSDK()
	{
		int sDKType = NativeCallManager.GetSDKType();
		return sDKType != 27 && sDKType < 10000 && sDKType > 0;
	}

	public bool IsAndroidYSDK()
	{
		int sDKType = NativeCallManager.GetSDKType();
		return sDKType == 23 || sDKType == 25;
	}

	public bool IsIOSYSDK()
	{
		int sDKType = NativeCallManager.GetSDKType();
		return sDKType == 24 || sDKType == 26;
	}

	public bool IsUserCenterOn()
	{
		return this.IsAndroidYSDK() || this.GetSDKType() == 59;
	}

	public bool IsLogin()
	{
		return !this.HasSDK() || NativeCallManager.GetIsLogin();
	}

	public void TryLogin()
	{
		if (this.HasSDK())
		{
			if (this.IsAndroidYSDK())
			{
				this.Login(new OnSDKResultCallback(LoginManager.Instance.OnGetLoginResp), 0);
				if (!this.IsLogin() && LoginPanel.Instance != null)
				{
					LoginPanel.Instance.ShowLoginSDK(true);
				}
			}
			else
			{
				this.Login(new OnSDKResultCallback(LoginManager.Instance.OnGetLoginResp), 0);
			}
		}
	}

	public void Login(OnSDKResultCallback callback, int type)
	{
		this.RegisterCallback("login", callback);
		NativeCallManager.CallSdkApi("login", new object[]
		{
			type
		});
	}

	public void Logout()
	{
		NativeCallManager.CallSdkApi("exitSDK", new object[0]);
	}

	private void OnLogoutResp(SDKStatusCode code, string data)
	{
		if (UIManagerControl.Instance.IsOpen("LoginUI"))
		{
			LoginPanel.Instance.ShowLoginSDK(true);
		}
		else if (ClientApp.Instance != null)
		{
			ClientApp.Instance.ReInit();
		}
	}

	public bool ApplicationQuit()
	{
		return NativeCallManager.ApplicationQuitSDK();
	}

	public void OnApplicationQuitResp(SDKStatusCode code, string data)
	{
		ClientApp.QuitApp();
	}

	public void Pay(string productId, string productName, double price)
	{
		string order = SDKManager.GetOrder(productId, productName, price);
		NativeCallManager.CallSdkApi("pay", new object[]
		{
			order
		});
	}

	public void OnPayToRechargeDiamondResp(SDKStatusCode code, string data)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnPayToRechargeDiamondResp : code = ",
			code,
			", data = ",
			data
		}));
		WaitUI.CloseUI(0u);
		if (SDKManager.Instance.IsAndroidYSDK())
		{
			this.OnPayToRechargeDiamondResp_YSDK(code, data);
		}
		else if (code != SDKStatusCode.SUCCESS)
		{
		}
	}

	private void OnPayToRechargeDiamondResp_YSDK(SDKStatusCode code, string data)
	{
		if (code == (SDKStatusCode)1)
		{
			DialogBoxUIViewModel.Instance.ShowAsConfirm("提示", "服务器没有响应,请稍后再试", null, "确定", "button_orange_1", null);
		}
		else if (code == (SDKStatusCode)2)
		{
			this.YSDK_CheckBalanceSuccess(data);
		}
		else if (code == (SDKStatusCode)3)
		{
			Debug.Log("YSDK: 记录订单");
			JsonData jsonData = JsonMapper.ToObject(data);
			string value = (string)jsonData["orderId"];
			PlayerPrefsExt.SetStringPrefs("YSDK_ORDER_RECORD", value);
		}
		else if (code == (SDKStatusCode)4)
		{
			Debug.Log("YSDK: 移除订单记录");
			PlayerPrefsExt.SetStringPrefs("YSDK_ORDER_RECORD", string.Empty);
		}
	}

	private void YSDK_CheckBalanceSuccess(string data)
	{
		JsonData jsonData = JsonMapper.ToObject(data);
		int num = (int)jsonData["payWayId"];
		if (num < 0)
		{
			num = 0;
		}
		if (num == 0)
		{
			Debug.Log("OnPayToRechargeDiamondResp:SDK余额为0");
			this.Pay(this.m_productId, this.m_productName, this.m_price);
		}
		else
		{
			int num2 = (int)(this.m_price * 10.0);
			int num3 = num2 - num;
			if (num3 > 0)
			{
				Debug.Log("OnPayToRechargeDiamondResp:SDK余额不足");
				double rmbNow = (double)num3 / 10.0;
				string content = string.Format("你的账号余额不足,还需要充值{0}元\n您是否充值?", rmbNow.ToString("f1"));
				DialogBoxUIViewModel.Instance.ShowAsOKCancel("支付提示", content, null, delegate
				{
					this.Pay(this.m_productId, this.m_productName, rmbNow);
				}, "取消", "确定", "button_orange_1", "button_yellow_1", null, true, true);
			}
			else
			{
				Debug.Log("OnPayToRechargeDiamondResp:SDK余额足够");
				string content2 = string.Format("是否消费{0}钻石", num2);
				DialogBoxUIViewModel.Instance.ShowAsOKCancel("消费提示", content2, null, delegate
				{
					this.Pay(this.m_productId, this.m_productName, 0.0);
				}, "取消", "确定", "button_orange_1", "button_yellow_1", null, true, true);
			}
		}
	}

	public void TrySendCacheOrderToPHP()
	{
		if (SDKManager.Instance.IsAndroidYSDK())
		{
			string stringPrefs = PlayerPrefsExt.GetStringPrefs("YSDK_ORDER_RECORD");
			Debug.Log("TrySendCacheOrderToPHP: URL = " + stringPrefs);
			if (string.IsNullOrEmpty(stringPrefs))
			{
				return;
			}
			this.SubmitExtendData(null, SDKManager.SubmitTypeURLToPHP, stringPrefs);
			PlayerPrefsExt.SetStringPrefs("YSDK_ORDER_RECORD", string.Empty);
		}
	}

	public void CheckBalanceOnPay(string productId, string productName, double price)
	{
		if (this.HasSDK())
		{
			this.m_productId = productId;
			this.m_productName = productName;
			this.m_price = price;
			string order = SDKManager.GetOrder(productId, productName, price);
			this.SubmitExtendData(null, SDKManager.SubmitTypeCheckBalanceOnPay, order);
		}
	}

	private static string GetOrder(string productId, string productName, double price)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.set_Item(SDKManager.key_order_productId, productId);
		hashtable.set_Item(SDKManager.key_order_productName, productName);
		hashtable.set_Item(SDKManager.key_order_productUnit, "钻石");
		hashtable.set_Item(SDKManager.key_order_price, price);
		if (EntityWorld.Instance.EntSelf != null)
		{
			hashtable.set_Item(SDKManager.key_order_balance, EntityWorld.Instance.EntSelf.Diamond);
			hashtable.set_Item(SDKManager.key_order_vip, "VIP" + EntityWorld.Instance.EntSelf.VipLv);
			hashtable.set_Item(SDKManager.key_order_level, EntityWorld.Instance.EntSelf.Lv);
			hashtable.set_Item(SDKManager.key_order_guildName, GuildManager.Instance.GetGuildName());
			hashtable.set_Item(SDKManager.key_order_roleName, EntityWorld.Instance.EntSelf.Name);
			hashtable.set_Item(SDKManager.key_order_roleId, EntityWorld.Instance.EntSelf.ID.ToString());
			hashtable.set_Item(SDKManager.key_order_serverId, LoginManager.Instance.GetCurrentServerId().ToString());
			hashtable.set_Item(SDKManager.key_order_serverName, LoginManager.Instance.GetCurrentServerName());
			hashtable.set_Item(SDKManager.key_order_orderTime, DateTime.get_Now().ToString("yyyyMMddHHmmss"));
		}
		else
		{
			hashtable.set_Item(SDKManager.key_order_balance, "2000");
			hashtable.set_Item(SDKManager.key_order_vip, "VIP0");
			hashtable.set_Item(SDKManager.key_order_level, "20");
			hashtable.set_Item(SDKManager.key_order_guildName, "军团");
			hashtable.set_Item(SDKManager.key_order_roleName, "角色名称");
			hashtable.set_Item(SDKManager.key_order_roleId, "角色id");
			hashtable.set_Item(SDKManager.key_order_serverId, "1");
			hashtable.set_Item(SDKManager.key_order_serverName, "所在服务器");
			hashtable.set_Item(SDKManager.key_order_orderTime, DateTime.get_Now().ToString("yyyyMMddHHmmss"));
		}
		return JsonMapper.ToJson(hashtable);
	}

	public void SubmitExtendData(OnSDKResultCallback callback, string type, string data)
	{
		NativeCallManager.CallSdkApi("submitExtendData", new object[]
		{
			type,
			data
		});
	}

	public static string GetSubmitData()
	{
		Hashtable hashtable = new Hashtable();
		if (EntityWorld.Instance.EntSelf != null)
		{
			hashtable.set_Item(SDKManager.key_order_balance, EntityWorld.Instance.EntSelf.Diamond);
			hashtable.set_Item(SDKManager.key_order_vip, "VIP" + EntityWorld.Instance.EntSelf.VipLv);
			hashtable.set_Item(SDKManager.key_order_level, EntityWorld.Instance.EntSelf.Lv);
			hashtable.set_Item(SDKManager.key_order_guildName, (!(GuildManager.Instance.GetGuildName() != string.Empty)) ? "无" : GuildManager.Instance.GetGuildName());
			hashtable.set_Item(SDKManager.key_order_roleName, (EntityWorld.Instance.EntSelf.Name == null) ? "无" : EntityWorld.Instance.EntSelf.Name);
			hashtable.set_Item(SDKManager.key_order_roleId, EntityWorld.Instance.EntSelf.ID.ToString());
			hashtable.set_Item(SDKManager.key_order_serverId, LoginManager.Instance.GetCurrentServerId().ToString());
			hashtable.set_Item(SDKManager.key_order_serverName, LoginManager.Instance.GetCurrentServerName());
			if (SDKManager.mCreateTime > 0)
			{
				hashtable.set_Item(SDKManager.key_order_createRoleTime, SDKManager.mCreateTime.ToString());
			}
			hashtable.set_Item(SDKManager.key_order_guildId, GuildManager.Instance.GetGuildId().ToString());
			hashtable.set_Item(SDKManager.key_order_guildTitleId, GuildManager.Instance.GetGuildSDKTitleID().ToString());
			hashtable.set_Item(SDKManager.key_order_guildTitleName, GuildManager.Instance.GetGuilSDKdTitleName());
			hashtable.set_Item(SDKManager.key_order_roleProfessionId, (EntityWorld.Instance.EntSelf.TypeID <= 0) ? "0" : EntityWorld.Instance.EntSelf.TypeID.ToString());
			hashtable.set_Item(SDKManager.key_order_roleProfessionName, (!(UIUtils.GetRoleName(EntityWorld.Instance.EntSelf.TypeID) != string.Empty)) ? "无" : UIUtils.GetRoleName(EntityWorld.Instance.EntSelf.TypeID));
			hashtable.set_Item(SDKManager.key_order_roleGender, "无");
			hashtable.set_Item(SDKManager.key_order_rolePower, EntityWorld.Instance.EntSelf.Fighting.ToString());
		}
		else
		{
			hashtable.set_Item(SDKManager.key_order_balance, "2000");
			hashtable.set_Item(SDKManager.key_order_vip, "VIP0");
			hashtable.set_Item(SDKManager.key_order_level, "20");
			hashtable.set_Item(SDKManager.key_order_guildName, "军团");
			hashtable.set_Item(SDKManager.key_order_roleName, "角色名称");
			hashtable.set_Item(SDKManager.key_order_roleId, "角色id");
			hashtable.set_Item(SDKManager.key_order_serverId, "1");
			hashtable.set_Item(SDKManager.key_order_serverName, "所在服务器");
			hashtable.set_Item(SDKManager.key_order_guildId, "0");
			hashtable.set_Item(SDKManager.key_order_guildTitleId, "0");
			hashtable.set_Item(SDKManager.key_order_guildTitleName, "无");
		}
		return JsonMapper.ToJson(hashtable);
	}

	public static string GetSubmitData(RoleInfo roleInfo, int createTime = 0)
	{
		if (createTime > 0)
		{
			SDKManager.mCreateTime = createTime;
		}
		Hashtable hashtable = new Hashtable();
		hashtable.set_Item(SDKManager.key_order_balance, roleInfo.cityInfo.Diamond);
		hashtable.set_Item(SDKManager.key_order_level, roleInfo.lv);
		hashtable.set_Item(SDKManager.key_order_roleId, roleInfo.roleId);
		hashtable.set_Item(SDKManager.key_order_roleName, roleInfo.roleName);
		hashtable.set_Item(SDKManager.key_order_serverId, LoginManager.Instance.GetCurrentServerId().ToString());
		hashtable.set_Item(SDKManager.key_order_serverName, LoginManager.Instance.GetCurrentServerName());
		hashtable.set_Item(SDKManager.key_order_createRoleTime, SDKManager.mCreateTime.ToString());
		hashtable.set_Item(SDKManager.key_order_vip, "VIP" + roleInfo.cityInfo.baseInfo.simpleInfo.VipLv);
		return JsonMapper.ToJson(hashtable);
	}

	public static string GetSubmitData(RoleBriefInfo roleInfo)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.set_Item(SDKManager.key_order_balance, 0);
		hashtable.set_Item(SDKManager.key_order_level, roleInfo.lv);
		hashtable.set_Item(SDKManager.key_order_roleId, roleInfo.roleId);
		hashtable.set_Item(SDKManager.key_order_roleName, roleInfo.roleName);
		hashtable.set_Item(SDKManager.key_order_serverId, LoginManager.Instance.GetCurrentServerId().ToString());
		hashtable.set_Item(SDKManager.key_order_serverName, LoginManager.Instance.GetCurrentServerName());
		hashtable.set_Item(SDKManager.key_order_vip, "VIP0");
		return JsonMapper.ToJson(hashtable);
	}

	public void OpenUserCenter()
	{
		this.SubmitExtendData(null, SDKManager.SubmitTypeUserCenter, string.Empty);
	}

	public void RegisterCallback(string tag, OnSDKResultCallback callback)
	{
		this.m_callbacks.set_Item(tag, callback);
	}

	public void OnSDKCallback(string jsonstr)
	{
		Debug.Log("OnSDKCallback message: jsonstr= " + jsonstr);
		JsonData jsonData = JsonMapper.ToObject(jsonstr);
		string text = (string)jsonData["callbackType"];
		int code = (int)jsonData["code"];
		JsonData data = jsonData["data"];
		if (this.m_callbacks.ContainsKey(text) && this.m_callbacks.get_Item(text) != null)
		{
			this.m_callbacks.get_Item(text)((SDKStatusCode)code, (string)data);
		}
	}

	public void OpenKF()
	{
		this.SubmitExtendData(null, SDKManager.SubmitTypeOpenKF, SDKManager.GetSubmitData());
	}

	public void CloseKF()
	{
		this.SubmitExtendData(null, SDKManager.SubmitTypeCloseKF, SDKManager.GetSubmitData());
	}
}
