using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(SkeletonRenderer))]
public class SkeletonRagdoll2D : MonoBehaviour
{
	private static Transform helper;

	[SpineBone("", ""), Header("Hierarchy")]
	public string startingBoneName = string.Empty;

	[SpineBone("", "")]
	public List<string> stopBoneNames = new List<string>();

	[Header("Parameters")]
	public bool applyOnStart;

	[Tooltip("Set RootRigidbody IsKinematic to true when Apply is called.")]
	public bool pinStartBone;

	public float gravityScale = 1f;

	[Tooltip("Warning!  You will have to re-enable and tune mix values manually if attempting to remove the ragdoll system.")]
	public bool disableIK = true;

	[Tooltip("If no BoundingBox Attachment is attached to a bone, this becomes the default Width or Radius of a Bone's ragdoll Rigidbody")]
	public float thickness = 0.125f;

	[Tooltip("Default rotational limit value.  Min is negative this value, Max is this value.")]
	public float rotationLimit = 20f;

	public float rootMass = 20f;

	[Range(0.01f, 1f), Tooltip("If your ragdoll seems unstable or uneffected by limits, try lowering this value.")]
	public float massFalloffFactor = 0.4f;

	[Tooltip("The layer assigned to all of the rigidbody parts.")]
	public int colliderLayer;

	[Range(0f, 1f)]
	public float mix = 1f;

	private Rigidbody2D rootRigidbody;

	private ISkeletonAnimation skeletonAnim;

	private Skeleton skeleton;

	private Dictionary<Bone, Transform> boneTable = new Dictionary<Bone, Transform>();

	private Bone startingBone;

	private Transform ragdollRoot;

	private Vector2 rootOffset;

	private bool isActive;

	public Rigidbody2D RootRigidbody
	{
		get
		{
			return this.rootRigidbody;
		}
	}

	public Vector3 RootOffset
	{
		get
		{
			return this.rootOffset;
		}
	}

	public Vector3 EstimatedSkeletonPosition
	{
		get
		{
			return this.rootRigidbody.get_position() - this.rootOffset;
		}
	}

	public bool IsActive
	{
		get
		{
			return this.isActive;
		}
	}

	[DebuggerHidden]
	private IEnumerator Start()
	{
		SkeletonRagdoll2D.<Start>c__Iterator9 <Start>c__Iterator = new SkeletonRagdoll2D.<Start>c__Iterator9();
		<Start>c__Iterator.<>f__this = this;
		return <Start>c__Iterator;
	}

	public Coroutine SmoothMix(float target, float duration)
	{
		return base.StartCoroutine(this.SmoothMixCoroutine(target, duration));
	}

	[DebuggerHidden]
	private IEnumerator SmoothMixCoroutine(float target, float duration)
	{
		SkeletonRagdoll2D.<SmoothMixCoroutine>c__IteratorA <SmoothMixCoroutine>c__IteratorA = new SkeletonRagdoll2D.<SmoothMixCoroutine>c__IteratorA();
		<SmoothMixCoroutine>c__IteratorA.target = target;
		<SmoothMixCoroutine>c__IteratorA.duration = duration;
		<SmoothMixCoroutine>c__IteratorA.<$>target = target;
		<SmoothMixCoroutine>c__IteratorA.<$>duration = duration;
		<SmoothMixCoroutine>c__IteratorA.<>f__this = this;
		return <SmoothMixCoroutine>c__IteratorA;
	}

	public void SetSkeletonPosition(Vector3 worldPosition)
	{
		if (!this.isActive)
		{
			Debug.LogWarning("Can't call SetSkeletonPosition while Ragdoll is not active!");
			return;
		}
		Vector3 vector = worldPosition - base.get_transform().get_position();
		base.get_transform().set_position(worldPosition);
		using (Dictionary<Bone, Transform>.ValueCollection.Enumerator enumerator = this.boneTable.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Transform current = enumerator.get_Current();
				Transform expr_53 = current;
				expr_53.set_position(expr_53.get_position() - vector);
			}
		}
		this.UpdateWorld(null);
		this.skeleton.UpdateWorldTransform();
	}

	public Rigidbody2D[] GetRigidbodyArray()
	{
		if (!this.isActive)
		{
			return new Rigidbody2D[0];
		}
		Rigidbody2D[] array = new Rigidbody2D[this.boneTable.get_Count()];
		int num = 0;
		using (Dictionary<Bone, Transform>.ValueCollection.Enumerator enumerator = this.boneTable.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Transform current = enumerator.get_Current();
				array[num] = current.GetComponent<Rigidbody2D>();
				num++;
			}
		}
		return array;
	}

	public Rigidbody2D GetRigidbody(string boneName)
	{
		Bone bone = this.skeleton.FindBone(boneName);
		if (bone == null)
		{
			return null;
		}
		if (this.boneTable.ContainsKey(bone))
		{
			return this.boneTable.get_Item(bone).GetComponent<Rigidbody2D>();
		}
		return null;
	}

	public void Remove()
	{
		this.isActive = false;
		using (Dictionary<Bone, Transform>.ValueCollection.Enumerator enumerator = this.boneTable.get_Values().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Transform current = enumerator.get_Current();
				Object.Destroy(current.get_gameObject());
			}
		}
		Object.Destroy(this.ragdollRoot.get_gameObject());
		this.boneTable.Clear();
		this.skeletonAnim.UpdateWorld -= new UpdateBonesDelegate(this.UpdateWorld);
	}

	public void Apply()
	{
		this.isActive = true;
		this.skeleton = this.skeletonAnim.Skeleton;
		this.mix = 1f;
		Bone bone = this.skeleton.FindBone(this.startingBoneName);
		this.startingBone = bone;
		this.RecursivelyCreateBoneProxies(bone);
		this.rootRigidbody = this.boneTable.get_Item(bone).GetComponent<Rigidbody2D>();
		this.rootRigidbody.set_isKinematic(this.pinStartBone);
		this.rootRigidbody.set_mass(this.rootMass);
		List<Collider2D> list = new List<Collider2D>();
		using (Dictionary<Bone, Transform>.Enumerator enumerator = this.boneTable.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<Bone, Transform> current = enumerator.get_Current();
				Bone key = current.get_Key();
				Transform value = current.get_Value();
				Transform transform = base.get_transform();
				list.Add(value.GetComponent<Collider2D>());
				if (key != this.startingBone)
				{
					Bone parent = key.Parent;
					transform = this.boneTable.get_Item(parent);
				}
				else
				{
					this.ragdollRoot = new GameObject("RagdollRoot").get_transform();
					this.ragdollRoot.set_parent(base.get_transform());
					if (key == this.skeleton.RootBone)
					{
						this.ragdollRoot.set_localPosition(new Vector3(key.WorldX, key.WorldY, 0f));
						this.ragdollRoot.set_localRotation(Quaternion.Euler(0f, 0f, this.GetCompensatedRotationIK(key)));
						transform = this.ragdollRoot;
					}
					else
					{
						this.ragdollRoot.set_localPosition(new Vector3(key.Parent.WorldX, key.Parent.WorldY, 0f));
						this.ragdollRoot.set_localRotation(Quaternion.Euler(0f, 0f, this.GetCompensatedRotationIK(key.Parent)));
						transform = this.ragdollRoot;
					}
					this.rootOffset = value.get_position() - base.get_transform().get_position();
				}
				Rigidbody2D component = transform.GetComponent<Rigidbody2D>();
				if (component != null)
				{
					HingeJoint2D hingeJoint2D = value.get_gameObject().AddComponent<HingeJoint2D>();
					hingeJoint2D.set_connectedBody(component);
					Vector3 vector = transform.InverseTransformPoint(value.get_position());
					vector.x *= 1f;
					hingeJoint2D.set_connectedAnchor(vector);
					hingeJoint2D.GetComponent<Rigidbody2D>().set_mass(hingeJoint2D.get_connectedBody().get_mass() * this.massFalloffFactor);
					JointAngleLimits2D limits = default(JointAngleLimits2D);
					limits.set_min(-this.rotationLimit);
					limits.set_max(this.rotationLimit);
					hingeJoint2D.set_limits(limits);
					hingeJoint2D.set_useLimits(true);
				}
			}
		}
		for (int i = 0; i < list.get_Count(); i++)
		{
			for (int j = 0; j < list.get_Count(); j++)
			{
				if (i != j)
				{
					Physics2D.IgnoreCollision(list.get_Item(i), list.get_Item(j));
				}
			}
		}
		SkeletonUtilityBone[] componentsInChildren = base.GetComponentsInChildren<SkeletonUtilityBone>();
		if (componentsInChildren.Length > 0)
		{
			List<string> list2 = new List<string>();
			SkeletonUtilityBone[] array = componentsInChildren;
			for (int k = 0; k < array.Length; k++)
			{
				SkeletonUtilityBone skeletonUtilityBone = array[k];
				if (skeletonUtilityBone.mode == SkeletonUtilityBone.Mode.Override)
				{
					list2.Add(skeletonUtilityBone.get_gameObject().get_name());
					Object.Destroy(skeletonUtilityBone.get_gameObject());
				}
			}
			if (list2.get_Count() > 0)
			{
				string text = "Destroyed Utility Bones: ";
				for (int l = 0; l < list2.get_Count(); l++)
				{
					text += list2.get_Item(l);
					if (l != list2.get_Count() - 1)
					{
						text += ",";
					}
				}
				Debug.LogWarning(text);
			}
		}
		if (this.disableIK)
		{
			foreach (IkConstraint current2 in this.skeleton.IkConstraints)
			{
				current2.Mix = 0f;
			}
		}
		this.skeletonAnim.UpdateWorld += new UpdateBonesDelegate(this.UpdateWorld);
	}

	private void RecursivelyCreateBoneProxies(Bone b)
	{
		if (this.stopBoneNames.Contains(b.Data.Name))
		{
			return;
		}
		GameObject gameObject = new GameObject(b.Data.Name);
		gameObject.set_layer(this.colliderLayer);
		Transform transform = gameObject.get_transform();
		this.boneTable.Add(b, transform);
		transform.set_parent(base.get_transform());
		transform.set_localPosition(new Vector3(b.WorldX, b.WorldY, 0f));
		transform.set_localRotation(Quaternion.Euler(0f, 0f, (!b.WorldFlipX) ? b.WorldRotation : (-b.WorldRotation)));
		transform.set_localScale(new Vector3(b.WorldScaleX, b.WorldScaleY, 0f));
		float length = b.Data.Length;
		List<Collider2D> list = this.AttachBoundingBoxRagdollColliders(b);
		if (length == 0f)
		{
			if (list.get_Count() == 0)
			{
				CircleCollider2D circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
				circleCollider2D.set_radius(this.thickness / 2f);
			}
		}
		else if (list.get_Count() == 0)
		{
			BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
			boxCollider2D.set_size(new Vector2(length, this.thickness));
			boxCollider2D.set_offset(new Vector2(((!b.WorldFlipX) ? length : (-length)) / 2f, 0f));
		}
		Rigidbody2D rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
		rigidbody2D.set_gravityScale(this.gravityScale);
		foreach (Bone current in b.Children)
		{
			this.RecursivelyCreateBoneProxies(current);
		}
	}

	private List<Collider2D> AttachBoundingBoxRagdollColliders(Bone b)
	{
		List<Collider2D> list = new List<Collider2D>();
		Transform transform = this.boneTable.get_Item(b);
		GameObject gameObject = transform.get_gameObject();
		Skin skin = this.skeleton.Skin;
		if (skin == null)
		{
			skin = this.skeleton.Data.DefaultSkin;
		}
		bool worldFlipX = b.WorldFlipX;
		bool worldFlipY = b.WorldFlipY;
		List<Attachment> list2 = new List<Attachment>();
		foreach (Slot current in this.skeleton.Slots)
		{
			if (current.Bone == b)
			{
				skin.FindAttachmentsForSlot(this.skeleton.Slots.IndexOf(current), list2);
				using (List<Attachment>.Enumerator enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Attachment current2 = enumerator2.get_Current();
						if (current2 is BoundingBoxAttachment)
						{
							if (current2.Name.ToLower().Contains("ragdoll"))
							{
								PolygonCollider2D polygonCollider2D = SkeletonUtility.AddBoundingBoxAsComponent((BoundingBoxAttachment)current2, gameObject, false);
								if (worldFlipX || worldFlipY)
								{
									Vector2[] points = polygonCollider2D.get_points();
									for (int i = 0; i < points.Length; i++)
									{
										if (worldFlipX)
										{
											Vector2[] expr_11D_cp_0 = points;
											int expr_11D_cp_1 = i;
											expr_11D_cp_0[expr_11D_cp_1].x = expr_11D_cp_0[expr_11D_cp_1].x * -1f;
										}
										if (worldFlipY)
										{
											Vector2[] expr_13E_cp_0 = points;
											int expr_13E_cp_1 = i;
											expr_13E_cp_0[expr_13E_cp_1].y = expr_13E_cp_0[expr_13E_cp_1].y * -1f;
										}
									}
									polygonCollider2D.set_points(points);
								}
								list.Add(polygonCollider2D);
							}
						}
					}
				}
			}
		}
		return list;
	}

	private void UpdateWorld(SkeletonRenderer skeletonRenderer)
	{
		using (Dictionary<Bone, Transform>.Enumerator enumerator = this.boneTable.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<Bone, Transform> current = enumerator.get_Current();
				Bone key = current.get_Key();
				Transform value = current.get_Value();
				Transform transform = base.get_transform();
				Bone parent;
				bool flag;
				bool flag2;
				if (key != this.startingBone)
				{
					parent = key.Parent;
					transform = this.boneTable.get_Item(parent);
					flag = parent.WorldFlipX;
					flag2 = parent.WorldFlipY;
				}
				else
				{
					parent = key.Parent;
					transform = this.ragdollRoot;
					if (key.Parent != null)
					{
						flag = key.worldFlipX;
						flag2 = key.WorldFlipY;
					}
					else
					{
						flag = key.Skeleton.FlipX;
						flag2 = key.Skeleton.FlipY;
					}
				}
				bool flag3 = flag ^ flag2;
				SkeletonRagdoll2D.helper.set_position(transform.get_position());
				SkeletonRagdoll2D.helper.set_rotation(transform.get_rotation());
				SkeletonRagdoll2D.helper.set_localScale(new Vector3((!flag) ? transform.get_localScale().x : (-transform.get_localScale().x), (!flag2) ? transform.get_localScale().y : (-transform.get_localScale().y), 1f));
				Vector3 vector = value.get_position();
				vector = SkeletonRagdoll2D.helper.InverseTransformPoint(vector);
				key.X = Mathf.Lerp(key.X, vector.x, this.mix);
				key.Y = Mathf.Lerp(key.Y, vector.y, this.mix);
				Vector3 vector2 = SkeletonRagdoll2D.helper.InverseTransformDirection(value.get_right());
				float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
				if (key.WorldFlipX ^ key.WorldFlipY)
				{
					num *= -1f;
				}
				if (parent != null && (key.WorldFlipX ^ key.WorldFlipY) != flag3)
				{
					num -= this.GetCompensatedRotationIK(parent) * 2f;
				}
				key.Rotation = Mathf.Lerp(key.Rotation, num, this.mix);
				key.RotationIK = Mathf.Lerp(key.rotationIK, num, this.mix);
			}
		}
	}

	private float GetCompensatedRotationIK(Bone b)
	{
		Bone parent = b.Parent;
		float num = b.RotationIK;
		while (parent != null)
		{
			num += parent.RotationIK;
			parent = parent.parent;
		}
		return num;
	}

	private void OnDrawGizmosSelected()
	{
		if (this.isActive)
		{
			Gizmos.DrawWireSphere(base.get_transform().get_position(), this.thickness * 1.2f);
			Vector3 vector = this.rootRigidbody.get_position() - this.rootOffset;
			Gizmos.DrawLine(base.get_transform().get_position(), vector);
			Gizmos.DrawWireSphere(vector, this.thickness * 1.2f);
		}
	}
}
