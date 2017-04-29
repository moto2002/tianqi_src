using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerListItem : MonoBehaviour
{
	private GameObject isHasRole;

	private Image state;

	private Text serverName;

	private LoginManager.ServerInfo serData;

	private void Awake()
	{
		this.isHasRole = base.get_transform().FindChild("IsHasRole").get_gameObject();
		this.state = base.get_transform().FindChild("stateIcon").GetComponent<Image>();
		this.serverName = base.get_transform().FindChild("ItemText").GetComponent<Text>();
		ButtonCustom expr_57 = base.GetComponent<ButtonCustom>();
		expr_57.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_57.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickServer));
	}

	private void OnClickServer(GameObject go)
	{
		if (this.serData != null)
		{
			if (!string.IsNullOrEmpty(this.serData.desc))
			{
				UIManagerControl.Instance.ShowToastText(this.serData.desc, 1f, 1f);
			}
			LoginManager.Instance.SetCurrentServer(this.serData);
			LoginManager.Instance.CloseServerUI();
		}
	}

	public void UpdateUI(LoginManager.ServerInfo item)
	{
		if (item != null)
		{
			this.serData = item;
			this.serverName.set_text(item.serverName);
			ResourceManager.SetSprite(this.state, ResourceManager.GetCodeSprite(ServerPanel.Instance.GerPageIconByState(item.status)));
			if (!this.IsInRecentLoginHistoryData(item.serverId))
			{
				this.isHasRole.SetActive(false);
			}
			else
			{
				this.isHasRole.SetActive(true);
			}
		}
	}

	private bool IsInRecentLoginHistoryData(int serverId)
	{
		if (LoginManager.Instance.RecentLoginHistoryDatas != null)
		{
			List<LoginManager.LoginHistoryData> recentLoginHistoryDatas = LoginManager.Instance.RecentLoginHistoryDatas;
			if (recentLoginHistoryDatas.get_Count() > 0)
			{
				for (int i = 0; i < recentLoginHistoryDatas.get_Count(); i++)
				{
					if (serverId == recentLoginHistoryDatas.get_Item(i).serverID)
					{
						return true;
					}
				}
			}
		}
		return false;
	}
}
