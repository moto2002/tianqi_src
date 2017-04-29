using System;
using UnityEngine;

public class FastWaveMesh : MonoBehaviour
{
	public float _waveHeight = 0.25f;

	public float _waveFrag = 0.2f;

	public float _waveSpeed = 1f;

	public float _wave2Height = 0.1f;

	public float _wave2Frag = 0.5f;

	public float _wave2Speed = 0.25f;

	public bool _reCalNormal;

	private Mesh _shareMesh;

	private Mesh _mesh;

	private Vector3[] _oriVertices;

	private Vector3[] _modVertices;

	private float[] _oriHeight;

	private float[] _oriHeight2;

	private void Start()
	{
		this._shareMesh = base.GetComponent<MeshFilter>().get_sharedMesh();
		this._mesh = base.GetComponent<MeshFilter>().get_mesh();
		this._mesh.MarkDynamic();
		this._oriVertices = this._shareMesh.get_vertices();
		this._modVertices = this._shareMesh.get_vertices();
		this._oriHeight = new float[this._modVertices.Length];
		for (int i = 0; i < this._modVertices.Length; i++)
		{
			this._oriHeight[i] = this._modVertices[i].x + this._modVertices[i].z;
			this._oriHeight[i] *= this._waveFrag;
		}
		this._oriHeight2 = new float[this._modVertices.Length];
		for (int j = 0; j < this._modVertices.Length; j++)
		{
			this._oriHeight2[j] = this._modVertices[j].x * 0.5f + this._modVertices[j].z;
			this._oriHeight2[j] *= this._wave2Frag;
		}
	}

	private void Update()
	{
		float time = Time.get_time();
		int num = this._modVertices.Length;
		for (int i = 0; i < num; i++)
		{
			this._modVertices[i].y = this._oriVertices[i].y + Mathf.Sin(time * this._waveSpeed + this._oriHeight[i]) * this._waveHeight;
			Vector3[] expr_62_cp_0 = this._modVertices;
			int expr_62_cp_1 = i;
			expr_62_cp_0[expr_62_cp_1].y = expr_62_cp_0[expr_62_cp_1].y + Mathf.Sin(time * this._wave2Speed + this._oriHeight2[i]) * this._wave2Height;
		}
		this._mesh.set_vertices(this._modVertices);
		if (this._reCalNormal)
		{
			this._mesh.RecalculateNormals();
		}
	}
}
