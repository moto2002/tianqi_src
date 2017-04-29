using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadarMapUIView : UIBase
{
	public static RadarMapUIView Instance;

	private UIPool m_poolFlags;

	private UIPool m_poolPaths;

	private UIPool m_poolFlagNpcs;

	private RectTransform mFlagSelf;

	private RectTransform mFlagPoint;

	private Image m_spFlagPoint;

	private Transform mFlagZero;

	private RawImage m_texRadarMap;

	private Image m_spName;

	private ListPool m_poolTeleport;

	private List<GameObject> list_flag_npcs = new List<GameObject>();

	private List<GameObject> list_flags = new List<GameObject>();

	private Vector3 mScreenPositionZero = Vector3.get_zero();

	private List<Vector3> m_rough_paths;

	private float mRefreshTime;

	private List<GameObject> m_listPathPoint = new List<GameObject>();

	private int path_index;

	private RadarTransferItem mPreviousTeleport;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isEndNav = false;
		this.isInterruptStick = true;
	}

	private void Awake()
	{
		RadarMapUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.m_poolFlags = new UIPool("RadarFlagMonster", base.FindTransform("FlagMonsters"), false);
		this.m_poolPaths = new UIPool("RadarPath", base.FindTransform("Paths"), false);
		this.m_poolFlagNpcs = new UIPool("RadarFlagNpcs", base.FindTransform("FlagNpcs"), false);
		base.FindTransform("RadarMap").GetComponent<ButtonCustom>().onClickEventData = new ButtonCustom.VoidDelegateEventData(this.ClickNav);
		this.InitScreenPositionZero();
		this.m_poolTeleport = base.FindTransform("TransferItems").GetComponent<ListPool>();
		this.m_poolTeleport.SetItem("RadarTransferItem");
		this.SetCitys();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mFlagSelf = (base.FindTransform("FlagSelf") as RectTransform);
		this.mFlagPoint = (base.FindTransform("FlagPoint") as RectTransform);
		this.m_spFlagPoint = base.FindTransform("FlagPoint").GetComponent<Image>();
		this.m_texRadarMap = base.FindTransform("RadarMap").GetComponent<RawImage>();
		this.m_texRadarMap.get_rectTransform().set_sizeDelta(RadarManager.size_mapImage_minmap);
		this.m_spName = base.FindTransform("Name").GetComponent<Image>();
		RectTransform rectTransform = base.FindTransform("RadarMapBG") as RectTransform;
		rectTransform.set_sizeDelta(new Vector2(rectTransform.get_sizeDelta().x, (float)UIConst.GetRealScreenSizeHeight() - 75f));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		CurrenciesUIViewModel.Show(true);
		CurrenciesUIViewModel.Instance.SetSubUI(true, ResourceManager.GetCodeSprite(110042), string.Empty, delegate
		{
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}, false);
		ResourceManager.SetTexture(this.m_texRadarMap, RadarManager.Instance.Minimap);
		this.SetName();
		this.RefreshBossFlags();
		this.RefreshPathPointNow();
		this.SetSelected();
		RadarMapUIView.initBusinessmanNPC(this.list_flag_npcs, this.m_poolFlagNpcs, new Vector3(0.8f, 0.8f, 0.8f));
	}

	public static void initBusinessmanNPC(List<GameObject> listFlagNpcs, UIPool poolFlagNpcs, Vector3 scale)
	{
		for (int i = 0; i < listFlagNpcs.get_Count(); i++)
		{
			poolFlagNpcs.ReUse(listFlagNpcs.get_Item(i));
		}
		listFlagNpcs.Clear();
		List<ActorNPC> nPCLogicList = NPCManager.Instance.NPCLogicList;
		for (int j = 0; j < nPCLogicList.get_Count(); j++)
		{
			ActorNPC actorNPC = nPCLogicList.get_Item(j);
			string name = actorNPC.get_name();
			int num = 0;
			try
			{
				num = Convert.ToInt32(name);
			}
			catch
			{
				Debug.LogWarning("字符串转整形异常...");
			}
			NPC nPC = null;
			if (DataReader<NPC>.Contains(num))
			{
				nPC = DataReader<NPC>.Get(num);
			}
			if (nPC != null && nPC.function.get_Count() > 0 && RadarMapUIView.isShowNpcFlag(nPC.function.get_Item(0)))
			{
				GameObject gameObject = poolFlagNpcs.Get(string.Empty);
				int mapPic = nPC.mapPic;
				if (mapPic == 0)
				{
					Debug.LogWarning("npc表没有配置mapPic字段,id=" + num);
				}
				ResourceManager.SetSprite(gameObject.GetComponent<Image>(), GameDataUtils.GetIcon(mapPic));
				Vector2 anchoredPosition = RadarManager.Instance.WorldPosToMapPosWithRotation(actorNPC.get_transform().get_position().x, actorNPC.get_transform().get_position().z, RadarManager.size_mapImage_minmap);
				(gameObject.get_transform() as RectTransform).set_anchoredPosition(anchoredPosition);
				(gameObject.get_transform() as RectTransform).set_localScale(scale);
				listFlagNpcs.Add(gameObject);
			}
		}
	}

	public static bool isShowNpcFlag(int npcTypeId)
	{
		return npcTypeId == TransactionNPCManager.Instance.SystemId || npcTypeId == 104;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool callDestroy)
	{
		RadarMapUIView.Instance = null;
		base.ReleaseSelf(true);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(WildBossManagerEvent.ClearBoss, new Callback(this.RefreshBossFlags));
		EventDispatcher.AddListener(WildBossManagerEvent.CreateBoss, new Callback(this.RefreshBossFlags));
		EventDispatcher.AddListener(WildBossManagerEvent.RemoveBoss, new Callback(this.RefreshBossFlags));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(WildBossManagerEvent.ClearBoss, new Callback(this.RefreshBossFlags));
		EventDispatcher.RemoveListener(WildBossManagerEvent.CreateBoss, new Callback(this.RefreshBossFlags));
		EventDispatcher.RemoveListener(WildBossManagerEvent.RemoveBoss, new Callback(this.RefreshBossFlags));
	}

	protected void SetName()
	{
		ResourceManager.SetSprite(this.m_spName, GameDataUtils.GetIcon(RadarManager.Instance.TitleIcon));
		this.m_spName.SetNativeSize();
	}

	public void StopNavSetting()
	{
		this.m_spFlagPoint.set_enabled(false);
		this.ClearPathPoints();
	}

	private void Update()
	{
		this.mFlagSelf.set_anchoredPosition(RadarManager.Instance.GetSelfPosInMap(RadarManager.size_mapImage_minmap));
		this.mFlagSelf.set_localEulerAngles(new Vector3(0f, 0f, RadarManager.Instance.GetSelfRotationZ()));
		this.RefreshPathPointInterval();
	}

	private void RefreshBossFlags()
	{
		for (int i = 0; i < this.list_flags.get_Count(); i++)
		{
			this.m_poolFlags.ReUse(this.list_flags.get_Item(i));
		}
		this.list_flags.Clear();
		for (int j = 0; j < WildBossManager.Instance.BossData.Count; j++)
		{
			WildBossData wildBossData = WildBossManager.Instance.BossData.ElementValueAt(j);
			GameObject gameObject = this.m_poolFlags.Get(string.Empty);
			(gameObject.get_transform() as RectTransform).set_anchoredPosition(RadarManager.Instance.WorldPosToMapPosWithRotation(wildBossData.positionX, wildBossData.positionZ, RadarManager.size_mapImage_minmap));
			this.list_flags.Add(gameObject);
		}
	}

	private void InitScreenPositionZero()
	{
		this.mFlagZero = base.FindTransform("FlagZero");
		this.mScreenPositionZero = CamerasMgr.CameraUI.WorldToScreenPoint(this.mFlagZero.get_position());
	}

	private void ClickNav(PointerEventData eventData)
	{
		RadarManager.Instance.StopNav();
		Vector2 anchoredPosition = new Vector2(eventData.get_position().x - this.mScreenPositionZero.x, eventData.get_position().y - this.mScreenPositionZero.y);
		anchoredPosition = new Vector2(anchoredPosition.x * UIConst.ScreenToUISizeScaleWidth, anchoredPosition.y * UIConst.ScreenToUISizeScaleHeight);
		this.mFlagPoint.set_anchoredPosition(anchoredPosition);
		Vector3 vector = RadarManager.Instance.MapPosToWorldPosWithRotation(anchoredPosition.x, anchoredPosition.y, RadarManager.size_mapImage_minmap);
		if (XUtility.GetRoughPathPoint(vector.x, vector.y, RadarManager.Instance.DISTANCE_3D_INSERT, out this.m_rough_paths))
		{
			this.m_spFlagPoint.set_enabled(true);
			this.SetPathPoints(this.m_rough_paths);
			RadarManager.Instance.BeginNav(vector.x, vector.y, delegate
			{
				RadarManager.Instance.StopNav();
			});
			this.Show(false);
			UIStackManager.Instance.PopUIPrevious(base.uiType);
		}
		else
		{
			UIManagerControl.Instance.ShowToastText("该区域无法到达");
		}
	}

	private void RefreshPathPointInterval()
	{
		if (RadarManager.Instance.IsNaving)
		{
			this.mRefreshTime += Time.get_deltaTime();
			if (this.mRefreshTime < 1f)
			{
				return;
			}
			this.mRefreshTime = 0f;
			this.RefreshPathPointNow();
		}
		else
		{
			this.mRefreshTime = 0f;
			this.StopNavSetting();
		}
	}

	private void RefreshPathPointNow()
	{
		if (RadarManager.Instance.IsNaving)
		{
			this.m_spFlagPoint.set_enabled(true);
			this.mFlagPoint.set_anchoredPosition(RadarManager.Instance.WorldPosToMapPosWithRotation(RadarManager.Instance.WorldPosEnd.x, RadarManager.Instance.WorldPosEnd.y, RadarManager.size_mapImage_minmap));
			if (XUtility.GetRoughPathPoint(RadarManager.Instance.WorldPosEnd.x, RadarManager.Instance.WorldPosEnd.y, RadarManager.Instance.DISTANCE_3D_INSERT, out this.m_rough_paths))
			{
				this.SetPathPoints(this.m_rough_paths);
			}
		}
	}

	private void SetPathPoints(List<Vector3> navKeyPathPoints)
	{
		this.ClearPathPoints();
		Vector3 pre_point = EntityWorld.Instance.ActSelf.FixTransform.get_position();
		if (navKeyPathPoints.get_Count() > 0)
		{
			pre_point = navKeyPathPoints.get_Item(0);
		}
		for (int i = 0; i < navKeyPathPoints.get_Count(); i++)
		{
			Vector3 vector = navKeyPathPoints.get_Item(i);
			if (this.DealKeyPathPoint(pre_point, vector))
			{
				this.InsertPathPoint(pre_point, vector);
				pre_point = vector;
			}
		}
	}

	private bool DealKeyPathPoint(Vector3 pre_point, Vector3 current_point)
	{
		float num = XUtility.DistanceNoY(pre_point, current_point);
		if (num < RadarManager.Instance.DISTANCE_3D_MIN)
		{
			return false;
		}
		this.InstantiatePathPoint(current_point);
		return true;
	}

	private void InsertPathPoint(Vector3 pre_point, Vector3 current_point)
	{
		float num = XUtility.DistanceNoY(pre_point, current_point);
		if (num > RadarManager.Instance.DISTANCE_3D_INSERT)
		{
			int num2 = (int)(num / RadarManager.Instance.DISTANCE_3D_INSERT);
			for (int i = 1; i <= num2; i++)
			{
				this.InstantiatePathPoint(Vector3.Lerp(pre_point, current_point, 1f / (float)num2 * (float)i));
			}
		}
	}

	private void InstantiatePathPoint(Vector3 point)
	{
		GameObject gameObject = this.m_poolPaths.Get(string.Empty);
		(gameObject.get_transform() as RectTransform).set_anchoredPosition(RadarManager.Instance.WorldPosToMapPosWithRotation(point.x, point.z, RadarManager.size_mapImage_minmap));
		gameObject.set_name(this.path_index.ToString());
		this.path_index++;
		this.m_listPathPoint.Add(gameObject);
	}

	private void ClearPathPoints()
	{
		this.path_index = 0;
		for (int i = 0; i < this.m_listPathPoint.get_Count(); i++)
		{
			this.m_poolPaths.ReUse(this.m_listPathPoint.get_Item(i));
		}
		this.m_listPathPoint.Clear();
	}

	public void OnTeleportClick(int id, RadarTransferItem item)
	{
		if (this.mPreviousTeleport != null)
		{
			this.mPreviousTeleport.SetIsSelected(false);
		}
		this.mPreviousTeleport = item;
		this.TeleportToCity(id);
	}

	private void SetCitys()
	{
		this.m_poolTeleport.Create(RadarManager.Instance.RadarItemList.get_Count(), delegate(int index)
		{
			if (index < RadarManager.Instance.RadarItemList.get_Count() && index < this.m_poolTeleport.Items.get_Count())
			{
				RadarTransferItem component = this.m_poolTeleport.Items.get_Item(index).GetComponent<RadarTransferItem>();
				RadarItemMessage radarItemMessage = RadarManager.Instance.RadarItemList.get_Item(index);
				component.mTransferID = radarItemMessage.scene;
				component.SetName(GameDataUtils.GetChineseContent(radarItemMessage.name, false));
				if (radarItemMessage.scene == MySceneManager.Instance.CurSceneID)
				{
					component.SetIsSelected(true);
					this.mPreviousTeleport = component;
				}
			}
		});
	}

	public void SetSelected()
	{
		if (this.mPreviousTeleport != null)
		{
			this.mPreviousTeleport.SetIsSelected(false);
		}
		for (int i = 0; i < this.m_poolTeleport.Items.get_Count(); i++)
		{
			if (!(this.m_poolTeleport.Items.get_Item(i) == null))
			{
				RadarTransferItem component = this.m_poolTeleport.Items.get_Item(i).GetComponent<RadarTransferItem>();
				if (component != null && component.mTransferID == MySceneManager.Instance.CurSceneID)
				{
					this.mPreviousTeleport = component;
					component.SetIsSelected(true);
					return;
				}
			}
		}
	}

	private void TeleportToCity(int cityID)
	{
		if (CityManager.Instance.CurrentCityID == cityID)
		{
			EventDispatcher.Broadcast<int>(CityManagerEvent.ChangeCityByIntegrationHearth, cityID);
		}
		else if (!this.IsTeleportOn(cityID))
		{
			UIManagerControl.Instance.ShowToastText(GameDataUtils.GetChineseContent(517001, false));
			if (this.mPreviousTeleport != null)
			{
				this.mPreviousTeleport.SetIsSelected(false);
			}
		}
		else
		{
			string content = string.Format("是否确定传送到{0}", RadarManager.Instance.GetSceneName(cityID));
			DialogBoxUIViewModel.Instance.ShowAsOKCancel("快速传送", content, delegate
			{
				this.SetSelected();
			}, delegate
			{
				if (CityManager.Instance.CurrentCityID == cityID)
				{
					EventDispatcher.Broadcast<int>(CityManagerEvent.ChangeCityByIntegrationHearth, cityID);
					return;
				}
				RadarManager.Instance.StopNav();
				this.SetSelected();
				EventDispatcher.Broadcast<int>(CityManagerEvent.ChangeCityByIntegrationHearth, cityID);
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
			DialogBoxUIView.Instance.MaskAction = delegate
			{
				this.SetSelected();
			};
		}
	}

	private bool IsTeleportOn(int cityID)
	{
		ZhuChengPeiZhi zhuChengPeiZhi = DataReader<ZhuChengPeiZhi>.Get(cityID);
		return zhuChengPeiZhi != null && zhuChengPeiZhi.teleport == 1;
	}
}
