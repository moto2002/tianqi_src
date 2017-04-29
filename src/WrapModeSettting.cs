using System;
using UnityEngine;

public class WrapModeSettting : MonoBehaviour
{
	public Texture2D targetTex;

	public bool repeat;

	private void OnEnable()
	{
		if (this.targetTex != null)
		{
			if (!this.repeat)
			{
				this.targetTex.set_wrapMode(1);
			}
			else
			{
				this.targetTex.set_wrapMode(0);
			}
		}
	}
}
