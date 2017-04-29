using System;
using UnityEngine;

public class FixSlowDistortionOnMobile : MonoBehaviour
{
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest);
	}
}
