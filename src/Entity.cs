using Package;
using System;

public abstract class Entity
{
	public abstract void OnCreate(MapObjInfo info, bool isClient = false);

	public abstract void OnDestroy();

	public abstract void OnEnterField();

	public abstract void OnLeaveField();

	public abstract void CreateActor();
}
