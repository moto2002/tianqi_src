using System;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSMR : MonoBehaviour
{
	public Object src_go;

	private string[] slot_name_list = new string[]
	{
		"head_fx_node",
		"wing_fx_node",
		"body_y_node",
		"foot_y_node",
		"body_fx_node",
		"foot_fx_node",
		"camerapoint",
		"camerapoint2",
		"weapon_slot_city",
		"weapon_slot_city2"
	};

	public void ChangeMesh()
	{
		if (this.src_go == null)
		{
			Debug.LogError("请指定源网格渲染器");
			return;
		}
		GameObject gameObject = Object.Instantiate(this.src_go) as GameObject;
		this.DoChangeSMR(ref gameObject);
		Animator componentInChildren = base.GetComponentInChildren<Animator>();
		Animator componentInChildren2 = gameObject.GetComponentInChildren<Animator>();
		if (componentInChildren != null && componentInChildren2 != null)
		{
			componentInChildren.set_avatar(componentInChildren2.get_avatar());
		}
		Object.DestroyImmediate(gameObject);
	}

	private void DoChangeSMR(ref GameObject srcInstantiate)
	{
		SkinnedMeshRenderer[] componentsInChildren = base.GetComponentsInChildren<SkinnedMeshRenderer>();
		if (componentsInChildren == null)
		{
			Debug.LogError("找不到目标网格渲染器");
			return;
		}
		SkinnedMeshRenderer[] componentsInChildren2 = srcInstantiate.GetComponentsInChildren<SkinnedMeshRenderer>();
		if (componentsInChildren2 == null)
		{
			Debug.LogError("请指定源网格渲染器");
			return;
		}
		int num = 0;
		while (num < componentsInChildren.Length && num < componentsInChildren2.Length)
		{
			this.DoChangeSMR(ref componentsInChildren[num], ref componentsInChildren2[num]);
			num++;
		}
	}

	private void DoChangeSMR(ref SkinnedMeshRenderer dst_smr, ref SkinnedMeshRenderer src_smr)
	{
		dst_smr.set_sharedMesh(src_smr.get_sharedMesh());
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < src_smr.get_bones().Length; i++)
		{
			Transform transform = XUtility.RecursiveFindTransform(base.get_transform(), src_smr.get_bones()[i].get_name());
			list.Add(transform);
		}
		dst_smr.set_bones(list.ToArray());
		dst_smr.set_shadowCastingMode(0);
		dst_smr.set_receiveShadows(false);
		dst_smr.set_useLightProbes(false);
	}

	public void AddSlots()
	{
		if (this.src_go == null)
		{
			Debug.LogError("请指定源预设");
			return;
		}
		GameObject gameObject = Object.Instantiate(this.src_go) as GameObject;
		for (int i = 0; i < this.slot_name_list.Length; i++)
		{
			string text = this.slot_name_list[i];
			Transform transform = XUtility.RecursiveFindTransform(gameObject.get_transform(), text);
			if (!(transform == null))
			{
				Transform transform2 = XUtility.RecursiveFindTransform(base.get_transform(), text);
				if (transform2 == null)
				{
					Transform parent = transform.get_parent();
					if (!(parent == null))
					{
						Transform transform3 = XUtility.RecursiveFindTransform(base.get_transform(), parent.get_name());
						if (!(transform3 == null))
						{
							transform2 = new GameObject(text).get_transform();
							transform2.SetParent(transform3);
							transform2.set_localPosition(transform.get_localPosition());
							transform2.set_localRotation(transform.get_localRotation());
							transform2.set_localScale(transform.get_localScale());
						}
					}
				}
			}
		}
		Object.DestroyImmediate(gameObject);
	}
}
