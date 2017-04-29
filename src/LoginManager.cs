using LitJson;
using LuaFramework;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using XNetwork;
using XUPorterJSON;

public class LoginManager
{
	public class ServerInfo
	{
		public enum ServerStatusType
		{
			NEW,
			HOT,
			MAINTAIN,
			FULL,
			HIDE,
			CLOSE,
			TIPS
		}

		public int serverId;

		public string host;

		public string host2;

		public int port;

		public string serverName;

		public LoginManager.ServerInfo.ServerStatusType status;

		public string desc;

		public int is_test;

		public int is_ping = 1;

		public int is_sn = 1;
	}

	public class LoginHistoryData
	{
		public string domainName;

		public int serverID;

		public string accountName;
	}

	public class LoginDomainData
	{
		public string domainName;

		public List<LoginManager.LoginServerData> serverDatas = new List<LoginManager.LoginServerData>();
	}

	public class LoginServerData
	{
		public int serverID;

		public List<string> accountNames = new List<string>();
	}

	public class SaveLoginDomainData
	{
		public string domainName;

		public List<int> serverDataIndexes;
	}

	public class SaveLoginServerData
	{
		public int serverID;

		public List<int> accountIndexes;
	}

	public enum AddressType
	{
		DOMAIN,
		CMCC,
		CUCC,
		CACHE
	}

	public const string ServerFileName = "server.json";

	public const string NoticeFileName = "notice.json";

	protected const int MaxRecentLoginHistoryDataCount = 10;

	protected const string LoginPrefsSaveKey = "LoginMessage";

	protected const string SaveRecentLoginHistoryDatasKey = "SaveRecentLoginHistoryDatasKey";

	protected const string SaveLoginDomainDataKey = "SaveLoginDomainDataKey";

	protected const string SaveLoginServerDataKey = "SaveLoginServerDataKey";

	protected const string SaveLoginAccountDataKey = "SaveLoginAccountDataKey";

	protected const int GetDataServerMaxCount = 5;

	protected const int GetDataServerInfoMaxInterval = 8000;

	protected const int GetDataServerInfoIntervalFactor = 2000;

	protected XDict<ServerType, int> getDataServerInfoCount = new XDict<ServerType, int>();

	protected XDict<ServerType, uint> getDataServerInfoTimer = new XDict<ServerType, uint>();

	public List<LoginManager.ServerInfo> serverInfoList = new List<LoginManager.ServerInfo>();

	public List<SceneServerInfo> protoServerInfoList = new List<SceneServerInfo>();

	protected LoginManager.ServerInfo currentServerInfo;

	protected string currentAccountName = string.Empty;

	protected Action loginDataSuccessCallback;

	protected Action loginDataFailedCallback;

	protected List<RoleBriefInfo> roleBriefInfoList = new List<RoleBriefInfo>();

	protected ChatServerInfo chatServerInfo;

	protected Action loginChatSuccessCallback;

	protected Action loginChatFailedCallback;

	protected List<LoginManager.LoginHistoryData> recentLoginHistoryDatas = new List<LoginManager.LoginHistoryData>();

	protected List<LoginManager.LoginDomainData> domainDatas = new List<LoginManager.LoginDomainData>();

	protected bool hasInitLocalData;

	protected bool isCreatAnimationing;

	public DateTime lastLoginChatTime;

	protected LoginManager.AddressType mAddressType;

	protected static LoginManager instance;

	public bool ExcelDataUpdated;

	private List<ClickRoleTime> m_listClickRoleTime = new List<ClickRoleTime>();

	private bool mIsEntering;

	private string m_login_callback_data = string.Empty;

	public static string m_host = "172.19.1.47";

	public static int m_port = 8000;

	public LoginManager.ServerInfo CurrentServerInfo
	{
		get
		{
			return this.currentServerInfo;
		}
		set
		{
			this.currentServerInfo = value;
		}
	}

	public string CurrentAccountName
	{
		get
		{
			return this.currentAccountName;
		}
		set
		{
			this.currentAccountName = value;
		}
	}

	public ChatServerInfo ChatServerInfo
	{
		get
		{
			return this.chatServerInfo;
		}
		protected set
		{
			this.chatServerInfo = value;
		}
	}

	public List<LoginManager.LoginHistoryData> RecentLoginHistoryDatas
	{
		get
		{
			return this.recentLoginHistoryDatas;
		}
	}

	protected bool HasInitLocalData
	{
		get
		{
			return this.hasInitLocalData;
		}
		set
		{
			this.hasInitLocalData = value;
		}
	}

	public bool IsCreatAnimationing
	{
		get
		{
			return this.isCreatAnimationing;
		}
		set
		{
			this.isCreatAnimationing = value;
		}
	}

	public bool IsAnalysisSuccess
	{
		get;
		protected set;
	}

	public static LoginManager Instance
	{
		get
		{
			if (LoginManager.instance == null)
			{
				LoginManager.instance = new LoginManager();
			}
			return LoginManager.instance;
		}
	}

	protected LoginManager()
	{
	}

	public void Init()
	{
		global::NetworkManager.AddListenEvent<GetServerListRes>(new NetCallBackMethod<GetServerListRes>(this.OnGetDataServerList));
		global::NetworkManager.AddListenEvent<LoginAuthRes>(new NetCallBackMethod<LoginAuthRes>(this.OnGetAccountDataRes));
		global::NetworkManager.AddListenEvent<CreateRoleRes>(new NetCallBackMethod<CreateRoleRes>(this.OnCreateRoleRes));
		global::NetworkManager.AddListenEvent<LoginRes>(new NetCallBackMethod<LoginRes>(this.OnLoginRoleRes));
		global::NetworkManager.AddListenEvent<ChatServerInfo>(new NetCallBackMethod<ChatServerInfo>(this.OnChatServerInfoRes));
		global::NetworkManager.AddListenEvent<ChatLoginRes>(new NetCallBackMethod<ChatLoginRes>(this.OnLoginChatRes));
		global::NetworkManager.AddListenEvent<QueueLoginNty>(new NetCallBackMethod<QueueLoginNty>(this.OnQueueLoginNty));
		global::NetworkManager.AddListenEvent<CancelLoginRes>(new NetCallBackMethod<CancelLoginRes>(this.OnCancelLoginRes));
	}

	public void Release()
	{
		global::NetworkManager.RemoveListenEvent<GetServerListRes>(new NetCallBackMethod<GetServerListRes>(this.OnGetDataServerList));
		global::NetworkManager.RemoveListenEvent<LoginAuthRes>(new NetCallBackMethod<LoginAuthRes>(this.OnGetAccountDataRes));
		global::NetworkManager.RemoveListenEvent<CreateRoleRes>(new NetCallBackMethod<CreateRoleRes>(this.OnCreateRoleRes));
		global::NetworkManager.RemoveListenEvent<LoginRes>(new NetCallBackMethod<LoginRes>(this.OnLoginRoleRes));
		global::NetworkManager.RemoveListenEvent<ChatServerInfo>(new NetCallBackMethod<ChatServerInfo>(this.OnChatServerInfoRes));
		global::NetworkManager.RemoveListenEvent<ChatLoginRes>(new NetCallBackMethod<ChatLoginRes>(this.OnLoginChatRes));
		global::NetworkManager.RemoveListenEvent<QueueLoginNty>(new NetCallBackMethod<QueueLoginNty>(this.OnQueueLoginNty));
		global::NetworkManager.RemoveListenEvent<CancelLoginRes>(new NetCallBackMethod<CancelLoginRes>(this.OnCancelLoginRes));
		this.serverInfoList.Clear();
		this.currentServerInfo = null;
		this.SetAccount(string.Empty);
		this.loginDataSuccessCallback = null;
		this.loginDataFailedCallback = null;
		this.roleBriefInfoList.Clear();
		this.isCreatAnimationing = false;
		this.chatServerInfo = null;
		this.loginChatSuccessCallback = null;
		this.loginChatFailedCallback = null;
		this.recentLoginHistoryDatas.Clear();
		this.domainDatas.Clear();
		this.hasInitLocalData = false;
	}

	public void GetServerListFile(LoginManager.AddressType type = LoginManager.AddressType.DOMAIN)
	{
		this.mIsEntering = false;
		this.IsAnalysisSuccess = false;
		string sDKName = SDKManager.Instance.GetSDKName();
		if (!string.IsNullOrEmpty(sDKName))
		{
			this.mAddressType = type;
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			list.Add(AppConst.GetRemoteFilePath(sDKName, "server.json", (int)type));
			list2.Add(Path.Combine(Util.DataPath, "server.json"));
			Downloader.Instance.Download(list, list2, null, null, null, new Action<bool>(this.UpdateServerList));
			WaitUI.OpenUI(0u);
		}
		else
		{
			NetworkDialogUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), "无法获取渠道类型，请稍后重试！", delegate
			{
				this.GetServerListFile(LoginManager.AddressType.DOMAIN);
			}, GameDataUtils.GetChineseContent(621271, false), "button_orange_1");
		}
	}

	protected void UpdateServerList(bool isSuccess)
	{
		WaitUI.CloseUI(0u);
		if (!isSuccess)
		{
			if (this.mAddressType == LoginManager.AddressType.DOMAIN)
			{
				Debug.LogError("域名下载服务器列表失败, 尝试用电信IP下载");
				this.GetServerListFile(LoginManager.AddressType.CMCC);
			}
			else if (this.mAddressType == LoginManager.AddressType.CMCC)
			{
				Debug.LogError("电信IP下载服务器列表失败, 尝试用联通IP下载");
				this.GetServerListFile(LoginManager.AddressType.CUCC);
			}
			else if (this.mAddressType == LoginManager.AddressType.CUCC)
			{
				Debug.LogError("联通IP下载服务器列表失败, 用本地缓存");
				this.mAddressType = LoginManager.AddressType.CACHE;
				this.UpdateServerList(true);
			}
			else if (this.mAddressType == LoginManager.AddressType.CACHE)
			{
				Debug.LogError("本地缓存服务器列为空, 继续重连!!!");
				NetworkDialogUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(621282, false), delegate
				{
					this.GetServerListFile(LoginManager.AddressType.DOMAIN);
				}, GameDataUtils.GetChineseContent(621271, false), "button_orange_1");
			}
		}
		else
		{
			DownloadAnnouncementFile.Instance.Down();
			this.IsAnalysisSuccess = this.AnalysisServerList();
			if (this.IsAnalysisSuccess)
			{
				ClientApp.Instance.DelayAction(1f, delegate
				{
					this.serverInfoList.Sort((LoginManager.ServerInfo a, LoginManager.ServerInfo b) => b.serverId.CompareTo(a.serverId));
					this.TrySetCurrentServer();
					this.ExcelDataUpdated = true;
				});
			}
		}
	}

	private string OpenFile(string filename)
	{
		string result = string.Empty;
		if (File.Exists(filename))
		{
			StreamReader streamReader = File.OpenText(filename);
			result = streamReader.ReadToEnd();
			streamReader.Close();
			streamReader.Dispose();
		}
		return result;
	}

	private List<string> compare(string[] newList, string[] oldList)
	{
		return Enumerable.ToList<string>(Enumerable.Except<string>(newList, oldList));
	}

	private string GetExcelDataPath(string name)
	{
		return Path.Combine(Application.get_persistentDataPath(), Path.Combine(Util.GetRuntimeFolderName(Application.get_platform()).ToLower(), "resources/data/" + name));
	}

	private void SyncServerDir(string serverUrl, string fileName, Action<bool> callback)
	{
		string localPath = this.GetExcelDataPath(fileName);
		Debug.Log(Path.Combine(serverUrl, fileName));
		string currentFiles = this.OpenFile(localPath);
		if (string.IsNullOrEmpty(serverUrl))
		{
			callback.Invoke(true);
		}
		else
		{
			serverUrl += "1.7.0";
			Downloader arg_C2_0 = Downloader.Instance;
			List<string> list = new List<string>();
			list.Add(Path.Combine(serverUrl, fileName));
			List<string> arg_C2_1 = list;
			list = new List<string>();
			list.Add(localPath);
			arg_C2_0.Download(arg_C2_1, list, null, null, null, delegate(bool isSuccess)
			{
				if (isSuccess)
				{
					string text = this.OpenFile(localPath);
					List<string> list2 = this.compare(text.Split(new char[]
					{
						'\n'
					}), currentFiles.Split(new char[]
					{
						'\n'
					}));
					List<string> list3 = new List<string>();
					List<string> list4 = new List<string>();
					List<string> list5 = new List<string>();
					List<long> list6 = new List<long>();
					for (int i = 0; i < list2.get_Count(); i++)
					{
						list3.Add(Path.Combine(serverUrl, list2.get_Item(i).Split(new char[]
						{
							';'
						})[0]));
						list5.Add(list2.get_Item(i).Split(new char[]
						{
							';'
						})[1]);
						list4.Add(this.GetExcelDataPath(list2.get_Item(i).Split(new char[]
						{
							';'
						})[0]));
					}
					Debug.LogError(list3.Pack(" "));
					Debug.LogError(list4.Pack(" "));
					if (list3.get_Count() == 0)
					{
						callback.Invoke(true);
					}
					else
					{
						Downloader.Instance.Download(list3, list4, list5, null, null, callback);
					}
				}
				else
				{
					callback.Invoke(true);
				}
			});
		}
	}

	protected bool AnalysisServerList()
	{
		string text = Path.Combine(Util.DataPath, "server.json");
		string text2 = string.Empty;
		if (this.mAddressType == LoginManager.AddressType.CACHE)
		{
			text2 = PlayerPrefsExt.GetStringPrefs(text);
		}
		else
		{
			StreamReader streamReader = File.OpenText(text);
			text2 = streamReader.ReadToEnd();
			streamReader.Close();
		}
		JsonData jsonData = null;
		try
		{
			jsonData = JsonMapper.ToObject(text2);
			GameManager.Instance.serverVersion = (string)jsonData["version"];
			if (jsonData.Inst_Object.ContainsKey("audit_version"))
			{
				GameManager.Instance.auditVersion = (string)jsonData["audit_version"];
			}
			if (jsonData.Inst_Object.ContainsKey("update_url"))
			{
				GameManager.Instance.update_url = (string)jsonData["update_url"];
			}
			Debug.Log(string.Concat(new string[]
			{
				"[clientVer:1.7.0] [serverVer:",
				GameManager.Instance.serverVersion,
				"] [auditVer:",
				GameManager.Instance.auditVersion,
				"]"
			}));
			JsonData data = null;
			if (jsonData.Inst_Object.TryGetValue("version_code", ref data))
			{
				GameManager.Instance.ServerVersionCode = int.Parse((string)data);
			}
			data = null;
			if (jsonData.Inst_Object.TryGetValue("patch_url", ref data))
			{
				GameManager.Instance.PatchUrlRoot = (string)data;
			}
			data = null;
			if (jsonData.Inst_Object.TryGetValue("res_version", ref data))
			{
				GameManager.Instance.ResVersion = (string)data;
			}
			string text3 = PathUtil.Combine(new string[]
			{
				PathSystem.PersistentDataPath,
				"test_version.txt"
			});
			if (File.Exists(text3))
			{
				GameManager.Instance.ResVersion = File.ReadAllText(text3);
			}
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
			if (this.mAddressType == LoginManager.AddressType.DOMAIN)
			{
				Debug.LogError("域名获取服务器列表格式有误, 尝试用电信IP获取");
				this.GetServerListFile(LoginManager.AddressType.CMCC);
			}
			else if (this.mAddressType == LoginManager.AddressType.CMCC)
			{
				Debug.LogError("电信IP获取服务器列表格式有误, 尝试用联通IP获取");
				this.GetServerListFile(LoginManager.AddressType.CUCC);
			}
			else if (this.mAddressType == LoginManager.AddressType.CUCC)
			{
				Debug.LogError("联通IP获取服务器列表格式有误, 尝试用本地缓存");
				this.mAddressType = LoginManager.AddressType.CACHE;
				this.UpdateServerList(true);
			}
			else if (this.mAddressType == LoginManager.AddressType.CACHE)
			{
				Debug.LogError("本地缓存服务器列表格式有误, 继续重连!!!");
				NetworkDialogUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(621282, false), delegate
				{
					this.GetServerListFile(LoginManager.AddressType.DOMAIN);
				}, GameDataUtils.GetChineseContent(621271, false), "button_orange_1");
			}
			bool result = false;
			return result;
		}
		SystemConfig.IsAudit = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		string text4 = "1.7.0";
		string serverVersion = GameManager.Instance.serverVersion;
		string auditVersion = GameManager.Instance.auditVersion;
		try
		{
			num = int.Parse(this.FixNumber(this.GetVersionString(text4.Split(new char[]
			{
				'.'
			}), 0, string.Empty), 8));
			num2 = int.Parse(this.FixNumber(this.GetVersionString(serverVersion.Split(new char[]
			{
				'.'
			}), 0, string.Empty), 8));
			if (!string.IsNullOrEmpty(auditVersion))
			{
				num3 = int.Parse(this.FixNumber(this.GetVersionString(auditVersion.Split(new char[]
				{
					'.'
				}), 0, string.Empty), 8));
				SystemConfig.IsAudit = (num2 == num3);
			}
			Debug.Log(string.Concat(new object[]
			{
				"客户端版本:<color=#white>",
				num,
				"</color>, 服务器版本:<color=#white>",
				num2,
				"</color>, 提审版本:<color=#white>",
				num3,
				"</color>"
			}));
		}
		catch
		{
			Debug.LogError(string.Concat(new string[]
			{
				"版本格式有误, client:",
				text4,
				", servre:",
				serverVersion,
				", audit:",
				auditVersion
			}));
			bool result = false;
			return result;
		}
		if (jsonData != null)
		{
			JsonData jsonData2 = jsonData["serverlist"];
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < jsonData2.Count; i++)
			{
				LoginManager.ServerInfo info = new LoginManager.ServerInfo();
				info.serverId = (int)jsonData2[i]["sid"];
				info.host = (string)jsonData2[i]["ip"];
				info.host2 = (string)jsonData2[i]["ip2"];
				info.port = (int)jsonData2[i]["port"];
				info.serverName = (string)jsonData2[i]["name"];
				info.status = (LoginManager.ServerInfo.ServerStatusType)((int)jsonData2[i]["state"]);
				info.is_test = (int)jsonData2[i]["is_test"];
				info.desc = (string)jsonData2[i]["desc"];
				int num4;
				if (jsonData2[i].Inst_Object.ContainsKey("is_ping") && int.TryParse(jsonData2[i]["is_ping"].ToString(), ref num4))
				{
					info.is_ping = num4;
				}
				if (jsonData2[i].Inst_Object.ContainsKey("is_sn") && int.TryParse(jsonData2[i]["is_sn"].ToString(), ref num4))
				{
					info.is_sn = num4;
				}
				if (!this.serverInfoList.Exists((LoginManager.ServerInfo e) => e.serverId == info.serverId))
				{
					if (SystemConfig.IsAudit && info.is_test == 2)
					{
						this.serverInfoList.Add(info);
					}
					else if (num > num2 && info.is_test == 1)
					{
						this.serverInfoList.Add(info);
					}
					else if (num <= num2 && info.is_test == 0)
					{
						this.serverInfoList.Add(info);
					}
					stringBuilder.Append(info.serverId).Append(":").Append(info.is_test).Append(" ");
				}
			}
			Debug.Log("所有服务器列表[id:is_test]: " + stringBuilder.ToString());
		}
		PlayerPrefsExt.SetStringPrefs(text, text2);
		return true;
	}

	private string GetVersionString(string[] versionStr, int index = 0, string version = "")
	{
		if (index >= versionStr.Length)
		{
			return version;
		}
		version += this.FixNumber(versionStr[index], (index != 0) ? 3 : 2);
		return this.GetVersionString(versionStr, ++index, version);
	}

	private string FixNumber(string version, int maxLength)
	{
		for (int i = version.get_Length(); i < maxLength; i++)
		{
			version += "0";
		}
		return version;
	}

	public void GetDataServerList(ServerType serverType)
	{
		this.StopGetDataServerInfoTimer(serverType);
		if (this.protoServerInfoList.get_Count() != 0)
		{
			if (this.getDataServerInfoCount.ContainsKey(serverType))
			{
				this.getDataServerInfoCount.Remove(serverType);
			}
			return;
		}
		if (this.getDataServerInfoCount.ContainsKey(serverType))
		{
			XDict<ServerType, int> xDict;
			XDict<ServerType, int> expr_4C = xDict = this.getDataServerInfoCount;
			int num = xDict[serverType];
			expr_4C[serverType] = num + 1;
		}
		else
		{
			this.getDataServerInfoCount[serverType] = 0;
		}
		Debug.Log(string.Concat(new object[]
		{
			"GetDataServerList: ",
			serverType,
			" ",
			this.getDataServerInfoCount[serverType]
		}));
		if (this.getDataServerInfoCount[serverType] > 5)
		{
			Debug.LogError("getDataServerInfoCount[serverType] > GetDataServerMaxCount");
			NetworkDialogUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(621282, false), delegate
			{
				ClientApp.Instance.ReInit();
			}, GameDataUtils.GetChineseContent(621271, false), "button_orange_1");
			return;
		}
		if (this.getDataServerInfoCount[serverType] > 0)
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(621278, false), 1f, 1f);
		}
		global::NetworkManager.Send(new GetServerListReq
		{
			localtime = DateTime.get_Now().ToString("T")
		}, serverType);
		if (!this.getDataServerInfoTimer.ContainsKey(serverType))
		{
			this.getDataServerInfoTimer.Add(serverType, 0u);
		}
		this.getDataServerInfoTimer[serverType] = TimerHeap.AddTimer<ServerType>((uint)(((this.getDataServerInfoCount[serverType] + 1) * 2000 <= 8000) ? ((this.getDataServerInfoCount[serverType] + 1) * 2000) : 8000), 0, new Action<ServerType>(this.GetDataServerList), serverType);
	}

	public void StopGetDataServerInfoTimer(ServerType serverType)
	{
		if (!this.getDataServerInfoTimer.ContainsKey(serverType))
		{
			return;
		}
		TimerHeap.DelTimer(this.getDataServerInfoTimer[serverType]);
	}

	protected void OnGetDataServerList(short state, GetServerListRes down)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			this.protoServerInfoList.Clear();
			this.protoServerInfoList.AddRange(down.servers);
			Debug.Log(string.Concat(new object[]
			{
				"Get Serverlist: ",
				down.servers.get_Count(),
				" Current Time: ",
				DateTime.get_Now()
			}));
			StringBuilder stringBuilder = new StringBuilder();
			SceneServerInfo info = null;
			for (int i = 0; i < down.servers.get_Count(); i++)
			{
				info = down.servers.get_Item(i);
				if (!this.serverInfoList.Exists((LoginManager.ServerInfo e) => e.serverId == info.serverId))
				{
					LoginManager.ServerInfo serverInfo = new LoginManager.ServerInfo();
					serverInfo.serverId = info.serverId;
					serverInfo.host = info.host;
					serverInfo.host2 = info.host2;
					serverInfo.port = info.port;
					serverInfo.is_test = 1;
					serverInfo.serverName = info.serverName;
					serverInfo.desc = string.Empty;
					this.serverInfoList.Add(serverInfo);
					if (stringBuilder.get_Length() == 0)
					{
						stringBuilder.Append(serverInfo.serverId);
					}
					else
					{
						stringBuilder.Append('|').Append(serverInfo.serverId);
					}
				}
			}
			Debug.Log("NtyServerIds:" + stringBuilder.ToString());
		}
		ClientApp.Instance.DelayAction(1f, delegate
		{
			this.serverInfoList.Sort((LoginManager.ServerInfo a, LoginManager.ServerInfo b) => b.serverId.CompareTo(a.serverId));
			this.TrySetCurrentServer();
		});
	}

	private void OnQueueLoginNty(short state, QueueLoginNty down)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down != null)
		{
			int num = Mathf.Max(1, Mathf.Min(60, down.sequent / Mathf.Max(1, down.enterPerMinute)));
			Debug.LogFormat("登录排队通知: 排队人数:{0} | 每分钟可进入人数:{1} | 等待时间:{2}", new object[]
			{
				down.sequent,
				down.enterPerMinute,
				num
			});
			EventDispatcher.Broadcast<int, int>(EventNames.ServerQueueLoginNty, down.sequent, num);
		}
	}

	public void SendCancelLoginReq()
	{
		Debug.Log("请求取消登录排队!");
		global::NetworkManager.Send(new CancelLoginReq(), ServerType.Data);
	}

	private void OnCancelLoginRes(short state, CancelLoginRes down)
	{
		this.mIsEntering = false;
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		Debug.Log("取消登录排队返回!");
	}

	public void LoginDataServer(Action loginSuccessCallback = null, Action loginFailedCallback = null)
	{
		if (!SDKManager.Instance.HasSDK() && string.IsNullOrEmpty(this.CurrentAccountName))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(621279, false), 1f, 1f);
			if (loginFailedCallback != null)
			{
				loginFailedCallback.Invoke();
			}
			return;
		}
		if (this.CurrentServerInfo == null)
		{
			Debug.Log("CurrentServer is null");
			if (loginFailedCallback != null)
			{
				loginFailedCallback.Invoke();
			}
			return;
		}
		Debug.Log("时间:" + Time.get_realtimeSinceStartup());
		this.loginDataSuccessCallback = loginSuccessCallback;
		this.loginDataFailedCallback = loginFailedCallback;
		global::NetworkManager.Instance.IsEnableAck = (this.CurrentServerInfo.is_sn == 1);
		global::NetworkManager.Instance.IsEnablePing = (this.CurrentServerInfo.is_ping == 1);
		global::NetworkManager.Instance.ShutDownLoginServer();
		global::NetworkManager.Instance.ConnectDataServer(this.CurrentServerInfo.host, this.CurrentServerInfo.port, new Action(this.OnDataServerConnectSuccess), new Action(this.OnDataServerConnectFailed));
	}

	protected void OnDataServerConnectSuccess()
	{
		this.GetAccountData();
		HeartbeatManager.Instance.Init();
	}

	protected void OnDataServerConnectFailed()
	{
		DialogBoxUIViewModel.Instance.ShowAsConfirm(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(621280, false), delegate
		{
		}, GameDataUtils.GetChineseContent(621271, false), "button_orange_1", null);
		if (this.loginDataFailedCallback != null)
		{
			this.loginDataFailedCallback.Invoke();
		}
	}

	protected void GetAccountData()
	{
		string text = (!Application.get_isEditor()) ? XUtility.GetConfigTxt("version/data_version", ".txt") : "hsh";
		string text2 = (!Application.get_isEditor()) ? XUtility.GetConfigTxt("version/proto_version", ".txt") : "tqzm";
		string text3 = string.Format("{0}.{1}", text, text2);
		Debug.LogFormat("获取帐号信息: {0} | SDK:{1} | 数据版本:{2} | {3}", new object[]
		{
			this.CurrentAccountName,
			SDKManager.Instance.GetSDKType(),
			text3,
			this.m_login_callback_data
		});
		global::NetworkManager.Send(new LoginAuthReq
		{
			account = this.CurrentAccountName,
			sdk_type = SDKManager.Instance.GetSDKType(),
			param1 = this.m_login_callback_data,
			param2 = text3
		}, ServerType.Data);
	}

	protected void OnGetAccountDataRes(short state, LoginAuthRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			if (this.loginDataFailedCallback != null)
			{
				this.loginDataFailedCallback.Invoke();
			}
			return;
		}
		if (down == null)
		{
			if (this.loginDataFailedCallback != null)
			{
				this.loginDataFailedCallback.Invoke();
			}
			return;
		}
		if (down.roles.get_Count() > 0)
		{
			Debug.LogFormat("返回帐号信息: {0} | Lv.{1} {2} | 职业:{3} | ID:{4}", new object[]
			{
				down.account,
				down.roles.get_Item(0).lv,
				down.roles.get_Item(0).roleName,
				down.roles.get_Item(0).typeId,
				down.roles.get_Item(0).roleId
			});
		}
		else
		{
			Debug.LogFormat("返回帐号信息: {0} 未创建角色", new object[]
			{
				down.account
			});
		}
		SDKManager.Instance.SubmitExtendData(null, SDKManager.SubmitTypeLogin, down.sdkRes);
		TimeManager.Instance.ForceSyncTime(down.nowServerTime);
		if (down.roles.get_Count() > 0)
		{
			if (LoginPanel.Instance != null)
			{
				LoginPanel.Instance.Show(false);
			}
			LoadingUIView.Open(true);
			this.roleBriefInfoList.AddRange(down.roles);
			long roleId = down.roles.get_Item(0).roleId;
			this.LoginRole(roleId);
			WaitUI.OpenUI(0u);
			NativeCallManager.RegisterPush(roleId.ToString());
		}
		else
		{
			this.LeadToRoleCreation();
		}
		this.SaveLoginPrefs();
	}

	protected void LeadToRoleCreation()
	{
		if (LoginPanel.Instance != null)
		{
			LoginPanel.Instance.Show(false);
		}
		if (DialogBoxUIViewModel.Instance != null)
		{
			DialogBoxUIViewModel.Instance.Close();
		}
		UIManagerControl.Instance.OpenUI("RoleLoadingUI", UINodesManager.T3RootOfSpecial, false, UIType.NonPush);
		RoleLoadingUI.ResetProgress();
		RoleLoadingUI.PlayProgressFX();
		RoleLoadingUI.SetProgress(0.5f);
		RoleLoadingUI.SetSpeed(string.Empty);
		SelectRoleUI.PreAllLoadModel(delegate
		{
			RoleLoadingUI.SetProgress(1f);
			RoleLoadingUI.Close();
			UIManagerControl.Instance.OpenUI("SelectRoleUI", UINodesManager.NormalUIRoot, true, UIType.NonPush);
		});
	}

	public void ResetClickRoleTime()
	{
		this.m_listClickRoleTime.Clear();
	}

	public void AddClickRoleTime(int career)
	{
		for (int i = 0; i < this.m_listClickRoleTime.get_Count(); i++)
		{
			if (this.m_listClickRoleTime.get_Item(i).career == career)
			{
				this.m_listClickRoleTime.get_Item(i).times++;
				return;
			}
		}
		ClickRoleTime clickRoleTime = new ClickRoleTime();
		clickRoleTime.career = career;
		clickRoleTime.times = 1;
		this.m_listClickRoleTime.Add(clickRoleTime);
	}

	public void CreateRole(int career, string name)
	{
		CreateRoleReq createRoleReq = new CreateRoleReq();
		createRoleReq.roleName = name;
		createRoleReq.typeId = career;
		createRoleReq.clickTimes.AddRange(this.m_listClickRoleTime);
		this.m_listClickRoleTime.Clear();
		global::NetworkManager.Send(createRoleReq, ServerType.Data);
	}

	protected void OnCreateRoleRes(short state, CreateRoleRes down = null)
	{
		WaitUI.CloseUI(0u);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		if (down.info == null)
		{
			return;
		}
		this.roleBriefInfoList.Add(down.info);
		long roleId = down.info.roleId;
		WaitUI.OpenUI(0u);
		this.LoginRole(roleId);
		NativeCallManager.RegisterPush(roleId.ToString());
		SDKManager.Instance.SubmitExtendData(null, SDKManager.SubmitTypeRegister, SDKManager.GetSubmitData(down.info));
		LoadingUIView.Open(true);
		EventDispatcher.Broadcast(EventNames.CLOSE_SELECTCRATEPANEL);
	}

	protected void LoginRole(long roleID)
	{
		global::NetworkManager.Send(new LoginReq
		{
			roleId = roleID
		}, ServerType.Data);
	}

	protected void OnLoginRoleRes(short state, LoginRes down = null)
	{
		if (state != 0)
		{
			if (state != 18)
			{
				StateManager.Instance.StateShow(state, 0);
			}
			else
			{
				this.OnLoginRoleBlocked((down != null) ? down.data.endTime : -1);
			}
			if (this.loginDataFailedCallback != null)
			{
				this.loginDataFailedCallback.Invoke();
			}
			Debug.Log("LoginRoleRes请求失败!!!");
			return;
		}
		if (down == null)
		{
			if (this.loginDataFailedCallback != null)
			{
				this.loginDataFailedCallback.Invoke();
			}
			Debug.Log("LoginRoleRes返回数据down为空!!!");
			return;
		}
		SDKManager.Instance.SubmitExtendData(null, SDKManager.SubmitTypeEnterGameServer, SDKManager.GetSubmitData(down.info, down.createTime));
		this.SaveLoginPrefs();
		this.SendDeviceInfo();
		global::NetworkManager.Instance.InitPing();
		try
		{
			if (this.loginDataSuccessCallback != null)
			{
				this.loginDataSuccessCallback.Invoke();
			}
			EntityWorld.Instance.CreateSelf(down.info);
			SkillDataManager.Instance.SetFixedSkill(down.info.skills);
			if (down.info.isFirstLogin)
			{
				InstanceManager.ChangeInstanceManager(ExperienceInstance.Instance, false);
				ExperienceInstanceManager.Instance.EnterExperienceInstance();
			}
			else
			{
				InstanceManager.ChangeInstanceManager(CityInstance.Instance, true);
			}
			TimeManager.Instance.Init();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.get_Message());
		}
	}

	protected void SendDeviceInfo()
	{
		int sDKType = NativeCallManager.GetSDKType();
		if (sDKType == 53 || sDKType == 56)
		{
			Debug.Log("imei " + NativeCallManager.GetDeviceIMEI());
			global::NetworkManager.Send(new LoginExtraData
			{
				deviceModel = SystemInfoTools.GetDeviceModel(),
				deviceSys = SystemInfo.get_operatingSystem(),
				deviceId = SystemInfo.get_deviceUniqueIdentifier(),
				imei = NativeCallManager.GetDeviceIMEI()
			}, ServerType.Data);
		}
		else
		{
			global::NetworkManager.Send(new LoginExtraData
			{
				deviceModel = SystemInfoTools.GetDeviceModel(),
				deviceSys = SystemInfo.get_operatingSystem(),
				deviceId = SystemInfo.get_deviceUniqueIdentifier()
			}, ServerType.Data);
		}
	}

	protected void OnLoginRoleBlocked(int unblockedTime)
	{
		string chineseContent = GameDataUtils.GetChineseContent(621264, false);
		string text = GameDataUtils.GetChineseContent(621261, false);
		if (unblockedTime < 0)
		{
			DateTime dateTime = DateTime.get_Now().AddYears(1);
			text = string.Format(text, new object[]
			{
				dateTime.get_Year(),
				dateTime.get_Month(),
				dateTime.get_Day(),
				dateTime.ToString("HH:mm:ss")
			});
		}
		else
		{
			DateTime dateTime2 = TimeManager.Instance.CalculateLocalServerTimeBySecond(unblockedTime);
			text = string.Format(text, new object[]
			{
				dateTime2.get_Year(),
				dateTime2.get_Month(),
				dateTime2.get_Day(),
				dateTime2.ToString("HH:mm:ss")
			});
		}
		NetworkDialogUIViewModel.Instance.ShowAsConfirm(chineseContent, text, delegate
		{
			ClientApp.Instance.ReInit();
		}, GameDataUtils.GetChineseContent(621271, false), "button_orange_1");
	}

	protected void OnChatServerInfoRes(short state, ChatServerInfo down = null)
	{
		Debug.Log("OnChatServerInfoRes: " + state);
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		this.ChatServerInfo = down;
		this.LoginChatServer();
	}

	public void LoginChatServer()
	{
		global::NetworkManager.Instance.ConnectChatServer(this.ChatServerInfo.ip, this.ChatServerInfo.port, new Action(this.OnChatServerConnectSuccess), new Action(this.OnChatServerConnectFailed));
	}

	protected void OnChatServerConnectSuccess()
	{
		Debug.Log("OnChatServerConnectSuccess");
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		this.SendLoginChat(null, null);
	}

	protected void OnChatServerConnectFailed()
	{
		Debug.Log("连接聊天服失败");
	}

	public void SendLoginChat(Action loginSuccessCallback = null, Action loginFailedCallback = null)
	{
		this.lastLoginChatTime = DateTime.get_Now();
		this.loginChatSuccessCallback = loginSuccessCallback;
		this.loginChatFailedCallback = loginFailedCallback;
		global::NetworkManager.Send(new ChatLoginReq
		{
			roleId = EntityWorld.Instance.EntSelf.ID,
			key = this.ChatServerInfo.key,
			token = this.ChatServerInfo.token
		}, ServerType.Chat);
	}

	protected void OnLoginChatRes(short state, ChatLoginRes down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			if (this.loginChatFailedCallback != null)
			{
				this.loginChatFailedCallback.Invoke();
			}
			return;
		}
		if (this.loginChatSuccessCallback != null)
		{
			this.loginChatSuccessCallback.Invoke();
		}
	}

	public void TrySetCurrentServer()
	{
		if (this.CurrentServerInfo == null)
		{
			this.GetLoginPrefs();
			int serverID;
			string account;
			if (this.TryGetLastServerAndAccount(out serverID, out account))
			{
				this.CurrentServerInfo = this.GetServerInfoByID(serverID);
				this.SetAccount(account);
			}
		}
		if (this.CurrentServerInfo == null && this.serverInfoList != null)
		{
			if (this.serverInfoList.get_Count() > 0)
			{
				this.CurrentServerInfo = this.serverInfoList.get_Item(0);
				string account2;
				if (this.TryGetLastAccountByServerID(this.GetDomainName(this.CurrentServerInfo.host, this.CurrentServerInfo.port), this.CurrentServerInfo.serverId, out account2))
				{
					this.SetAccount(account2);
				}
				else
				{
					this.SetAccount(string.Empty);
				}
			}
			this.OpenAccountUI();
		}
		if (this.CurrentServerInfo != null)
		{
			string serverInfoNameByID = this.GetServerInfoNameByID(this.CurrentServerInfo.serverId);
			EventDispatcher.Broadcast<string>(EventNames.Update_CurrentServer_Name, serverInfoNameByID);
		}
		else
		{
			Debug.Log("当前服务器数据为空!!!");
		}
	}

	protected bool TryGetLastServerAndAccount(out int serverID, out string accountName)
	{
		serverID = 0;
		accountName = string.Empty;
		if (this.domainDatas.get_Count() == 0)
		{
			return false;
		}
		if (this.domainDatas.get_Item(0).serverDatas == null)
		{
			return false;
		}
		if (this.domainDatas.get_Item(0).serverDatas.get_Count() == 0)
		{
			return false;
		}
		if (this.domainDatas.get_Item(0).serverDatas.get_Item(0).accountNames == null)
		{
			return false;
		}
		if (this.domainDatas.get_Item(0).serverDatas.get_Item(0).accountNames.get_Count() == 0)
		{
			return false;
		}
		serverID = this.domainDatas.get_Item(0).serverDatas.get_Item(0).serverID;
		accountName = this.domainDatas.get_Item(0).serverDatas.get_Item(0).accountNames.get_Item(0);
		return true;
	}

	protected bool TryGetLastAccountByServerID(string domainName, int serverID, out string accountName)
	{
		for (int i = 0; i < this.domainDatas.get_Count(); i++)
		{
			if (!(this.domainDatas.get_Item(i).domainName != domainName))
			{
				int j = 0;
				while (j < this.domainDatas.get_Item(i).serverDatas.get_Count())
				{
					if (this.domainDatas.get_Item(i).serverDatas.get_Item(j).serverID != serverID)
					{
						j++;
					}
					else
					{
						if (this.domainDatas.get_Item(i).serverDatas.get_Item(j).accountNames == null)
						{
							break;
						}
						if (this.domainDatas.get_Item(i).serverDatas.get_Item(j).accountNames.get_Count() == 0)
						{
							break;
						}
						accountName = this.domainDatas.get_Item(i).serverDatas.get_Item(j).accountNames.get_Item(0);
						return true;
					}
				}
				break;
			}
		}
		accountName = string.Empty;
		return false;
	}

	protected string GetDomainName(string ipStr, int port)
	{
		return string.Format("{0}:{1}", ipStr, port);
	}

	protected void GetLoginPrefs()
	{
		if (this.HasInitLocalData)
		{
			return;
		}
		this.HasInitLocalData = true;
		this.recentLoginHistoryDatas.Clear();
		this.domainDatas.Clear();
		PlayerPrefsExt.SetStringPrefs("LoginMessage", string.Empty);
		try
		{
			string stringPrefs = PlayerPrefsExt.GetStringPrefs("SaveRecentLoginHistoryDatasKey");
			this.recentLoginHistoryDatas = this.ParseRecentLoginHistoryDatas(stringPrefs);
			string stringPrefs2 = PlayerPrefsExt.GetStringPrefs("SaveLoginDomainDataKey");
			string stringPrefs3 = PlayerPrefsExt.GetStringPrefs("SaveLoginServerDataKey");
			string stringPrefs4 = PlayerPrefsExt.GetStringPrefs("SaveLoginAccountDataKey");
			List<LoginManager.SaveLoginDomainData> list = this.ParseSaveLoginDomainDataString(stringPrefs2);
			List<LoginManager.SaveLoginServerData> list2 = this.ParseSaveLoginServerDataString(stringPrefs3);
			List<string> list3 = this.ParseSaveLoginAccountDataString(stringPrefs4);
			bool flag = false;
			for (int i = 0; i < list.get_Count(); i++)
			{
				LoginManager.LoginDomainData loginDomainData = new LoginManager.LoginDomainData();
				loginDomainData.domainName = list.get_Item(i).domainName;
				loginDomainData.serverDatas = new List<LoginManager.LoginServerData>();
				for (int j = 0; j < list.get_Item(i).serverDataIndexes.get_Count(); j++)
				{
					if (list2.get_Count() <= list.get_Item(i).serverDataIndexes.get_Item(j))
					{
						flag = true;
						break;
					}
					LoginManager.LoginServerData loginServerData = new LoginManager.LoginServerData();
					loginServerData.serverID = list2.get_Item(list.get_Item(i).serverDataIndexes.get_Item(j)).serverID;
					loginServerData.accountNames = new List<string>();
					for (int k = 0; k < list2.get_Item(list.get_Item(i).serverDataIndexes.get_Item(j)).accountIndexes.get_Count(); k++)
					{
						if (list3.get_Count() <= list2.get_Item(list.get_Item(i).serverDataIndexes.get_Item(j)).accountIndexes.get_Item(k))
						{
							flag = true;
							break;
						}
						loginServerData.accountNames.Add(UnicodeEscaper.UnEscape(list3.get_Item(list2.get_Item(list.get_Item(i).serverDataIndexes.get_Item(j)).accountIndexes.get_Item(k))));
					}
					if (flag)
					{
						break;
					}
					loginDomainData.serverDatas.Add(loginServerData);
				}
				if (flag)
				{
					break;
				}
				this.domainDatas.Add(loginDomainData);
			}
			if (flag)
			{
				this.recentLoginHistoryDatas.Clear();
				this.domainDatas.Clear();
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.get_Message());
			this.recentLoginHistoryDatas.Clear();
			this.domainDatas.Clear();
		}
	}

	protected void SaveLoginPrefs()
	{
		if (this.CurrentServerInfo == null || string.IsNullOrEmpty(this.CurrentAccountName))
		{
			Debuger.Error("当前选择的服务器为空!!!", new object[0]);
			return;
		}
		string domainName = this.GetDomainName(this.CurrentServerInfo.host, this.CurrentServerInfo.port);
		LoginManager.LoginHistoryData loginHistoryData = null;
		for (int i = 0; i < this.recentLoginHistoryDatas.get_Count(); i++)
		{
			if (!(this.recentLoginHistoryDatas.get_Item(i).domainName != domainName) && this.recentLoginHistoryDatas.get_Item(i).serverID == this.CurrentServerInfo.serverId)
			{
				loginHistoryData = this.recentLoginHistoryDatas.get_Item(i);
				break;
			}
		}
		if (loginHistoryData == null)
		{
			loginHistoryData = new LoginManager.LoginHistoryData();
			loginHistoryData.domainName = domainName;
			loginHistoryData.serverID = this.CurrentServerInfo.serverId;
			loginHistoryData.accountName = this.CurrentAccountName;
		}
		List<LoginManager.LoginHistoryData> list = new List<LoginManager.LoginHistoryData>(this.recentLoginHistoryDatas);
		this.recentLoginHistoryDatas.Clear();
		this.recentLoginHistoryDatas.Add(loginHistoryData);
		int num = 0;
		while (num < list.get_Count() && this.recentLoginHistoryDatas.get_Count() < 11)
		{
			if (!(list.get_Item(num).domainName == domainName) || list.get_Item(num).serverID != this.CurrentServerInfo.serverId)
			{
				this.recentLoginHistoryDatas.Add(list.get_Item(num));
			}
			num++;
		}
		LoginManager.LoginDomainData loginDomainData = null;
		for (int j = 0; j < this.domainDatas.get_Count(); j++)
		{
			if (!(this.domainDatas.get_Item(j).domainName != domainName))
			{
				loginDomainData = this.domainDatas.get_Item(j);
				break;
			}
		}
		if (loginDomainData == null)
		{
			loginDomainData = new LoginManager.LoginDomainData();
			loginDomainData.domainName = domainName;
			loginDomainData.serverDatas = new List<LoginManager.LoginServerData>();
			LoginManager.LoginServerData loginServerData = new LoginManager.LoginServerData();
			loginServerData.serverID = this.CurrentServerInfo.serverId;
			loginServerData.accountNames = new List<string>();
			loginServerData.accountNames.Add(this.CurrentAccountName);
			loginDomainData.serverDatas.Add(loginServerData);
		}
		else
		{
			LoginManager.LoginServerData loginServerData2 = null;
			for (int k = 0; k < loginDomainData.serverDatas.get_Count(); k++)
			{
				if (loginDomainData.serverDatas.get_Item(k).serverID == this.CurrentServerInfo.serverId)
				{
					loginServerData2 = loginDomainData.serverDatas.get_Item(k);
					break;
				}
			}
			if (loginServerData2 == null)
			{
				loginServerData2 = new LoginManager.LoginServerData();
				loginServerData2.serverID = this.CurrentServerInfo.serverId;
				loginServerData2.accountNames = new List<string>();
				loginServerData2.accountNames.Add(this.CurrentAccountName);
			}
			else
			{
				List<string> list2 = new List<string>(loginServerData2.accountNames);
				loginServerData2.accountNames.Clear();
				loginServerData2.accountNames.Add(this.CurrentAccountName);
				for (int l = 0; l < list2.get_Count(); l++)
				{
					if (!(list2.get_Item(l) == this.CurrentAccountName))
					{
						loginServerData2.accountNames.Add(list2.get_Item(l));
					}
				}
			}
			List<LoginManager.LoginServerData> list3 = new List<LoginManager.LoginServerData>(loginDomainData.serverDatas);
			loginDomainData.serverDatas.Clear();
			loginDomainData.serverDatas.Add(loginServerData2);
			for (int m = 0; m < list3.get_Count(); m++)
			{
				if (list3.get_Item(m).serverID != this.CurrentServerInfo.serverId)
				{
					loginDomainData.serverDatas.Add(list3.get_Item(m));
				}
			}
		}
		List<LoginManager.LoginDomainData> list4 = new List<LoginManager.LoginDomainData>(this.domainDatas);
		this.domainDatas.Clear();
		this.domainDatas.Add(loginDomainData);
		for (int n = 0; n < list4.get_Count(); n++)
		{
			if (!(list4.get_Item(n).domainName == domainName))
			{
				this.domainDatas.Add(list4.get_Item(n));
			}
		}
		List<LoginManager.SaveLoginDomainData> list5 = new List<LoginManager.SaveLoginDomainData>();
		List<LoginManager.SaveLoginServerData> list6 = new List<LoginManager.SaveLoginServerData>();
		List<string> list7 = new List<string>();
		for (int num2 = 0; num2 < this.domainDatas.get_Count(); num2++)
		{
			LoginManager.SaveLoginDomainData saveLoginDomainData = new LoginManager.SaveLoginDomainData();
			saveLoginDomainData.domainName = this.domainDatas.get_Item(num2).domainName;
			saveLoginDomainData.serverDataIndexes = new List<int>();
			for (int num3 = 0; num3 < this.domainDatas.get_Item(num2).serverDatas.get_Count(); num3++)
			{
				LoginManager.SaveLoginServerData saveLoginServerData = new LoginManager.SaveLoginServerData();
				saveLoginServerData.serverID = this.domainDatas.get_Item(num2).serverDatas.get_Item(num3).serverID;
				saveLoginServerData.accountIndexes = new List<int>();
				for (int num4 = 0; num4 < this.domainDatas.get_Item(num2).serverDatas.get_Item(num3).accountNames.get_Count(); num4++)
				{
					string text = UnicodeEscaper.Escape(this.domainDatas.get_Item(num2).serverDatas.get_Item(num3).accountNames.get_Item(num4));
					list7.Add(text);
					saveLoginServerData.accountIndexes.Add(list7.get_Count() - 1);
				}
				list6.Add(saveLoginServerData);
				saveLoginDomainData.serverDataIndexes.Add(list6.get_Count() - 1);
			}
			list5.Add(saveLoginDomainData);
		}
		PlayerPrefsExt.SetStringPrefs("SaveRecentLoginHistoryDatasKey", this.GetRecentLoginHistoryDatasString());
		PlayerPrefsExt.SetStringPrefs("SaveLoginDomainDataKey", this.GetSaveLoginDomainDataString(list5));
		PlayerPrefsExt.SetStringPrefs("SaveLoginServerDataKey", this.GetSaveLoginServerDataString(list6));
		PlayerPrefsExt.SetStringPrefs("SaveLoginAccountDataKey", this.GetSaveLoginAccountDataString(list7));
	}

	protected string GetRecentLoginHistoryDatasString()
	{
		ArrayList arrayList = new ArrayList();
		int num = 0;
		while (num < this.recentLoginHistoryDatas.get_Count() && num < 10)
		{
			arrayList.Add(this.recentLoginHistoryDatas.get_Item(num).domainName);
			arrayList.Add(this.recentLoginHistoryDatas.get_Item(num).serverID);
			arrayList.Add(this.recentLoginHistoryDatas.get_Item(num).accountName);
			num++;
		}
		return MiniJSON.jsonEncode(arrayList);
	}

	protected List<LoginManager.LoginHistoryData> ParseRecentLoginHistoryDatas(string str)
	{
		List<LoginManager.LoginHistoryData> list = new List<LoginManager.LoginHistoryData>();
		ArrayList arrayList = MiniJSON.jsonDecode(str) as ArrayList;
		if (arrayList == null)
		{
			return list;
		}
		for (int i = 0; i < arrayList.get_Count(); i += 3)
		{
			list.Add(new LoginManager.LoginHistoryData
			{
				domainName = arrayList.get_Item(i).ToString(),
				serverID = int.Parse(arrayList.get_Item(i + 1).ToString()),
				accountName = arrayList.get_Item(i + 2).ToString()
			});
		}
		return list;
	}

	protected string GetSaveLoginDomainDataString(List<LoginManager.SaveLoginDomainData> saveDomainDatas)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < saveDomainDatas.get_Count(); i++)
		{
			ArrayList arrayList2 = new ArrayList();
			for (int j = 0; j < saveDomainDatas.get_Item(i).serverDataIndexes.get_Count(); j++)
			{
				arrayList2.Add(saveDomainDatas.get_Item(i).serverDataIndexes.get_Item(j));
			}
			arrayList.Add(saveDomainDatas.get_Item(i).domainName);
			arrayList.Add(MiniJSON.jsonEncode(arrayList2));
		}
		return MiniJSON.jsonEncode(arrayList);
	}

	protected List<LoginManager.SaveLoginDomainData> ParseSaveLoginDomainDataString(string str)
	{
		ArrayList arrayList = MiniJSON.jsonDecode(str) as ArrayList;
		if (arrayList == null)
		{
			return new List<LoginManager.SaveLoginDomainData>();
		}
		List<LoginManager.SaveLoginDomainData> list = new List<LoginManager.SaveLoginDomainData>();
		for (int i = 0; i < arrayList.get_Count(); i += 2)
		{
			LoginManager.SaveLoginDomainData saveLoginDomainData = new LoginManager.SaveLoginDomainData();
			saveLoginDomainData.domainName = arrayList.get_Item(i).ToString();
			saveLoginDomainData.serverDataIndexes = new List<int>();
			ArrayList arrayList2 = MiniJSON.jsonDecode(arrayList.get_Item(i + 1).ToString()) as ArrayList;
			if (arrayList2 == null)
			{
				return new List<LoginManager.SaveLoginDomainData>();
			}
			for (int j = 0; j < arrayList2.get_Count(); j++)
			{
				saveLoginDomainData.serverDataIndexes.Add(int.Parse(arrayList2.get_Item(j).ToString()));
			}
			list.Add(saveLoginDomainData);
		}
		return list;
	}

	protected string GetSaveLoginServerDataString(List<LoginManager.SaveLoginServerData> saveServerDatas)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < saveServerDatas.get_Count(); i++)
		{
			ArrayList arrayList2 = new ArrayList();
			for (int j = 0; j < saveServerDatas.get_Item(i).accountIndexes.get_Count(); j++)
			{
				arrayList2.Add(saveServerDatas.get_Item(i).accountIndexes.get_Item(j));
			}
			arrayList.Add(saveServerDatas.get_Item(i).serverID);
			arrayList.Add(MiniJSON.jsonEncode(arrayList2));
		}
		return MiniJSON.jsonEncode(arrayList);
	}

	protected List<LoginManager.SaveLoginServerData> ParseSaveLoginServerDataString(string str)
	{
		ArrayList arrayList = MiniJSON.jsonDecode(str) as ArrayList;
		if (arrayList == null)
		{
			return new List<LoginManager.SaveLoginServerData>();
		}
		List<LoginManager.SaveLoginServerData> list = new List<LoginManager.SaveLoginServerData>();
		for (int i = 0; i < arrayList.get_Count(); i += 2)
		{
			LoginManager.SaveLoginServerData saveLoginServerData = new LoginManager.SaveLoginServerData();
			saveLoginServerData.serverID = int.Parse(arrayList.get_Item(i).ToString());
			saveLoginServerData.accountIndexes = new List<int>();
			ArrayList arrayList2 = MiniJSON.jsonDecode(arrayList.get_Item(i + 1).ToString()) as ArrayList;
			if (arrayList2 == null)
			{
				return new List<LoginManager.SaveLoginServerData>();
			}
			for (int j = 0; j < arrayList2.get_Count(); j++)
			{
				saveLoginServerData.accountIndexes.Add(int.Parse(arrayList2.get_Item(j).ToString()));
			}
			list.Add(saveLoginServerData);
		}
		return list;
	}

	protected string GetSaveLoginAccountDataString(List<string> saveAccountDatas)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < saveAccountDatas.get_Count(); i++)
		{
			arrayList.Add(saveAccountDatas.get_Item(i));
		}
		return MiniJSON.jsonEncode(arrayList);
	}

	protected List<string> ParseSaveLoginAccountDataString(string str)
	{
		ArrayList arrayList = MiniJSON.jsonDecode(str) as ArrayList;
		if (arrayList == null)
		{
			return new List<string>();
		}
		List<string> list = new List<string>();
		for (int i = 0; i < arrayList.get_Count(); i++)
		{
			list.Add(arrayList.get_Item(i).ToString());
		}
		return list;
	}

	protected string GetServerInfoNameByID(int serverID)
	{
		string result = string.Empty;
		if (this.serverInfoList != null)
		{
			int num = this.serverInfoList.FindIndex((LoginManager.ServerInfo a) => a.serverId == serverID);
			if (num >= 0)
			{
				result = this.serverInfoList.get_Item(num).serverName;
			}
		}
		return result;
	}

	public void OpenServerUI()
	{
		this.mIsEntering = false;
		UIManagerControl.Instance.OpenUI("ServerUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
	}

	public void OpenAccountUI()
	{
		if (!SDKManager.Instance.HasSDK())
		{
			UIManagerControl.Instance.OpenUI("AccountUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush);
		}
	}

	public void OpenAnnouncementUI()
	{
		UIManagerControl.Instance.OpenUI("AnnouncementUI", UINodesManager.TopUIRoot, false, UIType.NonPush);
	}

	public void SetCurrentServer(LoginManager.ServerInfo serverInfo)
	{
		if (serverInfo != null)
		{
			this.CurrentServerInfo = serverInfo;
			UIManagerControl.Instance.OpenUI_Async("LoginUI", delegate(UIBase uibase)
			{
				LoginPanel.Instance = (uibase as LoginPanel);
				string serverInfoNameByID = this.GetServerInfoNameByID(this.CurrentServerInfo.serverId);
				EventDispatcher.Broadcast<string>(EventNames.Update_CurrentServer_Name, serverInfoNameByID);
			}, null);
		}
		else
		{
			Debug.Log("设置服务器数据为空!!!");
		}
	}

	public LoginManager.ServerInfo GetServerInfoByID(int serverID)
	{
		for (int i = 0; i < this.serverInfoList.get_Count(); i++)
		{
			if (this.serverInfoList.get_Item(i).serverId == serverID)
			{
				return this.serverInfoList.get_Item(i);
			}
		}
		return null;
	}

	public void CloseServerUI()
	{
		if (ServerPanel.Instance != null)
		{
			ServerPanel.Instance.Show(false);
		}
		if (LoginPanel.Instance != null)
		{
			LoginPanel.Instance.OpenFXMeshRender();
		}
	}

	public void SetAccount(string accountName)
	{
		if (!SDKManager.Instance.HasSDK())
		{
			this.CurrentAccountName = accountName;
		}
	}

	public void SetAccountForSDK(string accountName)
	{
		this.CurrentAccountName = accountName;
	}

	public int GetCurrentServerId()
	{
		if (this.CurrentServerInfo != null)
		{
			return this.CurrentServerInfo.serverId;
		}
		return 0;
	}

	public string GetCurrentServerName()
	{
		if (this.CurrentServerInfo != null)
		{
			return this.CurrentServerInfo.serverName;
		}
		return string.Empty;
	}

	public void LoginProcess(string serverName)
	{
		if (!SDKManager.Instance.HasSDK())
		{
			this.Login(serverName);
		}
		else if (!SDKManager.Instance.IsLogin())
		{
			SDKManager.Instance.TryLogin();
		}
		else
		{
			this.Login(serverName);
		}
	}

	private void Login(string serverName)
	{
		if (string.IsNullOrEmpty(serverName))
		{
			UIManagerControl.Instance.ShowToastText("没有服务器", 1f, 1f);
			return;
		}
		if (!this.mIsEntering)
		{
			this.mIsEntering = true;
			WaitUI.OpenUI(0u);
			LoginManager.Instance.LoginDataServer(delegate
			{
				this.mIsEntering = false;
				WaitUI.CloseUI(0u);
			}, delegate
			{
				this.mIsEntering = false;
				WaitUI.CloseUI(0u);
				GameManager.Instance.CurrentUpdateManager.DownloadServerListAndUpdate();
			});
		}
	}

	public void OnGetLoginResp(SDKStatusCode code, string data)
	{
		if (code == SDKStatusCode.SUCCESS)
		{
			this.m_login_callback_data = data;
			this.LoginSDKSuccess();
			SDKManager.Instance.TrySendCacheOrderToPHP();
		}
	}

	private void LoginSDKSuccess()
	{
		Debug.Log("SDK.LoginSDKSuccess");
		if (ClientApp.Instance == null && NativeCallManager.m_isTest)
		{
			this.SetAccountForSDK("123");
			this.CurrentServerInfo = new LoginManager.ServerInfo
			{
				host = LoginManager.m_host,
				port = LoginManager.m_port
			};
			LoginManager.Instance.LoginDataServer(delegate
			{
			}, null);
		}
		else
		{
			if (LoginPanel.Instance != null)
			{
				LoginPanel.Instance.ShowLoginSDK(false);
			}
			string accountFromCallbackData = this.GetAccountFromCallbackData();
			string accountForSDK = MD5Util.Encrypt(accountFromCallbackData);
			this.SetAccountForSDK(accountForSDK);
		}
	}

	private string GetAccountFromCallbackData()
	{
		JsonData jd = null;
		string sDKName = SDKManager.Instance.GetSDKName();
		try
		{
			jd = JsonMapper.ToObject(this.m_login_callback_data);
		}
		catch
		{
			Debug.LogError("m_login_callback_data parse failed!");
			return sDKName;
		}
		if (!this.TryGetString(jd, "sessionID", out sDKName) && !this.TryGetString(jd, "accountID", out sDKName))
		{
			sDKName = SDKManager.Instance.GetSDKName();
		}
		return sDKName;
	}

	private bool TryGetString(JsonData jd, string key, out string value)
	{
		bool result = true;
		value = string.Empty;
		try
		{
			value = (string)jd[key];
		}
		catch
		{
			value = string.Empty;
			result = false;
		}
		return result;
	}
}
