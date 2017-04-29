using System;
using UnityEngine;

public class MD5Test : MonoBehaviour
{
	private void Awake()
	{
		Debuger.Info(XUtility.Md5Sum(base.get_gameObject().get_name()), new object[0]);
	}
}
