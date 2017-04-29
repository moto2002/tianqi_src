using Package;
using System;
using UnityEngine;

public class BallElementItem : MonoBehaviour
{
	public float angle = 999f;

	public bool animating;

	public BallItemInfoUnit infoUnit;

	public bool isActor;

	public BlockInfo blockInfo;

	public GameObject gameObjectBlockInfo;

	private int deltaY = 50;

	private int deltaX = 230;

	private Vector3 cross;

	private float dot;

	private void LateUpdate()
	{
		if (this.infoUnit != null)
		{
			float num = Vector3.Distance(base.get_transform().get_position(), CamerasMgr.Camera2RTCommon.get_transform().get_position());
			if (num < BallElement.Instance.distanceVisable)
			{
				if (!this.infoUnit.get_gameObject().get_activeSelf())
				{
					this.infoUnit.get_gameObject().SetActive(true);
				}
				if (ElementInstanceUI.Instance.ImageArrow.get_gameObject().get_activeSelf())
				{
					ElementInstanceUI.Instance.ImageArrow.get_gameObject().SetActive(false);
				}
				Vector3 vector = CamerasMgr.Camera2RTCommon.WorldToScreenPoint(base.get_transform().get_position());
				Vector3 zero = Vector3.get_zero();
				zero.x = vector.x;
				zero.y = vector.y;
				zero.z = 0f;
				this.infoUnit.GetComponent<RectTransform>().set_anchoredPosition(zero);
			}
			else
			{
				if (this.infoUnit.get_gameObject().get_activeSelf())
				{
					this.infoUnit.get_gameObject().SetActive(false);
				}
				if (this.isActor)
				{
					if (!ElementInstanceUI.Instance.ImageArrow.get_gameObject().get_activeSelf())
					{
						ElementInstanceUI.Instance.ImageArrow.get_gameObject().SetActive(true);
					}
					Vector3 vector2 = CamerasMgr.Camera2RTCommon.WorldToScreenPoint(base.get_transform().get_position());
					int num2 = (int)vector2.x - this.deltaX;
					int num3 = (int)vector2.y - this.deltaY;
					int num4 = Screen.get_width() - (int)vector2.x;
					int num5 = Screen.get_height() - (int)vector2.y;
					int[] array = new int[]
					{
						num2,
						num3,
						num4,
						num5
					};
					int num6 = Mathf.Min(array);
					Vector2 zero2 = Vector2.get_zero();
					Quaternion localRotation = Quaternion.get_identity();
					if (num6 == num2)
					{
						zero2.x = 20f;
						zero2.y = vector2.y;
						localRotation = Quaternion.get_identity();
					}
					else if (num6 == num3)
					{
						zero2.x = vector2.x;
						zero2.y = 21f;
						localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
					}
					else if (num6 == num4)
					{
						zero2.x = (float)CamerasMgr.Camera2RTCommon.get_pixelWidth() - 20f;
						zero2.y = vector2.y;
						localRotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
					}
					else
					{
						zero2.x = vector2.x;
						zero2.y = (float)CamerasMgr.Camera2RTCommon.get_pixelHeight() - 20f;
						localRotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
					}
					if (zero2.y > (float)(CamerasMgr.Camera2RTCommon.get_pixelHeight() - 80))
					{
						zero2.y = (float)(CamerasMgr.Camera2RTCommon.get_pixelHeight() - 80);
					}
					ElementInstanceUI.Instance.ImageArrow.set_localRotation(localRotation);
					ElementInstanceUI.Instance.ImageArrow.set_anchoredPosition(zero2);
				}
			}
		}
	}

	public void SetAngle()
	{
		if (this.angle != 999f)
		{
			return;
		}
		Vector2 vector = new Vector2(BallElement.Instance.angleZeroItem.get_localPosition().x, BallElement.Instance.angleZeroItem.get_localPosition().y);
		Vector2 vector2 = new Vector2(base.get_transform().get_localPosition().x, base.get_transform().get_localPosition().y);
		this.dot = Vector3.Dot(vector.get_normalized(), vector2.get_normalized());
		this.angle = Mathf.Acos(this.dot) * 57.29578f;
		this.cross = Vector3.Cross(vector.get_normalized(), vector2.get_normalized());
		if (this.cross.z >= 0f)
		{
			this.angle = 360f - this.angle;
		}
	}

	public void DoOnClickAnimation()
	{
		this.animating = true;
		base.GetComponent<BaseTweenScale>().ChangeScaleTo(new Vector3(0.9f, 0.9f, 0.9f), 0.1f, delegate
		{
			base.GetComponent<BaseTweenScale>().ChangeScaleTo(new Vector3(1f, 1f, 1f), 0.1f, delegate
			{
				this.animating = false;
			});
		});
	}
}
