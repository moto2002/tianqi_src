using System;
using System.Collections.Generic;
using UnityEngine;

public class NcEffectBehaviour : MonoBehaviour
{
	public class _RuntimeIntance
	{
		public GameObject m_ParentGameObject;

		public GameObject m_ChildGameObject;

		public _RuntimeIntance(GameObject parentGameObject, GameObject childGameObject)
		{
			this.m_ParentGameObject = parentGameObject;
			this.m_ChildGameObject = childGameObject;
		}
	}

	private static bool m_bShuttingDown;

	public float m_fUserTag;

	protected MeshFilter m_MeshFilter;

	public NcEffectBehaviour()
	{
		this.m_MeshFilter = null;
	}

	public static float GetEngineTime()
	{
		if (Time.get_time() == 0f)
		{
			return 1E-06f;
		}
		return Time.get_time();
	}

	public static float GetEngineDeltaTime()
	{
		return Time.get_deltaTime();
	}

	public virtual int GetAnimationState()
	{
		return -1;
	}

	public static GameObject GetRootInstanceEffect()
	{
		if (!NcEffectBehaviour.IsSafe())
		{
			return null;
		}
		GameObject gameObject = GameObject.Find("_InstanceObject");
		if (gameObject == null)
		{
			gameObject = new GameObject("_InstanceObject");
		}
		return gameObject;
	}

	protected static void SetActive(GameObject target, bool bActive)
	{
		target.SetActive(bActive);
	}

	protected static void SetActiveRecursively(GameObject target, bool bActive)
	{
		target.SetActive(bActive);
	}

	protected static bool IsActive(GameObject target)
	{
		return target.get_activeSelf();
	}

	protected static void RemoveAllChildObject(GameObject parent, bool bImmediate)
	{
		int num = parent.get_transform().get_childCount() - 1;
		while (0 <= num)
		{
			if (num < parent.get_transform().get_childCount())
			{
				Transform child = parent.get_transform().GetChild(num);
				if (bImmediate)
				{
					Object.DestroyImmediate(child.get_gameObject());
				}
				else
				{
					Object.Destroy(child.get_gameObject());
				}
			}
			num--;
		}
	}

	public static void HideNcDelayActive(GameObject tarObj)
	{
		NcEffectBehaviour.SetActiveRecursively(tarObj, false);
	}

	public static Texture[] PreloadTexture(GameObject tarObj)
	{
		if (tarObj == null)
		{
			return new Texture[0];
		}
		List<GameObject> list = new List<GameObject>();
		list.Add(tarObj);
		return NcEffectBehaviour.PreloadTexture(tarObj, list);
	}

	private static Texture[] PreloadTexture(GameObject tarObj, List<GameObject> parentPrefabList)
	{
		if (!NcEffectBehaviour.IsSafe())
		{
			return null;
		}
		Renderer[] componentsInChildren = tarObj.GetComponentsInChildren<Renderer>(true);
		List<Texture> list = new List<Texture>();
		Renderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Renderer renderer = array[i];
			if (renderer.get_sharedMaterials() != null && renderer.get_sharedMaterials().Length > 0)
			{
				Material[] sharedMaterials = renderer.get_sharedMaterials();
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					Material material = sharedMaterials[j];
					if (material != null && material.get_mainTexture() != null)
					{
						list.Add(material.get_mainTexture());
					}
				}
			}
		}
		NcAttachPrefab[] componentsInChildren2 = tarObj.GetComponentsInChildren<NcAttachPrefab>(true);
		NcAttachPrefab[] array2 = componentsInChildren2;
		for (int k = 0; k < array2.Length; k++)
		{
			NcAttachPrefab ncAttachPrefab = array2[k];
			if (ncAttachPrefab.m_AttachPrefab != null)
			{
				Texture[] array3 = NcEffectBehaviour.PreloadPrefab(ncAttachPrefab.m_AttachPrefab, parentPrefabList, true);
				if (array3 == null)
				{
					ncAttachPrefab.m_AttachPrefab = null;
				}
				else
				{
					list.AddRange(array3);
				}
			}
		}
		NcParticleSystem[] componentsInChildren3 = tarObj.GetComponentsInChildren<NcParticleSystem>(true);
		NcParticleSystem[] array4 = componentsInChildren3;
		for (int l = 0; l < array4.Length; l++)
		{
			NcParticleSystem ncParticleSystem = array4[l];
			if (ncParticleSystem.m_AttachPrefab != null)
			{
				Texture[] array5 = NcEffectBehaviour.PreloadPrefab(ncParticleSystem.m_AttachPrefab, parentPrefabList, true);
				if (array5 == null)
				{
					ncParticleSystem.m_AttachPrefab = null;
				}
				else
				{
					list.AddRange(array5);
				}
			}
		}
		NcSpriteTexture[] componentsInChildren4 = tarObj.GetComponentsInChildren<NcSpriteTexture>(true);
		NcSpriteTexture[] array6 = componentsInChildren4;
		for (int m = 0; m < array6.Length; m++)
		{
			NcSpriteTexture ncSpriteTexture = array6[m];
			if (ncSpriteTexture.m_NcSpriteFactoryPrefab != null)
			{
				Texture[] array7 = NcEffectBehaviour.PreloadPrefab(ncSpriteTexture.m_NcSpriteFactoryPrefab, parentPrefabList, false);
				if (array7 != null)
				{
					list.AddRange(array7);
				}
			}
		}
		NcAttachSound[] componentsInChildren5 = tarObj.GetComponentsInChildren<NcAttachSound>(true);
		NcAttachSound[] array8 = componentsInChildren5;
		for (int n = 0; n < array8.Length; n++)
		{
			NcAttachSound ncAttachSound = array8[n];
			if (ncAttachSound.m_AudioClip != null)
			{
			}
		}
		NcSpriteFactory[] componentsInChildren6 = tarObj.GetComponentsInChildren<NcSpriteFactory>(true);
		NcSpriteFactory[] array9 = componentsInChildren6;
		for (int num = 0; num < array9.Length; num++)
		{
			NcSpriteFactory ncSpriteFactory = array9[num];
			if (ncSpriteFactory.m_SpriteList != null)
			{
				for (int num2 = 0; num2 < ncSpriteFactory.m_SpriteList.get_Count(); num2++)
				{
					if (ncSpriteFactory.m_SpriteList.get_Item(num2).m_EffectPrefab != null)
					{
						Texture[] array10 = NcEffectBehaviour.PreloadPrefab(ncSpriteFactory.m_SpriteList.get_Item(num2).m_EffectPrefab, parentPrefabList, true);
						if (array10 == null)
						{
							ncSpriteFactory.m_SpriteList.get_Item(num2).m_EffectPrefab = null;
						}
						else
						{
							list.AddRange(array10);
						}
						if (ncSpriteFactory.m_SpriteList.get_Item(num2).m_AudioClip != null)
						{
						}
					}
				}
			}
		}
		return list.ToArray();
	}

	private static Texture[] PreloadPrefab(GameObject tarObj, List<GameObject> parentPrefabList, bool bCheckDup)
	{
		if (!parentPrefabList.Contains(tarObj))
		{
			parentPrefabList.Add(tarObj);
			Texture[] result = NcEffectBehaviour.PreloadTexture(tarObj, parentPrefabList);
			parentPrefabList.Remove(tarObj);
			return result;
		}
		if (bCheckDup)
		{
			string text = string.Empty;
			for (int i = 0; i < parentPrefabList.get_Count(); i++)
			{
				text = text + parentPrefabList.get_Item(i).get_name() + "/";
			}
			Debuger.Warning("LoadError : Recursive Prefab - " + text + tarObj.get_name(), new object[0]);
			return null;
		}
		return null;
	}

	public static void AdjustSpeedRuntime(GameObject target, float fSpeedRate)
	{
		NcEffectBehaviour[] componentsInChildren = target.GetComponentsInChildren<NcEffectBehaviour>(true);
		NcEffectBehaviour[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			NcEffectBehaviour ncEffectBehaviour = array[i];
			ncEffectBehaviour.OnUpdateEffectSpeed(fSpeedRate, true);
		}
	}

	public static string GetMaterialColorName(Material mat)
	{
		string[] array = new string[]
		{
			"_Color",
			"_TintColor",
			"_EmisColor"
		};
		if (mat != null)
		{
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (mat.HasProperty(text))
				{
					return text;
				}
			}
		}
		return null;
	}

	protected void DisableEmit()
	{
		NcParticleSystem[] componentsInChildren = base.get_gameObject().GetComponentsInChildren<NcParticleSystem>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i] != null)
			{
				componentsInChildren[i].SetDisableEmit();
			}
		}
		NcAttachPrefab[] componentsInChildren2 = base.get_gameObject().GetComponentsInChildren<NcAttachPrefab>(true);
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			if (componentsInChildren2[j] != null)
			{
				componentsInChildren2[j].set_enabled(false);
			}
		}
		ParticleSystem[] componentsInChildren3 = base.get_gameObject().GetComponentsInChildren<ParticleSystem>(true);
		for (int k = 0; k < componentsInChildren3.Length; k++)
		{
			if (componentsInChildren3[k] != null)
			{
				componentsInChildren3[k].set_enableEmission(false);
			}
		}
		ParticleEmitter[] componentsInChildren4 = base.get_gameObject().GetComponentsInChildren<ParticleEmitter>(true);
		for (int l = 0; l < componentsInChildren4.Length; l++)
		{
			if (componentsInChildren4[l] != null)
			{
				componentsInChildren4[l].set_emit(false);
			}
		}
	}

	public static bool IsSafe()
	{
		return !NcEffectBehaviour.m_bShuttingDown;
	}

	protected GameObject CreateEditorGameObject(GameObject srcGameObj)
	{
		return srcGameObj;
	}

	public GameObject CreateGameObject(string name)
	{
		if (!NcEffectBehaviour.IsSafe())
		{
			return null;
		}
		return this.CreateEditorGameObject(new GameObject(name));
	}

	public GameObject CreateGameObject(GameObject original)
	{
		if (!NcEffectBehaviour.IsSafe())
		{
			return null;
		}
		return this.CreateEditorGameObject(Object.Instantiate<GameObject>(original));
	}

	public GameObject CreateGameObject(GameObject prefabObj, Vector3 position, Quaternion rotation)
	{
		if (!NcEffectBehaviour.IsSafe())
		{
			return null;
		}
		return this.CreateEditorGameObject((GameObject)Object.Instantiate(prefabObj, position, rotation));
	}

	public GameObject CreateGameObject(GameObject parentObj, GameObject prefabObj)
	{
		if (!NcEffectBehaviour.IsSafe())
		{
			return null;
		}
		GameObject gameObject = this.CreateGameObject(prefabObj);
		if (parentObj != null)
		{
			this.ChangeParent(parentObj.get_transform(), gameObject.get_transform(), true, null);
		}
		return gameObject;
	}

	public GameObject CreateGameObject(GameObject parentObj, Transform parentTrans, GameObject prefabObj)
	{
		if (!NcEffectBehaviour.IsSafe())
		{
			return null;
		}
		GameObject gameObject = this.CreateGameObject(prefabObj);
		if (parentObj != null)
		{
			this.ChangeParent(parentObj.get_transform(), gameObject.get_transform(), true, parentTrans);
		}
		return gameObject;
	}

	protected void ChangeParent(Transform newParent, Transform child, bool bKeepingLocalTransform, Transform addTransform)
	{
		NcTransformTool ncTransformTool = null;
		if (bKeepingLocalTransform)
		{
			ncTransformTool = new NcTransformTool(child.get_transform());
			if (addTransform != null)
			{
				ncTransformTool.AddTransform(addTransform);
			}
		}
		child.set_parent(newParent);
		if (bKeepingLocalTransform)
		{
			ncTransformTool.CopyToLocalTransform(child.get_transform());
		}
		if (bKeepingLocalTransform)
		{
			NcBillboard[] componentsInChildren = base.get_gameObject().GetComponentsInChildren<NcBillboard>();
			NcBillboard[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				NcBillboard ncBillboard = array[i];
				ncBillboard.UpdateBillboard();
			}
		}
	}

	protected void UpdateMeshColors(Color color)
	{
		if (this.m_MeshFilter == null)
		{
			this.m_MeshFilter = (MeshFilter)base.get_gameObject().GetComponent(typeof(MeshFilter));
		}
		if (this.m_MeshFilter == null || this.m_MeshFilter.get_sharedMesh() == null || this.m_MeshFilter.get_mesh() == null)
		{
			return;
		}
		Color[] array = new Color[this.m_MeshFilter.get_mesh().get_vertexCount()];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = color;
		}
		this.m_MeshFilter.get_mesh().set_colors(array);
	}

	public void OnApplicationQuit()
	{
		NcEffectBehaviour.m_bShuttingDown = true;
	}

	public virtual void OnUpdateEffectSpeed(float fSpeedRate, bool bRuntime)
	{
	}

	public virtual void OnUpdateToolData()
	{
	}
}
