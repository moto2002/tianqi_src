using System;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
	private void OnEnable()
	{
		LoadingUIView.Close();
		UIManagerControl.Instance.OpenUI("SelectRoleUI", UINodesManager.NormalUIRoot, true, UIType.NonPush);
	}
}
