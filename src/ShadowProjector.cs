using System;
using UnityEngine;

public class ShadowProjector
{
	private const string ShadowTexName = "Shadow";

	private const string FalloffTexName = "Falloff";

	private const string ShadowName = "Projector2Shadow";

	private static Material m_matProjector;

	public static Material MatProjector
	{
		get
		{
			if (ShadowProjector.m_matProjector == null)
			{
				ShadowProjector.m_matProjector = new Material(ShaderManager.Find("Hidden/ProjectorMultiply"));
				Texture2D texture2D = null;
				Texture2D texture2D2 = null;
				ShaderEffectUtils.SafeCreateTexture(ref texture2D, "Shadow");
				ShaderEffectUtils.SafeCreateTexture(ref texture2D2, "Falloff");
				ShadowProjector.m_matProjector.SetTexture("_ShadowTex", texture2D);
				ShadowProjector.m_matProjector.SetTexture("_FalloffTex", texture2D2);
			}
			return ShadowProjector.m_matProjector;
		}
	}

	public static void ShowShadowProjector(Transform actorTarget, bool bHideShadow)
	{
		if (!bHideShadow)
		{
			Transform transform = actorTarget.FindChild("Projector2Shadow");
			GameObject gameObject = (!(transform != null)) ? null : transform.get_gameObject();
			if (gameObject == null)
			{
				gameObject = new GameObject("Projector2Shadow");
				gameObject.get_transform().set_parent(actorTarget);
				gameObject.get_transform().set_localPosition(new Vector3(0f, 5f, 0f));
				gameObject.get_transform().set_localScale(Vector3.get_one());
				gameObject.get_transform().set_localEulerAngles(new Vector3(90f, 0f, 0f));
				Projector projector = gameObject.AddComponent<Projector>();
				projector.set_nearClipPlane(0.1f);
				projector.set_farClipPlane(50f);
				projector.set_fieldOfView(20f);
				projector.set_aspectRatio(1f);
				projector.set_orthographic(false);
				projector.set_material(ShadowProjector.MatProjector);
			}
			gameObject.GetComponent<Projector>().set_enabled(true);
		}
		else
		{
			Transform transform2 = actorTarget.FindChild("Projector2Shadow");
			if (transform2 != null)
			{
				transform2.GetComponent<Projector>().set_enabled(false);
			}
		}
	}
}
