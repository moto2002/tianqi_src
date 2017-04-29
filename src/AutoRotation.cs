using System;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{
	private Transform m_myTransform;

	private uint fxUid;

	private bool IsRotate;

	private float fAngleRotate;

	private void OnEnable()
	{
		this.DoRotation(this.IsRotate);
	}

	private void OnDisable()
	{
		this.DoRotation(false);
	}

	public void SetRotation(bool rotate, float angleRotate)
	{
		this.IsRotate = rotate;
		this.fAngleRotate = angleRotate;
		this.DoRotation(this.IsRotate);
	}

	public void DoRotation(bool rotate)
	{
		if (rotate)
		{
			TimerHeap.DelTimer(this.fxUid);
			this.m_myTransform = base.get_transform();
			this.fxUid = TimerHeap.AddTimer(0u, 40, delegate
			{
				if (this.m_myTransform != null)
				{
					Transform expr_17 = this.m_myTransform;
					expr_17.set_localEulerAngles(expr_17.get_localEulerAngles() - new Vector3(0f, 0f, this.fAngleRotate));
				}
			});
		}
		else
		{
			TimerHeap.DelTimer(this.fxUid);
		}
	}
}
