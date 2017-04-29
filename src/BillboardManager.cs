using System;
using System.Collections.Generic;
using UnityEngine;
using XEngineActor;

public class BillboardManager : BaseSubSystemManager
{
	public class EventNames
	{
		public const string RemoveBillboards = "BillboardManager.RemoveBillboards";

		public const string ShowBillboardsInfo = "BillboardManager.ShowBillboardsInfo";

		public const string Title = "BillboardManager.Title";

		public const string GuildTitle = "BillboardManager.GuildTitle";

		public const string MilitaryRank = "BillboardManager.MilitaryRank";
	}

	public enum BillboardInfoOffOption
	{
		WaveBlood = 1,
		Arrow,
		HeadInfo,
		BubbleDialogue,
		Shadow,
		BloodBar
	}

	public const int IntervalToSort = 2500;

	private const float HEIGHT_SCALE = 10f;

	private const float MAX_AVC_DISTANCE = 15f;

	private const float MAX_AVC_DISTANCE_NPC = 25f;

	public static readonly string HeadInfoPositionName = "Position2HeadInfo";

	private static BillboardManager instance;

	private static List<GameObject> listSort = new List<GameObject>();

	protected Dictionary<long, List<int>> listBillboardInfoOff = new Dictionary<long, List<int>>();

	public static BillboardManager Instance
	{
		get
		{
			if (BillboardManager.instance == null)
			{
				BillboardManager.instance = new BillboardManager();
				HeadInfoManager.Instance.Init();
				WaveBloodManager.Instance.Init();
				BubbleDialogueManager.Instance.Init();
				ArrowManager.Instance.Init();
			}
			return BillboardManager.instance;
		}
	}

	private BillboardManager()
	{
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
		EventDispatcher.AddListener<long, Transform>("BillboardManager.RemoveBillboards", new Callback<long, Transform>(this.RemoveBillboards));
		EventDispatcher.AddListener<long, bool>("BillboardManager.ShowBillboardsInfo", new Callback<long, bool>(this.ShowBillboardsInfo));
	}

	private void RemoveBillboards(long uuid, Transform actorRoot)
	{
		BillboardManager.Instance.RemoveBillboardsInfo(uuid, actorRoot);
		WaveBloodManager.Instance.RemoveWaveBloodControlByKey(uuid);
	}

	public void AddBillboardsInfo(int actorType, Transform actorRoot, float height, long uuid, bool isTarget = false, bool avc_control = true, bool isShowOfLogic = true)
	{
		ArrowManager.Instance.AddEnemy(actorType, actorRoot, null, height, uuid, isTarget, isShowOfLogic);
		HeadInfoManager.Instance.AddHeadInfo(actorType, actorRoot, height, uuid, avc_control, isShowOfLogic);
	}

	public void AddBillboardsInfo(int actorType, ActorParent actorParent, float height, long uuid, bool isTarget = false, bool avc_control = true, bool isShowOfLogic = true)
	{
		ArrowManager.Instance.AddEnemy(actorType, actorParent.FixTransform, actorParent, height, uuid, isTarget, isShowOfLogic);
		HeadInfoManager.Instance.AddHeadInfo(actorType, actorParent.FixTransform, height, uuid, avc_control, isShowOfLogic);
	}

	public void RemoveBillboardsInfo(long uuid, Transform actorRoot)
	{
		ArrowManager.Instance.RemoveEnemy(uuid, actorRoot);
		HeadInfoManager.Instance.RemoveHeadInfo(uuid);
		this.RemoveBillboardInfoOptionsOff(uuid);
	}

	public void ShowBillboardsInfo(long uuid, bool isShow)
	{
		HeadInfoManager.Instance.show_control_logic(uuid, isShow);
		ArrowManager.Instance.Show(uuid, isShow);
	}

	public static Transform AddHeadInfoPosition(Transform actorRoot, float height)
	{
		Transform transform = actorRoot.FindChild(BillboardManager.HeadInfoPositionName);
		if (transform == null)
		{
			GameObject gameObject = new GameObject(BillboardManager.HeadInfoPositionName);
			gameObject.get_transform().set_parent(actorRoot);
			transform = gameObject.get_transform();
			transform.set_localPosition(new Vector3(0f, BillboardManager.GetRealHeight(height), 0f));
		}
		return transform;
	}

	public static float GetRealHeight(float height)
	{
		return height / 10f;
	}

	public static void ResortOfZ(UIPool uipool)
	{
		if (CamerasMgr.CameraMain == null || !CamerasMgr.CameraMain.get_enabled())
		{
			return;
		}
		if (uipool.m_useds.get_Count() <= 1)
		{
			return;
		}
		BillboardManager.listSort.Clear();
		for (int i = 0; i < uipool.m_useds.get_Count(); i++)
		{
			if (uipool.m_useds.get_Item(i) != null)
			{
				BillboardManager.listSort.Add(uipool.m_useds.get_Item(i));
			}
		}
		BillboardManager.listSort.Sort((GameObject a, GameObject b) => CamerasMgr.CameraMain.WorldToScreenPoint(a.get_transform().get_position()).z.CompareTo(CamerasMgr.CameraMain.WorldToScreenPoint(b.get_transform().get_position()).z));
		for (int j = BillboardManager.listSort.get_Count() - 1; j >= 0; j--)
		{
			BillboardManager.listSort.get_Item(j).get_transform().SetAsLastSibling();
		}
	}

	public static float GetDistanceOfAVC(int type)
	{
		if (type == 31)
		{
			return 25f;
		}
		return 15f;
	}

	public void SetBillboardInfoOptionsOff(long uid, List<int> offOptions)
	{
		this.listBillboardInfoOff.set_Item(uid, offOptions);
		HeadInfoManager.Instance.ShowBloodBarByOff(uid, !this.listBillboardInfoOff.get_Item(uid).Contains(6));
	}

	public void SetBillboardInfoOption(long uid, BillboardManager.BillboardInfoOffOption offOption, bool isOff)
	{
		if (!this.listBillboardInfoOff.ContainsKey(uid))
		{
			this.listBillboardInfoOff.Add(uid, new List<int>());
		}
		if (isOff)
		{
			if (offOption == BillboardManager.BillboardInfoOffOption.BloodBar)
			{
				HeadInfoManager.Instance.ShowBloodBarByOff(uid, false);
			}
			if (!this.listBillboardInfoOff.get_Item(uid).Contains((int)offOption))
			{
				this.listBillboardInfoOff.get_Item(uid).Add((int)offOption);
			}
		}
		else
		{
			if (offOption == BillboardManager.BillboardInfoOffOption.BloodBar)
			{
				HeadInfoManager.Instance.ShowBloodBarByOff(uid, true);
			}
			if (this.listBillboardInfoOff.get_Item(uid).Contains((int)offOption))
			{
				this.listBillboardInfoOff.get_Item(uid).Remove((int)offOption);
			}
		}
	}

	public bool IsBillboardInfoOff(long uid, BillboardManager.BillboardInfoOffOption offOption)
	{
		return this.listBillboardInfoOff.ContainsKey(uid) && this.listBillboardInfoOff.get_Item(uid).Contains((int)offOption);
	}

	public void RemoveBillboardInfoOptionsOff(long uid)
	{
		if (this.listBillboardInfoOff.ContainsKey(uid))
		{
			this.listBillboardInfoOff.Remove(uid);
		}
	}

	public void SwitchBillboards(bool isOn)
	{
		HeadInfoManager.Pool2HeadInfo.get_gameObject().SetActive(isOn);
		ArrowManager.Pool2EnemyArrow.get_gameObject().SetActive(isOn);
		WaveBloodManager.Pool2WaveBloodControl.get_gameObject().SetActive(isOn);
	}
}
