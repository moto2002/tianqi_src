using System;
using UnityEngine;

public class NgMaterial
{
	public static bool IsMaterialColor(Material mat)
	{
		string[] array = new string[]
		{
			"_Color",
			"_TintColor",
			"_EmisColor"
		};
		if (mat != null)
		{
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (mat.HasProperty(text))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static string GetMaterialColorName(Material mat)
	{
		string[] array = new string[]
		{
			"_Color",
			"_TintColor",
			"_EmisColor"
		};
		if (mat != null)
		{
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (mat.HasProperty(text))
				{
					return text;
				}
			}
		}
		return null;
	}

	public static Color GetMaterialColor(Material mat)
	{
		return NgMaterial.GetMaterialColor(mat, Color.get_white());
	}

	public static Color GetMaterialColor(Material mat, Color defaultColor)
	{
		string[] array = new string[]
		{
			"_Color",
			"_TintColor",
			"_EmisColor"
		};
		if (mat != null)
		{
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (mat.HasProperty(text))
				{
					return mat.GetColor(text);
				}
			}
		}
		return defaultColor;
	}

	public static void SetMaterialColor(Material mat, Color color)
	{
		string[] array = new string[]
		{
			"_Color",
			"_TintColor",
			"_EmisColor"
		};
		if (mat != null)
		{
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (mat.HasProperty(text))
				{
					mat.SetColor(text, color);
				}
			}
		}
	}

	public static bool IsSameMaterial(Material mat1, Material mat2, bool bCheckAddress)
	{
		return (!bCheckAddress || !(mat1 != mat2)) && !(mat2 == null) && !(mat1.get_shader() != mat2.get_shader()) && !(mat1.get_mainTexture() != mat2.get_mainTexture()) && !(mat1.get_mainTextureOffset() != mat2.get_mainTextureOffset()) && !(mat1.get_mainTextureScale() != mat2.get_mainTextureScale()) && NgMaterial.IsSameColorProperty(mat1, mat2, "_Color") && NgMaterial.IsSameColorProperty(mat1, mat2, "_TintColor") && NgMaterial.IsSameColorProperty(mat1, mat2, "_EmisColor") && NgMaterial.IsSameFloatProperty(mat1, mat2, "_InvFade") && NgMaterial.IsMaskTexture(mat1) == NgMaterial.IsMaskTexture(mat2) && (!NgMaterial.IsMaskTexture(mat1) || !(NgMaterial.GetMaskTexture(mat1) != NgMaterial.GetMaskTexture(mat2)));
	}

	public static void CopyMaterialArgument(Material srcMat, Material tarMat)
	{
		tarMat.set_mainTexture(srcMat.get_mainTexture());
		tarMat.set_mainTextureOffset(srcMat.get_mainTextureOffset());
		tarMat.set_mainTextureScale(srcMat.get_mainTextureScale());
		if (NgMaterial.IsMaskTexture(srcMat) && NgMaterial.IsMaskTexture(tarMat))
		{
			NgMaterial.SetMaskTexture(tarMat, NgMaterial.GetMaskTexture(srcMat));
		}
		NgMaterial.SetMaterialColor(tarMat, NgMaterial.GetMaterialColor(srcMat, new Color(0.5f, 0.5f, 0.5f, 0.5f)));
	}

	public static bool IsSameColorProperty(Material mat1, Material mat2, string propertyName)
	{
		bool flag = mat1.HasProperty(propertyName);
		bool flag2 = mat2.HasProperty(propertyName);
		if (flag && flag2)
		{
			return mat1.GetColor(propertyName) == mat2.GetColor(propertyName);
		}
		return !flag && !flag2;
	}

	public static void CopyColorProperty(Material srcMat, Material tarMat, string propertyName)
	{
		bool flag = srcMat.HasProperty(propertyName);
		bool flag2 = tarMat.HasProperty(propertyName);
		if (flag && flag2)
		{
			tarMat.SetColor(propertyName, srcMat.GetColor(propertyName));
		}
	}

	public static bool IsSameFloatProperty(Material mat1, Material mat2, string propertyName)
	{
		bool flag = mat1.HasProperty(propertyName);
		bool flag2 = mat2.HasProperty(propertyName);
		if (flag && flag2)
		{
			return mat1.GetFloat(propertyName) == mat2.GetFloat(propertyName);
		}
		return !flag && !flag2;
	}

	public static Texture GetTexture(Material mat, bool bMask)
	{
		if (mat == null)
		{
			return null;
		}
		if (!bMask)
		{
			return mat.get_mainTexture();
		}
		if (NgMaterial.IsMaskTexture(mat))
		{
			return mat.GetTexture("_Mask");
		}
		return null;
	}

	public static void SetMaskTexture(Material mat, bool bMask, Texture newTexture)
	{
		if (mat == null)
		{
			return;
		}
		if (bMask)
		{
			NgMaterial.SetMaskTexture(mat, newTexture);
		}
		else
		{
			mat.set_mainTexture(newTexture);
		}
	}

	public static bool IsMaskTexture(Material tarMat)
	{
		return tarMat.HasProperty("_Mask");
	}

	public static void SetMaskTexture(Material tarMat, Texture maskTex)
	{
		tarMat.SetTexture("_Mask", maskTex);
	}

	public static Texture GetMaskTexture(Material mat)
	{
		if (mat == null || !mat.HasProperty("_Mask"))
		{
			return null;
		}
		return mat.GetTexture("_Mask");
	}
}
