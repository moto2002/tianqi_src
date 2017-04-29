using System;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
	private string m_resName = string.Empty;

	public bool IsAwake;

	public string ResName
	{
		get
		{
			return this.m_resName;
		}
		set
		{
			this.m_resName = value;
			if (this.IsAwake && !string.IsNullOrEmpty(this.m_resName))
			{
				AssetManager.AddAssetRef(this.m_resName);
			}
		}
	}

	private void Awake()
	{
		this.IsAwake = true;
		if (!string.IsNullOrEmpty(this.m_resName))
		{
			AssetManager.AddAssetRef(this.m_resName);
		}
	}

	protected virtual void OnDestroy()
	{
		if (this.IsAwake && !string.IsNullOrEmpty(this.m_resName))
		{
			AssetManager.MinusAssetRef(this.m_resName);
		}
	}
}
