using System;
using UnityEngine;
using UnityEngine.UI;

public class BallItemInfoUnit : MonoBehaviour
{
	public Text textDes;

	private void Awake()
	{
		this.textDes = base.get_transform().FindChild("Text").GetComponent<Text>();
	}
}
