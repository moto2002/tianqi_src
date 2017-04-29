using System;
using UnityEngine;

public class RoleRotate : MonoBehaviour
{
	public Transform boy;

	public Transform girl;

	private bool isZoom;

	public Transform objMen;

	public Transform objWomen;

	private void Start()
	{
		EventDispatcher.AddListener<Vector2, PlayerCareerType>("DragRoleAround", new Callback<Vector2, PlayerCareerType>(this.OnDragRole));
		EventDispatcher.AddListener<bool>("ClickCameraMove", new Callback<bool>(this.ClickBtn));
		EventDispatcher.AddListener<bool>("UpdateSwitchMove", new Callback<bool>(this.SwitchState));
	}

	private void SwitchState(bool arg1)
	{
		EventDispatcher.Broadcast<bool>("RoleMoveRole", arg1);
	}

	private void ClickBtn(bool arg1)
	{
		this.isZoom = !this.isZoom;
		if (this.isZoom)
		{
			EventDispatcher.Broadcast<bool>("CameraMoveEnter", arg1);
		}
		else
		{
			EventDispatcher.Broadcast<bool>("CameraMoveOut", arg1);
		}
	}

	public void OnDragRole(Vector2 delta, PlayerCareerType type)
	{
		float num = -delta.x * Time.get_deltaTime() * 20f;
		if (type == PlayerCareerType.None)
		{
			base.get_transform().Rotate(Vector3.get_up(), num);
		}
	}
}
