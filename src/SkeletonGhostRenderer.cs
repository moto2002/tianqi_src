using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class SkeletonGhostRenderer : MonoBehaviour
{
	public float fadeSpeed = 10f;

	private Color32[] colors;

	private Color32 black = new Color32(0, 0, 0, 0);

	private MeshFilter meshFilter;

	private MeshRenderer meshRenderer;

	private void Awake()
	{
		this.meshRenderer = base.get_gameObject().AddComponent<MeshRenderer>();
		this.meshFilter = base.get_gameObject().AddComponent<MeshFilter>();
	}

	public void Initialize(Mesh mesh, Material[] materials, Color32 color, bool additive, float speed, int sortingLayerID, int sortingOrder)
	{
		base.StopAllCoroutines();
		base.get_gameObject().SetActive(true);
		this.meshRenderer.set_sharedMaterials(materials);
		this.meshRenderer.set_sortingLayerID(sortingLayerID);
		this.meshRenderer.set_sortingOrder(sortingOrder);
		this.meshFilter.set_sharedMesh(Object.Instantiate<Mesh>(mesh));
		this.colors = this.meshFilter.get_sharedMesh().get_colors32();
		if (color.a + color.r + color.g + color.b > 0)
		{
			for (int i = 0; i < this.colors.Length; i++)
			{
				this.colors[i] = color;
			}
		}
		this.fadeSpeed = speed;
		if (additive)
		{
			base.StartCoroutine(this.FadeAdditive());
		}
		else
		{
			base.StartCoroutine(this.Fade());
		}
	}

	[DebuggerHidden]
	private IEnumerator Fade()
	{
		SkeletonGhostRenderer.<Fade>c__Iterator5 <Fade>c__Iterator = new SkeletonGhostRenderer.<Fade>c__Iterator5();
		<Fade>c__Iterator.<>f__this = this;
		return <Fade>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator FadeAdditive()
	{
		SkeletonGhostRenderer.<FadeAdditive>c__Iterator6 <FadeAdditive>c__Iterator = new SkeletonGhostRenderer.<FadeAdditive>c__Iterator6();
		<FadeAdditive>c__Iterator.<>f__this = this;
		return <FadeAdditive>c__Iterator;
	}

	public void Cleanup()
	{
		if (this.meshFilter != null && this.meshFilter.get_sharedMesh() != null)
		{
			Object.Destroy(this.meshFilter.get_sharedMesh());
		}
		Object.Destroy(base.get_gameObject());
	}
}
