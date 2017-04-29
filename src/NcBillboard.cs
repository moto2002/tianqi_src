using System;
using UnityEngine;

public class NcBillboard : NcEffectBehaviour
{
	public enum AXIS_TYPE
	{
		AXIS_FORWARD,
		AXIS_BACK,
		AXIS_RIGHT,
		AXIS_LEFT,
		AXIS_UP,
		AXIS_DOWN
	}

	public enum ROTATION
	{
		NONE,
		RND,
		ROTATE
	}

	public enum AXIS
	{
		X,
		Y,
		Z
	}

	public bool m_bCameraLookAt;

	public bool m_bFixedObjectUp;

	public bool m_bFixedStand;

	public NcBillboard.AXIS_TYPE m_FrontAxis;

	public NcBillboard.ROTATION m_RatationMode;

	public NcBillboard.AXIS m_RatationAxis = NcBillboard.AXIS.Z;

	public float m_fRotationValue = 180f;

	protected float m_fRndValue;

	protected float m_fTotalRotationValue;

	protected Quaternion m_qOiginal;

	private void Awake()
	{
	}

	private void OnEnable()
	{
		this.UpdateBillboard();
	}

	public void UpdateBillboard()
	{
		this.m_fRndValue = Random.Range(0f, 360f);
		if (base.get_enabled())
		{
			this.Update();
		}
	}

	private void Start()
	{
		this.m_qOiginal = base.get_transform().get_rotation();
	}

	private void Update()
	{
		if (Camera.get_main() == null)
		{
			return;
		}
		Vector3 vector;
		if (this.m_bFixedObjectUp)
		{
			vector = base.get_transform().get_up();
		}
		else
		{
			vector = Camera.get_main().get_transform().get_rotation() * Vector3.get_up();
		}
		if (this.m_bCameraLookAt)
		{
			base.get_transform().LookAt(Camera.get_main().get_transform(), vector);
		}
		else
		{
			base.get_transform().LookAt(base.get_transform().get_position() + Camera.get_main().get_transform().get_rotation() * Vector3.get_back(), vector);
		}
		switch (this.m_FrontAxis)
		{
		case NcBillboard.AXIS_TYPE.AXIS_BACK:
			base.get_transform().Rotate(base.get_transform().get_up(), 180f, 0);
			break;
		case NcBillboard.AXIS_TYPE.AXIS_RIGHT:
			base.get_transform().Rotate(base.get_transform().get_up(), 270f, 0);
			break;
		case NcBillboard.AXIS_TYPE.AXIS_LEFT:
			base.get_transform().Rotate(base.get_transform().get_up(), 90f, 0);
			break;
		case NcBillboard.AXIS_TYPE.AXIS_UP:
			base.get_transform().Rotate(base.get_transform().get_right(), 90f, 0);
			break;
		case NcBillboard.AXIS_TYPE.AXIS_DOWN:
			base.get_transform().Rotate(base.get_transform().get_right(), 270f, 0);
			break;
		}
		if (this.m_bFixedStand)
		{
			base.get_transform().set_rotation(Quaternion.Euler(new Vector3(0f, base.get_transform().get_rotation().get_eulerAngles().y, base.get_transform().get_rotation().get_eulerAngles().z)));
		}
		if (this.m_RatationMode == NcBillboard.ROTATION.RND)
		{
			Transform expr_1E6 = base.get_transform();
			expr_1E6.set_localRotation(expr_1E6.get_localRotation() * Quaternion.Euler((this.m_RatationAxis != NcBillboard.AXIS.X) ? 0f : this.m_fRndValue, (this.m_RatationAxis != NcBillboard.AXIS.Y) ? 0f : this.m_fRndValue, (this.m_RatationAxis != NcBillboard.AXIS.Z) ? 0f : this.m_fRndValue));
		}
		if (this.m_RatationMode == NcBillboard.ROTATION.ROTATE)
		{
			float num = this.m_fTotalRotationValue + NcEffectBehaviour.GetEngineDeltaTime() * this.m_fRotationValue;
			base.get_transform().Rotate((this.m_RatationAxis != NcBillboard.AXIS.X) ? 0f : num, (this.m_RatationAxis != NcBillboard.AXIS.Y) ? 0f : num, (this.m_RatationAxis != NcBillboard.AXIS.Z) ? 0f : num, 1);
			this.m_fTotalRotationValue = num;
		}
	}
}
