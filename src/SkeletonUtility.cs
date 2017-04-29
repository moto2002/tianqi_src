using Spine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(ISkeletonAnimation))]
public class SkeletonUtility : MonoBehaviour
{
	public delegate void SkeletonUtilityDelegate();

	public Transform boneRoot;

	[HideInInspector]
	public SkeletonRenderer skeletonRenderer;

	[HideInInspector]
	public ISkeletonAnimation skeletonAnimation;

	[NonSerialized]
	public List<SkeletonUtilityBone> utilityBones = new List<SkeletonUtilityBone>();

	[NonSerialized]
	public List<SkeletonUtilityConstraint> utilityConstraints = new List<SkeletonUtilityConstraint>();

	protected bool hasTransformBones;

	protected bool hasUtilityConstraints;

	protected bool needToReprocessBones;

	public event SkeletonUtility.SkeletonUtilityDelegate OnReset
	{
		[MethodImpl(32)]
		add
		{
			this.OnReset = (SkeletonUtility.SkeletonUtilityDelegate)Delegate.Combine(this.OnReset, value);
		}
		[MethodImpl(32)]
		remove
		{
			this.OnReset = (SkeletonUtility.SkeletonUtilityDelegate)Delegate.Remove(this.OnReset, value);
		}
	}

	public static T GetInParent<T>(Transform origin) where T : Component
	{
		return origin.GetComponentInParent<T>();
	}

	public static PolygonCollider2D AddBoundingBox(Skeleton skeleton, string skinName, string slotName, string attachmentName, Transform parent, bool isTrigger = true)
	{
		if (skinName == string.Empty)
		{
			skinName = skeleton.Data.DefaultSkin.Name;
		}
		Skin skin = skeleton.Data.FindSkin(skinName);
		if (skin == null)
		{
			Debug.LogError("Skin " + skinName + " not found!");
			return null;
		}
		Attachment attachment = skin.GetAttachment(skeleton.FindSlotIndex(slotName), attachmentName);
		if (attachment is BoundingBoxAttachment)
		{
			GameObject gameObject = new GameObject("[BoundingBox]" + attachmentName);
			gameObject.get_transform().set_parent(parent);
			gameObject.get_transform().set_localPosition(Vector3.get_zero());
			gameObject.get_transform().set_localRotation(Quaternion.get_identity());
			gameObject.get_transform().set_localScale(Vector3.get_one());
			PolygonCollider2D polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
			polygonCollider2D.set_isTrigger(isTrigger);
			BoundingBoxAttachment boundingBoxAttachment = (BoundingBoxAttachment)attachment;
			float[] vertices = boundingBoxAttachment.Vertices;
			int num = vertices.Length;
			int num2 = num / 2;
			Vector2[] array = new Vector2[num2];
			int num3 = 0;
			int i = 0;
			while (i < num)
			{
				array[num3].x = vertices[i];
				array[num3].y = vertices[i + 1];
				i += 2;
				num3++;
			}
			polygonCollider2D.SetPath(0, array);
			return polygonCollider2D;
		}
		return null;
	}

	public static PolygonCollider2D AddBoundingBoxAsComponent(BoundingBoxAttachment boundingBox, GameObject gameObject, bool isTrigger = true)
	{
		if (boundingBox == null)
		{
			return null;
		}
		PolygonCollider2D polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
		polygonCollider2D.set_isTrigger(isTrigger);
		float[] vertices = boundingBox.Vertices;
		int num = vertices.Length;
		int num2 = num / 2;
		Vector2[] array = new Vector2[num2];
		int num3 = 0;
		int i = 0;
		while (i < num)
		{
			array[num3].x = vertices[i];
			array[num3].y = vertices[i + 1];
			i += 2;
			num3++;
		}
		polygonCollider2D.SetPath(0, array);
		return polygonCollider2D;
	}

	public static Bounds GetBoundingBoxBounds(BoundingBoxAttachment boundingBox, float depth = 0f)
	{
		float[] vertices = boundingBox.Vertices;
		int num = vertices.Length;
		Bounds result = default(Bounds);
		result.set_center(new Vector3(vertices[0], vertices[1], 0f));
		for (int i = 2; i < num; i += 2)
		{
			result.Encapsulate(new Vector3(vertices[i], vertices[i + 1], 0f));
		}
		Vector3 size = result.get_size();
		size.z = depth;
		result.set_size(size);
		return result;
	}

	private void Update()
	{
		if (this.boneRoot != null && this.skeletonRenderer.skeleton != null)
		{
			Vector3 one = Vector3.get_one();
			if (this.skeletonRenderer.skeleton.FlipX)
			{
				one.x = -1f;
			}
			if (this.skeletonRenderer.skeleton.FlipY)
			{
				one.y = -1f;
			}
			this.boneRoot.set_localScale(one);
		}
	}

	private void OnEnable()
	{
		if (this.skeletonRenderer == null)
		{
			this.skeletonRenderer = base.GetComponent<SkeletonRenderer>();
		}
		if (this.skeletonAnimation == null)
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			if (this.skeletonAnimation == null)
			{
				this.skeletonAnimation = base.GetComponent<SkeletonAnimator>();
			}
		}
		SkeletonRenderer expr_51 = this.skeletonRenderer;
		expr_51.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Remove(expr_51.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleRendererReset));
		SkeletonRenderer expr_78 = this.skeletonRenderer;
		expr_78.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Combine(expr_78.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleRendererReset));
		if (this.skeletonAnimation != null)
		{
			this.skeletonAnimation.UpdateLocal -= new UpdateBonesDelegate(this.UpdateLocal);
			this.skeletonAnimation.UpdateLocal += new UpdateBonesDelegate(this.UpdateLocal);
		}
		this.CollectBones();
	}

	private void Start()
	{
	}

	private void OnDisable()
	{
		SkeletonRenderer expr_06 = this.skeletonRenderer;
		expr_06.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Remove(expr_06.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleRendererReset));
		if (this.skeletonAnimation != null)
		{
			this.skeletonAnimation.UpdateLocal -= new UpdateBonesDelegate(this.UpdateLocal);
			this.skeletonAnimation.UpdateWorld -= new UpdateBonesDelegate(this.UpdateWorld);
			this.skeletonAnimation.UpdateComplete -= new UpdateBonesDelegate(this.UpdateComplete);
		}
	}

	private void HandleRendererReset(SkeletonRenderer r)
	{
		if (this.OnReset != null)
		{
			this.OnReset();
		}
		this.CollectBones();
	}

	public void RegisterBone(SkeletonUtilityBone bone)
	{
		if (this.utilityBones.Contains(bone))
		{
			return;
		}
		this.utilityBones.Add(bone);
		this.needToReprocessBones = true;
	}

	public void UnregisterBone(SkeletonUtilityBone bone)
	{
		this.utilityBones.Remove(bone);
	}

	public void RegisterConstraint(SkeletonUtilityConstraint constraint)
	{
		if (this.utilityConstraints.Contains(constraint))
		{
			return;
		}
		this.utilityConstraints.Add(constraint);
		this.needToReprocessBones = true;
	}

	public void UnregisterConstraint(SkeletonUtilityConstraint constraint)
	{
		this.utilityConstraints.Remove(constraint);
	}

	public void CollectBones()
	{
		if (this.skeletonRenderer.skeleton == null)
		{
			return;
		}
		if (this.boneRoot != null)
		{
			List<string> list = new List<string>();
			ExposedList<IkConstraint> ikConstraints = this.skeletonRenderer.skeleton.IkConstraints;
			int i = 0;
			int count = ikConstraints.Count;
			while (i < count)
			{
				list.Add(ikConstraints.Items[i].Target.Data.Name);
				i++;
			}
			using (List<SkeletonUtilityBone>.Enumerator enumerator = this.utilityBones.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SkeletonUtilityBone current = enumerator.get_Current();
					if (current.bone == null)
					{
						return;
					}
					if (current.mode == SkeletonUtilityBone.Mode.Override)
					{
						this.hasTransformBones = true;
					}
					if (list.Contains(current.bone.Data.Name))
					{
						this.hasUtilityConstraints = true;
					}
				}
			}
			if (this.utilityConstraints.get_Count() > 0)
			{
				this.hasUtilityConstraints = true;
			}
			if (this.skeletonAnimation != null)
			{
				this.skeletonAnimation.UpdateWorld -= new UpdateBonesDelegate(this.UpdateWorld);
				this.skeletonAnimation.UpdateComplete -= new UpdateBonesDelegate(this.UpdateComplete);
				if (this.hasTransformBones || this.hasUtilityConstraints)
				{
					this.skeletonAnimation.UpdateWorld += new UpdateBonesDelegate(this.UpdateWorld);
				}
				if (this.hasUtilityConstraints)
				{
					this.skeletonAnimation.UpdateComplete += new UpdateBonesDelegate(this.UpdateComplete);
				}
			}
			this.needToReprocessBones = false;
		}
		else
		{
			this.utilityBones.Clear();
			this.utilityConstraints.Clear();
		}
	}

	private void UpdateLocal(SkeletonRenderer anim)
	{
		if (this.needToReprocessBones)
		{
			this.CollectBones();
		}
		if (this.utilityBones == null)
		{
			return;
		}
		using (List<SkeletonUtilityBone>.Enumerator enumerator = this.utilityBones.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SkeletonUtilityBone current = enumerator.get_Current();
				current.transformLerpComplete = false;
			}
		}
		this.UpdateAllBones();
	}

	private void UpdateWorld(SkeletonRenderer anim)
	{
		this.UpdateAllBones();
		using (List<SkeletonUtilityConstraint>.Enumerator enumerator = this.utilityConstraints.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SkeletonUtilityConstraint current = enumerator.get_Current();
				current.DoUpdate();
			}
		}
	}

	private void UpdateComplete(SkeletonRenderer anim)
	{
		this.UpdateAllBones();
	}

	private void UpdateAllBones()
	{
		if (this.boneRoot == null)
		{
			this.CollectBones();
		}
		if (this.utilityBones == null)
		{
			return;
		}
		using (List<SkeletonUtilityBone>.Enumerator enumerator = this.utilityBones.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				SkeletonUtilityBone current = enumerator.get_Current();
				current.DoUpdate();
			}
		}
	}

	public Transform GetBoneRoot()
	{
		if (this.boneRoot != null)
		{
			return this.boneRoot;
		}
		this.boneRoot = new GameObject("SkeletonUtility-Root").get_transform();
		this.boneRoot.set_parent(base.get_transform());
		this.boneRoot.set_localPosition(Vector3.get_zero());
		this.boneRoot.set_localRotation(Quaternion.get_identity());
		this.boneRoot.set_localScale(Vector3.get_one());
		return this.boneRoot;
	}

	public GameObject SpawnRoot(SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
	{
		this.GetBoneRoot();
		Skeleton skeleton = this.skeletonRenderer.skeleton;
		GameObject result = this.SpawnBone(skeleton.RootBone, this.boneRoot, mode, pos, rot, sca);
		this.CollectBones();
		return result;
	}

	public GameObject SpawnHierarchy(SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
	{
		this.GetBoneRoot();
		Skeleton skeleton = this.skeletonRenderer.skeleton;
		GameObject result = this.SpawnBoneRecursively(skeleton.RootBone, this.boneRoot, mode, pos, rot, sca);
		this.CollectBones();
		return result;
	}

	public GameObject SpawnBoneRecursively(Bone bone, Transform parent, SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
	{
		GameObject gameObject = this.SpawnBone(bone, parent, mode, pos, rot, sca);
		ExposedList<Bone> children = bone.Children;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Bone bone2 = children.Items[i];
			this.SpawnBoneRecursively(bone2, gameObject.get_transform(), mode, pos, rot, sca);
			i++;
		}
		return gameObject;
	}

	public GameObject SpawnBone(Bone bone, Transform parent, SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
	{
		GameObject gameObject = new GameObject(bone.Data.Name);
		gameObject.get_transform().set_parent(parent);
		SkeletonUtilityBone skeletonUtilityBone = gameObject.AddComponent<SkeletonUtilityBone>();
		skeletonUtilityBone.skeletonUtility = this;
		skeletonUtilityBone.position = pos;
		skeletonUtilityBone.rotation = rot;
		skeletonUtilityBone.scale = sca;
		skeletonUtilityBone.mode = mode;
		skeletonUtilityBone.zPosition = true;
		skeletonUtilityBone.Reset();
		skeletonUtilityBone.bone = bone;
		skeletonUtilityBone.boneName = bone.Data.Name;
		skeletonUtilityBone.valid = true;
		if (mode == SkeletonUtilityBone.Mode.Override)
		{
			if (rot)
			{
				gameObject.get_transform().set_localRotation(Quaternion.Euler(0f, 0f, skeletonUtilityBone.bone.RotationIK));
			}
			if (pos)
			{
				gameObject.get_transform().set_localPosition(new Vector3(skeletonUtilityBone.bone.X, skeletonUtilityBone.bone.Y, 0f));
			}
			gameObject.get_transform().set_localScale(new Vector3(skeletonUtilityBone.bone.scaleX, skeletonUtilityBone.bone.scaleY, 0f));
		}
		return gameObject;
	}

	public void SpawnSubRenderers(bool disablePrimaryRenderer)
	{
		int subMeshCount = base.GetComponent<MeshFilter>().get_sharedMesh().get_subMeshCount();
		for (int i = 0; i < subMeshCount; i++)
		{
			GameObject gameObject = new GameObject("Submesh " + i, new Type[]
			{
				typeof(MeshFilter),
				typeof(MeshRenderer)
			});
			gameObject.get_transform().set_parent(base.get_transform());
			gameObject.get_transform().set_localPosition(Vector3.get_zero());
			gameObject.get_transform().set_localRotation(Quaternion.get_identity());
			gameObject.get_transform().set_localScale(Vector3.get_one());
			SkeletonUtilitySubmeshRenderer skeletonUtilitySubmeshRenderer = gameObject.AddComponent<SkeletonUtilitySubmeshRenderer>();
			skeletonUtilitySubmeshRenderer.GetComponent<Renderer>().set_sortingOrder(i * 10);
			skeletonUtilitySubmeshRenderer.submeshIndex = i;
		}
		this.skeletonRenderer.CollectSubmeshRenderers();
		if (disablePrimaryRenderer)
		{
			base.GetComponent<Renderer>().set_enabled(false);
		}
	}
}
