using System;
using UnityEngine;

public abstract class ResourceManagerBase
{
	public const string NullTextureName = "EmptyTexture";

	public const string NullIconName = "Empty";

	private static Texture NullTexture;

	private static SpriteRenderer NullSprite;

	public static Texture GetNullTexture()
	{
		if (ResourceManagerBase.NullTexture == null)
		{
			ResourceManagerBase.NullTexture = AssetManager.GetTexture("EmptyTexture");
		}
		return ResourceManagerBase.NullTexture;
	}

	public static SpriteRenderer GetNullSprite()
	{
		if (ResourceManagerBase.NullSprite == null)
		{
			ResourceManagerBase.NullSprite = AssetManager.AssetOfTPManager.GetSpriteRenderer("Empty");
		}
		return ResourceManagerBase.NullSprite;
	}
}
