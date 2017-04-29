using System;
using UnityEngine;

public class NcTransformTool
{
	public Vector3 m_vecPos;

	public Quaternion m_vecRot;

	public Vector3 m_vecRotHint;

	public Vector3 m_vecScale;

	public NcTransformTool()
	{
		this.m_vecPos = default(Vector3);
		this.m_vecRot = default(Quaternion);
		this.m_vecRotHint = default(Vector3);
		this.m_vecScale = new Vector3(1f, 1f, 1f);
	}

	public NcTransformTool(Transform val)
	{
		this.SetLocalTransform(val);
	}

	public static Vector3 GetZeroVector()
	{
		return Vector3.get_zero();
	}

	public static Vector3 GetUnitVector()
	{
		return new Vector3(1f, 1f, 1f);
	}

	public static Quaternion GetIdenQuaternion()
	{
		return Quaternion.get_identity();
	}

	public static void InitLocalTransform(Transform dst)
	{
		dst.set_localPosition(NcTransformTool.GetZeroVector());
		dst.set_localRotation(NcTransformTool.GetIdenQuaternion());
		dst.set_localScale(NcTransformTool.GetUnitVector());
	}

	public static void InitWorldTransform(Transform dst)
	{
		dst.set_position(NcTransformTool.GetZeroVector());
		dst.set_rotation(NcTransformTool.GetIdenQuaternion());
		NcTransformTool.InitWorldScale(dst);
	}

	public static void InitWorldScale(Transform dst)
	{
		dst.set_localScale(NcTransformTool.GetUnitVector());
		dst.set_localScale(new Vector3((dst.get_lossyScale().x != 0f) ? (1f / dst.get_lossyScale().x) : 1f, (dst.get_lossyScale().y != 0f) ? (1f / dst.get_lossyScale().y) : 1f, (dst.get_lossyScale().z != 0f) ? (1f / dst.get_lossyScale().z) : 1f));
	}

	public static void CopyLocalTransform(Transform src, Transform dst)
	{
		dst.set_localPosition(src.get_localPosition());
		dst.set_localRotation(src.get_localRotation());
		dst.set_localScale(src.get_localScale());
	}

	public static void CopyLossyToLocalScale(Vector3 srcLossyScale, Transform dst)
	{
		dst.set_localScale(NcTransformTool.GetUnitVector());
		dst.set_localScale(new Vector3((dst.get_lossyScale().x != 0f) ? (srcLossyScale.x / dst.get_lossyScale().x) : srcLossyScale.x, (dst.get_lossyScale().y != 0f) ? (srcLossyScale.y / dst.get_lossyScale().y) : srcLossyScale.y, (dst.get_lossyScale().z != 0f) ? (srcLossyScale.z / dst.get_lossyScale().z) : srcLossyScale.z));
	}

	public void CopyToLocalTransform(Transform dst)
	{
		dst.set_localPosition(this.m_vecPos);
		dst.set_localRotation(this.m_vecRot);
		dst.set_localScale(this.m_vecScale);
	}

	public void CopyToTransform(Transform dst)
	{
		dst.set_position(this.m_vecPos);
		dst.set_rotation(this.m_vecRot);
		NcTransformTool.CopyLossyToLocalScale(this.m_vecScale, dst);
	}

	public void AddLocalTransform(Transform val)
	{
		this.m_vecPos += val.get_localPosition();
		this.m_vecRot = Quaternion.Euler(this.m_vecRot.get_eulerAngles() + val.get_localRotation().get_eulerAngles());
		this.m_vecScale = Vector3.Scale(this.m_vecScale, val.get_localScale());
	}

	public void SetLocalTransform(Transform val)
	{
		this.m_vecPos = val.get_localPosition();
		this.m_vecRot = val.get_localRotation();
		this.m_vecScale = val.get_localScale();
	}

	public bool IsLocalEquals(Transform val)
	{
		return !(this.m_vecPos != val.get_localPosition()) && !(this.m_vecRot != val.get_localRotation()) && !(this.m_vecScale != val.get_localScale());
	}

	public void AddTransform(Transform val)
	{
		this.m_vecPos += val.get_position();
		this.m_vecRot = Quaternion.Euler(this.m_vecRot.get_eulerAngles() + val.get_rotation().get_eulerAngles());
		this.m_vecScale = Vector3.Scale(this.m_vecScale, val.get_lossyScale());
	}

	public void SetTransform(Transform val)
	{
		this.m_vecPos = val.get_position();
		this.m_vecRot = val.get_rotation();
		this.m_vecScale = val.get_lossyScale();
	}

	public bool IsEquals(Transform val)
	{
		return !(this.m_vecPos != val.get_position()) && !(this.m_vecRot != val.get_rotation()) && !(this.m_vecScale != val.get_lossyScale());
	}

	public void SetTransform(NcTransformTool val)
	{
		this.m_vecPos = val.m_vecPos;
		this.m_vecRot = val.m_vecRot;
		this.m_vecScale = val.m_vecScale;
	}

	public static float GetTransformScaleMeanValue(Transform srcTrans)
	{
		return (srcTrans.get_lossyScale().x + srcTrans.get_lossyScale().y + srcTrans.get_lossyScale().z) / 3f;
	}

	public static Vector3 GetTransformScaleMeanVector(Transform srcTrans)
	{
		float transformScaleMeanValue = NcTransformTool.GetTransformScaleMeanValue(srcTrans);
		return new Vector3(transformScaleMeanValue, transformScaleMeanValue, transformScaleMeanValue);
	}
}
