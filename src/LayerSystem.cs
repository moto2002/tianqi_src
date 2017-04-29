using GameData;
using System;
using UnityEngine;

public class LayerSystem
{
	public class Layers
	{
		public const string Default = "Default";

		public const string UI = "UI";

		public const string Terrian = "Terrian";

		public const string Gear = "Gear";

		public const string FX = "FX";

		public const string FX_Distortion = "FX_Distortion";

		public const string CameraRange = "CameraRange";

		public const string CityPlayer = "CityPlayer";

		public const string NPC = "NPC";

		public const string LayerA = "LayerA";

		public const string LayerB = "LayerB";

		public const string LayerC = "LayerC";

		public const string LayerD = "LayerD";

		public const string LayerE = "LayerE";

		public const string LayerF = "LayerF";

		public const string LayerG = "LayerG";

		public const string LayerH = "LayerH";

		public const string LayerI = "LayerI";

		public const string LayerZero = "LayerZero";

		public const string BallItem = "BallItem";

		public const string BallObject = "BallObject";
	}

	public class IgnoreLayerType
	{
		public const int None = 1;

		public const int FX = 2;
	}

	private static XDict<int, string> LayerToNames = new XDict<int, string>();

	public static string LayerToName(int layer)
	{
		if (!LayerSystem.LayerToNames.ContainsKey(layer))
		{
			LayerSystem.LayerToNames[layer] = LayerMask.LayerToName(layer);
		}
		return LayerSystem.LayerToNames[layer];
	}

	public static int NameToLayer(string name)
	{
		return LayerMask.NameToLayer(name);
	}

	public static int GetMask(string[] layerNames)
	{
		return LayerMask.GetMask(layerNames);
	}

	public static bool IsIgnoreLayers(int layer, int ignoreType)
	{
		return ignoreType != 1 && ignoreType == 2 && LayerSystem.IsSpecialEffectLayers(layer);
	}

	public static bool IsSpecialEffectLayers(int layer)
	{
		string text = LayerSystem.LayerToName(layer);
		return text.Equals("FX") || text.Equals("FX_Distortion");
	}

	public static void SetGameObjectLayer(GameObject root, string layerName, int ignoreType)
	{
		LayerSystem.SetGameObjectLayer(root, LayerSystem.NameToLayer(layerName), ignoreType);
	}

	public static string GetGameObjectLayerName(int camp, int entityType, int state)
	{
		int layerCampGroup = LayerSystem.GetLayerCampGroup(camp);
		if (layerCampGroup == -1)
		{
			return "Default";
		}
		int key = layerCampGroup * 100 + entityType * 10 + state;
		if (!DataReader<ShiTiPengZhuangBiao>.Contains(key))
		{
			return "Default";
		}
		ShiTiPengZhuangBiao shiTiPengZhuangBiao = DataReader<ShiTiPengZhuangBiao>.Get(key);
		switch (shiTiPengZhuangBiao.layer)
		{
		case 1:
			return "LayerA";
		case 2:
			return "LayerB";
		case 3:
			return "LayerC";
		case 4:
			return "LayerD";
		case 5:
			return "LayerE";
		case 6:
			return "LayerF";
		case 7:
			return "LayerG";
		case 8:
			return "LayerH";
		case 9:
			return "LayerI";
		default:
			return "Default";
		}
	}

	protected static int GetLayerCampGroup(int camp)
	{
		if (!DataReader<ZhenYingZu>.Contains(camp))
		{
			return -1;
		}
		return DataReader<ZhenYingZu>.Get(camp).Group;
	}

	private static void SetGameObjectLayer(GameObject root, int layerValue, int ignoreType)
	{
		if (layerValue >= 0 && layerValue <= 31 && root != null)
		{
			if (!LayerSystem.IsIgnoreLayers(root.get_layer(), ignoreType))
			{
				root.set_layer(layerValue);
			}
			LayerSystem.SetChildLayerRecursively(root.get_transform(), layerValue, ignoreType);
		}
	}

	private static void SetChildLayerRecursively(Transform childTransform, int layerValue, int ignoreType)
	{
		if (layerValue >= 0 && layerValue <= 31)
		{
			for (int i = 0; i < childTransform.get_childCount(); i++)
			{
				Transform child = childTransform.GetChild(i);
				if (!LayerSystem.IsIgnoreLayers(child.get_gameObject().get_layer(), ignoreType))
				{
					child.get_gameObject().set_layer(layerValue);
				}
				LayerSystem.SetChildLayerRecursively(child, layerValue, ignoreType);
			}
		}
	}
}
