using System;
using UnityEngine;

public class FXNodeTool : MonoBehaviour
{
	public Transform ParentNode;

	public string ParentNodeName;

	public bool IsRunning;

	public void ResetPosition()
	{
		GameObject gameObject = null;
		if (this.ParentNode != null)
		{
			gameObject = this.ParentNode.get_gameObject();
		}
		else if (!string.IsNullOrEmpty(this.ParentNodeName))
		{
			gameObject = GameObject.Find(this.ParentNodeName);
		}
		if (gameObject == null)
		{
			Debuger.Error("parent is null", new object[0]);
			return;
		}
		FXNodeTool.ResetPosition(base.get_transform(), gameObject.get_transform(), this.IsRunning);
	}

	public static void ResetPosition(Transform fx, Transform parent, bool isRunning = false)
	{
		if (fx == null)
		{
			Debuger.Error("fx is null", new object[0]);
			return;
		}
		if (parent == null)
		{
			Debuger.Error("parent is null", new object[0]);
			return;
		}
		if (isRunning)
		{
			fx.SetParent(parent.get_transform());
			fx.set_localPosition(Vector3.get_zero());
			fx.set_localRotation(Quaternion.get_identity());
			fx.set_localScale(Vector3.get_one());
		}
		else
		{
			fx.set_position(parent.get_transform().get_position());
		}
	}
}
