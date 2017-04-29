using GameData;
using System;
using System.Collections.Generic;

public class RadarBattleManager : BaseSubSystemManager, IRadarMessage
{
	protected int minimapName;

	protected string minimap = string.Empty;

	protected int titleIcon;

	protected List<float> anchorPoint;

	private List<RadarItemMessage> radarItemList;

	protected int linkWay;

	protected static RadarBattleManager instance;

	public int MinimapName
	{
		get
		{
			return this.minimapName;
		}
	}

	public string Minimap
	{
		get
		{
			return this.minimap;
		}
	}

	public int TitleIcon
	{
		get
		{
			return this.titleIcon;
		}
	}

	public List<float> AnchorPoint
	{
		get
		{
			return this.anchorPoint;
		}
	}

	public List<RadarItemMessage> RadarItemList
	{
		get
		{
			return null;
		}
	}

	public int LinkWay
	{
		get
		{
			return this.linkWay;
		}
	}

	public static RadarBattleManager Instance
	{
		get
		{
			if (RadarBattleManager.instance == null)
			{
				RadarBattleManager.instance = new RadarBattleManager();
			}
			return RadarBattleManager.instance;
		}
	}

	protected RadarBattleManager()
	{
	}

	public override void Init()
	{
		base.Init();
	}

	public override void Release()
	{
		if (this.radarItemList != null)
		{
			this.radarItemList.Clear();
			this.radarItemList = null;
		}
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.LoadSceneEnd));
	}

	private void LoadSceneEnd(int scene)
	{
		if (this.TryUpdateData(scene))
		{
			RadarManager.Instance.SetMessage(this);
		}
	}

	protected bool TryUpdateData(int scene)
	{
		if (!DataReader<Scene>.Contains(scene))
		{
			return false;
		}
		Scene scene2 = DataReader<Scene>.Get(scene);
		if (scene2.anchorPoint.get_Count() == 0)
		{
			return false;
		}
		this.minimapName = scene2.name;
		this.titleIcon = scene2.title;
		this.minimap = scene2.miniMap;
		this.anchorPoint = scene2.anchorPoint;
		this.linkWay = scene2.@interface;
		return true;
	}
}
