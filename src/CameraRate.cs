using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class CameraRate : MonoBehaviour
{
	public Transform boyPoint;

	public Transform boyLock;

	public Transform girlPoint;

	public Transform girlLock;

	public Transform rolePoint;

	private Transform mCameraPos;

	private float mainDistance;

	private float mainHeight;

	private bool isMainAround;

	private Vector3 aroundPoint = Vector3.get_zero();

	private Vector3 finalTarget = Vector3.get_zero();

	private float preAngle;

	private void Start()
	{
		this.isMainAround = true;
		this.aroundPoint = this.rolePoint.get_position();
		this.mCameraPos = CamerasMgr.MainCameraRoot;
		this.mainDistance = this.GetNoYDistance(base.get_transform().get_position(), this.rolePoint.get_position());
		this.mainHeight = base.get_transform().get_position().y;
		if (this.mCameraPos != null)
		{
			base.StartCoroutine(this.Rotate());
		}
		EventDispatcher.AddListener<bool>("CameraMoveEnter", new Callback<bool>(this.OnClickEnter));
		EventDispatcher.AddListener<bool>("CameraMoveOut", new Callback<bool>(this.OnClickOut));
		EventDispatcher.AddListener<bool>("RoleMoveRole", new Callback<bool>(this.RoleMoveToRole));
		EventDispatcher.AddListener<Vector2, PlayerCareerType>("DragRoleAround", new Callback<Vector2, PlayerCareerType>(this.OnDragRole));
	}

	private void OnDragRole(Vector2 delta, PlayerCareerType arg2)
	{
		if (arg2 == PlayerCareerType.None)
		{
			return;
		}
		float num = delta.x * Time.get_deltaTime() * 20f;
		Vector3 noYNormalized = this.GetNoYNormalized(this.finalTarget, this.aroundPoint);
		Vector3 noYNormalized2 = this.GetNoYNormalized(base.get_transform().get_position(), this.aroundPoint);
		float num2 = Vector3.Angle(noYNormalized, noYNormalized2);
		if (Vector3.Cross(noYNormalized, noYNormalized2).y < 0f)
		{
			num2 *= -1f;
		}
		if (num2 + num > 75f)
		{
			num = 75f - num2;
		}
		else if (num2 + num < -75f)
		{
			num = -75f - num2;
		}
		if (Mathf.Abs(num2 + num) > 75f)
		{
			return;
		}
		this.RotateByAngleAndRadius(num);
	}

	public void RoleMoveToRole(bool isMen)
	{
		if (this.isMainAround)
		{
			return;
		}
		LoginManager.Instance.IsCreatAnimationing = true;
		Vector3 position;
		if (isMen)
		{
			position = this.boyLock.get_position();
		}
		else
		{
			position = this.girlLock.get_position();
		}
		float angle;
		this.finalTarget = this.GetAngleAndDir(isMen, out angle);
		base.StartCoroutine(this.RoleToRole(angle, this.finalTarget, position));
	}

	[DebuggerHidden]
	private IEnumerator RoleToRole(float angle, Vector3 target, Vector3 lockPoint)
	{
		CameraRate.<RoleToRole>c__Iterator54 <RoleToRole>c__Iterator = new CameraRate.<RoleToRole>c__Iterator54();
		<RoleToRole>c__Iterator.target = target;
		<RoleToRole>c__Iterator.angle = angle;
		<RoleToRole>c__Iterator.lockPoint = lockPoint;
		<RoleToRole>c__Iterator.<$>target = target;
		<RoleToRole>c__Iterator.<$>angle = angle;
		<RoleToRole>c__Iterator.<$>lockPoint = lockPoint;
		<RoleToRole>c__Iterator.<>f__this = this;
		return <RoleToRole>c__Iterator;
	}

	private void OnClickOut(bool isMen)
	{
		if (this.isMainAround)
		{
			return;
		}
		LoginManager.Instance.IsCreatAnimationing = true;
		float angle;
		this.finalTarget = this.GetAngleAndDir(isMen, out angle);
		base.StartCoroutine(this.MoveToCamera(angle, this.finalTarget));
	}

	[DebuggerHidden]
	private IEnumerator MoveToCamera(float angle, Vector3 target)
	{
		CameraRate.<MoveToCamera>c__Iterator55 <MoveToCamera>c__Iterator = new CameraRate.<MoveToCamera>c__Iterator55();
		<MoveToCamera>c__Iterator.angle = angle;
		<MoveToCamera>c__Iterator.<$>angle = angle;
		<MoveToCamera>c__Iterator.<>f__this = this;
		return <MoveToCamera>c__Iterator;
	}

	private void OnClickEnter(bool isMen)
	{
		if (!this.isMainAround)
		{
			return;
		}
		LoginManager.Instance.IsCreatAnimationing = true;
		this.isMainAround = false;
		Vector3 position;
		if (isMen)
		{
			position = this.boyLock.get_position();
		}
		else
		{
			position = this.girlLock.get_position();
		}
		float angle;
		this.finalTarget = this.GetAngleAndDir(isMen, out angle);
		base.StartCoroutine(this.MoveToRole(angle, this.finalTarget, position));
	}

	[DebuggerHidden]
	private IEnumerator MoveToRole(float angle, Vector3 target, Vector3 lockPoint)
	{
		CameraRate.<MoveToRole>c__Iterator56 <MoveToRole>c__Iterator = new CameraRate.<MoveToRole>c__Iterator56();
		<MoveToRole>c__Iterator.target = target;
		<MoveToRole>c__Iterator.angle = angle;
		<MoveToRole>c__Iterator.lockPoint = lockPoint;
		<MoveToRole>c__Iterator.<$>target = target;
		<MoveToRole>c__Iterator.<$>angle = angle;
		<MoveToRole>c__Iterator.<$>lockPoint = lockPoint;
		<MoveToRole>c__Iterator.<>f__this = this;
		return <MoveToRole>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator SwitchRadius(float fromRadius, float toRadius, float fromHeight, float toHeight, float angle, Vector3 lockPoint, float moveTime = 0.8f)
	{
		CameraRate.<SwitchRadius>c__Iterator57 <SwitchRadius>c__Iterator = new CameraRate.<SwitchRadius>c__Iterator57();
		<SwitchRadius>c__Iterator.angle = angle;
		<SwitchRadius>c__Iterator.lockPoint = lockPoint;
		<SwitchRadius>c__Iterator.moveTime = moveTime;
		<SwitchRadius>c__Iterator.fromRadius = fromRadius;
		<SwitchRadius>c__Iterator.toRadius = toRadius;
		<SwitchRadius>c__Iterator.fromHeight = fromHeight;
		<SwitchRadius>c__Iterator.toHeight = toHeight;
		<SwitchRadius>c__Iterator.<$>angle = angle;
		<SwitchRadius>c__Iterator.<$>lockPoint = lockPoint;
		<SwitchRadius>c__Iterator.<$>moveTime = moveTime;
		<SwitchRadius>c__Iterator.<$>fromRadius = fromRadius;
		<SwitchRadius>c__Iterator.<$>toRadius = toRadius;
		<SwitchRadius>c__Iterator.<$>fromHeight = fromHeight;
		<SwitchRadius>c__Iterator.<$>toHeight = toHeight;
		<SwitchRadius>c__Iterator.<>f__this = this;
		return <SwitchRadius>c__Iterator;
	}

	private Vector3 GetTarget(bool isMen)
	{
		Vector3 position;
		if (isMen)
		{
			position = this.boyPoint.get_position();
		}
		else
		{
			position = this.girlPoint.get_position();
		}
		return position;
	}

	private Vector3 GetAngleAndDir(bool isMen, out float ang)
	{
		Vector3 target = this.GetTarget(isMen);
		Vector3 noYNormalized = this.GetNoYNormalized(this.rolePoint.get_position(), target);
		Vector3 noYNormalized2 = this.GetNoYNormalized(this.rolePoint.get_position(), base.get_transform().get_position());
		ang = Vector3.Angle(noYNormalized2, noYNormalized);
		if (Vector3.Cross(noYNormalized2, noYNormalized).y < 0f)
		{
			ang *= -1f;
		}
		return target;
	}

	[DebuggerHidden]
	private IEnumerator FastRotate(float ang, float moveTime = 1f)
	{
		CameraRate.<FastRotate>c__Iterator58 <FastRotate>c__Iterator = new CameraRate.<FastRotate>c__Iterator58();
		<FastRotate>c__Iterator.ang = ang;
		<FastRotate>c__Iterator.moveTime = moveTime;
		<FastRotate>c__Iterator.<$>ang = ang;
		<FastRotate>c__Iterator.<$>moveTime = moveTime;
		<FastRotate>c__Iterator.<>f__this = this;
		return <FastRotate>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator FastPosition(Vector3 target, float moveTime = 1f)
	{
		CameraRate.<FastPosition>c__Iterator59 <FastPosition>c__Iterator = new CameraRate.<FastPosition>c__Iterator59();
		<FastPosition>c__Iterator.target = target;
		<FastPosition>c__Iterator.moveTime = moveTime;
		<FastPosition>c__Iterator.<$>target = target;
		<FastPosition>c__Iterator.<$>moveTime = moveTime;
		<FastPosition>c__Iterator.<>f__this = this;
		return <FastPosition>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator Rotate()
	{
		CameraRate.<Rotate>c__Iterator5A <Rotate>c__Iterator5A = new CameraRate.<Rotate>c__Iterator5A();
		<Rotate>c__Iterator5A.<>f__this = this;
		return <Rotate>c__Iterator5A;
	}

	private void RotateByAngleAndRadius(float angle, float radius, float height, Vector3 lookPoint)
	{
		Vector3 noYNormalized = this.GetNoYNormalized(this.rolePoint.get_position(), base.get_transform().get_position());
		Vector3 vector = Quaternion.Euler(0f, angle, 0f) * noYNormalized;
		vector *= radius;
		vector.y = height;
		base.get_transform().set_position(vector);
		base.get_transform().LookAt(lookPoint);
	}

	private void RotateByAngleAndRadius(float angle)
	{
		base.get_transform().RotateAround(this.aroundPoint, Vector3.get_up(), angle);
	}

	private Vector3 GetNoYNormalized(Vector3 start, Vector3 end)
	{
		start.y = 0f;
		end.y = 0f;
		return (end - start).get_normalized();
	}

	private float GetNoYDistance(Vector3 start, Vector3 end)
	{
		start.y = 0f;
		end.y = 0f;
		return Vector3.Distance(end, start);
	}
}
