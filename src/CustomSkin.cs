using Spine;
using System;
using UnityEngine;

public class CustomSkin : MonoBehaviour
{
	[Serializable]
	public class SkinPair
	{
		[SpineAttachment(false, true, false, "", "skinSource")]
		public string sourceAttachment;

		[SpineSlot("", "", false)]
		public string targetSlot;

		[SpineAttachment(true, false, true, "", "")]
		public string targetAttachment;
	}

	public SkeletonDataAsset skinSource;

	public CustomSkin.SkinPair[] skinning;

	public Skin customSkin;

	private SkeletonRenderer skeletonRenderer;

	private void Start()
	{
		this.skeletonRenderer = base.GetComponent<SkeletonRenderer>();
		Skeleton skeleton = this.skeletonRenderer.skeleton;
		this.customSkin = new Skin("CustomSkin");
		CustomSkin.SkinPair[] array = this.skinning;
		for (int i = 0; i < array.Length; i++)
		{
			CustomSkin.SkinPair skinPair = array[i];
			Attachment attachment = SpineAttachment.GetAttachment(skinPair.sourceAttachment, this.skinSource);
			this.customSkin.AddAttachment(skeleton.FindSlotIndex(skinPair.targetSlot), skinPair.targetAttachment, attachment);
		}
		skeleton.SetSkin(this.customSkin);
	}
}
