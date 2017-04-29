using System;
using UnityEngine;

public class FXMakerLayout : NgLayout
{
	public enum WINDOWID
	{
		NONE,
		TOP_LEFT = 10,
		TOP_CENTER,
		TOP_RIGHT,
		EFFECT_LIST,
		EFFECT_HIERARCHY,
		EFFECT_CONTROLS,
		TOOLIP_CURSOR,
		MODAL_MSG,
		MENUPOPUP,
		SPRITEPOPUP,
		POPUP = 100,
		RESOURCE_START = 200,
		HINTRECT = 300
	}

	public enum MODAL_TYPE
	{
		MODAL_NONE,
		MODAL_MSG,
		MODAL_OK,
		MODAL_YESNO,
		MODAL_OKCANCEL
	}

	public enum MODALRETURN_TYPE
	{
		MODALRETURN_SHOW,
		MODALRETURN_OK,
		MODALRETURN_CANCEL
	}

	public const string m_CurrentVersion = "v1.2.12";

	public const int m_nMaxResourceListCount = 100;

	public const int m_nMaxPrefabListCount = 500;

	public const int m_nMaxTextureListCount = 500;

	public const int m_nMaxMaterialListCount = 1000;

	public const float m_fScreenShotEffectZoomRate = 1f;

	public const float m_fScreenShotBackZoomRate = 0.6f;

	public const float m_fScrollButtonAspect = 0.55f;

	public const float m_fReloadPreviewTime = 0.5f;

	public const int m_nThumbCaptureSize = 512;

	public const int m_nThumbImageSize = 128;

	protected static float m_fFixedWindowWidth = -1f;

	protected static float m_fTopMenuHeight = -1f;

	protected static bool m_bLastStateTopMenuMini = false;

	public static bool m_bDevelopState = false;

	public static bool m_bDevelopPrefs = false;

	public static Rect m_rectOuterMargin = new Rect(2f, 2f, 0f, 0f);

	public static Rect m_rectInnerMargin = new Rect(7f, 19f, 7f, 4f);

	public static int m_nSidewindowWidthCount = 2;

	public static float m_fButtonMargin = 3f;

	public static float m_fScrollButtonHeight = 70f;

	public static bool m_bMinimizeTopMenu = false;

	public static bool m_bMinimizeAll = false;

	public static float m_fMinimizeClickWidth = 60f;

	public static float m_fMinimizeClickHeight = 20f;

	public static float m_fOriActionToolbarHeight = 126f;

	public static float m_fActionToolbarHeight = FXMakerLayout.m_fOriActionToolbarHeight;

	public static float m_MinimizeHeight = 43f;

	public static float m_fToolMessageHeight = 50f;

	public static float m_fTooltipHeight = 60f;

	public static float m_fModalMessageWidth = 500f;

	public static float m_fModalMessageHeight = 200f;

	public static Color m_ColorButtonHover = new Color(0.7f, 1f, 0.9f, 1f);

	public static Color m_ColorButtonActive = new Color(1f, 1f, 0.6f, 1f);

	public static Color m_ColorButtonMatNormal = new Color(0.5f, 0.7f, 0.7f, 1f);

	public static Color m_ColorButtonUnityEngine = new Color(0.7f, 0.7f, 0.7f, 1f);

	public static Color m_ColorDropFocused = new Color(0.2f, 1f, 0.6f, 0.8f);

	public static Color m_ColorHelpBox = new Color(1f, 0.1f, 0.1f, 1f);

	protected static float m_fArrowIntervalStartTime = 0.2f;

	protected static float m_fArrowIntervalRepeatTime = 0.1f;

	protected static float m_fKeyLastTime;

	public static float GetFixedWindowWidth()
	{
		return 115f;
	}

	public static float GetTopMenuHeight()
	{
		return (!FXMakerLayout.m_bMinimizeAll && !FXMakerLayout.m_bMinimizeTopMenu) ? 92f : FXMakerLayout.m_MinimizeHeight;
	}

	public static int GetWindowId(FXMakerLayout.WINDOWID id)
	{
		return (int)id;
	}

	public static Rect GetChildTopRect(Rect rectParent, int topMargin, int nHeight)
	{
		return new Rect(FXMakerLayout.m_rectInnerMargin.get_x(), (float)topMargin + FXMakerLayout.m_rectInnerMargin.get_y(), rectParent.get_width() - FXMakerLayout.m_rectInnerMargin.get_x() - FXMakerLayout.m_rectInnerMargin.get_width(), (float)nHeight);
	}

	public static Rect GetChildBottomRect(Rect rectParent, int nHeight)
	{
		return new Rect(FXMakerLayout.m_rectInnerMargin.get_x(), rectParent.get_height() - (float)nHeight - FXMakerLayout.m_rectInnerMargin.get_height(), rectParent.get_width() - FXMakerLayout.m_rectInnerMargin.get_x() - FXMakerLayout.m_rectInnerMargin.get_width(), (float)nHeight);
	}

	public static Rect GetChildVerticalRect(Rect rectParent, int topMargin, int count, int pos, int sumCount)
	{
		return new Rect(FXMakerLayout.m_rectInnerMargin.get_x(), (float)topMargin + FXMakerLayout.m_rectInnerMargin.get_y() + (rectParent.get_height() - (float)topMargin - FXMakerLayout.m_rectInnerMargin.get_y() - FXMakerLayout.m_rectInnerMargin.get_height()) / (float)count * (float)pos, rectParent.get_width() - FXMakerLayout.m_rectInnerMargin.get_x() - FXMakerLayout.m_rectInnerMargin.get_width(), (rectParent.get_height() - (float)topMargin - FXMakerLayout.m_rectInnerMargin.get_y() - FXMakerLayout.m_rectInnerMargin.get_height()) / (float)count * (float)sumCount - FXMakerLayout.m_fButtonMargin);
	}

	public static Rect GetInnerVerticalRect(Rect rectBase, int count, int pos, int sumCount)
	{
		return new Rect(rectBase.get_x(), rectBase.get_y() + (rectBase.get_height() + FXMakerLayout.m_fButtonMargin) / (float)count * (float)pos, rectBase.get_width(), (rectBase.get_height() + FXMakerLayout.m_fButtonMargin) / (float)count * (float)sumCount - FXMakerLayout.m_fButtonMargin);
	}

	public static Rect GetChildHorizontalRect(Rect rectParent, int topMargin, int count, int pos, int sumCount)
	{
		return new Rect(FXMakerLayout.m_rectInnerMargin.get_x() + (rectParent.get_width() - FXMakerLayout.m_rectInnerMargin.get_x() - FXMakerLayout.m_rectInnerMargin.get_width()) / (float)count * (float)pos, (float)topMargin + FXMakerLayout.m_rectInnerMargin.get_y(), (rectParent.get_width() - FXMakerLayout.m_rectInnerMargin.get_x() - FXMakerLayout.m_rectInnerMargin.get_width()) / (float)count * (float)sumCount - FXMakerLayout.m_fButtonMargin, rectParent.get_height() - FXMakerLayout.m_rectInnerMargin.get_y() - FXMakerLayout.m_rectInnerMargin.get_height());
	}

	public static Rect GetInnerHorizontalRect(Rect rectBase, int count, int pos, int sumCount)
	{
		return new Rect(rectBase.get_x() + (rectBase.get_width() + FXMakerLayout.m_fButtonMargin) / (float)count * (float)pos, rectBase.get_y(), (rectBase.get_width() + FXMakerLayout.m_fButtonMargin) / (float)count * (float)sumCount - FXMakerLayout.m_fButtonMargin, rectBase.get_height());
	}

	public static Rect GetMenuChangeRect()
	{
		return new Rect(FXMakerLayout.m_rectOuterMargin.get_x(), FXMakerLayout.m_rectOuterMargin.get_y(), FXMakerLayout.GetFixedWindowWidth(), FXMakerLayout.GetTopMenuHeight());
	}

	public static Rect GetMenuToolbarRect()
	{
		return new Rect(FXMakerLayout.GetMenuChangeRect().get_xMax() + FXMakerLayout.m_rectOuterMargin.get_x(), FXMakerLayout.m_rectOuterMargin.get_y(), (float)Screen.get_width() - FXMakerLayout.GetMenuChangeRect().get_width() - FXMakerLayout.GetMenuTopRightRect().get_width() - FXMakerLayout.m_rectOuterMargin.get_x() * 4f, FXMakerLayout.GetTopMenuHeight());
	}

	public static Rect GetMenuTopRightRect()
	{
		return new Rect((float)Screen.get_width() - FXMakerLayout.GetFixedWindowWidth() - FXMakerLayout.m_rectOuterMargin.get_x(), FXMakerLayout.m_rectOuterMargin.get_y(), FXMakerLayout.GetFixedWindowWidth(), FXMakerLayout.GetTopMenuHeight());
	}

	public static Rect GetResListRect(int nIndex)
	{
		return new Rect(FXMakerLayout.m_rectOuterMargin.get_x() + (FXMakerLayout.GetFixedWindowWidth() + FXMakerLayout.m_rectOuterMargin.get_x()) * (float)nIndex, FXMakerLayout.GetMenuChangeRect().get_yMax() + FXMakerLayout.m_rectOuterMargin.get_y(), FXMakerLayout.GetFixedWindowWidth(), (float)Screen.get_height() - FXMakerLayout.GetMenuChangeRect().get_yMax() - FXMakerLayout.m_rectOuterMargin.get_y() * 2f);
	}

	public static Rect GetEffectListRect()
	{
		return new Rect(FXMakerLayout.m_rectOuterMargin.get_x(), FXMakerLayout.GetMenuChangeRect().get_yMax() + FXMakerLayout.m_rectOuterMargin.get_y(), FXMakerLayout.GetFixedWindowWidth() * (float)FXMakerLayout.m_nSidewindowWidthCount + FXMakerLayout.m_rectOuterMargin.get_x(), (float)Screen.get_height() - FXMakerLayout.GetMenuChangeRect().get_yMax() - FXMakerLayout.m_rectOuterMargin.get_y() * 2f);
	}

	public static Rect GetEffectHierarchyRect()
	{
		return new Rect((float)Screen.get_width() - (FXMakerLayout.GetFixedWindowWidth() + FXMakerLayout.m_rectOuterMargin.get_x()) * (float)FXMakerLayout.m_nSidewindowWidthCount, FXMakerLayout.GetMenuChangeRect().get_yMax() + FXMakerLayout.m_rectOuterMargin.get_y(), FXMakerLayout.GetFixedWindowWidth() * (float)FXMakerLayout.m_nSidewindowWidthCount + FXMakerLayout.m_rectOuterMargin.get_x(), (float)Screen.get_height() - FXMakerLayout.GetMenuChangeRect().get_yMax() - FXMakerLayout.m_rectOuterMargin.get_y() * 2f);
	}

	public static Rect GetActionToolbarRect()
	{
		return new Rect(FXMakerLayout.m_rectOuterMargin.get_x() * 3f + FXMakerLayout.GetFixedWindowWidth() * (float)FXMakerLayout.m_nSidewindowWidthCount, (float)Screen.get_height() - FXMakerLayout.m_fActionToolbarHeight - FXMakerLayout.m_rectOuterMargin.get_y(), (float)Screen.get_width() - FXMakerLayout.GetMenuChangeRect().get_width() * 4f - FXMakerLayout.m_rectOuterMargin.get_x() * 6f, FXMakerLayout.m_fActionToolbarHeight);
	}

	public static Rect GetToolMessageRect()
	{
		return new Rect(FXMakerLayout.GetFixedWindowWidth() * 2.5f, (float)Screen.get_height() - FXMakerLayout.m_fActionToolbarHeight - FXMakerLayout.m_rectOuterMargin.get_y() - FXMakerLayout.m_fToolMessageHeight - FXMakerLayout.m_fTooltipHeight, (float)Screen.get_width() - FXMakerLayout.GetFixedWindowWidth() * (float)(FXMakerLayout.m_nSidewindowWidthCount * 2 + 1), FXMakerLayout.m_fToolMessageHeight);
	}

	public static Rect GetTooltipRect()
	{
		return new Rect(FXMakerLayout.m_rectOuterMargin.get_x() * 3f + FXMakerLayout.GetFixedWindowWidth() * (float)FXMakerLayout.m_nSidewindowWidthCount, (float)Screen.get_height() - FXMakerLayout.m_fActionToolbarHeight - FXMakerLayout.m_rectOuterMargin.get_y() - FXMakerLayout.m_fTooltipHeight, (float)Screen.get_width() - FXMakerLayout.GetMenuChangeRect().get_width() * 4f - FXMakerLayout.m_rectOuterMargin.get_x() * 6f, FXMakerLayout.m_fTooltipHeight);
	}

	public static Rect GetCursorTooltipRect(Vector2 size)
	{
		return NgLayout.ClampWindow(new Rect(Input.get_mousePosition().x + 15f, (float)Screen.get_height() - Input.get_mousePosition().y + 80f, size.x, size.y));
	}

	public static Rect GetModalMessageRect()
	{
		return new Rect(((float)Screen.get_width() - FXMakerLayout.m_fModalMessageWidth) / 2f, ((float)Screen.get_height() - FXMakerLayout.m_fModalMessageHeight - FXMakerLayout.m_fModalMessageHeight / 8f) / 2f, FXMakerLayout.m_fModalMessageWidth, FXMakerLayout.m_fModalMessageHeight);
	}

	public static Rect GetMenuGizmoRect()
	{
		return new Rect(FXMakerLayout.m_rectOuterMargin.get_x() * 3f + FXMakerLayout.GetFixedWindowWidth() * (float)FXMakerLayout.m_nSidewindowWidthCount, FXMakerLayout.GetTopMenuHeight() + FXMakerLayout.m_rectOuterMargin.get_y(), 490f, 26f);
	}

	public static Rect GetClientRect()
	{
		return new Rect(FXMakerLayout.m_rectOuterMargin.get_x() * 3f + FXMakerLayout.GetFixedWindowWidth() * (float)FXMakerLayout.m_nSidewindowWidthCount, FXMakerLayout.GetTopMenuHeight() + FXMakerLayout.m_rectOuterMargin.get_y(), (float)Screen.get_width() - (FXMakerLayout.m_rectOuterMargin.get_x() * 3f + FXMakerLayout.GetFixedWindowWidth() * (float)FXMakerLayout.m_nSidewindowWidthCount) * 2f, (float)Screen.get_height() - FXMakerLayout.m_fActionToolbarHeight - FXMakerLayout.m_rectOuterMargin.get_y() * 3f - FXMakerLayout.GetTopMenuHeight());
	}

	public static Rect GetScrollViewRect(int nWidth, int nButtonCount, int nColumn)
	{
		return new Rect(0f, 0f, (float)(nWidth - 2), FXMakerLayout.m_fScrollButtonHeight * (float)(nButtonCount / nColumn + ((0 >= nButtonCount % nColumn) ? 0 : 1)) + 25f);
	}

	public static Rect GetScrollGridRect(int nWidth, int nButtonCount, int nColumn)
	{
		return new Rect(0f, 0f, (float)(nWidth - 2), FXMakerLayout.m_fScrollButtonHeight * (float)(nButtonCount / nColumn + ((0 >= nButtonCount % nColumn) ? 0 : 1)));
	}

	public static Rect GetAspectScrollViewRect(int nWidth, float fAspect, int nButtonCount, int nColumn, bool bImageOnly)
	{
		return new Rect(0f, 0f, (float)(nWidth - 4), ((float)((nWidth - 4) / nColumn) * fAspect + (float)((!bImageOnly) ? 10 : 0)) * (float)(nButtonCount / nColumn + ((0 >= nButtonCount % nColumn) ? 0 : 1)) + 25f);
	}

	public static Rect GetAspectScrollGridRect(int nWidth, float fAspect, int nButtonCount, int nColumn, bool bImageOnly)
	{
		return new Rect(0f, 0f, (float)(nWidth - 4), ((float)((nWidth - 4) / nColumn) * fAspect + (float)((!bImageOnly) ? 10 : 0)) * (float)(nButtonCount / nColumn + ((0 >= nButtonCount % nColumn) ? 0 : 1)));
	}

	public static KeyCode GetVaildInputKey(KeyCode key, bool bPress)
	{
		if (bPress || FXMakerLayout.m_fKeyLastTime + FXMakerLayout.m_fArrowIntervalRepeatTime * Time.get_timeScale() < Time.get_time())
		{
			FXMakerLayout.m_fKeyLastTime = ((!bPress) ? Time.get_time() : (Time.get_time() + FXMakerLayout.m_fArrowIntervalStartTime));
			return key;
		}
		return 0;
	}

	public static void ModalWindow(Rect rect, GUI.WindowFunction func, string title)
	{
		GUI.Window(GUIUtility.GetControlID(2), rect, delegate(int id)
		{
			GUI.set_depth(0);
			int controlID = GUIUtility.GetControlID(0);
			if (GUIUtility.get_hotControl() < controlID)
			{
				FXMakerLayout.setHotControl(0);
			}
			func.Invoke(id);
			int controlID2 = GUIUtility.GetControlID(0);
			if (GUIUtility.get_hotControl() < controlID || (GUIUtility.get_hotControl() > controlID2 && controlID2 != -1))
			{
				FXMakerLayout.setHotControl(-1);
			}
			GUI.FocusWindow(id);
			GUI.BringWindowToFront(id);
		}, title);
	}

	private static void setHotControl(int id)
	{
		Rect rect = new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height());
		if (rect.Contains(GUIUtility.GUIToScreenPoint(Event.get_current().get_mousePosition())))
		{
			GUIUtility.set_hotControl(id);
		}
	}
}
