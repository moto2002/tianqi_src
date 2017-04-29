using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PostProcessBase : TimerInterval
{
	protected bool IsInitSuccessed;

	public bool IsDestoryOnDisable = true;

	protected List<string> m_shaderNames = new List<string>();

	protected Dictionary<string, Material> m_materials = new Dictionary<string, Material>();

	private bool IsAddListeners;

	public static RenderTextureFormat RTFormat
	{
		get
		{
			return (!SystemInfo.SupportsRenderTextureFormat(4)) ? 7 : 4;
		}
	}

	private bool InitResources()
	{
		if (this.m_shaderNames.get_Count() == 0 || this.m_materials.get_Count() > 0)
		{
			return true;
		}
		if (!ShaderEffectUtils.CheckSupport())
		{
			base.set_enabled(false);
			return false;
		}
		if (!this.CreateMaterials())
		{
			this.DestroyMaterials();
			base.set_enabled(false);
			return false;
		}
		this.GetMaterials();
		return true;
	}

	private bool CreateMaterials()
	{
		for (int i = 0; i < this.m_shaderNames.get_Count(); i++)
		{
			Material material = null;
			string text = this.m_shaderNames.get_Item(i);
			ShaderEffectUtils.SafeCreateMaterial(ref material, text);
			if (!ShaderEffectUtils.CheckMaterialAndShader(material, text))
			{
				return false;
			}
			this.m_materials.set_Item(text, material);
		}
		return true;
	}

	private void DestroyMaterials()
	{
		using (Dictionary<string, Material>.ValueCollection.Enumerator enumerator = this.m_materials.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Material current = enumerator.get_Current();
				ShaderEffectUtils.SafeDestroyMaterial(current);
			}
		}
		this.m_materials.Clear();
	}

	protected virtual void OnEnable()
	{
		this.AddListeners();
	}

	protected virtual void OnDisable()
	{
		this.RemoveListeners();
		if (this.IsDestoryOnDisable)
		{
			this.DestroyMaterials();
			Object.Destroy(this);
		}
	}

	private void AddListeners()
	{
		if (!this.IsAddListeners)
		{
			this.IsAddListeners = true;
			EventDispatcher.AddListener<int, int>(SceneManagerEvent.UnloadScene, new Callback<int, int>(this.OnUnloadScene));
			EventDispatcher.AddListener(ShaderEffectEvent.PostProcessOff, new Callback(this.OnPostProcessOff));
		}
	}

	private void RemoveListeners()
	{
		if (this.IsAddListeners)
		{
			this.IsAddListeners = false;
			EventDispatcher.RemoveListener<int, int>(SceneManagerEvent.UnloadScene, new Callback<int, int>(this.OnUnloadScene));
			EventDispatcher.RemoveListener(ShaderEffectEvent.PostProcessOff, new Callback(this.OnPostProcessOff));
		}
	}

	private void OnUnloadScene(int sceneId, int nextId)
	{
		base.set_enabled(false);
	}

	private void OnPostProcessOff()
	{
		base.set_enabled(false);
	}

	public virtual void Initialization()
	{
		if (this.IsInitSuccessed)
		{
			return;
		}
		this.SetShaders();
		this.IsInitSuccessed = this.InitResources();
	}

	protected virtual void SetShaders()
	{
	}

	protected virtual void GetMaterials()
	{
	}
}
