using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AssetManagerBase
{
	protected static Dictionary<string, Object> mapAssets = new Dictionary<string, Object>();

	protected static Dictionary<string, int> mapAssetsToAssetBundleRef = new Dictionary<string, int>();

	protected static Dictionary<string, int> mapAssetsRef = new Dictionary<string, int>();
}
