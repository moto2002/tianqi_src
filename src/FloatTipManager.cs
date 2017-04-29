using System;
using System.Collections.Generic;
using UnityEngine;

public class FloatTipManager
{
	public struct FloatTip
	{
		public Transform actorRoot;

		public string content;

		public string color;

		public bool isShowBg;

		public float duration;

		public float duration_alpha;

		public float height;

		public float floatHeight;
	}

	private static FloatTipManager instance;

	public static UIPool FloatTipPool;

	public static Transform Pool2FloatTip;

	private List<FloatTipManager.FloatTip> listStack = new List<FloatTipManager.FloatTip>();

	public static FloatTipManager Instance
	{
		get
		{
			if (FloatTipManager.instance == null)
			{
				FloatTipManager.instance = new FloatTipManager();
			}
			return FloatTipManager.instance;
		}
	}

	private FloatTipManager()
	{
		FloatTipManager.CreatePools();
		TimerHeap.AddTimer(0u, 300, delegate
		{
			this.CheckStack();
		});
	}

	private static void CreatePools()
	{
		Transform transform = new GameObject("Pool2FloatTip").get_transform();
		transform.set_parent(UINodesManager.NoEventsUIRoot);
		transform.get_gameObject().set_layer(LayerSystem.NameToLayer("UI"));
		FloatTipManager.Pool2FloatTip = transform;
		UGUITools.ResetTransform(FloatTipManager.Pool2FloatTip);
		FloatTipManager.FloatTipPool = new UIPool("FloatTipUnit", FloatTipManager.Pool2FloatTip, false);
	}

	private void CheckStack()
	{
		if (this.listStack.get_Count() > 0)
		{
			this.PopOne();
		}
	}

	private void PopOne()
	{
		FloatTipManager.FloatTip floatTip = this.listStack.get_Item(0);
		if (floatTip.actorRoot != null && floatTip.actorRoot.get_gameObject().get_activeSelf())
		{
			FloatTipManager.FloatTipPool.Get(string.Empty).GetComponent<FloatTipUnit>().ShowAsFloatTip(floatTip.actorRoot, floatTip.content, floatTip.color, floatTip.isShowBg, floatTip.duration, floatTip.duration_alpha, floatTip.height, floatTip.floatHeight);
		}
		this.listStack.RemoveAt(0);
	}

	public void AddFloatTip(long uuid, Transform actorRoot, string content, string color = "", bool isShowBg = true, float duration = 2.5f, float duration_alpha = 1f, float height = 1f, float floatHeight = 150f)
	{
		if (!SystemConfig.IsBillboardOn)
		{
			return;
		}
		this.listStack.Add(new FloatTipManager.FloatTip
		{
			actorRoot = actorRoot,
			content = content,
			color = color,
			isShowBg = isShowBg,
			duration = duration,
			duration_alpha = duration_alpha,
			height = height,
			floatHeight = floatHeight
		});
	}
}
