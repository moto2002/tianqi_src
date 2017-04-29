using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletCurtainManager : BaseSubSystemManager
{
	private const string IsBulletCurtainOnTag = "IsBulletCurtainOn";

	private const int CallInterval = 1000;

	public bool IsRandomSize = true;

	public bool IsRandomSpeed = true;

	public bool IsRandomColor;

	[Range(0f, 1f)]
	public float Alpha = 0.5f;

	private Dictionary<int, bool> IsBulletCurtainOnList = new Dictionary<int, bool>();

	private static BulletCurtainManager instance;

	private List<ChatManager.ChatInfo> BulletCurtainNews = new List<ChatManager.ChatInfo>();

	private List<ChatInfo2BulletCurtain> BulletCurtainUnits = new List<ChatInfo2BulletCurtain>();

	private static UIPool BulletCurtainPool;

	public static Transform Pool2BulletCurtain;

	public static BulletCurtainManager Instance
	{
		get
		{
			if (BulletCurtainManager.instance == null)
			{
				BulletCurtainManager.instance = new BulletCurtainManager();
			}
			return BulletCurtainManager.instance;
		}
	}

	private BulletCurtainManager()
	{
		this.IsBulletCurtainOnList.set_Item(1, PlayerPrefsExt.GetBool("IsBulletCurtainOn" + 1, false));
		this.IsBulletCurtainOnList.set_Item(2, PlayerPrefsExt.GetBool("IsBulletCurtainOn" + 2, false));
		this.IsBulletCurtainOnList.set_Item(32, PlayerPrefsExt.GetBool("IsBulletCurtainOn" + 32, true));
		this.IsBulletCurtainOnList.set_Item(128, PlayerPrefsExt.GetBool("IsBulletCurtainOn" + 128, false));
		this.IsBulletCurtainOnList.set_Item(4, PlayerPrefsExt.GetBool("IsBulletCurtainOn" + 4, true));
		BulletCurtainManager.CreatePools();
		TimerHeap.AddTimer(0u, 1000, delegate
		{
			this.CheckStack();
		});
	}

	public void SetBulletCurtainOn(int channel, bool isOn)
	{
		if (this.IsBulletCurtainOnList.ContainsKey(channel))
		{
			this.IsBulletCurtainOnList.set_Item(channel, isOn);
			PlayerPrefsExt.SetBool("IsBulletCurtainOn" + channel, isOn);
			this.ShowBulletCurtainUnits(channel, isOn);
		}
	}

	public bool CheckIsBulletCurtainOn(int channel)
	{
		return this.IsBulletCurtainOnList.ContainsKey(channel) && this.IsBulletCurtainOnList.get_Item(channel);
	}

	public bool CheckIsBulletCurtainShow(int channel)
	{
		return this.IsBulletCurtainOnList.ContainsKey(channel);
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		this.BulletCurtainNews.Clear();
		this.ClearBulletCurtainUnits();
	}

	protected override void AddListener()
	{
	}

	private void CheckStack()
	{
		if (this.BulletCurtainNews.get_Count() > 0)
		{
			this.PopOne();
		}
	}

	private void PopOne()
	{
		if (this.BulletCurtainNews.get_Count() > 0)
		{
			ChatManager.ChatInfo chatInfo = this.BulletCurtainNews.get_Item(0);
			this.BulletCurtainNews.RemoveAt(0);
			if (this.CheckIsBulletCurtainOn(chatInfo.src_channel))
			{
				ChatInfo2BulletCurtain gridUI = this.CreatePrefab2BulletCurtain();
				this.BulletCurtainUnits.Add(gridUI);
				gridUI.SetChat(chatInfo, delegate
				{
					this.BulletCurtainUnits.Remove(gridUI);
					this.Reuse2BulletCurtains(gridUI);
				});
			}
			else
			{
				this.PopOne();
			}
		}
	}

	private void ClearBulletCurtainUnits()
	{
		for (int i = 0; i < this.BulletCurtainUnits.get_Count(); i++)
		{
			this.Reuse2BulletCurtains(this.BulletCurtainUnits.get_Item(i));
		}
		this.BulletCurtainUnits.Clear();
	}

	private void ShowBulletCurtainUnits(int channel, bool isShow)
	{
		for (int i = 0; i < this.BulletCurtainUnits.get_Count(); i++)
		{
			if (!(this.BulletCurtainUnits.get_Item(i) == null))
			{
				if (this.BulletCurtainUnits.get_Item(i).m_srcChannel == channel)
				{
					this.BulletCurtainUnits.get_Item(i).get_gameObject().get_transform().set_localScale((!isShow) ? Vector3.get_zero() : Vector3.get_one());
				}
			}
		}
	}

	private static void CreatePools()
	{
		BulletCurtainManager.Pool2BulletCurtain = new GameObject("Pool2BulletCurtain").get_transform();
		BulletCurtainManager.Pool2BulletCurtain.set_parent(UINodesManager.T2RootOfSpecial);
		BulletCurtainManager.Pool2BulletCurtain.set_localPosition(Vector3.get_zero());
		BulletCurtainManager.Pool2BulletCurtain.set_localRotation(Quaternion.get_identity());
		BulletCurtainManager.Pool2BulletCurtain.set_localScale(Vector3.get_one());
		BulletCurtainManager.BulletCurtainPool = new UIPool("ChatInfo2BulletCurtain", BulletCurtainManager.Pool2BulletCurtain, false);
	}

	public void Reuse2BulletCurtains(ChatInfo2BulletCurtain gridUI)
	{
		if (gridUI != null && gridUI.get_gameObject() != null)
		{
			gridUI.Clear();
			BulletCurtainManager.BulletCurtainPool.ReUse(gridUI.get_gameObject());
		}
	}

	public ChatInfo2BulletCurtain CreatePrefab2BulletCurtain()
	{
		ChatInfo2BulletCurtain chatInfo2BulletCurtain = BulletCurtainManager.BulletCurtainPool.Get(string.Empty).AddMissingComponent<ChatInfo2BulletCurtain>();
		chatInfo2BulletCurtain.get_gameObject().get_transform().set_localScale(Vector3.get_one());
		return chatInfo2BulletCurtain;
	}

	public void Add2BulletCurtain(ChatManager.ChatInfo chatInfo)
	{
		if (this.CheckIsBulletCurtainOn(chatInfo.src_channel))
		{
			this.BulletCurtainNews.Add(chatInfo);
		}
	}
}
