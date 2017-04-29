using Spine;
using System;
using System.Collections;
using UnityEngine;

public class AtlasRegionAttacher : MonoBehaviour
{
	[Serializable]
	public class SlotRegionPair
	{
		[SpineSlot("", "", false)]
		public string slot;

		[SpineAtlasRegion]
		public string region;
	}

	public AtlasAsset atlasAsset;

	public AtlasRegionAttacher.SlotRegionPair[] attachments;

	private Atlas atlas;

	private void Awake()
	{
		SkeletonRenderer expr_06 = base.GetComponent<SkeletonRenderer>();
		expr_06.OnReset = (SkeletonRenderer.SkeletonRendererDelegate)Delegate.Combine(expr_06.OnReset, new SkeletonRenderer.SkeletonRendererDelegate(this.Apply));
	}

	private void Apply(SkeletonRenderer skeletonRenderer)
	{
		this.atlas = this.atlasAsset.GetAtlas();
		AtlasAttachmentLoader atlasAttachmentLoader = new AtlasAttachmentLoader(new Atlas[]
		{
			this.atlas
		});
		float scale = skeletonRenderer.skeletonDataAsset.scale;
		IEnumerator enumerator = this.attachments.GetEnumerator();
		while (enumerator.MoveNext())
		{
			AtlasRegionAttacher.SlotRegionPair slotRegionPair = (AtlasRegionAttacher.SlotRegionPair)enumerator.get_Current();
			RegionAttachment regionAttachment = atlasAttachmentLoader.NewRegionAttachment(null, slotRegionPair.region, slotRegionPair.region);
			regionAttachment.Width = regionAttachment.RegionOriginalWidth * scale;
			regionAttachment.Height = regionAttachment.RegionOriginalHeight * scale;
			regionAttachment.SetColor(new Color(1f, 1f, 1f, 1f));
			regionAttachment.UpdateOffset();
			Slot slot = skeletonRenderer.skeleton.FindSlot(slotRegionPair.slot);
			slot.Attachment = regionAttachment;
		}
	}
}
