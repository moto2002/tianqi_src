using GameData;
using System;
using UnityEngine;

public class ModelPipeline : ObjectPipeline
{
	public sealed override string GetPath(int guid)
	{
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(guid);
		if (avatarModel == null)
		{
			Debug.LogError("[AvatarModel] no find, id = " + guid);
			return null;
		}
		return avatarModel.path;
	}
}
