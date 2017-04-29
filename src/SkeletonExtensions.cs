using Spine;
using System;
using UnityEngine;

public static class SkeletonExtensions
{
	public static void SetColor(this Skeleton skeleton, Color color)
	{
		skeleton.A = color.a;
		skeleton.R = color.r;
		skeleton.G = color.g;
		skeleton.B = color.b;
	}

	public static void SetColor(this Skeleton skeleton, Color32 color)
	{
		skeleton.A = (float)color.a / 255f;
		skeleton.R = (float)color.r / 255f;
		skeleton.G = (float)color.g / 255f;
		skeleton.B = (float)color.b / 255f;
	}

	public static void SetColor(this Slot slot, Color color)
	{
		slot.A = color.a;
		slot.R = color.r;
		slot.G = color.g;
		slot.B = color.b;
	}

	public static void SetColor(this Slot slot, Color32 color)
	{
		slot.A = (float)color.a / 255f;
		slot.R = (float)color.r / 255f;
		slot.G = (float)color.g / 255f;
		slot.B = (float)color.b / 255f;
	}

	public static void SetColor(this RegionAttachment attachment, Color color)
	{
		attachment.A = color.a;
		attachment.R = color.r;
		attachment.G = color.g;
		attachment.B = color.b;
	}

	public static void SetColor(this RegionAttachment attachment, Color32 color)
	{
		attachment.A = (float)color.a / 255f;
		attachment.R = (float)color.r / 255f;
		attachment.G = (float)color.g / 255f;
		attachment.B = (float)color.b / 255f;
	}

	public static void SetColor(this MeshAttachment attachment, Color color)
	{
		attachment.A = color.a;
		attachment.R = color.r;
		attachment.G = color.g;
		attachment.B = color.b;
	}

	public static void SetColor(this MeshAttachment attachment, Color32 color)
	{
		attachment.A = (float)color.a / 255f;
		attachment.R = (float)color.r / 255f;
		attachment.G = (float)color.g / 255f;
		attachment.B = (float)color.b / 255f;
	}

	public static void SetColor(this SkinnedMeshAttachment attachment, Color color)
	{
		attachment.A = color.a;
		attachment.R = color.r;
		attachment.G = color.g;
		attachment.B = color.b;
	}

	public static void SetColor(this SkinnedMeshAttachment attachment, Color32 color)
	{
		attachment.A = (float)color.a / 255f;
		attachment.R = (float)color.r / 255f;
		attachment.G = (float)color.g / 255f;
		attachment.B = (float)color.b / 255f;
	}

	public static void SetPosition(this Bone bone, Vector2 position)
	{
		bone.X = position.x;
		bone.Y = position.y;
	}

	public static void SetPosition(this Bone bone, Vector3 position)
	{
		bone.X = position.x;
		bone.Y = position.y;
	}

	public static Attachment AttachUnitySprite(this Skeleton skeleton, string slotName, Sprite sprite, string shaderName = "Spine/Skeleton")
	{
		RegionAttachment regionAttachment = sprite.ToRegionAttachment(shaderName);
		skeleton.FindSlot(slotName).Attachment = regionAttachment;
		return regionAttachment;
	}

	public static Attachment AddUnitySprite(this SkeletonData skeletonData, string slotName, Sprite sprite, string skinName = "", string shaderName = "Spine/Skeleton")
	{
		RegionAttachment regionAttachment = sprite.ToRegionAttachment(shaderName);
		int slotIndex = skeletonData.FindSlotIndex(slotName);
		Skin skin = skeletonData.defaultSkin;
		if (skinName != string.Empty)
		{
			skin = skeletonData.FindSkin(skinName);
		}
		skin.AddAttachment(slotIndex, regionAttachment.Name, regionAttachment);
		return regionAttachment;
	}

	public static RegionAttachment ToRegionAttachment(this Sprite sprite, string shaderName = "Spine/Skeleton")
	{
		SpriteAttachmentLoader spriteAttachmentLoader = new SpriteAttachmentLoader(sprite, Shader.Find(shaderName));
		return spriteAttachmentLoader.NewRegionAttachment(null, sprite.get_name(), string.Empty);
	}
}
