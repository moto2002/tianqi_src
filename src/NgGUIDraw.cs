using System;
using UnityEngine;

public class NgGUIDraw
{
	private static Texture2D aaHLineTex;

	private static Texture2D aaVLineTex;

	private static Texture2D _aaLineTex;

	private static Texture2D _lineTex;

	private static Texture2D _whiteTexture;

	private static Texture2D adLineTex
	{
		get
		{
			if (!NgGUIDraw._aaLineTex)
			{
				NgGUIDraw._aaLineTex = new Texture2D(1, 3, 5, true);
				NgGUIDraw._aaLineTex.SetPixel(0, 0, new Color(1f, 1f, 1f, 0f));
				NgGUIDraw._aaLineTex.SetPixel(0, 1, Color.get_white());
				NgGUIDraw._aaLineTex.SetPixel(0, 2, new Color(1f, 1f, 1f, 0f));
				NgGUIDraw._aaLineTex.Apply();
			}
			return NgGUIDraw._aaLineTex;
		}
	}

	private static Texture2D lineTex
	{
		get
		{
			if (!NgGUIDraw._lineTex)
			{
				NgGUIDraw._lineTex = new Texture2D(1, 1, 5, true);
				NgGUIDraw._lineTex.SetPixel(0, 0, Color.get_white());
				NgGUIDraw._lineTex.Apply();
			}
			return NgGUIDraw._lineTex;
		}
	}

	public static Texture2D whiteTexture
	{
		get
		{
			if (!NgGUIDraw._whiteTexture)
			{
				NgGUIDraw._whiteTexture = new Texture2D(1, 1, 5, true);
				NgGUIDraw._whiteTexture.SetPixel(0, 0, Color.get_white());
				NgGUIDraw._whiteTexture.Apply();
			}
			return NgGUIDraw._whiteTexture;
		}
	}

	public static void DrawHorizontalLine(Vector2 pointA, int nlen, Color color, float width, bool antiAlias)
	{
		Color color2 = GUI.get_color();
		Matrix4x4 matrix = GUI.get_matrix();
		if (!NgGUIDraw.aaHLineTex)
		{
			NgGUIDraw.aaHLineTex = new Texture2D(1, 3, 5, true);
			NgGUIDraw.aaHLineTex.SetPixel(0, 0, new Color(1f, 1f, 1f, 0f));
			NgGUIDraw.aaHLineTex.SetPixel(0, 1, Color.get_white());
			NgGUIDraw.aaHLineTex.SetPixel(0, 2, new Color(1f, 1f, 1f, 0f));
			NgGUIDraw.aaHLineTex.Apply();
		}
		GUI.set_color(color);
		if (!antiAlias)
		{
			GUI.DrawTexture(new Rect(pointA.x - width / 2f, pointA.y - width / 2f, (float)nlen + width, width), NgGUIDraw.lineTex);
		}
		else
		{
			GUI.DrawTexture(new Rect(pointA.x - width / 2f, pointA.y - width / 2f - 1f, (float)nlen + width, width * 3f), NgGUIDraw.aaHLineTex);
		}
		GUI.set_matrix(matrix);
		GUI.set_color(color2);
	}

	public static void DrawVerticalLine(Vector2 pointA, int nlen, Color color, float width, bool antiAlias)
	{
		Color color2 = GUI.get_color();
		Matrix4x4 matrix = GUI.get_matrix();
		if (!NgGUIDraw.aaVLineTex)
		{
			NgGUIDraw.aaVLineTex = new Texture2D(3, 1, 5, true);
			NgGUIDraw.aaVLineTex.SetPixel(0, 0, new Color(1f, 1f, 1f, 0f));
			NgGUIDraw.aaVLineTex.SetPixel(1, 0, Color.get_white());
			NgGUIDraw.aaVLineTex.SetPixel(2, 0, new Color(1f, 1f, 1f, 0f));
			NgGUIDraw.aaVLineTex.Apply();
		}
		GUI.set_color(color);
		if (!antiAlias)
		{
			GUI.DrawTexture(new Rect(pointA.x - width / 2f, pointA.y - width / 2f, width, (float)nlen + width), NgGUIDraw.lineTex);
		}
		else
		{
			GUI.DrawTexture(new Rect(pointA.x - width / 2f - 1f, pointA.y - width / 2f, width * 3f, (float)nlen + width), NgGUIDraw.aaVLineTex);
		}
		GUI.set_matrix(matrix);
		GUI.set_color(color2);
	}

	public static void DrawBox(Rect rect, Color color, float width, bool antiAlias)
	{
		if (width == 0f)
		{
			return;
		}
		NgGUIDraw.DrawHorizontalLine(new Vector2(rect.get_x(), rect.get_y()), (int)rect.get_width(), color, width, antiAlias);
		NgGUIDraw.DrawHorizontalLine(new Vector2(rect.get_x(), rect.get_yMax()), (int)rect.get_width(), color, width, antiAlias);
		NgGUIDraw.DrawVerticalLine(new Vector2(rect.get_x(), rect.get_y()), (int)rect.get_height(), color, width, antiAlias);
		NgGUIDraw.DrawVerticalLine(new Vector2(rect.get_xMax(), rect.get_y()), (int)rect.get_height(), color, width, antiAlias);
	}

	public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
	{
		if (Application.get_platform() == 7)
		{
			NgGUIDraw.DrawLineWindows(pointA, pointB, color, width, antiAlias);
		}
		else if (Application.get_platform() == null)
		{
			NgGUIDraw.DrawLineMac(pointA, pointB, color, width, antiAlias);
		}
	}

	public static void DrawBezierLine(Vector2 start, Vector2 startTangent, Vector2 end, Vector2 endTangent, Color color, float width, bool antiAlias, int segments)
	{
		Vector2 pointA = NgGUIDraw.cubeBezier(start, startTangent, end, endTangent, 0f);
		for (int i = 1; i < segments; i++)
		{
			Vector2 vector = NgGUIDraw.cubeBezier(start, startTangent, end, endTangent, (float)i / (float)segments);
			NgGUIDraw.DrawLine(pointA, vector, color, width, antiAlias);
			pointA = vector;
		}
	}

	private static void DrawLineMac(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
	{
		if (pointA == pointB)
		{
			return;
		}
		Color color2 = GUI.get_color();
		Matrix4x4 matrix = GUI.get_matrix();
		float num = width;
		if (antiAlias)
		{
			width *= 3f;
		}
		float num2 = Vector3.Angle(pointB - pointA, Vector2.get_right()) * (float)((pointA.y > pointB.y) ? -1 : 1);
		float magnitude = (pointB - pointA).get_magnitude();
		if (magnitude > 0.01f)
		{
			Vector3 vector = new Vector3(pointA.x, pointA.y, 0f);
			Vector3 vector2 = new Vector3((pointB.x - pointA.x) * 0.5f, (pointB.y - pointA.y) * 0.5f, 0f);
			Vector3 zero = Vector3.get_zero();
			if (antiAlias)
			{
				zero = new Vector3(-num * 1.5f * Mathf.Sin(num2 * 0.0174532924f), num * 1.5f * Mathf.Cos(num2 * 0.0174532924f));
			}
			else
			{
				zero = new Vector3(-num * 0.5f * Mathf.Sin(num2 * 0.0174532924f), num * 0.5f * Mathf.Cos(num2 * 0.0174532924f));
			}
			GUI.set_color(color);
			GUI.set_matrix(NgGUIDraw.translationMatrix(vector) * GUI.get_matrix());
			GUIUtility.ScaleAroundPivot(new Vector2(magnitude, width), new Vector2(-0.5f, 0f));
			GUI.set_matrix(NgGUIDraw.translationMatrix(-vector) * GUI.get_matrix());
			GUIUtility.RotateAroundPivot(num2, Vector2.get_zero());
			GUI.set_matrix(NgGUIDraw.translationMatrix(vector - zero - vector2) * GUI.get_matrix());
			GUI.DrawTexture(new Rect(0f, 0f, 1f, 1f), (!antiAlias) ? NgGUIDraw.lineTex : NgGUIDraw.adLineTex);
		}
		GUI.set_matrix(matrix);
		GUI.set_color(color2);
	}

	private static void DrawLineWindows(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
	{
		if (pointA == pointB)
		{
			return;
		}
		Color color2 = GUI.get_color();
		Matrix4x4 matrix = GUI.get_matrix();
		if (antiAlias)
		{
			width *= 3f;
		}
		float num = Vector3.Angle(pointB - pointA, Vector2.get_right()) * (float)((pointA.y > pointB.y) ? -1 : 1);
		float magnitude = (pointB - pointA).get_magnitude();
		Vector3 vector = new Vector3(pointA.x, pointA.y, 0f);
		GUI.set_color(color);
		GUI.set_matrix(NgGUIDraw.translationMatrix(vector) * GUI.get_matrix());
		GUIUtility.ScaleAroundPivot(new Vector2(magnitude, width), new Vector2(-0.5f, 0f));
		GUI.set_matrix(NgGUIDraw.translationMatrix(-vector) * GUI.get_matrix());
		GUIUtility.RotateAroundPivot(num, new Vector2(0f, 0f));
		GUI.set_matrix(NgGUIDraw.translationMatrix(vector + new Vector3(width / 2f, -magnitude / 2f) * Mathf.Sin(num * 0.0174532924f)) * GUI.get_matrix());
		GUI.DrawTexture(new Rect(0f, 0f, 1f, 1f), antiAlias ? NgGUIDraw.adLineTex : NgGUIDraw.lineTex);
		GUI.set_matrix(matrix);
		GUI.set_color(color2);
	}

	private static Vector2 cubeBezier(Vector2 s, Vector2 st, Vector2 e, Vector2 et, float t)
	{
		float num = 1f - t;
		return num * num * num * s + 3f * num * num * t * st + 3f * num * t * t * et + t * t * t * e;
	}

	private static Matrix4x4 translationMatrix(Vector3 v)
	{
		return Matrix4x4.TRS(v, Quaternion.get_identity(), Vector3.get_one());
	}
}
