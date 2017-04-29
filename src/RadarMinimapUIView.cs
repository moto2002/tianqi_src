using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RadarMinimapUIView : BaseUIBehaviour
{
	private UIPool pool_flags;

	private UIPool m_poolFlagNpcs;

	private RectTransform mFlagSelf;

	private RectTransform mRadarMap;

	private RawImage m_texRadarMap;

	private Text m_lblName;

	private List<GameObject> list_flag_npcs = new List<GameObject>();

	private static Vector2 MiniMapHalfSize = new Vector2(68f, 68f);

	private List<GameObject> list_flags = new List<GameObject>();

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.pool_flags = new UIPool("RadarFlagMonster", base.FindTransform("FlagMonsters"), false);
		this.m_poolFlagNpcs = new UIPool("RadarFlagNpcs", base.FindTransform("FlagNpcs"), false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mFlagSelf = (base.FindTransform("FlagSelf") as RectTransform);
		this.mRadarMap = (base.FindTransform("RadarMap") as RectTransform);
		this.m_texRadarMap = base.FindTransform("RadarMap").GetComponent<RawImage>();
		this.m_texRadarMap.get_rectTransform().set_sizeDelta(RadarManager.size_mapImage_minmap);
		this.m_lblName = base.FindTransform("Name").GetComponent<Text>();
		RectTransform rectTransform = base.FindTransform("RadarRegion") as RectTransform;
		RadarMinimapUIView.MiniMapHalfSize = rectTransform.get_sizeDelta() * 0.5f;
		base.FindTransform("RadarRegion").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnRadarRegionClick));
	}

	private void OnEnable()
	{
		ResourceManager.SetTexture(this.m_texRadarMap, RadarManager.Instance.Minimap);
		this.RefreshBossFlags();
		this.SetName(GameDataUtils.GetChineseContent(RadarManager.Instance.MinimapName, false));
		RadarMapUIView.initBusinessmanNPC(this.list_flag_npcs, this.m_poolFlagNpcs, new Vector3(0.8f, 0.8f, 0.8f));
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

	private void OnRadarRegionClick()
	{
		int linkWay = RadarManager.Instance.LinkWay;
		if (linkWay != 1)
		{
			if (linkWay == 2)
			{
				LinkNavigationManager.OpenGuildWarInfoUI();
			}
		}
		else
		{
			LinkNavigationManager.SystemLink(71, true, null);
		}
	}

	private void Update()
	{
		this.mFlagSelf.set_anchoredPosition(RadarManager.Instance.GetSelfPosInMap(RadarManager.size_mapImage_minmap));
		this.mFlagSelf.set_localEulerAngles(new Vector3(0f, 0f, RadarManager.Instance.GetSelfRotationZ()));
		Vector2 vector = -RadarManager.Instance.GetSelfPosInMap(RadarManager.size_mapImage_minmap) + RadarMinimapUIView.MiniMapHalfSize;
		Vector2 vector2 = -RadarManager.size_mapImage_minmap + RadarMinimapUIView.MiniMapHalfSize * 2f;
		this.mRadarMap.set_anchoredPosition(new Vector2(Mathf.Clamp(vector.x, vector2.x, 0f), Mathf.Clamp(vector.y, vector2.y, 0f)));
	}

	private void RefreshBossFlags()
	{
		for (int i = 0; i < this.list_flags.get_Count(); i++)
		{
			this.pool_flags.ReUse(this.list_flags.get_Item(i));
		}
		this.list_flags.Clear();
		for (int j = 0; j < WildBossManager.Instance.BossData.Count; j++)
		{
			WildBossData wildBossData = WildBossManager.Instance.BossData.ElementValueAt(j);
			GameObject gameObject = this.pool_flags.Get(string.Empty);
			(gameObject.get_transform() as RectTransform).set_anchoredPosition(RadarManager.Instance.WorldPosToMapPosWithRotation(wildBossData.positionX, wildBossData.positionZ, RadarManager.size_mapImage_minmap));
			this.list_flags.Add(gameObject);
		}
	}

	public void SetName(string name)
	{
		this.m_lblName.set_text(name);
	}
}
