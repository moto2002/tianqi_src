using System;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonUtilityKinematicShadow : MonoBehaviour
{
	public bool hideShadow = true;

	public Transform parent;

	private Dictionary<Transform, Transform> shadowTable;

	private GameObject shadowRoot;

	private void Start()
	{
		this.shadowRoot = Object.Instantiate<GameObject>(base.get_gameObject());
		if (this.hideShadow)
		{
			this.shadowRoot.set_hideFlags(1);
		}
		if (this.parent == null)
		{
			this.shadowRoot.get_transform().set_parent(base.get_transform().get_root());
		}
		else
		{
			this.shadowRoot.get_transform().set_parent(this.parent);
		}
		this.shadowTable = new Dictionary<Transform, Transform>();
		Object.Destroy(this.shadowRoot.GetComponent<SkeletonUtilityKinematicShadow>());
		this.shadowRoot.get_transform().set_position(base.get_transform().get_position());
		this.shadowRoot.get_transform().set_rotation(base.get_transform().get_rotation());
		Vector3 vector = base.get_transform().TransformPoint(Vector3.get_right());
		float num = Vector3.Distance(base.get_transform().get_position(), vector);
		this.shadowRoot.get_transform().set_localScale(Vector3.get_one());
		Joint[] componentsInChildren = this.shadowRoot.GetComponentsInChildren<Joint>();
		Joint[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Joint joint = array[i];
			Joint expr_116 = joint;
			expr_116.set_connectedAnchor(expr_116.get_connectedAnchor() * num);
		}
		Joint[] componentsInChildren2 = base.GetComponentsInChildren<Joint>();
		Joint[] array2 = componentsInChildren2;
		for (int j = 0; j < array2.Length; j++)
		{
			Joint joint2 = array2[j];
			Object.Destroy(joint2);
		}
		Rigidbody[] componentsInChildren3 = base.GetComponentsInChildren<Rigidbody>();
		Rigidbody[] array3 = componentsInChildren3;
		for (int k = 0; k < array3.Length; k++)
		{
			Rigidbody rigidbody = array3[k];
			Object.Destroy(rigidbody);
		}
		Collider[] componentsInChildren4 = base.GetComponentsInChildren<Collider>();
		Collider[] array4 = componentsInChildren4;
		for (int l = 0; l < array4.Length; l++)
		{
			Collider collider = array4[l];
			Object.Destroy(collider);
		}
		SkeletonUtilityBone[] componentsInChildren5 = this.shadowRoot.GetComponentsInChildren<SkeletonUtilityBone>();
		SkeletonUtilityBone[] componentsInChildren6 = base.GetComponentsInChildren<SkeletonUtilityBone>();
		SkeletonUtilityBone[] array5 = componentsInChildren6;
		for (int m = 0; m < array5.Length; m++)
		{
			SkeletonUtilityBone skeletonUtilityBone = array5[m];
			if (!(skeletonUtilityBone.get_gameObject() == base.get_gameObject()))
			{
				SkeletonUtilityBone[] array6 = componentsInChildren5;
				for (int n = 0; n < array6.Length; n++)
				{
					SkeletonUtilityBone skeletonUtilityBone2 = array6[n];
					if (!(skeletonUtilityBone2.GetComponent<Rigidbody>() == null))
					{
						if (skeletonUtilityBone2.boneName == skeletonUtilityBone.boneName)
						{
							this.shadowTable.Add(skeletonUtilityBone2.get_transform(), skeletonUtilityBone.get_transform());
							break;
						}
					}
				}
			}
		}
		SkeletonUtilityBone[] array7 = componentsInChildren5;
		for (int num2 = 0; num2 < array7.Length; num2++)
		{
			SkeletonUtilityBone skeletonUtilityBone3 = array7[num2];
			Object.Destroy(skeletonUtilityBone3);
		}
	}

	private void FixedUpdate()
	{
		this.shadowRoot.GetComponent<Rigidbody>().MovePosition(base.get_transform().get_position());
		this.shadowRoot.GetComponent<Rigidbody>().MoveRotation(base.get_transform().get_rotation());
		using (Dictionary<Transform, Transform>.Enumerator enumerator = this.shadowTable.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<Transform, Transform> current = enumerator.get_Current();
				current.get_Value().set_localPosition(current.get_Key().get_localPosition());
				current.get_Value().set_localRotation(current.get_Key().get_localRotation());
			}
		}
	}
}
