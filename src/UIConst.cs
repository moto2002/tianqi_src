using System;
using UnityEngine;

public class UIConst
{
	public static readonly float UI_CAMERA_CLIP = 2000f;

	public static readonly float UI_SIZE_WIDTH = 1280f;

	public static readonly float UI_SIZE_HEIGHT = 720f;

	public static readonly Vector3 HSV_DEFAULT = new Vector3(0f, 1f, 1f);

	public static string TEXTURE_MAIN = "_MainTex";

	public static string TEXTURE_A = "_AlphaTex";

	public static string WhiteBlack = "_WhiteBlack";

	public static float mScreenToUISizeScaleWidth = 0f;

	public static float mScreenToUISizeScaleHeight = 0f;

	public static float ScreenToUISizeScaleWidth
	{
		get
		{
			if (UIConst.mScreenToUISizeScaleWidth == 0f)
			{
				UIConst.mScreenToUISizeScaleWidth = UIConst.UI_SIZE_WIDTH / (float)Screen.get_width();
			}
			return UIConst.mScreenToUISizeScaleWidth;
		}
	}

	public static float ScreenToUISizeScaleHeight
	{
		get
		{
			if (UIConst.mScreenToUISizeScaleHeight == 0f)
			{
				UIConst.mScreenToUISizeScaleHeight = UIConst.UI_SIZE_HEIGHT / (float)Screen.get_height();
			}
			return UIConst.mScreenToUISizeScaleHeight;
		}
	}

	public static void GetRealScreenSize(out int realScreenWidth, out int realScreenHeight)
	{
		float num = UIConst.UI_SIZE_WIDTH / UIConst.UI_SIZE_HEIGHT;
		float num2 = (float)Screen.get_width() / (float)Screen.get_height();
		realScreenWidth = (int)UIConst.UI_SIZE_WIDTH;
		realScreenHeight = (int)UIConst.UI_SIZE_HEIGHT;
		if (num < num2)
		{
			realScreenWidth = Mathf.CeilToInt((float)realScreenHeight * num2);
		}
		else if (num > num2)
		{
			realScreenHeight = Mathf.CeilToInt((float)realScreenWidth / num2);
		}
	}

	public static int GetRealScreenSizeHeight()
	{
		int num;
		int result;
		UIConst.GetRealScreenSize(out num, out result);
		return result;
	}

	public static void GetRealScreenScale(out float realScaleScreenWidth, out float realScaleScreenHeight)
	{
		int num;
		int num2;
		UIConst.GetRealScreenSize(out num, out num2);
		realScaleScreenWidth = (float)num / UIConst.UI_SIZE_WIDTH;
		realScaleScreenHeight = (float)num2 / UIConst.UI_SIZE_HEIGHT;
	}
}
