using System;
using UnityEngine;

public class NgLayout
{
	protected static Color m_GuiOldColor;

	protected static bool m_GuiOldEnable;

	public static Rect GetZeroRect()
	{
		return new Rect(0f, 0f, 0f, 0f);
	}

	public static Rect GetSumRect(Rect rect1, Rect rect2)
	{
		return NgLayout.GetOffsetRect(rect1, Mathf.Min(0f, rect2.get_xMin() - rect1.get_xMin()), Mathf.Min(0f, rect2.get_yMin() - rect1.get_yMin()), Mathf.Max(0f, rect2.get_xMax() - rect1.get_xMax()), Mathf.Max(0f, rect2.get_yMax() - rect1.get_yMax()));
	}

	public static Rect GetOffsetRect(Rect rect, float left, float top)
	{
		return new Rect(rect.get_x() + left, rect.get_y() + top, rect.get_width(), rect.get_height());
	}

	public static Rect GetOffsetRect(Rect rect, float left, float top, float right, float bottom)
	{
		return new Rect(rect.get_x() + left, rect.get_y() + top, rect.get_width() - left + right, rect.get_height() - top + bottom);
	}

	public static Rect GetOffsetRect(Rect rect, float fOffset)
	{
		return new Rect(rect.get_x() - fOffset, rect.get_y() - fOffset, rect.get_width() + fOffset * 2f, rect.get_height() + fOffset * 2f);
	}

	public static Rect GetVOffsetRect(Rect rect, float fOffset)
	{
		return new Rect(rect.get_x(), rect.get_y() - fOffset, rect.get_width(), rect.get_height() + fOffset * 2f);
	}

	public static Rect GetHOffsetRect(Rect rect, float fOffset)
	{
		return new Rect(rect.get_x() - fOffset, rect.get_y(), rect.get_width() + fOffset * 2f, rect.get_height());
	}

	public static Rect GetOffsetRateRect(Rect rect, float fOffsetRate)
	{
		return new Rect(rect.get_x() - Mathf.Abs(rect.get_x()) * fOffsetRate, rect.get_y() - Mathf.Abs(rect.get_y()) * fOffsetRate, rect.get_width() + Mathf.Abs(rect.get_x()) * fOffsetRate * 2f, rect.get_height() + Mathf.Abs(rect.get_y()) * fOffsetRate * 2f);
	}

	public static Rect GetZeroStartRect(Rect rect)
	{
		return new Rect(0f, 0f, rect.get_width(), rect.get_height());
	}

	public static Rect GetLeftRect(Rect rect, float width)
	{
		return new Rect(rect.get_x(), rect.get_y(), width, rect.get_height());
	}

	public static Rect GetRightRect(Rect rect, float width)
	{
		return new Rect(rect.get_x() + rect.get_width() - width, rect.get_y(), width, rect.get_height());
	}

	public static Rect GetInnerTopRect(Rect rectBase, int topMargin, int nHeight)
	{
		return new Rect(rectBase.get_x(), (float)topMargin + rectBase.get_y(), rectBase.get_width(), (float)nHeight);
	}

	public static Rect GetInnerBottomRect(Rect rectBase, int nHeight)
	{
		return new Rect(rectBase.get_x(), rectBase.get_y() + rectBase.get_height() - (float)nHeight, rectBase.get_width(), (float)nHeight);
	}

	public static Vector2 ClampPoint(Rect rect, Vector2 point)
	{
		if (point.x < rect.get_xMin())
		{
			point.x = rect.get_xMin();
		}
		if (point.y < rect.get_yMin())
		{
			point.y = rect.get_yMin();
		}
		if (rect.get_xMax() < point.x)
		{
			point.x = rect.get_xMax();
		}
		if (rect.get_yMax() < point.y)
		{
			point.y = rect.get_yMax();
		}
		return point;
	}

	public static Vector3 ClampPoint(Rect rect, Vector3 point)
	{
		if (point.x < rect.get_xMin())
		{
			point.x = rect.get_xMin();
		}
		if (point.y < rect.get_yMin())
		{
			point.y = rect.get_yMin();
		}
		if (rect.get_xMax() < point.x)
		{
			point.x = rect.get_xMax();
		}
		if (rect.get_yMax() < point.y)
		{
			point.y = rect.get_yMax();
		}
		return point;
	}

	public static Rect ClampWindow(Rect popupRect)
	{
		if (popupRect.get_y() < 0f)
		{
			popupRect.set_y(0f);
		}
		if ((float)Screen.get_width() < popupRect.get_xMax())
		{
			popupRect.set_x(popupRect.get_x() - (popupRect.get_xMax() - (float)Screen.get_width()));
		}
		if ((float)Screen.get_height() < popupRect.get_yMax())
		{
			popupRect.set_y(popupRect.get_y() - (popupRect.get_yMax() - (float)Screen.get_height()));
		}
		return popupRect;
	}

	public static bool GUIToggle(Rect pos, bool bToggle, GUIContent content, bool bEnable)
	{
		bool enabled = GUI.get_enabled();
		if (!bEnable)
		{
			GUI.set_enabled(false);
		}
		bToggle = GUI.Toggle(pos, bToggle, content);
		GUI.set_enabled(enabled);
		return bToggle;
	}

	public static bool GUIButton(Rect pos, string name, bool bEnable)
	{
		bool enabled = GUI.get_enabled();
		if (!bEnable)
		{
			GUI.set_enabled(false);
		}
		bool result = GUI.Button(pos, name);
		GUI.set_enabled(enabled);
		return result;
	}

	public static bool GUIButton(Rect pos, GUIContent content, bool bEnable)
	{
		bool enabled = GUI.get_enabled();
		if (!bEnable)
		{
			GUI.set_enabled(false);
		}
		bool result = GUI.Button(pos, content);
		GUI.set_enabled(enabled);
		return result;
	}

	public static bool GUIButton(Rect pos, GUIContent content, GUIStyle style, bool bEnable)
	{
		bool enabled = GUI.get_enabled();
		if (!bEnable)
		{
			GUI.set_enabled(false);
		}
		bool result = GUI.Button(pos, content, style);
		GUI.set_enabled(enabled);
		return result;
	}

	public static string GUITextField(Rect pos, string name, bool bEnable)
	{
		bool enabled = GUI.get_enabled();
		if (!bEnable)
		{
			GUI.set_enabled(false);
		}
		string result = GUI.TextField(pos, name);
		GUI.set_enabled(enabled);
		return result;
	}

	public static bool GUIEnableBackup(bool newEnable)
	{
		NgLayout.m_GuiOldEnable = GUI.get_enabled();
		GUI.set_enabled(newEnable);
		return NgLayout.m_GuiOldEnable;
	}

	public static void GUIEnableRestore()
	{
		GUI.set_enabled(NgLayout.m_GuiOldEnable);
	}

	public static Color GUIColorBackup(Color newColor)
	{
		NgLayout.m_GuiOldColor = GUI.get_color();
		GUI.set_color(newColor);
		return NgLayout.m_GuiOldColor;
	}

	public static void GUIColorRestore()
	{
		GUI.set_color(NgLayout.m_GuiOldColor);
	}

	public static Vector2 GetGUIMousePosition()
	{
		return new Vector2(Input.get_mousePosition().x, (float)Screen.get_height() - Input.get_mousePosition().y);
	}

	public static float GetWorldPerScreenPixel(Vector3 worldPoint)
	{
		Camera main = Camera.get_main();
		if (main == null)
		{
			return 0f;
		}
		Plane plane = new Plane(main.get_transform().get_forward(), main.get_transform().get_position());
		float distanceToPoint = plane.GetDistanceToPoint(worldPoint);
		float num = 100f;
		return Vector3.Distance(main.ScreenToWorldPoint(new Vector3((float)(Screen.get_width() / 2), (float)(Screen.get_height() / 2) - num / 2f, distanceToPoint)), main.ScreenToWorldPoint(new Vector3((float)(Screen.get_width() / 2), (float)(Screen.get_height() / 2) + num / 2f, distanceToPoint))) / num;
	}

	public static Vector3 GetScreenToWorld(Vector3 targetWorld, Vector2 screenPos)
	{
		Camera main = Camera.get_main();
		if (main == null)
		{
			return Vector3.get_zero();
		}
		Plane plane = new Plane(main.get_transform().get_forward(), main.get_transform().get_position());
		float distanceToPoint = plane.GetDistanceToPoint(targetWorld);
		return main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distanceToPoint));
	}

	public static Vector3 GetWorldToScreen(Vector3 targetWorld)
	{
		Camera main = Camera.get_main();
		if (main == null)
		{
			return Vector3.get_zero();
		}
		return main.WorldToScreenPoint(targetWorld);
	}
}
