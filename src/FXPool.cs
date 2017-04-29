using System;
using UnityEngine;
using XEngine;
using XEngineActor;

public class FXPool : ActorPool<ActorFX>
{
	public static readonly FXPool Instance = new FXPool();

	protected override void SetPipeline()
	{
		this.pipeline = new FXPipeline();
	}

	public ActorFX Get(int guid, bool isLow)
	{
		string text = this.pipeline.GetPath(guid);
		if (text == null)
		{
			return null;
		}
		text = FXPool.GetPathWithLOD(text, isLow);
		GameObject gameObject = this.GetPool().Get(text);
		if (gameObject == null)
		{
			Debug.LogError(string.Format("马上联系左总，m_loader.Get拿到空值，路径是{0}", text));
			return null;
		}
		return base.SetGameObject(gameObject, guid);
	}

	public static string GetPathWithLOD(string path, bool isLow)
	{
		string text;
		if (isLow)
		{
			text = path + "_low";
		}
		else if (GameLevelManager.GameLevelVariable.GetRealLODLEVEL() < 300)
		{
			text = path + "_low";
		}
		else
		{
			text = path;
		}
		if (FileSystem.HasValue(text))
		{
			return text;
		}
		return path;
	}
}
