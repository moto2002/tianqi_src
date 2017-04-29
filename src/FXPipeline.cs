using GameData;
using System;
using UnityEngine;

public class FXPipeline : ObjectPipeline
{
	public sealed override string GetPath(int guid)
	{
		Fx fx = DataReader<Fx>.Get(guid);
		if (fx == null)
		{
			Debug.LogError("[Fx] no find, id = " + guid);
			return null;
		}
		return fx.path;
	}
}
