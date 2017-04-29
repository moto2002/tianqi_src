using System;
using UnityEngine;

public class CameraDeAlpha : MonoBehaviour
{
	private Material mat;

	private void OnPostRender()
	{
		if (!this.mat)
		{
			this.mat = new Material(ShaderManager.Find("Hsh(Mobile)/FX/ParticleAlphaBlended"));
			this.mat.get_shader().set_hideFlags(61);
			this.mat.set_hideFlags(61);
		}
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < this.mat.get_passCount(); i++)
		{
			this.mat.SetPass(i);
			GL.Begin(7);
			GL.Vertex3(0f, 0f, 0.1f);
			GL.Vertex3(1f, 0f, 0.1f);
			GL.Vertex3(1f, 1f, 0.1f);
			GL.Vertex3(0f, 1f, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}
}
