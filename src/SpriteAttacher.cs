using Spine;
using System;
using UnityEngine;

public class SpriteAttacher : MonoBehaviour
{
	public bool attachOnStart = true;

	public bool keepLoaderInMemory = true;

	public Sprite sprite;

	[SpineSlot("", "", false)]
	public string slot;

	private SpriteAttachmentLoader loader;

	private RegionAttachment attachment;

	private void Start()
	{
		if (this.attachOnStart)
		{
			this.Attach();
		}
	}

	public void Attach()
	{
		SkeletonRenderer component = base.GetComponent<SkeletonRenderer>();
		if (this.loader == null)
		{
			this.loader = new SpriteAttachmentLoader(this.sprite, Shader.Find("Spine/Skeleton"));
		}
		if (this.attachment == null)
		{
			this.attachment = this.loader.NewRegionAttachment(null, this.sprite.get_name(), string.Empty);
		}
		component.skeleton.FindSlot(this.slot).Attachment = this.attachment;
		if (!this.keepLoaderInMemory)
		{
			this.loader = null;
		}
	}
}
