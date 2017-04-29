using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTextAddManager
{
	public class EventNames
	{
		public const string EnableFloating = "EventNames.EnableFloating";
	}

	private static FloatTextAddManager m_instance;

	private List<Hashtable> listQueue = new List<Hashtable>();

	private float perReleaseTime = 0.15f;

	private float perCheckTime = 0.3f;

	private float timeCal;

	private bool _EnableFloating = true;

	public static bool IsItemTipOn;

	public static UIPool FloatTextPool;

	public static Transform Pool2FloatText;

	public static FloatTextAddManager Instance
	{
		get
		{
			if (FloatTextAddManager.m_instance == null)
			{
				FloatTextAddManager.m_instance = new FloatTextAddManager();
			}
			return FloatTextAddManager.m_instance;
		}
	}

	private bool EnableFloating
	{
		get
		{
			return this._EnableFloating;
		}
		set
		{
			this._EnableFloating = value;
		}
	}

	public FloatTextAddManager()
	{
		FloatTextAddManager.CreatePools();
		TimerHeap.AddTimer(0u, (int)(this.perCheckTime * 1000f), delegate
		{
			this.CheckQueue();
		});
	}

	public void Init()
	{
		EventDispatcher.AddListener<bool>("EventNames.EnableFloating", new Callback<bool>(this.OnEnableFloating));
	}

	private void OnEnableFloating(bool floating)
	{
		this.EnableFloating = floating;
	}

	private static void CreatePools()
	{
		FloatTextAddManager.Pool2FloatText = new GameObject("Pool2FloatText").get_transform();
		FloatTextAddManager.Pool2FloatText.set_parent(UINodesManager.T2RootOfSpecial);
		UGUITools.ResetTransform(FloatTextAddManager.Pool2FloatText);
		FloatTextAddManager.FloatTextPool = new UIPool("FloatTextAddUI", FloatTextAddManager.Pool2FloatText, false);
	}

	public void AddFloatText(string text, Color textColor)
	{
		if (!this.EnableFloating)
		{
			return;
		}
		Hashtable hashtable = new Hashtable();
		hashtable.Add("Text", text);
		hashtable.Add("Color", textColor);
		this.listQueue.Add(hashtable);
	}

	private void CheckQueue()
	{
		if (this.listQueue.get_Count() > 0)
		{
			this.timeCal += this.perCheckTime;
			if (this.timeCal > this.perReleaseTime)
			{
				this.timeCal = 0f;
				this.PopOne();
			}
		}
	}

	private void PopOne()
	{
		if (!this.EnableFloating)
		{
			return;
		}
		Hashtable hashtable = this.listQueue.get_Item(0);
		string text = (string)hashtable.get_Item("Text");
		Color col = (Color)hashtable.get_Item("Color");
		FloatTextAddManager.FloatTextPool.Get(string.Empty).AddUniqueComponent<FloatTextUnit>().ShowAsFloatText(text, col);
		this.listQueue.RemoveAt(0);
	}
}
