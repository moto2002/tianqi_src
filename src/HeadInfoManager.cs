using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using XNetwork;

public class HeadInfoManager
{
	public class HeadInfoData
	{
		public long uuid;

		public int actorType;

		public bool isShowOfDistance;

		public bool isShowOfLogic = true;

		public bool isShowOfAVC = true;

		public bool isShowName;

		public string name = string.Empty;

		public bool isShowTitle;

		public int titleId;

		public bool isShowGuildTitle;

		public string guildTitle = string.Empty;

		public bool isShowCommonIcon;

		public int commonIcon;

		public bool isBloodBarScene;

		public bool isBloodBarOff = true;

		public bool isBloodBarOff2 = true;

		public int bloodBarType = 1;

		public float bloodFillAmount;

		public List<int> bloodBarSize;

		public bool Show(bool isShow)
		{
			return isShow && this.isShowOfDistance && this.isShowOfAVC && this.isShowOfLogic;
		}

		public bool ShowBloodBar(bool self = true)
		{
			if (self)
			{
				return this.isBloodBarScene && this.isBloodBarOff && this.isBloodBarOff2;
			}
			return this.Show(this.isBloodBarScene && this.isBloodBarOff && this.isBloodBarOff2);
		}
	}

	private static HeadInfoManager instance;

	public static bool IsUpdateLockOn;

	private static UIPool HeadInfoPool;

	public static Transform Pool2HeadInfo;

	private List<HeadInfoManager.HeadInfoData> m_datas = new List<HeadInfoManager.HeadInfoData>();

	private List<HeadInfoUnit> m_uis = new List<HeadInfoUnit>();

	private List<HeadInfoControl> m_controls = new List<HeadInfoControl>();

	private Transform _SelfHeadInfoPosition;

	public static HeadInfoManager Instance
	{
		get
		{
			if (HeadInfoManager.instance == null)
			{
				HeadInfoManager.instance = new HeadInfoManager();
			}
			return HeadInfoManager.instance;
		}
	}

	public Transform SelfHeadInfoPosition
	{
		get
		{
			if (this._SelfHeadInfoPosition == null)
			{
				if (EntityWorld.Instance == null || EntityWorld.Instance.ActSelf == null)
				{
					return null;
				}
				this._SelfHeadInfoPosition = EntityWorld.Instance.ActSelf.FixTransform.FindChild(BillboardManager.HeadInfoPositionName);
				if (this._SelfHeadInfoPosition == null)
				{
					this._SelfHeadInfoPosition = BillboardManager.AddHeadInfoPosition(EntityWorld.Instance.ActSelf.FixTransform, 0f);
				}
			}
			return this._SelfHeadInfoPosition;
		}
		set
		{
			this._SelfHeadInfoPosition = null;
		}
	}

	private HeadInfoManager()
	{
		HeadInfoManager.CreatePools();
	}

	private static void CreatePools()
	{
		Transform transform = new GameObject("Pool2HeadInfo").get_transform();
		transform.set_parent(UINodesManager.NoEventsUIRoot);
		transform.get_gameObject().set_layer(LayerSystem.NameToLayer("UI"));
		HeadInfoManager.Pool2HeadInfo = transform;
		UGUITools.ResetTransform(HeadInfoManager.Pool2HeadInfo);
		HeadInfoManager.HeadInfoPool = new UIPool("HeadInfoUnit", HeadInfoManager.Pool2HeadInfo, false);
		TimerHeap.AddTimer(10000u, 2500, delegate
		{
			HeadInfoManager.Instance.ResortOfZ2D();
		});
	}

	public void Init()
	{
		EventDispatcher.AddListener<long, int>("BillboardManager.Title", new Callback<long, int>(this.SetTitle));
		EventDispatcher.AddListener<long, string>("BillboardManager.GuildTitle", new Callback<long, string>(this.SetGuildTitle));
		EventDispatcher.AddListener<long, int>("BillboardManager.MilitaryRank", new Callback<long, int>(this.SetMilitaryRank));
		NetworkManager.AddListenEvent<RoleInfoBroadcastNty>(new NetCallBackMethod<RoleInfoBroadcastNty>(this.OnRoleInfoBroadcastNty));
	}

	public void AddHeadInfo(int actorType, Transform actorRoot, float height, long uuid, bool avc_control, bool isShowOfLogic)
	{
		if (!SystemConfig.IsBillboardOn)
		{
			return;
		}
		if (EntityWorld.Instance.EntSelf == null)
		{
			return;
		}
		this.RemoveHeadInfo(uuid);
		if (this.IsNoHeadInfoType(actorType))
		{
			return;
		}
		if (uuid == EntityWorld.Instance.EntSelf.ID)
		{
			this.SelfHeadInfoPosition = null;
		}
		HeadInfoManager.HeadInfoData headInfoData = this.AddData(uuid, avc_control);
		headInfoData.uuid = uuid;
		headInfoData.actorType = actorType;
		headInfoData.isShowOfLogic = isShowOfLogic;
		HeadInfoUnit headInfoUnit = this.AddUI(actorRoot, height, uuid);
		headInfoUnit.actorType = actorType;
		this.AddControl(actorRoot, height, uuid, headInfoUnit.get_transform(), actorType, avc_control);
	}

	public void RemoveHeadInfo(long uuid)
	{
		for (int i = 0; i < this.m_datas.get_Count(); i++)
		{
			HeadInfoManager.HeadInfoData headInfoData = this.m_datas.get_Item(i);
			if (headInfoData.uuid == uuid)
			{
				this.m_datas.RemoveAt(i);
				break;
			}
		}
		for (int j = 0; j < this.m_uis.get_Count(); j++)
		{
			HeadInfoUnit headInfoUnit = this.m_uis.get_Item(j);
			if (!(headInfoUnit == null))
			{
				if (headInfoUnit.uuid == uuid)
				{
					headInfoUnit.ResetAll();
					HeadInfoManager.HeadInfoPool.ReUse(headInfoUnit.get_gameObject());
					this.m_uis.RemoveAt(j);
					break;
				}
			}
		}
		for (int k = 0; k < this.m_controls.get_Count(); k++)
		{
			HeadInfoControl headInfoControl = this.m_controls.get_Item(k);
			if (!(headInfoControl == null))
			{
				if (headInfoControl.uuid == uuid)
				{
					headInfoControl.ResetAll();
					this.m_controls.RemoveAt(k);
					break;
				}
			}
		}
	}

	public void show_control_logic(long uuid, bool isShow)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		if (data.isShowOfLogic == isShow)
		{
			return;
		}
		data.isShowOfLogic = isShow;
		HeadInfoUnit uI = this.GetUI(uuid);
		this.UpdateShow(data, uI);
	}

	public void show_control_normal(long uuid, bool isShow)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		if (data.isShowOfDistance == isShow)
		{
			return;
		}
		data.isShowOfDistance = isShow;
		HeadInfoUnit uI = this.GetUI(uuid);
		this.UpdateShow(data, uI);
	}

	public void show_control_actorvisible(long uuid, bool isShow)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		if (data.isShowOfAVC == isShow)
		{
			return;
		}
		data.isShowOfAVC = isShow;
		HeadInfoUnit uI = this.GetUI(uuid);
		this.UpdateShow(data, uI);
	}

	public bool IsControlOn(long uuid)
	{
		if (uuid > 0L)
		{
			HeadInfoManager.HeadInfoData data = this.GetData(uuid);
			return data != null && (data.isShowName || data.ShowBloodBar(true) || data.isShowTitle) && (data.isShowOfLogic && data.isShowOfAVC);
		}
		return false;
	}

	public void OnRoleInfoBroadcastNty(short state, RoleInfoBroadcastNty down = null)
	{
		if (down != null)
		{
			int type = down.type;
			if (type != 1)
			{
				if (type == 2)
				{
					this.SetGuildTitle(down.roleId, HeadInfoManager.GetGuildTitle(down.guildInfo));
				}
			}
			else if (!string.IsNullOrEmpty(down.newValue))
			{
				this.SetTitle(down.roleId, (int)float.Parse(down.newValue));
			}
			else
			{
				this.SetTitle(down.roleId, 0);
			}
		}
	}

	public void ShowName(long uuid, bool isShow)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		HeadInfoUnit uI = this.GetUI(uuid);
		this.ShowName(uuid, isShow, data, uI);
	}

	public void ShowName(long uuid, bool isShow, HeadInfoManager.HeadInfoData data, HeadInfoUnit ui)
	{
		if (data == null)
		{
			return;
		}
		data.isShowName = isShow;
		if (ui != null)
		{
			ui.ShowName(data.Show(data.isShowName));
		}
	}

	public void SetName(int actorType, long uuid, int level, string name)
	{
		this.SetNameFollowCache(actorType, uuid, string.Concat(new object[]
		{
			"LV.",
			level,
			" ",
			name
		}));
	}

	public void SetName(int actorType, long uuid, string name)
	{
		this.SetNameFollowCache(actorType, uuid, name);
	}

	public void SetName(int actorType, long uuid, int level, string name, int quality)
	{
		this.SetNameFollowCache(actorType, uuid, TextColorMgr.GetColorByQuality(string.Concat(new object[]
		{
			"LV.",
			level,
			" ",
			name
		}), quality));
	}

	public void SetName(int actorType, long uuid, string name, int quality)
	{
		this.SetNameFollowCache(actorType, uuid, TextColorMgr.GetColorByQuality(name, quality));
	}

	private void SetNameFollowCache(int actorType, long uuid, string name)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		data.name = name;
		HeadInfoUnit uI = this.GetUI(uuid);
		if (uI != null)
		{
			uI.SetName(name);
		}
	}

	private int GetNameFontSize(int actorType)
	{
		return 18;
	}

	public void ShowBloodBarByScene(long uuid, bool isShow)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		HeadInfoUnit uI = this.GetUI(uuid);
		this.ShowBloodBarByScene(uuid, isShow, data, uI);
	}

	public void ShowBloodBarByScene(long uuid, bool isShow, HeadInfoManager.HeadInfoData data, HeadInfoUnit ui)
	{
		if (data == null)
		{
			return;
		}
		data.isBloodBarScene = isShow;
		if (ui != null)
		{
			ui.SetAndShowBloodBar(data.ShowBloodBar(false), data.bloodBarType);
		}
	}

	public void ShowBloodBarByOff(long uuid, bool isShow)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		data.isBloodBarOff = isShow;
		HeadInfoUnit uI = this.GetUI(uuid);
		if (uI != null)
		{
			uI.SetAndShowBloodBar(data.ShowBloodBar(false), data.bloodBarType);
		}
	}

	public void SetBloodBarType(long uuid, int type)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		data.bloodBarType = type;
		HeadInfoUnit uI = this.GetUI(uuid);
		if (uI != null)
		{
			uI.SetAndShowBloodBar(data.ShowBloodBar(false), data.bloodBarType);
		}
	}

	public void SetBloodBar(long uuid, float fillAmount, bool isLogicShow = true)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		data.isBloodBarOff2 = isLogicShow;
		data.bloodFillAmount = fillAmount;
		HeadInfoUnit uI = this.GetUI(uuid);
		if (uI != null)
		{
			uI.SetBloodBar(fillAmount);
			uI.SetAndShowBloodBar(data.ShowBloodBar(false), data.bloodBarType);
		}
	}

	public void SetBloodBarSize(long uuid, List<int> bloodBarSize)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		data.bloodBarSize = bloodBarSize;
		HeadInfoUnit uI = this.GetUI(uuid);
		if (uI != null)
		{
			uI.SetBloodBarSize(bloodBarSize);
		}
	}

	public void SetTitle(long uuid, int titleId)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		data.titleId = titleId;
		HeadInfoUnit uI = this.GetUI(uuid);
		if (uI != null)
		{
			uI.SetAndShowTitle(data.Show(data.isShowTitle), titleId);
		}
	}

	public void ShowTitle(long uuid, bool isShow)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		HeadInfoUnit uI = this.GetUI(uuid);
		this.ShowTitle(uuid, isShow, data, uI);
	}

	public void ShowTitle(long uuid, bool isShow, HeadInfoManager.HeadInfoData data, HeadInfoUnit ui)
	{
		if (data == null)
		{
			return;
		}
		data.isShowTitle = isShow;
		if (ui != null)
		{
			ui.SetAndShowTitle(data.Show(data.isShowTitle), data.titleId);
		}
	}

	public void SetGuildTitle(long uuid, string guildTitle)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		data.guildTitle = guildTitle;
		HeadInfoUnit uI = this.GetUI(uuid);
		if (uI != null)
		{
			uI.SetAndShowGuildTitle(data.Show(data.isShowGuildTitle), guildTitle);
		}
	}

	public void ShowGuildTitle(long uuid, bool isShow)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		HeadInfoUnit uI = this.GetUI(uuid);
		this.ShowGuildTitle(uuid, isShow, data, uI);
	}

	public void ShowGuildTitle(long uuid, bool isShow, HeadInfoManager.HeadInfoData data, HeadInfoUnit ui)
	{
		if (data == null)
		{
			return;
		}
		data.isShowGuildTitle = isShow;
		if (ui != null)
		{
			ui.SetAndShowGuildTitle(data.Show(data.isShowGuildTitle), data.guildTitle);
		}
	}

	public void SetMilitaryRank(long uuid, int militaryRankId)
	{
		int iconId = 0;
		if (militaryRankId > 0)
		{
			MilitaryRank militaryRank = DataReader<MilitaryRank>.Get(militaryRankId);
			if (militaryRank != null)
			{
				iconId = militaryRank.icon;
			}
		}
		this.SetCommonIcon(uuid, iconId);
	}

	public void SetCommonIcon(long uuid, int iconId)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		if (data == null)
		{
			return;
		}
		data.commonIcon = iconId;
		HeadInfoUnit uI = this.GetUI(uuid);
		if (uI != null)
		{
			uI.SetAndShowCommonIcon(data.Show(data.isShowCommonIcon), iconId);
		}
	}

	public void ShowCommonIcon(long uuid, bool isShow)
	{
		HeadInfoManager.HeadInfoData data = this.GetData(uuid);
		HeadInfoUnit uI = this.GetUI(uuid);
		this.ShowCommonIcon(uuid, isShow, data, uI);
	}

	public void ShowCommonIcon(long uuid, bool isShow, HeadInfoManager.HeadInfoData data, HeadInfoUnit ui)
	{
		if (data == null)
		{
			return;
		}
		data.isShowCommonIcon = isShow;
		if (ui != null)
		{
			ui.SetAndShowCommonIcon(data.Show(data.isShowCommonIcon), data.commonIcon);
		}
	}

	private HeadInfoManager.HeadInfoData AddData(long uuid, bool avc_control)
	{
		HeadInfoManager.HeadInfoData headInfoData = new HeadInfoManager.HeadInfoData();
		headInfoData.uuid = uuid;
		if (EntityWorld.Instance.EntSelf != null && !EntityWorld.Instance.EntSelf.IsInBattle && uuid != EntityWorld.Instance.EntSelf.ID)
		{
			headInfoData.isShowOfAVC = ActorVisibleManager.Instance.IsShow(uuid);
		}
		this.m_datas.Add(headInfoData);
		return headInfoData;
	}

	private HeadInfoUnit AddUI(Transform parent, float height, long uuid)
	{
		GameObject gameObject = HeadInfoManager.HeadInfoPool.Get(string.Empty);
		if (gameObject == null)
		{
			return null;
		}
		HeadInfoUnit component = gameObject.GetComponent<HeadInfoUnit>();
		component.uuid = uuid;
		component.set_enabled(true);
		this.m_uis.Add(component);
		return component;
	}

	private HeadInfoControl AddControl(Transform parent, float height, long uuid, Transform ui, int actorType, bool avc_control)
	{
		Transform transform = BillboardManager.AddHeadInfoPosition(parent, height);
		HeadInfoControl headInfoControl = transform.get_gameObject().AddMissingComponent<HeadInfoControl>();
		headInfoControl.uuid = uuid;
		headInfoControl.m_isInAVC = avc_control;
		headInfoControl.m_headInfoUI = ui;
		headInfoControl.m_actorType = actorType;
		headInfoControl.set_enabled(true);
		this.m_controls.Add(headInfoControl);
		return headInfoControl;
	}

	public HeadInfoManager.HeadInfoData GetData(long uuid)
	{
		for (int i = 0; i < this.m_datas.get_Count(); i++)
		{
			HeadInfoManager.HeadInfoData headInfoData = this.m_datas.get_Item(i);
			if (headInfoData.uuid == uuid)
			{
				return headInfoData;
			}
		}
		return null;
	}

	public HeadInfoUnit GetUI(long uuid)
	{
		for (int i = 0; i < this.m_uis.get_Count(); i++)
		{
			HeadInfoUnit headInfoUnit = this.m_uis.get_Item(i);
			if (headInfoUnit.uuid == uuid)
			{
				return headInfoUnit;
			}
		}
		return null;
	}

	private HeadInfoControl GetControl(long uuid)
	{
		for (int i = 0; i < this.m_controls.get_Count(); i++)
		{
			HeadInfoControl headInfoControl = this.m_controls.get_Item(i);
			if (headInfoControl.uuid == uuid)
			{
				return headInfoControl;
			}
		}
		return null;
	}

	private void UpdateShow(HeadInfoManager.HeadInfoData data, HeadInfoUnit ui)
	{
		if (data == null || ui == null)
		{
			return;
		}
		ui.ShowName(data.Show(data.isShowName));
		ui.SetAndShowTitle(data.Show(data.isShowTitle), data.titleId);
		ui.SetAndShowGuildTitle(data.Show(data.isShowGuildTitle), data.guildTitle);
		ui.ShowCommonIcon(data.Show(data.isShowCommonIcon), data.commonIcon);
		ui.SetAndShowBloodBar(data.ShowBloodBar(false), data.bloodBarType);
		HeadInfoControl control = this.GetControl(data.uuid);
		if (control != null)
		{
			control.UpdatePos();
		}
	}

	private void ResortOfZ2D()
	{
		if (CamerasMgr.CameraMain == null || !CamerasMgr.CameraMain.get_enabled())
		{
			return;
		}
		if (CamerasMgr.CameraUI == null || !CamerasMgr.CameraUI.get_enabled())
		{
			return;
		}
		if (this.m_controls.get_Count() <= 1)
		{
			return;
		}
		Utils.RemoveNull<HeadInfoControl>(this.m_controls);
		this.m_controls.Sort((HeadInfoControl a, HeadInfoControl b) => CamerasMgr.CameraUI.ScreenToViewportPoint(CamerasMgr.CameraMain.WorldToScreenPoint(b.get_transform().get_position())).z.CompareTo(CamerasMgr.CameraUI.ScreenToViewportPoint(CamerasMgr.CameraMain.WorldToScreenPoint(a.get_transform().get_position())).z));
		HeadInfoManager.IsUpdateLockOn = true;
		int num = 0;
		while (num < this.m_controls.get_Count() && num < this.m_uis.get_Count())
		{
			HeadInfoControl headInfoControl = this.m_controls.get_Item(num);
			HeadInfoUnit headInfoUnit = this.m_uis.get_Item(num);
			HeadInfoManager.HeadInfoData data = this.GetData(headInfoControl.uuid);
			if (data != null)
			{
				if (headInfoControl != null && headInfoUnit != null)
				{
					headInfoUnit.uuid = data.uuid;
					headInfoUnit.actorType = data.actorType;
					headInfoControl.m_headInfoUI = headInfoUnit.get_transform();
					headInfoControl.m_actorType = data.actorType;
					HeadInfoControl control = this.GetControl(data.uuid);
					if (control != null)
					{
						control.UpdatePos();
					}
				}
			}
			num++;
		}
		HeadInfoManager.IsUpdateLockOn = false;
		for (int i = 0; i < this.m_uis.get_Count(); i++)
		{
			HeadInfoUnit headInfoUnit2 = this.m_uis.get_Item(i);
			if (headInfoUnit2 != null)
			{
				headInfoUnit2.RefreshAll();
			}
		}
	}

	public static bool IsActorOcclusion(Vector3 actorPos2UI, Vector3 targetPos2UI)
	{
		return -100f < actorPos2UI.x - targetPos2UI.x && 130f > actorPos2UI.x - targetPos2UI.x && actorPos2UI.y - targetPos2UI.y > -220f && actorPos2UI.y - targetPos2UI.y < 1000f;
	}

	public static string GetGuildTitle(GuildInfo guildInfo)
	{
		if (guildInfo == null || guildInfo.titles.get_Count() == 0)
		{
			return string.Empty;
		}
		return HeadInfoManager.GetGuildTitle(guildInfo.guildName, guildInfo.titles.get_Item(0));
	}

	public static string GetGuildTitle(string guildName, int titleId)
	{
		return "[" + guildName + "]" + HeadInfoManager.GetGuildTitle(titleId);
	}

	private static string GetGuildTitle(int titleId)
	{
		return GameDataUtils.GetChineseContent(titleId + 515100, false);
	}

	private bool IsNoHeadInfoType(int actorType)
	{
		return actorType == 51 || actorType == 53;
	}
}
