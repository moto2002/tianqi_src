using System;
using System.Collections.Generic;
using UnityEngine;

public class RadarManager : BaseSubSystemManager, IRadarMessage
{
	protected IRadarMessage radarMessagehandler = RadarCityManager.Instance;

	private static RadarManager instance;

	private static float map3d_angle = 0f;

	private static float map3d_xMin = 0f;

	private static float map3d_xMax = 0f;

	private static float map3d_zMin = 0f;

	private static float map3d_zMax = 0f;

	private float size_map3d_x;

	private float size_map3d_z;

	public static readonly Vector2 size_mapImage_minmap = new Vector2(512f, 512f);

	public static readonly Vector2 size_mapImage_guildwar = new Vector2(718f, 510f);

	private float DISTANCE_2D_INSERT = 15f;

	private float DISTANCE_2D_MIN = 11f;

	public float DISTANCE_3D_INSERT = 10f;

	public float DISTANCE_3D_MIN = 3f;

	private bool IsNavByRadar;

	public int MinimapName
	{
		get
		{
			return this.radarMessagehandler.MinimapName;
		}
	}

	public string Minimap
	{
		get
		{
			return this.radarMessagehandler.Minimap;
		}
	}

	public int TitleIcon
	{
		get
		{
			return this.radarMessagehandler.TitleIcon;
		}
	}

	public List<float> AnchorPoint
	{
		get
		{
			return this.radarMessagehandler.AnchorPoint;
		}
	}

	public List<RadarItemMessage> RadarItemList
	{
		get
		{
			return this.radarMessagehandler.RadarItemList;
		}
	}

	public int LinkWay
	{
		get
		{
			return this.radarMessagehandler.LinkWay;
		}
	}

	public Vector2 WorldPosEnd
	{
		get
		{
			if (EntityWorld.Instance.ActSelf != null)
			{
				return new Vector2(EntityWorld.Instance.ActSelf.NavMeshPosition.x, EntityWorld.Instance.ActSelf.NavMeshPosition.z);
			}
			return Vector2.get_zero();
		}
	}

	public bool IsNaving
	{
		get
		{
			return EntityWorld.Instance.ActSelf != null && !EntityWorld.Instance.ActSelf.IsClearTargetPosition;
		}
	}

	public static RadarManager Instance
	{
		get
		{
			if (RadarManager.instance == null)
			{
				RadarManager.instance = new RadarManager();
			}
			return RadarManager.instance;
		}
	}

	private RadarManager()
	{
	}

	public override void Init()
	{
		base.Init();
		RadarCityManager.Instance.Init();
		RadarBattleManager.Instance.Init();
	}

	public override void Release()
	{
		RadarCityManager.Instance.Release();
		RadarBattleManager.Instance.Release();
	}

	protected override void AddListener()
	{
	}

	public void SetMessage(IRadarMessage theIRadarMessage)
	{
		this.radarMessagehandler = theIRadarMessage;
		this.UpdateMessage();
	}

	protected void UpdateMessage()
	{
		this.UpdateArthor();
	}

	protected void UpdateArthor()
	{
		if (this.AnchorPoint == null || this.AnchorPoint.get_Count() < 8)
		{
			Debug.LogError("anchorPoint参数不对, 应该有8个值");
		}
		else
		{
			Vector2 vector = new Vector2(this.AnchorPoint.get_Item(0), this.AnchorPoint.get_Item(1));
			Vector2 vector2 = new Vector2(this.AnchorPoint.get_Item(2), this.AnchorPoint.get_Item(3));
			Vector2 position = new Vector2(this.AnchorPoint.get_Item(4), this.AnchorPoint.get_Item(5));
			Vector2 position2 = new Vector2(this.AnchorPoint.get_Item(6), this.AnchorPoint.get_Item(7));
			RadarManager.map3d_angle = Vector2.Angle(vector2 - vector, new Vector2(1f, 0f));
			vector = this.RotationAngle(vector, false);
			vector2 = this.RotationAngle(vector2, false);
			position = this.RotationAngle(position, false);
			position2 = this.RotationAngle(position2, false);
			RadarManager.map3d_xMin = Mathf.Min(new float[]
			{
				vector.x,
				vector2.x,
				position.x,
				position2.x
			});
			RadarManager.map3d_xMax = Mathf.Max(new float[]
			{
				vector.x,
				vector2.x,
				position.x,
				position2.x
			});
			RadarManager.map3d_zMin = Mathf.Min(new float[]
			{
				vector.y,
				vector2.y,
				position.y,
				position2.y
			});
			RadarManager.map3d_zMax = Mathf.Max(new float[]
			{
				vector.y,
				vector2.y,
				position.y,
				position2.y
			});
			this.SetMap3DSize();
		}
	}

	public string GetSceneName(int scene)
	{
		if (this.RadarItemList == null)
		{
			return string.Empty;
		}
		for (int i = 0; i < this.RadarItemList.get_Count(); i++)
		{
			if (this.RadarItemList.get_Item(i).scene == scene)
			{
				return GameDataUtils.GetChineseContent(this.RadarItemList.get_Item(i).name, false);
			}
		}
		return string.Empty;
	}

	private void SetMap3DSize()
	{
		this.size_map3d_x = RadarManager.map3d_xMax - RadarManager.map3d_xMin;
		this.size_map3d_z = RadarManager.map3d_zMax - RadarManager.map3d_zMin;
		this.SetDistanceToPathPoint();
	}

	public Vector2 GetSelfPosInMap(Vector2 size_mapImage)
	{
		if (EntityWorld.Instance.ActSelf == null || EntityWorld.Instance.ActSelf.FixTransform == null)
		{
			return Vector2.get_zero();
		}
		return this.WorldPosToMapPosWithRotation(EntityWorld.Instance.ActSelf.FixTransform.get_position().x, EntityWorld.Instance.ActSelf.FixTransform.get_position().z, size_mapImage);
	}

	public Vector2 WorldPosToMapPosWithRotation(float worldPositionX, float worldPositionZ, Vector2 size_mapImage)
	{
		Vector2 vector = this.RotationAngle(new Vector2(worldPositionX, worldPositionZ), false);
		return this.WorldPosToMapPos(vector.x, vector.y, size_mapImage);
	}

	private Vector2 WorldPosToMapPos(float worldPositionX, float worldPositionZ, Vector2 size_mapImage)
	{
		if (this.size_map3d_x > 0f && this.size_map3d_z > 0f)
		{
			float num = (worldPositionX - RadarManager.map3d_xMin) / this.size_map3d_x;
			float num2 = (worldPositionZ - RadarManager.map3d_zMin) / this.size_map3d_z;
			return new Vector2(size_mapImage.x * num, size_mapImage.y * num2);
		}
		return Vector2.get_zero();
	}

	public Vector2 MapPosToWorldPosWithRotation(float mapPositionX, float mapPositionZ, Vector2 size_mapImage)
	{
		Vector2 position = new Vector2(mapPositionX / size_mapImage.x * this.size_map3d_x + RadarManager.map3d_xMin, mapPositionZ / size_mapImage.y * this.size_map3d_z + RadarManager.map3d_zMin);
		return this.RotationAngle(position, true);
	}

	private Vector2 MapPosToWorldPos(float mapPositionX, float mapPositionZ, Vector2 size_mapImage)
	{
		return new Vector2(mapPositionX / size_mapImage.x * this.size_map3d_x + RadarManager.map3d_xMin, mapPositionZ / size_mapImage.y * this.size_map3d_z + RadarManager.map3d_zMin);
	}

	public float GetSelfRotationZ()
	{
		if (EntityWorld.Instance.ActSelf == null || EntityWorld.Instance.ActSelf.FixTransform == null)
		{
			return 0f;
		}
		float y = EntityWorld.Instance.ActSelf.FixTransform.get_rotation().get_eulerAngles().y;
		return 360f - y - RadarManager.map3d_angle;
	}

	private Vector2 RotationAngle(Vector2 position, bool negative)
	{
		if (negative)
		{
			return XUtility.RotationAngle(position, -RadarManager.map3d_angle);
		}
		return XUtility.RotationAngle(position, RadarManager.map3d_angle);
	}

	private void SetDistanceToPathPoint()
	{
		if (RadarManager.size_mapImage_minmap.x > 0f)
		{
			float num = this.size_map3d_x / RadarManager.size_mapImage_minmap.x;
			this.DISTANCE_3D_INSERT = this.DISTANCE_2D_INSERT * num;
			this.DISTANCE_3D_MIN = this.DISTANCE_2D_MIN * num;
		}
	}

	public void BeginNav(float pointX, float pointZ, Action finishCallback)
	{
		this.IsNavByRadar = true;
		EntityWorld.Instance.EntSelf.NavToSameScenePoint(pointX, pointZ, 0f, delegate
		{
			if (finishCallback != null)
			{
				finishCallback.Invoke();
			}
		});
	}

	public void StopNav()
	{
		this.IsNavByRadar = false;
		EntityWorld.Instance.EntSelf.StopNavToNPC();
		EventDispatcher.Broadcast(EventNames.EndNav);
		EventDispatcher.Broadcast("GuideManager.OnEndNav");
		if (UIManagerControl.Instance.IsOpen("RadarMapUI"))
		{
			RadarMapUIView.Instance.StopNavSetting();
		}
	}

	public bool CheckIsNavByRadar()
	{
		return this.IsNavByRadar;
	}
}
