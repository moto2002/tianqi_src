using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ServerPanel : UIBase
{
	private const int ServerNumInOnePage = 10;

	public static ServerPanel Instance;

	public ListPool poolPart;

	public ListPool poolList;

	public ListPool LatelyLoginList;

	private LoginManager.ServerInfo stateData;

	private List<LoginManager.ServerInfo> owerList = new List<LoginManager.ServerInfo>();

	private void Awake()
	{
		ServerPanel.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.LatelyLoginList = base.get_transform().FindChild("LatelyLogin").FindChild("LatelyLoginGrid").GetComponent<ListPool>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		base.SetAsLastSibling();
		List<LoginManager.LoginHistoryData> recentLoginHistoryDatas = LoginManager.Instance.RecentLoginHistoryDatas;
		if (recentLoginHistoryDatas.get_Count() > 0)
		{
			for (int i = 0; i < recentLoginHistoryDatas.get_Count(); i++)
			{
				LoginManager.ServerInfo serverInfoByID = LoginManager.Instance.GetServerInfoByID(recentLoginHistoryDatas.get_Item(i).serverID);
				if (serverInfoByID != null)
				{
					this.owerList.Add(serverInfoByID);
				}
			}
		}
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			this.poolPart.Release();
			this.poolList.Release();
			ServerPanel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void OnClickState(GameObject go)
	{
		if (this.stateData != null)
		{
			LoginManager.Instance.SetCurrentServer(this.stateData);
			LoginManager.Instance.CloseServerUI();
		}
	}

	public void UpdateList()
	{
		int count = this.GetPageNum();
		if (count <= 0)
		{
			return;
		}
		this.poolPart.Create(count, delegate(int index)
		{
			if (index < count && index < this.poolPart.Items.get_Count())
			{
				this.poolPart.Items.get_Item(index).GetComponent<ServerItem>().UpdateText(count - index - 1);
				if (index == 0)
				{
					this.poolPart.Items.get_Item(0).GetComponent<ServerItem>().SetSelect();
				}
			}
		});
		this.UpdateServerList(this.GetLastPageData());
		this.UpdateLatelyLoginList(this.owerList);
	}

	private void UpdateLatelyLoginList(List<LoginManager.ServerInfo> gslist)
	{
		this.LatelyLoginList.Create(gslist.get_Count(), delegate(int index)
		{
			if (index < gslist.get_Count() && index < this.LatelyLoginList.Items.get_Count())
			{
				this.LatelyLoginList.Items.get_Item(index).GetComponent<ServerListItem>().UpdateUI(gslist.get_Item(index));
			}
		});
	}

	public void UpdateServerList(List<LoginManager.ServerInfo> gslist)
	{
		this.poolList.Create(gslist.get_Count(), delegate(int index)
		{
			if (index < gslist.get_Count() && index < this.poolList.Items.get_Count())
			{
				this.poolList.Items.get_Item(index).GetComponent<ServerListItem>().UpdateUI(gslist.get_Item(index));
			}
		});
	}

	public void UpdateOwerList()
	{
		this.UpdateServerList(this.owerList);
	}

	protected override void OnClickMaskAction()
	{
		LoginManager.Instance.CloseServerUI();
	}

	public int GetPageNum()
	{
		if (LoginManager.Instance.serverInfoList == null)
		{
			return 0;
		}
		int num = LoginManager.Instance.serverInfoList.get_Count() / 10;
		return (LoginManager.Instance.serverInfoList.get_Count() % 10 != 0) ? (num + 1) : num;
	}

	public List<LoginManager.ServerInfo> GetLastPageData()
	{
		if (LoginManager.Instance.serverInfoList == null)
		{
			return null;
		}
		int index = (LoginManager.Instance.serverInfoList.get_Count() % 10 != 0) ? (LoginManager.Instance.serverInfoList.get_Count() / 10) : (LoginManager.Instance.serverInfoList.get_Count() / 10 - 1);
		return this.GetDataByPageIndex(index);
	}

	public List<LoginManager.ServerInfo> GetDataByPageIndex(int index)
	{
		int num = index * 10;
		if (LoginManager.Instance.serverInfoList == null)
		{
			return null;
		}
		int num2 = (LoginManager.Instance.serverInfoList.get_Count() < num + 10) ? (LoginManager.Instance.serverInfoList.get_Count() % 10) : 10;
		if (LoginManager.Instance.serverInfoList.get_Count() > 0)
		{
			return LoginManager.Instance.serverInfoList.GetRange(num, num2);
		}
		return new List<LoginManager.ServerInfo>();
	}

	public string GerPageIconByState(LoginManager.ServerInfo.ServerStatusType status)
	{
		switch (status)
		{
		case LoginManager.ServerInfo.ServerStatusType.NEW:
		case LoginManager.ServerInfo.ServerStatusType.TIPS:
			return "fwq_biaoshi_3";
		case LoginManager.ServerInfo.ServerStatusType.HOT:
		case LoginManager.ServerInfo.ServerStatusType.FULL:
			return "fwq_biaoshi_1";
		case LoginManager.ServerInfo.ServerStatusType.MAINTAIN:
		case LoginManager.ServerInfo.ServerStatusType.HIDE:
		case LoginManager.ServerInfo.ServerStatusType.CLOSE:
			return "fwq_biaoshi_4";
		default:
			return "fwq_biaoshi_3";
		}
	}

	public string GerPageStringByIndex(int index)
	{
		string text = (index * 10 + 1).ToString();
		string text2 = ((index + 1) * 10).ToString();
		return text + "-" + text2 + "服";
	}
}
