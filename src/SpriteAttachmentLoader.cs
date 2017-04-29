using Spine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAttachmentLoader : AttachmentLoader
{
	public static Dictionary<int, AtlasRegion> atlasTable = new Dictionary<int, AtlasRegion>();

	public static List<int> premultipliedAtlasIds = new List<int>();

	private Sprite sprite;

	private Shader shader;

	public SpriteAttachmentLoader(Sprite sprite, Shader shader)
	{
		if (sprite.get_packed() && sprite.get_packingMode() == null)
		{
			Debug.LogError("Tight Packer Policy not supported yet!");
			return;
		}
		this.sprite = sprite;
		this.shader = shader;
		Texture2D texture = sprite.get_texture();
		int instanceID = texture.GetInstanceID();
		if (!SpriteAttachmentLoader.premultipliedAtlasIds.Contains(instanceID))
		{
			try
			{
				Color[] pixels = texture.GetPixels();
				for (int i = 0; i < pixels.Length; i++)
				{
					Color color = pixels[i];
					float a = color.a;
					color.r *= a;
					color.g *= a;
					color.b *= a;
					pixels[i] = color;
				}
				texture.SetPixels(pixels);
				texture.Apply();
				SpriteAttachmentLoader.premultipliedAtlasIds.Add(instanceID);
			}
			catch
			{
			}
		}
	}

	public RegionAttachment NewRegionAttachment(Skin skin, string name, string path)
	{
		RegionAttachment regionAttachment = new RegionAttachment(name);
		Texture2D texture = this.sprite.get_texture();
		int instanceID = texture.GetInstanceID();
		AtlasRegion atlasRegion;
		if (SpriteAttachmentLoader.atlasTable.ContainsKey(instanceID))
		{
			atlasRegion = SpriteAttachmentLoader.atlasTable.get_Item(instanceID);
		}
		else
		{
			Material material = new Material(this.shader);
			if (this.sprite.get_packed())
			{
				material.set_name("Unity Packed Sprite Material");
			}
			else
			{
				material.set_name(this.sprite.get_name() + " Sprite Material");
			}
			material.set_mainTexture(texture);
			atlasRegion = new AtlasRegion();
			atlasRegion.page = new AtlasPage
			{
				rendererObject = material
			};
			SpriteAttachmentLoader.atlasTable.set_Item(instanceID, atlasRegion);
		}
		Rect textureRect = this.sprite.get_textureRect();
		textureRect.set_x(Mathf.InverseLerp(0f, (float)texture.get_width(), textureRect.get_x()));
		textureRect.set_y(Mathf.InverseLerp(0f, (float)texture.get_height(), textureRect.get_y()));
		textureRect.set_width(Mathf.InverseLerp(0f, (float)texture.get_width(), textureRect.get_width()));
		textureRect.set_height(Mathf.InverseLerp(0f, (float)texture.get_height(), textureRect.get_height()));
		Bounds bounds = this.sprite.get_bounds();
		Vector3 size = bounds.get_size();
		bool rotate = false;
		if (this.sprite.get_packed())
		{
			rotate = (this.sprite.get_packingRotation() == 15);
		}
		regionAttachment.SetUVs(textureRect.get_xMin(), textureRect.get_yMax(), textureRect.get_xMax(), textureRect.get_yMin(), rotate);
		regionAttachment.RendererObject = atlasRegion;
		regionAttachment.SetColor(Color.get_white());
		regionAttachment.ScaleX = 1f;
		regionAttachment.ScaleY = 1f;
		regionAttachment.RegionOffsetX = this.sprite.get_rect().get_width() * (0.5f - Mathf.InverseLerp(bounds.get_min().x, bounds.get_max().x, 0f)) / this.sprite.get_pixelsPerUnit();
		regionAttachment.RegionOffsetY = this.sprite.get_rect().get_height() * (0.5f - Mathf.InverseLerp(bounds.get_min().y, bounds.get_max().y, 0f)) / this.sprite.get_pixelsPerUnit();
		regionAttachment.Width = size.x;
		regionAttachment.Height = size.y;
		regionAttachment.RegionWidth = size.x;
		regionAttachment.RegionHeight = size.y;
		regionAttachment.RegionOriginalWidth = size.x;
		regionAttachment.RegionOriginalHeight = size.y;
		regionAttachment.UpdateOffset();
		return regionAttachment;
	}

	public MeshAttachment NewMeshAttachment(Skin skin, string name, string path)
	{
		throw new NotImplementedException();
	}

	public SkinnedMeshAttachment NewSkinnedMeshAttachment(Skin skin, string name, string path)
	{
		throw new NotImplementedException();
	}

	public BoundingBoxAttachment NewBoundingBoxAttachment(Skin skin, string name)
	{
		throw new NotImplementedException();
	}
}
