using System;
using UnityEngine;

public class ResWeight
{
	public enum ResType
	{
		none,
		model,
		fx,
		uiprefab,
		uiatlas,
		spine
	}

	public const int weight_deal_actions = 10;

	public const int weight_mainscene = 600;

	public const int weight_async_release = 100;

	public const int weight_model = 50;

	public const int weight_fx = 10;

	public const int weight_uifx = 20;

	public const int weight_uiprefab = 20;

	public const int weight_uiatlas = 20;

	public const int weight_spine = 20;

	private static int m_total_model;

	private static int m_total_fx;

	private static int m_total_spine;

	private static int m_total_uiprefab;

	private static int m_total_uiatlas;

	private static int m_loaded_model;

	private static int m_loaded_fx;

	private static int m_loaded_uiprefab;

	private static int m_loaded_uiatlas;

	private static int m_loaded_spine;

	public static int total_model
	{
		get
		{
			return ResWeight.m_total_model;
		}
		set
		{
			ResWeight.m_total_model = value;
		}
	}

	public static int total_fx
	{
		get
		{
			return ResWeight.m_total_fx;
		}
		set
		{
			ResWeight.m_total_fx = value;
		}
	}

	public static int total_spine
	{
		get
		{
			return ResWeight.m_total_spine;
		}
		set
		{
			ResWeight.m_total_spine = value;
		}
	}

	public static int total_uiprefab
	{
		get
		{
			return ResWeight.m_total_uiprefab;
		}
		set
		{
			ResWeight.m_total_uiprefab = value;
		}
	}

	public static int total_uiatlas
	{
		get
		{
			return ResWeight.m_total_uiatlas;
		}
		set
		{
			ResWeight.m_total_uiatlas = value;
		}
	}

	public static int loaded_model
	{
		get
		{
			return ResWeight.m_loaded_model;
		}
		set
		{
			ResWeight.m_loaded_model = value;
		}
	}

	public static int loaded_fx
	{
		get
		{
			return ResWeight.m_loaded_fx;
		}
		set
		{
			ResWeight.m_loaded_fx = value;
		}
	}

	public static int loaded_uiprefab
	{
		get
		{
			return ResWeight.m_loaded_uiprefab;
		}
		set
		{
			ResWeight.m_loaded_uiprefab = value;
		}
	}

	public static int loaded_uiatlas
	{
		get
		{
			return ResWeight.m_loaded_uiatlas;
		}
		set
		{
			ResWeight.m_loaded_uiatlas = value;
		}
	}

	public static int loaded_spine
	{
		get
		{
			return ResWeight.m_loaded_spine;
		}
		set
		{
			ResWeight.m_loaded_spine = value;
		}
	}

	public static bool IsLoadFinished(ResWeight.ResType type)
	{
		switch (type)
		{
		case ResWeight.ResType.model:
			return ResWeight.loaded_model >= ResWeight.total_model;
		case ResWeight.ResType.fx:
			return ResWeight.loaded_fx >= ResWeight.total_fx;
		case ResWeight.ResType.uiprefab:
			return ResWeight.loaded_uiprefab >= ResWeight.total_uiprefab;
		case ResWeight.ResType.uiatlas:
			return ResWeight.loaded_uiatlas >= ResWeight.total_uiatlas;
		case ResWeight.ResType.spine:
			return ResWeight.loaded_spine >= ResWeight.total_spine;
		default:
			Debug.LogError("no contains type, type = " + type);
			return false;
		}
	}

	public static void LoadSuccess(ResWeight.ResType type)
	{
		switch (type)
		{
		case ResWeight.ResType.model:
			ResWeight.loaded_model += 50;
			break;
		case ResWeight.ResType.fx:
			ResWeight.loaded_fx += 10;
			break;
		case ResWeight.ResType.uiprefab:
			ResWeight.loaded_uiprefab += 20;
			break;
		case ResWeight.ResType.uiatlas:
			ResWeight.loaded_uiatlas += 20;
			break;
		case ResWeight.ResType.spine:
			ResWeight.loaded_spine += 20;
			break;
		default:
			Debug.LogError("no contains type, type = " + type);
			break;
		}
	}

	public static int GetWeight(ResWeight.ResType type)
	{
		switch (type)
		{
		case ResWeight.ResType.model:
			return 50;
		case ResWeight.ResType.fx:
			return 10;
		case ResWeight.ResType.uiprefab:
			return 20;
		case ResWeight.ResType.uiatlas:
			return 20;
		case ResWeight.ResType.spine:
			return 20;
		default:
			Debug.LogError("no contains type, type = " + type);
			return 0;
		}
	}

	public static void ResetAll()
	{
		ResWeight.loaded_model = 0;
		ResWeight.total_model = 0;
		ResWeight.loaded_fx = 0;
		ResWeight.total_fx = 0;
		ResWeight.loaded_uiprefab = 0;
		ResWeight.total_uiprefab = 0;
		ResWeight.loaded_uiatlas = 0;
		ResWeight.total_uiatlas = 0;
		ResWeight.loaded_spine = 0;
		ResWeight.total_spine = 0;
	}
}
