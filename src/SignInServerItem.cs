using System;
using UnityEngine;
using UnityEngine.UI;

public class SignInServerItem : MonoBehaviour
{
	public Image ImageFrame;

	public Image ImageIcon;

	public Text Text;

	public int itemIDCache;

	private void Awake()
	{
		this.ImageFrame = base.get_transform().FindChild("ImageFrame").GetComponent<Image>();
		this.ImageIcon = base.get_transform().FindChild("ImageIcon").GetComponent<Image>();
		this.Text = base.get_transform().FindChild("Text").GetComponent<Text>();
	}
}
