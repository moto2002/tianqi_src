using System;
using System.Collections.Generic;

public abstract class BaseSubSystemManager
{
	public static List<BaseSubSystemManager> ListManager = new List<BaseSubSystemManager>();

	private bool IsFirstInited;

	public virtual void Init()
	{
		if (this.IsFirstInited)
		{
			return;
		}
		this.IsFirstInited = true;
		BaseSubSystemManager.ListManager.Add(this);
		this.AddListener();
	}

	public abstract void Release();

	protected abstract void AddListener();
}
