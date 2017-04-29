using System;
using UnityEngine;

public class CameraRevolve : MonoBehaviour
{
	public static CameraRevolve instance;

	private Transform actor;

	private Vector3 fromRolePosition;

	private Vector3 fromCameraPosition;

	private Vector3 toCameraPosition;

	private Quaternion toCameraRotation;

	private bool isStart;

	private float moveYPerSec;

	private float moveXZPerSec;

	private float lineRotatePerSec;

	private float forwardRotatePerSec;

	private float fromRoleCameraDistance;

	private float toRoleCameraDistance;

	private float timeCounter;

	private float timeCount;

	private Vector3 fromRoleCameraLine;

	private void Awake()
	{
		CameraRevolve.instance = this;
	}

	public void Init(Transform actor, Vector3 position, Quaternion rotation, float rotateTime)
	{
		CameraGlobal.cameraType = CameraType.Revolve;
		this.actor = actor;
		this.timeCounter = 0f;
		this.timeCount = rotateTime;
		this.fromRolePosition = this.actor.get_position();
		this.fromCameraPosition = CamerasMgr.MainCameraRoot.get_position();
		this.toCameraPosition = this.actor.get_position() + this.actor.get_right() * position.x + this.actor.get_up() * position.y + this.actor.get_forward() * position.z;
		this.toCameraRotation = this.actor.get_rotation() * rotation;
		if (rotateTime == 0f)
		{
			CamerasMgr.MainCameraRoot.set_position(this.toCameraPosition);
			CamerasMgr.MainCameraRoot.set_rotation(this.toCameraRotation);
		}
		else
		{
			this.isStart = true;
			this.moveYPerSec = (this.toCameraPosition.y - this.fromCameraPosition.y) / rotateTime;
			Vector3 t = this.fromRolePosition - this.fromCameraPosition;
			Vector3 t2 = this.fromRolePosition - this.toCameraPosition;
			this.fromRoleCameraDistance = t.AssignYZero().get_magnitude();
			this.toRoleCameraDistance = t2.AssignYZero().get_magnitude();
			this.moveXZPerSec = (this.toRoleCameraDistance - this.fromRoleCameraDistance) / rotateTime;
			float num = Vector3.Angle(t.AssignYZero(), t2.AssignYZero());
			this.lineRotatePerSec = Mathf.Sign(Vector3.Cross(t.AssignYZero(), t2.AssignYZero()).y) * num / rotateTime;
			float num2 = Quaternion.Angle(CamerasMgr.MainCameraRoot.get_rotation(), this.toCameraRotation);
			this.forwardRotatePerSec = num2 / rotateTime;
			this.fromRoleCameraLine = this.fromCameraPosition - this.fromRolePosition;
		}
	}

	private void Update()
	{
		if (!this.isStart)
		{
			return;
		}
		if (CameraGlobal.cameraType != CameraType.Revolve)
		{
			this.isStart = false;
			return;
		}
		Quaternion quaternion = Quaternion.AngleAxis(this.lineRotatePerSec * Time.get_deltaTime(), Vector3.get_up());
		this.fromRoleCameraDistance += this.moveXZPerSec * Time.get_deltaTime();
		this.fromRoleCameraLine = quaternion * this.fromRoleCameraLine.AssignYZero().get_normalized();
		Vector3 vector = this.fromRoleCameraLine * this.fromRoleCameraDistance;
		this.fromCameraPosition.y = this.fromCameraPosition.y + this.moveYPerSec * Time.get_deltaTime();
		CamerasMgr.MainCameraRoot.set_position(this.actor.get_position().AssignY(this.fromCameraPosition.y) + vector);
		CamerasMgr.MainCameraRoot.set_rotation(Quaternion.RotateTowards(CamerasMgr.MainCameraRoot.get_rotation(), this.toCameraRotation, this.forwardRotatePerSec * Time.get_deltaTime()));
		this.timeCounter += Time.get_deltaTime();
		if (this.timeCounter >= this.timeCount)
		{
			Debug.Log("!!! CameraRevolve=" + this.timeCount);
			this.isStart = false;
		}
	}
}
