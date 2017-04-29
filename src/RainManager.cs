using System;
using UnityEngine;

public class RainManager : TimerInterval
{
	public float minYPosition = 1f;

	public float fallingSpeed = 20f;

	public Vector3 fallingDirection = new Vector3(10f, 0f, 30f);

	private int preGennedIndex;

	public int numberOfParticles = 400;

	public float areaSize = 40f;

	public float areaHeight = 15f;

	public float particleSize = 0.2f;

	public float flakeRandom = 0.1f;

	public Mesh[] preGennedMeshes;

	public bool generateNewAssetsOnStart;

	private RainBox[] m_RainBoxs;

	public Mesh GetPreGennedMesh()
	{
		if (this.preGennedMeshes.Length == 0)
		{
			return null;
		}
		return this.preGennedMeshes[this.preGennedIndex++ % this.preGennedMeshes.Length];
	}

	public void Start()
	{
	}

	private Mesh CreateMesh()
	{
		Transform transform = Camera.get_main().get_transform();
		Mesh mesh = new Mesh();
		Vector3 right = transform.get_right();
		Vector3 up = Vector3.get_up();
		int num = this.numberOfParticles;
		Vector3[] array = new Vector3[4 * num];
		Vector2[] array2 = new Vector2[4 * num];
		Vector2[] array3 = new Vector2[4 * num];
		Vector3[] array4 = new Vector3[4 * num];
		int[] array5 = new int[6 * num];
		for (int i = 0; i < num; i++)
		{
			int num2 = i * 4;
			int num3 = i * 6;
			Vector3 vector;
			vector.x = this.areaSize * (Random.get_value() - 0.5f);
			vector.y = this.areaHeight * Random.get_value();
			vector.z = this.areaSize * (Random.get_value() - 0.5f);
			float value = Random.get_value();
			float num4 = this.particleSize * 0.215f;
			float num5 = this.particleSize + value * this.flakeRandom;
			array[num2] = vector - right * num4 - up * num5;
			array[num2 + 1] = vector + right * num4 - up * num5;
			array[num2 + 2] = vector + right * num4 + up * num5;
			array[num2 + 3] = vector - right * num4 + up * num5;
			array4[num2] = -transform.get_forward();
			array4[num2 + 1] = -transform.get_forward();
			array4[num2 + 2] = -transform.get_forward();
			array4[num2 + 3] = -transform.get_forward();
			array2[num2] = new Vector2(0f, 0f);
			array2[num2 + 1] = new Vector2(1f, 0f);
			array2[num2 + 2] = new Vector2(1f, 1f);
			array2[num2 + 3] = new Vector2(0f, 1f);
			array3[num2] = new Vector2((float)((double)Random.Range(-2, 2) * 4.0), (float)((double)Random.Range(-1, 1) * 1.0));
			array3[num2 + 1] = new Vector2(array3[num2].x, array3[num2].y);
			array3[num2 + 2] = new Vector2(array3[num2].x, array3[num2].y);
			array3[num2 + 3] = new Vector2(array3[num2].x, array3[num2].y);
			array5[num3] = num2;
			array5[num3 + 1] = num2 + 1;
			array5[num3 + 2] = num2 + 2;
			array5[num3 + 3] = num2;
			array5[num3 + 4] = num2 + 2;
			array5[num3 + 5] = num2 + 3;
		}
		mesh.set_vertices(array);
		mesh.set_triangles(array5);
		mesh.set_normals(array4);
		mesh.set_uv(array2);
		mesh.set_uv2(array3);
		mesh.RecalculateBounds();
		return mesh;
	}

	private void Update()
	{
	}

	private void UpdateDirection()
	{
		if (this.m_RainBoxs == null)
		{
			this.m_RainBoxs = base.GetComponentsInChildren<RainBox>(true);
		}
		for (int i = 0; i < this.m_RainBoxs.Length; i++)
		{
			this.m_RainBoxs[i].SetDirection(this.fallingDirection);
		}
	}
}
