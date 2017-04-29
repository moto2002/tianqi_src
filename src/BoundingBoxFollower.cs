using Spine;
using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoundingBoxFollower : MonoBehaviour
{
	public SkeletonRenderer skeletonRenderer;

	[SpineSlot("", "skeletonRenderer", true)]
	public string slotName;

	[Tooltip("LOL JK, Someone else do it!")]
	public bool use3DMeshCollider;

	private Slot slot;

	private BoundingBoxAttachment currentAttachment;

	private PolygonCollider2D currentCollider;

	private string currentAttachmentName;

	private bool valid;

	private bool hasReset;

	public Dictionary<BoundingBoxAttachment, PolygonCollider2D> colliderTable = new Dictionary<BoundingBoxAttachment, PolygonCollider2D>();

	public Dictionary<BoundingBoxAttachment, string> attachmentNameTable = new Dictionary<BoundingBoxAttachment, string>();

	public string CurrentAttachmentName
	{
		get
		{
			return this.currentAttachmentName;
		}
	}

	public BoundingBoxAttachment CurrentAttachment
	{
		get
		{
			return this.currentAttachment;
		}
	}

	public PolygonCollider2D CurrentCollider
	{
		get
		{
			return this.currentCollider;
		}
	}

	public Slot Slot
	{
		get
		{
			return this.slot;
		}
	}

	private void OnEnable()
	{
		this.ClearColliders();
		if (this.skeletonRenderer == null)
		{
			this.skeletonRenderer = base.GetComponentInParent<SkeletonRenderer>();
		}
		if (this.skeletonRenderer != null)
		{
			SkeletonRenderer expr_3A = this.skeletonRenderer;
			expr_3A.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Remove(expr_3A.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleReset));
			SkeletonRenderer expr_61 = this.skeletonRenderer;
			expr_61.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Combine(expr_61.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleReset));
			if (this.hasReset)
			{
				this.HandleReset(this.skeletonRenderer);
			}
		}
	}

	private void OnDisable()
	{
		SkeletonRenderer expr_06 = this.skeletonRenderer;
		expr_06.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Remove(expr_06.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleReset));
	}

	private void Start()
	{
		if (!this.hasReset && this.skeletonRenderer != null)
		{
			this.HandleReset(this.skeletonRenderer);
		}
	}

	public void HandleReset(SkeletonRenderer renderer)
	{
		if (this.slotName == null || this.slotName == string.Empty)
		{
			return;
		}
		this.hasReset = true;
		this.ClearColliders();
		this.colliderTable.Clear();
		if (this.skeletonRenderer.skeleton == null)
		{
			SkeletonRenderer expr_4F = this.skeletonRenderer;
			expr_4F.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Remove(expr_4F.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleReset));
			this.skeletonRenderer.Reset();
			SkeletonRenderer expr_81 = this.skeletonRenderer;
			expr_81.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Combine(expr_81.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.HandleReset));
		}
		Skeleton skeleton = this.skeletonRenderer.skeleton;
		this.slot = skeleton.FindSlot(this.slotName);
		int slotIndex = skeleton.FindSlotIndex(this.slotName);
		foreach (Skin current in skeleton.Data.Skins)
		{
			List<string> list = new List<string>();
			current.FindNamesForSlot(slotIndex, list);
			using (List<string>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string current2 = enumerator2.get_Current();
					Attachment attachment = current.GetAttachment(slotIndex, current2);
					if (attachment is BoundingBoxAttachment)
					{
						PolygonCollider2D polygonCollider2D = SkeletonUtility.AddBoundingBoxAsComponent((BoundingBoxAttachment)attachment, base.get_gameObject(), true);
						polygonCollider2D.set_enabled(false);
						polygonCollider2D.set_hideFlags(2);
						this.colliderTable.Add((BoundingBoxAttachment)attachment, polygonCollider2D);
						this.attachmentNameTable.Add((BoundingBoxAttachment)attachment, current2);
					}
				}
			}
		}
		if (this.colliderTable.get_Count() == 0)
		{
			this.valid = false;
		}
		else
		{
			this.valid = true;
		}
		if (!this.valid)
		{
			Debug.LogWarning("Bounding Box Follower not valid! Slot [" + this.slotName + "] does not contain any Bounding Box Attachments!");
		}
	}

	private void ClearColliders()
	{
		PolygonCollider2D[] components = base.GetComponents<PolygonCollider2D>();
		if (Application.get_isPlaying())
		{
			PolygonCollider2D[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				PolygonCollider2D polygonCollider2D = array[i];
				Object.Destroy(polygonCollider2D);
			}
		}
		else
		{
			PolygonCollider2D[] array2 = components;
			for (int j = 0; j < array2.Length; j++)
			{
				PolygonCollider2D polygonCollider2D2 = array2[j];
				Object.DestroyImmediate(polygonCollider2D2);
			}
		}
		this.colliderTable.Clear();
		this.attachmentNameTable.Clear();
	}

	private void LateUpdate()
	{
		if (!this.skeletonRenderer.valid)
		{
			return;
		}
		if (this.slot != null && this.slot.Attachment != this.currentAttachment)
		{
			this.SetCurrent((BoundingBoxAttachment)this.slot.Attachment);
		}
	}

	private void SetCurrent(BoundingBoxAttachment attachment)
	{
		if (this.currentCollider)
		{
			this.currentCollider.set_enabled(false);
		}
		if (attachment != null)
		{
			this.currentCollider = this.colliderTable.get_Item(attachment);
			this.currentCollider.set_enabled(true);
		}
		else
		{
			this.currentCollider = null;
		}
		this.currentAttachment = attachment;
		this.currentAttachmentName = ((this.currentAttachment != null) ? this.attachmentNameTable.get_Item(attachment) : null);
	}
}
