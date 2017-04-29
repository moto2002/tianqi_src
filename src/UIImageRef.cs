using System;
using UnityEngine;
using UnityEngine.UI;
using XEngine;

[ExecuteInEditMode]
public class UIImageRef : MonoBehaviour
{
	public const string NO_BINDING_RES = "#";

	public Image mImage;

	public RawImage mRawImage;

	public string sprite_name = "#";

	private bool _IsAwake;

	public bool IsAwake
	{
		get
		{
			return this._IsAwake;
		}
		set
		{
			this._IsAwake = value;
		}
	}

	public bool IsBinding()
	{
		return !string.IsNullOrEmpty(this.sprite_name) && !(this.sprite_name == "#");
	}

	private void FindImageIfNull()
	{
		if (this.mImage == null)
		{
			this.mImage = base.GetComponent<Image>();
		}
	}

	private void FindRawImageIfNull()
	{
		if (this.mRawImage == null)
		{
			this.mRawImage = base.GetComponent<RawImage>();
		}
	}

	private void Awake()
	{
		this.IsAwake = true;
		if (this.IsBinding())
		{
			this.InitResBinder();
		}
		else
		{
			this.FindImageIfNull();
			if (this.mImage != null)
			{
				AssetManager.AssetOfTPManager.AddReferenceCount(this.mImage);
				return;
			}
			this.FindRawImageIfNull();
			if (this.mRawImage != null)
			{
				UIImageRef.AddReferenceCountOfRawImage(this.mRawImage);
				return;
			}
		}
	}

	private void OnDestroy()
	{
		if (this.mImage != null)
		{
			AssetManager.AssetOfTPManager.MinusReferenceCount(this.mImage);
			return;
		}
		if (this.mRawImage != null)
		{
			UIImageRef.MinusReferenceCountOfRawImage(this.mRawImage);
			return;
		}
	}

	public void InitSelf()
	{
		if (this.IsBinding())
		{
			this.InitResBinder();
		}
	}

	private void InitResBinder()
	{
		this.FindImageIfNull();
		if (this.mImage != null)
		{
			ResourceManager.SetIconSprite(this.mImage, this.sprite_name);
			return;
		}
		this.FindRawImageIfNull();
		if (this.mRawImage != null)
		{
			ResourceManager.SetTexture(this.mRawImage, this.sprite_name);
			return;
		}
	}

	public static bool GetIsRefAwakeWithSetNull(GameObject go)
	{
		UIImageRef component = go.GetComponent<UIImageRef>();
		if (component != null)
		{
			component.sprite_name = "#";
			return component.IsAwake;
		}
		return false;
	}

	public static void AddReferenceCountOfRawImage(RawImage rawImage)
	{
		if (rawImage != null && rawImage.get_texture() != null)
		{
			AssetManager.AddAssetRef(FileSystem.GetPath(rawImage.get_texture().get_name(), string.Empty));
		}
	}

	public static void AddReferenceCountOfRawImage(Texture texture)
	{
		if (texture != null)
		{
			AssetManager.AddAssetRef(FileSystem.GetPath(texture.get_name(), string.Empty));
		}
	}

	public static void MinusReferenceCountOfRawImage(RawImage rawImage)
	{
		if (rawImage != null && rawImage.get_texture() != null)
		{
			AssetManager.MinusAssetRef(FileSystem.GetPath(rawImage.get_texture().get_name(), string.Empty));
		}
	}
}
