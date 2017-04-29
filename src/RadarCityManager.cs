using GameData;
using System;
using System.Collections.Generic;

public class RadarCityManager : BaseSubSystemManager, IRadarMessage
{
	protected int minimapName;

	protected string minimap = string.Empty;

	protected int titleIcon;

	protected List<float> anchorPoint;

	private List<RadarItemMessage> radarItemList;

	protected int linkWay;

	protected static RadarCityManager instance;

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
			if (this.radarItemList == null)
			{
				this.radarItemList = new List<RadarItemMessage>();
				List<ZhuChengPeiZhi> dataList = DataReader<ZhuChengPeiZhi>.DataList;
				for (int i = 0; i < dataList.get_Count(); i++)
				{
					if (dataList.get_Item(i).listDisplay == 1)
					{
						this.radarItemList.Add(new RadarItemMessage
						{
							scene = dataList.get_Item(i).scene,
							name = dataList.get_Item(i).name,
							sortWeight = dataList.get_Item(i).sort
						});
					}
				}
				this.radarItemList.Sort(ComparisonUtility.RadarItemComparison);
			}
			return this.radarItemList;
		}
	}

	public int LinkWay
	{
		get
		{
			return this.linkWay;
		}
	}

	public static RadarCityManager Instance
	{
		get
		{
			if (RadarCityManager.instance == null)
			{
				RadarCityManager.instance = new RadarCityManager();
			}
			return RadarCityManager.instance;
		}
	}

	protected RadarCityManager()
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
		ZhuChengPeiZhi mainCity = this.GetMainCity(scene);
		if (mainCity == null)
		{
			return false;
		}
		this.minimapName = mainCity.name;
		this.titleIcon = mainCity.title;
		this.minimap = mainCity.miniMap;
		this.anchorPoint = mainCity.anchorPoint;
		this.linkWay = mainCity.@interface;
		return true;
	}

	protected ZhuChengPeiZhi GetMainCity(int scene)
	{
		List<ZhuChengPeiZhi> dataList = DataReader<ZhuChengPeiZhi>.DataList;
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			if (dataList.get_Item(i).scene == scene)
			{
				return dataList.get_Item(i);
			}
		}
		return null;
	}
}
