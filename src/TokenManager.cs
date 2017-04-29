using Package;
using System;
using UnityEngine;
using XNetwork;

public class TokenManager
{
	protected static TokenManager instance;

	protected string clientToken = string.Empty;

	protected string serverToken = string.Empty;

	public static TokenManager Instance
	{
		get
		{
			if (TokenManager.instance == null)
			{
				TokenManager.instance = new TokenManager();
			}
			return TokenManager.instance;
		}
	}

	protected string ClientToken
	{
		get
		{
			return this.clientToken;
		}
		set
		{
			this.clientToken = value;
		}
	}

	protected string ServerToken
	{
		get
		{
			return this.serverToken;
		}
		set
		{
			this.serverToken = value;
		}
	}

	public string Token
	{
		get
		{
			if (this.ClientToken == string.Empty || this.ServerToken == string.Empty || EntityWorld.Instance.EntSelf == null)
			{
				return null;
			}
			return MD5Util.Encrypt(this.ClientToken + this.ServerToken + EntityWorld.Instance.EntSelf.ID);
		}
	}

	protected TokenManager()
	{
	}

	public void Init()
	{
		this.AddListeners();
	}

	public void Release()
	{
		this.RemoveListeners();
		this.clientToken = string.Empty;
		this.serverToken = string.Empty;
	}

	protected void AddListeners()
	{
		NetworkManager.AddListenEvent<UpdateToken>(new NetCallBackMethod<UpdateToken>(this.OnReceiveServerToken));
	}

	protected void RemoveListeners()
	{
		NetworkManager.RemoveListenEvent<UpdateToken>(new NetCallBackMethod<UpdateToken>(this.OnReceiveServerToken));
	}

	protected void OnReceiveServerToken(short state, UpdateToken down = null)
	{
		if (state != 0)
		{
			StateManager.Instance.StateShow(state, 0);
			return;
		}
		if (down == null)
		{
			return;
		}
		if (down.seq == 1)
		{
			Debug.Log("OnReceiveServerToken: " + down.token);
			this.ServerToken = down.token;
			this.SendClientToken();
		}
	}

	protected void SendClientToken()
	{
		this.ClientToken = MD5Util.Encrypt("tqzm" + Random.Range(-2147483648, 2147483647));
		Debug.Log("SendClientToken: " + this.ClientToken);
		NetworkManager.Instance.SendVerify(new UpdateTokenAck
		{
			seq = 0,
			token = this.ClientToken
		}, ServerType.Data);
	}
}
