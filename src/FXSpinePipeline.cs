using GameData;
using System;
using UnityEngine;
using XEngine;

public class FXSpinePipeline : ObjectPipeline
{
	public sealed override string GetPath(int guid)
	{
		FXSpine fXSpine = DataReader<FXSpine>.Get(guid);
		if (fXSpine == null)
		{
			Debug.LogError("[FXSpine] no find, id = " + guid);
			return null;
		}
		return FileSystem.GetPathOfSpine(fXSpine.name);
	}
}
