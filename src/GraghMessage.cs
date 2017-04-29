using System;
using UnityEngine;

public class GraghMessage
{
	public XPoint fixBasePoint;

	public GraghShape graghShape;

	public float range;

	public float angle;

	public float width;

	public float height;

	public Vector3 forward;

	public bool IsCircle
	{
		get
		{
			return this.graghShape == GraghShape.Sector && this.range > 0f && this.angle == 360f;
		}
	}

	public Vector3 Center
	{
		get
		{
			GraghShape graghShape = this.graghShape;
			if (graghShape == GraghShape.Sector)
			{
				return this.fixBasePoint.position;
			}
			if (graghShape != GraghShape.Rect)
			{
				return Vector3.get_zero();
			}
			return this.fixBasePoint.position + this.forward * this.height / 2f;
		}
	}

	public GraghMessage(XPoint theFixBasePoint, GraghShape theGraghShape, float theRange, float theAngle, float theWidth, float theHeight)
	{
		this.fixBasePoint = theFixBasePoint;
		this.graghShape = theGraghShape;
		this.range = theRange;
		this.angle = theAngle;
		this.width = theWidth;
		this.height = theHeight;
		this.forward = Quaternion.Euler(this.fixBasePoint.rotation.get_eulerAngles().x, this.fixBasePoint.rotation.get_eulerAngles().y, this.fixBasePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
	}

	public bool InArea(Vector3 position, float hitRange = 0f)
	{
		if (this.fixBasePoint == null)
		{
			return false;
		}
		GraghShape graghShape = this.graghShape;
		if (graghShape != GraghShape.Sector)
		{
			return graghShape == GraghShape.Rect && this.InRectArea(position, hitRange);
		}
		if (this.IsCircle)
		{
			return this.InCircleArea(position, hitRange);
		}
		return this.InSectorArea(position, hitRange);
	}

	protected bool InCircleArea(Vector3 position, float hitRange = 0f)
	{
		return (position.x - this.fixBasePoint.position.x) * (position.x - this.fixBasePoint.position.x) + (position.z - this.fixBasePoint.position.z) * (position.z - this.fixBasePoint.position.z) <= (this.range + hitRange) * (this.range + hitRange);
	}

	protected bool InSectorArea(Vector3 position, float hitRange = 0f)
	{
		return this.InCircleArea(position, hitRange) && Vector2.Angle(new Vector2(position.x - this.fixBasePoint.position.x, position.z - this.fixBasePoint.position.z), new Vector2(this.forward.x, this.forward.z)) <= this.angle / 2f;
	}

	protected bool InRectArea(Vector3 position, float hitRange = 0f)
	{
		if (position == this.fixBasePoint.position)
		{
			return true;
		}
		Vector3 center = this.Center;
		float num = XUtility.DistanceNoY(center, position);
		float num2 = Vector2.Angle(new Vector2(position.x - center.x, position.z - center.z), new Vector2(this.forward.x, this.forward.z));
		if (num2 > 90f)
		{
			num2 = 180f - num2;
		}
		float num3 = Mathf.Abs(Mathf.Sin(num2 * 3.14159274f / 180f) * num);
		float num4 = Mathf.Abs(Mathf.Cos(num2 * 3.14159274f / 180f) * num);
		return num3 <= this.width / 2f + hitRange && num4 < this.height / 2f + hitRange;
	}

	public void DrawShape()
	{
		this.DrawShape(Color.get_black());
	}

	public void DrawShape(Color color)
	{
		if (!SystemConfig.IsOpenEffectDrawLine)
		{
			return;
		}
		if (this.fixBasePoint == null)
		{
			return;
		}
		GraghShape graghShape = this.graghShape;
		if (graghShape != GraghShape.Sector)
		{
			if (graghShape == GraghShape.Rect)
			{
				this.DrawRect(color);
			}
		}
		else if (this.IsCircle)
		{
			this.DrawCircle(color);
		}
		else
		{
			this.DrawSector(color);
		}
	}

	protected void DrawCircle(Color color)
	{
		Vector3 vector = this.fixBasePoint.position + this.forward * this.range;
		Vector3 vector2 = vector;
		Debug.DrawLine(this.Center, vector, color, 2f);
		for (int i = 0; i < 360; i += 15)
		{
			Vector3 vector3 = Quaternion.Euler(this.fixBasePoint.rotation.get_eulerAngles().x, this.fixBasePoint.rotation.get_eulerAngles().y + (float)i, this.fixBasePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
			Vector3 vector4 = this.fixBasePoint.position + vector3 * this.range;
			Debug.DrawLine(vector2, vector4, color, 2f);
			vector2 = vector4;
			if (i + 15 >= 360)
			{
				Debug.DrawLine(vector4, vector, color, 2f);
			}
		}
	}

	protected void DrawSector(Color color)
	{
		Vector3 vector = this.fixBasePoint.position + this.forward * this.range;
		Vector3 vector2 = vector;
		Vector3 vector3 = vector;
		Debug.DrawLine(this.Center, vector, color, 2f);
		for (int i = 1; i < 6; i++)
		{
			Vector3 vector4 = Quaternion.Euler(this.fixBasePoint.rotation.get_eulerAngles().x, this.fixBasePoint.rotation.get_eulerAngles().y + (float)i * this.angle / 10f, this.fixBasePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
			Vector3 vector5 = this.fixBasePoint.position + vector4 * this.range;
			Vector3 vector6 = Quaternion.Euler(this.fixBasePoint.rotation.get_eulerAngles().x, this.fixBasePoint.rotation.get_eulerAngles().y - (float)i * this.angle / 10f, this.fixBasePoint.rotation.get_eulerAngles().z) * Vector3.get_forward();
			Vector3 vector7 = this.fixBasePoint.position + vector6 * this.range;
			if (i + 1 >= 6)
			{
				Debug.DrawLine(this.Center, vector5, color, 2f);
				Debug.DrawLine(this.Center, vector7, color, 2f);
			}
			Debug.DrawLine(vector2, vector5, color, 2f);
			Debug.DrawLine(vector3, vector7, color, 2f);
			vector2 = vector5;
			vector3 = vector7;
		}
	}

	protected void DrawRect(Color color)
	{
		Vector3 vector = this.fixBasePoint.position + this.forward * this.height;
		Vector3 vector2 = Quaternion.Euler(this.fixBasePoint.rotation.get_eulerAngles().x, this.fixBasePoint.rotation.get_eulerAngles().y, this.fixBasePoint.rotation.get_eulerAngles().z) * Vector3.get_left() * this.width / 2f;
		Vector3 vector3 = this.fixBasePoint.position + vector2;
		Vector3 vector4 = this.fixBasePoint.position - vector2;
		Vector3 vector5 = vector - vector2;
		Vector3 vector6 = vector + vector2;
		Debug.DrawLine(vector3, vector4, color, 2f);
		Debug.DrawLine(vector4, vector5, color, 2f);
		Debug.DrawLine(vector5, vector6, color, 2f);
		Debug.DrawLine(vector6, vector3, color, 2f);
	}
}
