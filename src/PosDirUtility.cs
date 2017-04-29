using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PosDirUtility
{
	public enum PositionFlagType
	{
		Self,
		Player,
		Pet,
		Monster
	}

	public enum DirectionFlagType
	{
		Self,
		Player,
		Pet,
		Monster
	}

	protected static GameObject flagPool;

	protected static int positionFlagRemainTime = 600;

	public static GameObject FlagPool
	{
		get
		{
			if (PosDirUtility.flagPool == null)
			{
				PosDirUtility.flagPool = new GameObject("FlagPool");
				Object.DontDestroyOnLoad(PosDirUtility.flagPool);
				PosDirUtility.flagPool.get_transform().set_localPosition(Vector3.get_zero());
				PosDirUtility.flagPool.get_transform().set_localScale(Vector3.get_one());
			}
			return PosDirUtility.flagPool;
		}
	}

	public static int PositionFlagRemainTime
	{
		get
		{
			return PosDirUtility.positionFlagRemainTime;
		}
		set
		{
			PosDirUtility.positionFlagRemainTime = value;
		}
	}

	public static void AddPositionFlag(PosDirUtility.PositionFlagType positionFlagType, Vector3 position)
	{
		if (!PosDirUtility.CheckPositionFlagEnable(positionFlagType))
		{
			return;
		}
		GameObject go = (GameObject)Object.Instantiate(AssetManager.AssetOfNoPool.LoadAssetNowNoAB(PosDirUtility.GetPositionFlagAssetName(positionFlagType), typeof(Object)));
		go.set_name(PosDirUtility.GetPositionFlagGameObjectName(positionFlagType));
		go.get_transform().set_parent(PosDirUtility.FlagPool.get_transform());
		go.get_transform().set_position(position);
		TimerHeap.AddTimer((uint)PosDirUtility.PositionFlagRemainTime, 0, delegate
		{
			Object.DestroyImmediate(go, true);
		});
	}

	protected static bool CheckPositionFlagEnable(PosDirUtility.PositionFlagType positionFlagType)
	{
		switch (positionFlagType)
		{
		case PosDirUtility.PositionFlagType.Self:
			return SystemConfig.IsSelfSyncPointFlagOn;
		case PosDirUtility.PositionFlagType.Player:
		case PosDirUtility.PositionFlagType.Pet:
		case PosDirUtility.PositionFlagType.Monster:
			return SystemConfig.IsPetAndMonsterSyncPointFlagOn;
		default:
			return false;
		}
	}

	protected static string GetPositionFlagAssetName(PosDirUtility.PositionFlagType positionFlagType)
	{
		switch (positionFlagType)
		{
		case PosDirUtility.PositionFlagType.Self:
			return "Flag_Self";
		case PosDirUtility.PositionFlagType.Player:
			return "Flag_Pet";
		case PosDirUtility.PositionFlagType.Pet:
			return "Flag_Pet";
		case PosDirUtility.PositionFlagType.Monster:
			return "Flag_Monster";
		default:
			return "Flag_Pet";
		}
	}

	protected static string GetPositionFlagGameObjectName(PosDirUtility.PositionFlagType positionFlagType)
	{
		switch (positionFlagType)
		{
		case PosDirUtility.PositionFlagType.Self:
			return "ZZZFlag_Self";
		case PosDirUtility.PositionFlagType.Player:
			return "ZZZFlag_Player";
		case PosDirUtility.PositionFlagType.Pet:
			return "ZZZFlag_Pet";
		case PosDirUtility.PositionFlagType.Monster:
			return "ZZZFlag_Monster";
		default:
			return "ZZZFlag_Pet";
		}
	}

	public static void AddDirectionFlag(PosDirUtility.DirectionFlagType directionFlagType, Transform root)
	{
		if (!PosDirUtility.CheckDirectionFlagEnable(directionFlagType))
		{
			return;
		}
		if (root.FindChild(PosDirUtility.GetDirectionFlagGameObjectName(directionFlagType)))
		{
			return;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(AssetManager.AssetOfNoPool.LoadAssetNowNoAB(PosDirUtility.GetDirectionFlagAssetName(directionFlagType), typeof(Object)));
		gameObject.set_name(PosDirUtility.GetDirectionFlagGameObjectName(directionFlagType));
		gameObject.get_transform().set_parent(root);
		gameObject.get_transform().set_localPosition(new Vector3(0f, 0f, 3f));
		gameObject.get_transform().set_localRotation(Quaternion.Euler(-90f, 0f, 0f));
		gameObject.get_transform().set_localScale(new Vector3(0.1f, 6f, 0.1f));
	}

	protected static bool CheckDirectionFlagEnable(PosDirUtility.DirectionFlagType positionFlagType)
	{
		switch (positionFlagType)
		{
		case PosDirUtility.DirectionFlagType.Self:
		case PosDirUtility.DirectionFlagType.Player:
		case PosDirUtility.DirectionFlagType.Pet:
		case PosDirUtility.DirectionFlagType.Monster:
			return SystemConfig.IsShowMonsterDir;
		default:
			return false;
		}
	}

	protected static string GetDirectionFlagAssetName(PosDirUtility.DirectionFlagType directionFlagType)
	{
		return "Flag_MonsterForward";
	}

	protected static string GetDirectionFlagGameObjectName(PosDirUtility.DirectionFlagType directionFlagType)
	{
		return "Flag_MonsterForward";
	}

	public static string ToDetailString(Vector3 vector)
	{
		return string.Concat(new object[]
		{
			"(",
			vector.x,
			" " + vector.y,
			" ",
			vector.z,
			") "
		});
	}

	public static string ToDetailString(Pos pos)
	{
		return pos.ToString();
	}

	public static string ToDetailString(Vector2 vector)
	{
		return vector.ToString();
	}

	public static Vector3 ToTerrainPoint(Vector3 pos)
	{
		return MySceneManager.GetTerrainPoint(pos.x, pos.z, pos.y);
	}

	public static Vector3 ToTerrainPoint(Vector2 pos, float height)
	{
		return MySceneManager.GetTerrainPoint(pos.x, pos.y, height);
	}

	public static Vector3 ToTerrainPoint(Pos serverPos, float height)
	{
		return MySceneManager.GetTerrainPoint(serverPos.x * 0.01f, serverPos.y * 0.01f, height);
	}

	public static Vector3 ToTerrainPoint(List<int> posData)
	{
		return MySceneManager.GetTerrainPoint((float)posData.get_Item(0) * 0.01f, (float)posData.get_Item(2) * 0.01f, (float)posData.get_Item(1) * 0.01f);
	}

	public static Vector3 ToEulerAnglesFromErrorFormatData(List<int> errorEulerAngleData)
	{
		return new Vector3((float)errorEulerAngleData.get_Item(0) * 0.01f, (float)errorEulerAngleData.get_Item(1) * 0.01f, (float)errorEulerAngleData.get_Item(2) * 0.01f);
	}
}
