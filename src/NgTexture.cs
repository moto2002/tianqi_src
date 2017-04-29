using System;
using System.Collections.Generic;
using UnityEngine;

public class NgTexture
{
	public static void UnloadTextures(GameObject rootObj)
	{
		if (rootObj == null)
		{
			return;
		}
		Renderer[] componentsInChildren = rootObj.GetComponentsInChildren<Renderer>(true);
		Renderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Renderer renderer = array[i];
			if (renderer.get_material() != null && renderer.get_material().get_mainTexture() != null)
			{
				Debuger.Info("UnloadTextures - " + renderer.get_material().get_mainTexture(), new object[0]);
				Resources.UnloadAsset(renderer.get_material().get_mainTexture());
			}
		}
	}

	public static Texture2D CopyTexture(Texture2D srcTex, Texture2D tarTex)
	{
		Color32[] pixels = srcTex.GetPixels32();
		tarTex.SetPixels32(pixels);
		tarTex.Apply(false);
		return tarTex;
	}

	public static Texture2D InverseTexture32(Texture2D srcTex, Texture2D tarTex)
	{
		Color32[] pixels = srcTex.GetPixels32();
		for (int i = 0; i < pixels.Length; i++)
		{
			pixels[i].a = 255 - pixels[i].a;
		}
		tarTex.SetPixels32(pixels);
		tarTex.Apply(false);
		return tarTex;
	}

	public static Texture2D CombineTexture(Texture2D baseTexture, Texture2D combineTexture)
	{
		Texture2D texture2D = new Texture2D(baseTexture.get_width(), baseTexture.get_height(), baseTexture.get_format(), false);
		Debuger.Warning("need \tObject.DestroyImmediate(returnTexture);", new object[0]);
		Color[] pixels = baseTexture.GetPixels();
		Color[] pixels2 = combineTexture.GetPixels();
		Color[] array = new Color[pixels.Length];
		int num = pixels.Length;
		for (int i = 0; i < num; i++)
		{
			array[i] = Color.Lerp(pixels[i], pixels2[i], pixels2[i].a);
		}
		texture2D.SetPixels(array);
		texture2D.Apply(false);
		return texture2D;
	}

	public static bool CompareTexture(Texture2D tex1, Texture2D tex2)
	{
		Color[] pixels = tex1.GetPixels();
		Color[] pixels2 = tex2.GetPixels();
		if (pixels.Length != pixels2.Length)
		{
			return false;
		}
		int num = pixels.Length;
		for (int i = 0; i < num; i++)
		{
			if (pixels[i] != pixels2[i])
			{
				return false;
			}
		}
		return true;
	}

	public static Texture2D FindTexture(List<Texture2D> findList, Texture2D findTex)
	{
		for (int i = 0; i < findList.get_Count(); i++)
		{
			if (NgTexture.CompareTexture(findList.get_Item(i), findTex))
			{
				return findList.get_Item(i);
			}
		}
		return null;
	}

	public static int FindTextureIndex(List<Texture2D> findList, Texture2D findTex)
	{
		for (int i = 0; i < findList.get_Count(); i++)
		{
			if (NgTexture.CompareTexture(findList.get_Item(i), findTex))
			{
				return i;
			}
		}
		return -1;
	}

	public static Texture2D CopyTexture(Texture2D srcTex, Rect srcRect, Texture2D tarTex, Rect tarRect)
	{
		Color[] pixels = srcTex.GetPixels((int)srcRect.get_x(), (int)srcRect.get_y(), (int)srcRect.get_width(), (int)srcRect.get_height());
		tarTex.SetPixels((int)tarRect.get_x(), (int)tarRect.get_y(), (int)tarRect.get_width(), (int)tarRect.get_height(), pixels);
		tarTex.Apply();
		return tarTex;
	}

	public static Texture2D CopyTextureHalf(Texture2D srcTexture, Texture2D tarHalfTexture)
	{
		if (srcTexture.get_width() != tarHalfTexture.get_width() * 2)
		{
			Debuger.Error("size error", new object[0]);
		}
		if (srcTexture.get_height() != tarHalfTexture.get_height() * 2)
		{
			Debuger.Error("size error", new object[0]);
		}
		Color[] pixels = srcTexture.GetPixels();
		Color[] array = new Color[pixels.Length / 4];
		int width = tarHalfTexture.get_width();
		int height = tarHalfTexture.get_height();
		int num = 0;
		int num2 = 2;
		int num3 = num2 * 2;
		for (int i = 0; i < height; i++)
		{
			int j = 0;
			while (j < width)
			{
				array[num] = Color.Lerp(Color.Lerp(pixels[i * width * num3 + j * num2], pixels[i * width * num3 + j * num2 + 1], 0.5f), Color.Lerp(pixels[i * width * num3 + width * num2 + j * num2], pixels[i * width * num3 + width * num2 + j * num2 + 1], 0.5f), 0.5f);
				j++;
				num++;
			}
		}
		tarHalfTexture.SetPixels(array);
		tarHalfTexture.Apply(false);
		return tarHalfTexture;
	}

	public static Texture2D CopyTextureQuad(Texture2D srcTexture, Texture2D tarQuadTexture)
	{
		if (srcTexture.get_width() != tarQuadTexture.get_width() * 4)
		{
			Debuger.Error("size error", new object[0]);
		}
		if (srcTexture.get_height() != tarQuadTexture.get_height() * 4)
		{
			Debuger.Error("size error", new object[0]);
		}
		Color[] pixels = srcTexture.GetPixels();
		Color[] array = new Color[pixels.Length / 16];
		int width = tarQuadTexture.get_width();
		int height = tarQuadTexture.get_height();
		int num = 0;
		int num2 = 4;
		int num3 = num2 * 4;
		for (int i = 0; i < height; i++)
		{
			int j = 0;
			while (j < width)
			{
				array[num] = Color.Lerp(Color.Lerp(Color.Lerp(Color.Lerp(pixels[i * width * num3 + j * num2], pixels[i * width * num3 + j * num2 + 1], 0.5f), Color.Lerp(pixels[i * width * num3 + width * num2 + j * num2], pixels[i * width * num3 + width * num2 + j * num2 + 1], 0.5f), 0.5f), Color.Lerp(Color.Lerp(pixels[i * width * num3 + j * num2 + 2], pixels[i * width * num3 + j * num2 + 3], 0.5f), Color.Lerp(pixels[i * width * num3 + width * num2 + j * num2 + 2], pixels[i * width * num3 + width * num2 + j * num2 + 3], 0.5f), 0.5f), 0.5f), Color.Lerp(Color.Lerp(Color.Lerp(pixels[i * width * num3 + width * num2 * 2 + j * num2], pixels[i * width * num3 + width * num2 * 2 + j * num2 + 1], 0.5f), Color.Lerp(pixels[i * width * num3 + width * num2 * 3 + j * num2], pixels[i * width * num3 + width * num2 * 3 + j * num2 + 1], 0.5f), 0.5f), Color.Lerp(Color.Lerp(pixels[i * width * num3 + width * num2 * 2 + j * num2 + 2], pixels[i * width * num3 + width * num2 * 2 + j * num2 + 3], 0.5f), Color.Lerp(pixels[i * width * num3 + width * num2 * 3 + j * num2 + 2], pixels[i * width * num3 + width * num2 * 3 + j * num2 + 3], 0.5f), 0.5f), 0.5f), 0.5f);
				j++;
				num++;
			}
		}
		tarQuadTexture.SetPixels(array);
		tarQuadTexture.Apply(false);
		return tarQuadTexture;
	}

	public static Texture2D CopyTexture(Texture2D srcTex, Texture2D tarTex, Rect drawRect)
	{
		Rect srcRect = new Rect(0f, 0f, (float)srcTex.get_width(), (float)srcTex.get_height());
		if (drawRect.get_x() < 0f)
		{
			srcRect.set_x(srcRect.get_x() - drawRect.get_x());
			srcRect.set_width(srcRect.get_width() + drawRect.get_x());
			drawRect.set_width(drawRect.get_width() + drawRect.get_x());
			drawRect.set_x(0f);
		}
		if (drawRect.get_y() < 0f)
		{
			srcRect.set_y(srcRect.get_y() - drawRect.get_y());
			srcRect.set_height(srcRect.get_height() + drawRect.get_y());
			drawRect.set_height(drawRect.get_height() + drawRect.get_y());
			drawRect.set_y(0f);
		}
		if ((float)tarTex.get_width() < drawRect.get_x() + drawRect.get_width())
		{
			srcRect.set_width(srcRect.get_width() - (drawRect.get_x() + drawRect.get_width() - (float)tarTex.get_width()));
			drawRect.set_width(drawRect.get_width() - (drawRect.get_x() + drawRect.get_width() - (float)tarTex.get_width()));
		}
		if ((float)tarTex.get_height() < drawRect.get_y() + drawRect.get_height())
		{
			srcRect.set_height(srcRect.get_height() - (drawRect.get_y() + drawRect.get_height() - (float)tarTex.get_height()));
			drawRect.set_height(drawRect.get_height() - (drawRect.get_y() + drawRect.get_height() - (float)tarTex.get_height()));
		}
		return NgTexture.CopyTexture(srcTex, srcRect, tarTex, drawRect);
	}
}
