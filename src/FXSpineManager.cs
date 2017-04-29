using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;

public class FXSpineManager
{
	public static readonly FXSpineManager Instance = new FXSpineManager();

	private Dictionary<int, ActorFXSpine> m_fxSpines = new Dictionary<int, ActorFXSpine>();

	private int m_fxOfcounter;

	private bool IsLockWhenDeleteAllSpine;

	private List<int> m_deletes = new List<int>();

	private Dictionary<int, ActorFXSpine> m_noauto_deletes = new Dictionary<int, ActorFXSpine>();

	private int fx_battlestart01;

	private int fx_battlestart02;

	public int PlaySpine(int templateId, Transform host, string uibase, int depthValue = 2001, Action finishCallback = null, string layer = "UI", float xOffset = 0f, float yOffset = 0f, float xScale = 1f, float yScale = 1f, bool stencilMask = false, FXMaskLayer.MaskState maskState = FXMaskLayer.MaskState.None)
	{
		if (this.IsLockWhenDeleteAllSpine)
		{
			return 0;
		}
		if (!string.IsNullOrEmpty(ClientGMManager.spineout_ids))
		{
			string[] array = ClientGMManager.spineout_ids.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (int.Parse(array[i]) == templateId)
				{
					return 0;
				}
			}
		}
		if (!SystemConfig.IsUIFXOn)
		{
			return 0;
		}
		if (templateId <= 0)
		{
			return 0;
		}
		if (host == null)
		{
			return 0;
		}
		FXSpine fXSpine = DataReader<FXSpine>.Get(templateId);
		if (fXSpine == null)
		{
			return 0;
		}
		if (string.IsNullOrEmpty(fXSpine.name))
		{
			return 0;
		}
		int uid = ++this.m_fxOfcounter;
		this.m_fxSpines.set_Item(uid, null);
		this.PreloadAsset(templateId, delegate
		{
			if (this.m_fxSpines.ContainsKey(uid))
			{
				this.JustPlaySpine(uid, templateId, host, uibase, depthValue, finishCallback, layer, xOffset, yOffset, xScale, yScale, stencilMask, maskState);
			}
		});
		return uid;
	}

	public int ReplaySpine(int uid, int templateId, Transform host, string uibase, int depthValue = 2001, Action finishCallback = null, string layer = "UI", float xOffset = 0f, float yOffset = 0f, float xScale = 1f, float yScale = 1f, bool stencilMask = false, FXMaskLayer.MaskState maskState = FXMaskLayer.MaskState.None)
	{
		if (this.IsLockWhenDeleteAllSpine)
		{
			return 0;
		}
		if (!SystemConfig.IsUIFXOn)
		{
			return 0;
		}
		if (templateId <= 0)
		{
			return 0;
		}
		if (host == null)
		{
			return 0;
		}
		if (uid <= 0 || !this.m_fxSpines.ContainsKey(uid))
		{
			return this.PlaySpine(templateId, host, uibase, depthValue, finishCallback, layer, xOffset, yOffset, xScale, yScale, stencilMask, maskState);
		}
		ActorFXSpine actorFXSpine = this.m_fxSpines.get_Item(uid);
		if (actorFXSpine != null)
		{
			actorFXSpine.PlaySpine();
			return uid;
		}
		this.m_fxSpines.Remove(uid);
		return this.PlaySpine(templateId, host, uibase, depthValue, finishCallback, layer, xOffset, yOffset, xScale, yScale, stencilMask, maskState);
	}

	public void DeleteSpine(int uid, bool isCallback = true)
	{
		if (uid <= 0)
		{
			return;
		}
		if (this.m_fxSpines.ContainsKey(uid))
		{
			ActorFXSpine actorFXSpine = this.m_fxSpines.get_Item(uid);
			this.m_fxSpines.Remove(uid);
			if (actorFXSpine != null)
			{
				actorFXSpine.FXFinished(isCallback);
				actorFXSpine.PoolRecycle();
			}
		}
	}

	public void DeleteAllSpine()
	{
		this.IsLockWhenDeleteAllSpine = true;
		this.m_deletes.Clear();
		this.m_noauto_deletes.Clear();
		using (Dictionary<int, ActorFXSpine>.Enumerator enumerator = this.m_fxSpines.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, ActorFXSpine> current = enumerator.get_Current();
				if (current.get_Value() != null)
				{
					if (this.IsInNoAutoDeleteWhitelist(current.get_Value()._dataSpine.id))
					{
						this.m_noauto_deletes.set_Item(current.get_Key(), current.get_Value());
					}
					else
					{
						this.m_deletes.Add(current.get_Key());
					}
				}
			}
		}
		for (int i = 0; i < this.m_deletes.get_Count(); i++)
		{
			this.DeleteSpine(this.m_deletes.get_Item(i), true);
		}
		this.m_fxSpines.Clear();
		using (Dictionary<int, ActorFXSpine>.Enumerator enumerator2 = this.m_noauto_deletes.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				KeyValuePair<int, ActorFXSpine> current2 = enumerator2.get_Current();
				if (current2.get_Value() != null)
				{
					this.m_fxSpines.set_Item(current2.get_Key(), current2.get_Value());
				}
			}
		}
		this.m_noauto_deletes.Clear();
		this.IsLockWhenDeleteAllSpine = false;
	}

	private bool IsInNoAutoDeleteWhitelist(int templateId)
	{
		return templateId >= 1401 && templateId <= 1499;
	}

	public ActorFXSpine FindSpine(int uid)
	{
		if (this.m_fxSpines.ContainsKey(uid))
		{
			return this.m_fxSpines.get_Item(uid);
		}
		return null;
	}

	public void PreloadAsset(int templateId, Action action)
	{
		FXSpine fXSpine = DataReader<FXSpine>.Get(templateId);
		if (fXSpine == null)
		{
			if (action != null)
			{
				action.Invoke();
			}
			return;
		}
		AssetManager.AssetOfSpineManager.LoadAssetWithPool(FileSystem.GetPathOfSpine(fXSpine.name), delegate(bool isSuccess)
		{
			if (action != null)
			{
				action.Invoke();
			}
		});
	}

	public void PreloadAsset(string path, Action action)
	{
		AssetManager.AssetOfSpineManager.LoadAssetWithPool(path, delegate(bool isSuccess)
		{
			if (action != null)
			{
				action.Invoke();
			}
		});
	}

	private void JustPlaySpine(int uid, int templateId, Transform host, string uibase, int depthValue, Action finishCallback, string layer, float xOffset, float yOffset, float xScale, float yScale, bool stencilMask, FXMaskLayer.MaskState maskState = FXMaskLayer.MaskState.None)
	{
		FXSpine fXSpine = DataReader<FXSpine>.Get(templateId);
		if (fXSpine == null || string.IsNullOrEmpty(fXSpine.name))
		{
			return;
		}
		if (host == null)
		{
			return;
		}
		ActorFXSpine actorFXSpine = FXSpinePool.Instance.Get(templateId);
		if (actorFXSpine == null)
		{
			return;
		}
		actorFXSpine.get_transform().set_name(templateId.ToString());
		if (maskState != FXMaskLayer.MaskState.None)
		{
			FXMaskLayer fXMaskLayer = actorFXSpine.get_gameObject().AddUniqueComponent<FXMaskLayer>();
			fXMaskLayer.state = maskState;
		}
		this.m_fxSpines.set_Item(uid, actorFXSpine);
		actorFXSpine.get_transform().set_parent(host);
		actorFXSpine.get_transform().set_localPosition(new Vector3(xOffset, yOffset, 0f));
		actorFXSpine.get_transform().set_localRotation(Quaternion.get_identity());
		actorFXSpine.get_transform().set_localScale(new Vector3(xScale, yScale, 1f));
		LayerSystem.SetGameObjectLayer(actorFXSpine.get_gameObject(), layer, 1);
		DepthOfFX depthOfFX = actorFXSpine.get_gameObject().AddMissingComponent<DepthOfFX>();
		depthOfFX.SortingOrder = depthValue;
		actorFXSpine._uid = uid;
		actorFXSpine._dataSpine = fXSpine;
		actorFXSpine._uibase = uibase;
		actorFXSpine._depth = depthValue;
		actorFXSpine._stencilMask = stencilMask;
		actorFXSpine._finishCallback = finishCallback;
		actorFXSpine.AwakeSelf();
		actorFXSpine.get_gameObject().SetActive(true);
		actorFXSpine.PlaySpine();
		this.DoSetShader(actorFXSpine, stencilMask, fXSpine);
		this.PlaySound(fXSpine);
	}

	private void DoSetShader(ActorFXSpine actorSpine, bool stencilMask, FXSpine dataSpine)
	{
		if (actorSpine == null)
		{
			return;
		}
		using (Dictionary<int, ActorFXSpine>.ValueCollection.Enumerator enumerator = this.m_fxSpines.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ActorFXSpine current = enumerator.get_Current();
				if (!(current == null))
				{
					if (current._dataSpine != null && actorSpine._dataSpine != null)
					{
						if (current._uid != actorSpine._uid)
						{
							if (!(current.myRenderer == null) && !(actorSpine.myRenderer == null))
							{
								if (current.myRenderer.get_sharedMaterial() == null)
								{
									Debug.Log("tspine.myRenderer.sharedMaterial is null");
								}
								else if (!(current.myRenderer.get_sharedMaterial().get_mainTexture() == null))
								{
									if (current._dataSpine.id == actorSpine._dataSpine.id && current._stencilMask == actorSpine._stencilMask)
									{
										actorSpine.myRenderer.set_sharedMaterial(current.myRenderer.get_sharedMaterial());
										return;
									}
									if (current._dataSpine.name == actorSpine._dataSpine.name && current._dataSpine.blendWay == actorSpine._dataSpine.blendWay && current._dataSpine.BrightnessRatio == actorSpine._dataSpine.BrightnessRatio && current._stencilMask == actorSpine._stencilMask && (current._dataSpine.blendWay != 0 || current._dataSpine.rgbEffect == actorSpine._dataSpine.rgbEffect))
									{
										actorSpine.myRenderer.set_sharedMaterial(current.myRenderer.get_sharedMaterial());
										return;
									}
								}
							}
						}
					}
				}
			}
		}
		Material material = actorSpine.GetComponent<MeshRenderer>().get_material();
		Shader shader;
		if (stencilMask)
		{
			if (dataSpine.blendWay == 0)
			{
				shader = ShaderManager.Find("Hsh(Mobile)/UI/UIStencilAlphaBlended");
			}
			else
			{
				shader = ShaderManager.Find("Hsh(Mobile)/UI/UIStencilAdd");
			}
		}
		else
		{
			int blendWay = dataSpine.blendWay;
			if (blendWay != 1)
			{
				if (blendWay != 2)
				{
					if (dataSpine.rgbEffect == 0)
					{
						shader = ShaderManager.Find("Hsh(Mobile)/UI/UISpineAlphaBlended");
					}
					else
					{
						shader = ShaderManager.Find("Hsh(Mobile)/FX/ParticleAlphaBlended");
					}
				}
				else
				{
					shader = ShaderManager.Find("Particles/Additive");
				}
			}
			else
			{
				shader = ShaderManager.Find("Hsh(Mobile)/UI/UISpineAdd");
			}
		}
		material.set_shader(shader);
		material.SetFloat(ShaderPIDManager._BrightnessRatio, dataSpine.BrightnessRatio);
	}

	private void PlaySound(FXSpine dataSpine)
	{
		if (dataSpine != null && dataSpine.audioId > 0)
		{
			SoundManager.PlayUI(dataSpine.audioId, dataSpine.audioLoop == 1);
		}
	}

	public void ShowBoxSpine1(Action action = null, float scale = 1f, float offsetX = 0f, float offsetY = 0f)
	{
		FXSpineManager.Instance.PlaySpine(801, UINodesManager.MiddleUIRoot, string.Empty, 14000, action, "UI", offsetX, offsetY, scale, scale, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(803, UINodesManager.MiddleUIRoot, string.Empty, 14000, null, "UI", offsetX, offsetY, scale, scale, false, FXMaskLayer.MaskState.None);
	}

	public void ShowBoxSpine2(Action action = null)
	{
		FXSpineManager.Instance.PlaySpine(802, UINodesManager.MiddleUIRoot, string.Empty, 14000, action, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void BossSuccess()
	{
		FXSpineManager.Instance.PlaySpine(1901, UINodesManager.TopUIRoot, string.Empty, 14000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(1902, UINodesManager.TopUIRoot, string.Empty, 14000, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void PVPSuccess()
	{
		FXSpineManager.Instance.PlaySpine(2001, UINodesManager.TopUIRoot, string.Empty, 14000, null, "UI", 0f, 150f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		FXSpineManager.Instance.PlaySpine(2002, UINodesManager.TopUIRoot, string.Empty, 14000, null, "UI", 0f, 150f, 1f, 1f, false, FXMaskLayer.MaskState.None);
	}

	public void ShowBattleStart1(int sceneID, Action action = null)
	{
		if (!MySceneManager.IsMainScene(sceneID))
		{
			TownUI.IsOpenAnimationOn = true;
			UIManagerControl.Instance.HideAll();
			this.fx_battlestart01 = FXSpineManager.Instance.PlaySpine(3501, UINodesManager.T4RootOfSpecial, string.Empty, 17000, null, "UI", 0f, 140f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.fx_battlestart02 = FXSpineManager.Instance.PlaySpine(3502, UINodesManager.T4RootOfSpecial, string.Empty, 17000, action, "UI", 0f, 140f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			BattleUI.IsOpenAnimationOn = true;
			UIManagerControl.Instance.HideAll();
			this.fx_battlestart01 = FXSpineManager.Instance.PlaySpine(3505, UINodesManager.T4RootOfSpecial, string.Empty, 17000, null, "UI", 0f, 140f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			this.fx_battlestart02 = FXSpineManager.Instance.PlaySpine(3506, UINodesManager.T4RootOfSpecial, string.Empty, 17000, action, "UI", 0f, 140f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	public void ShowBattleStart2(Action action = null)
	{
		if (Loading.Instance.isCurrentHasLoading)
		{
			if (action != null)
			{
				action.Invoke();
			}
			return;
		}
		if (!MySceneManager.IsMainScene(MySceneManager.Instance.CurSceneID))
		{
			FXSpineManager.Instance.PlaySpine(3503, UINodesManager.T4RootOfSpecial, string.Empty, 17000, null, "UI", 0f, 140f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			FXSpineManager.Instance.PlaySpine(3504, UINodesManager.T4RootOfSpecial, string.Empty, 17000, action, "UI", 0f, 140f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		else
		{
			FXSpineManager.Instance.PlaySpine(3507, UINodesManager.T4RootOfSpecial, string.Empty, 17000, null, "UI", 0f, 140f, 1f, 1f, false, FXMaskLayer.MaskState.None);
			FXSpineManager.Instance.PlaySpine(3508, UINodesManager.T4RootOfSpecial, string.Empty, 17000, action, "UI", 0f, 140f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
		TimerHeap.AddTimer(50u, 0, delegate
		{
			this.DeleteSpine(this.fx_battlestart01, true);
			this.DeleteSpine(this.fx_battlestart02, true);
		});
	}
}
