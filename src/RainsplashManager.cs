using System;
using UnityEngine;

public class RainsplashManager : MonoBehaviour
{
	public int numberOfParticles = 700;

	public float areaSize = 40f;

	public float areaHeight = 15f;

	public float fallingSpeed = 23f;

	public float flakeWidth = 0.4f;

	public float flakeHeight = 0.4f;

	public float flakeRandom = 0.1f;

	public Mesh[] preGennedMeshes;

	private int preGennedIndex;

	public bool generateNewAssetsOnStart;

	public void Start()
	{
	}

	public Mesh GetPreGennedMesh()
	{
		if (this.preGennedMeshes.Length == 0)
		{
			return null;
		}
		return this.preGennedMeshes[this.preGennedIndex++ % this.preGennedMeshes.Length];
	}

	private Mesh CreateMesh()
	{
		Mesh mesh = new Mesh();
		Vector3 vector = base.get_transform().get_right() * Random.Range(0.1f, 2f) + base.get_transform().get_forward() * Random.Range(0.1f, 2f);
		vector = Vector3.Normalize(vector);
		Vector3 vector2 = Vector3.Cross(vector, Vector3.get_up());
		vector2 = Vector3.Normalize(vector2);
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
			Vector3 vector3;
			vector3.x = this.areaSize * (Random.get_value() - 0.5f);
			vector3.y = 0f;
			vector3.z = this.areaSize * (Random.get_value() - 0.5f);
			float value = Random.get_value();
			float num4 = this.flakeWidth + value * this.flakeRandom;
			float num5 = num4;
			array[num2] = vector3 - vector * num4;
			array[num2 + 1] = vector3 + vector * num4;
			Vector3 vector4 = new Vector3((float)((double)vector2.x * 2.0 * (double)num5), (float)((double)vector2.y * 2.0 * (double)num5), (float)((double)vector2.z * 2.0 * (double)num5));
			array[num2 + 2] = vector3 + vector * num4 + vector4;
			array[num2 + 3] = vector3 - vector * num4 + vector4;
			array4[num2] = -CamerasMgr.MainCameraRoot.get_forward();
			array4[num2 + 1] = -CamerasMgr.MainCameraRoot.get_forward();
			array4[num2 + 2] = -CamerasMgr.MainCameraRoot.get_forward();
			array4[num2 + 3] = -CamerasMgr.MainCameraRoot.get_forward();
			array2[num2] = new Vector2(0f, 0f);
			array2[num2 + 1] = new Vector2(1f, 0f);
			array2[num2 + 2] = new Vector2(1f, 1f);
			array2[num2 + 3] = new Vector2(0f, 1f);
			Vector2 vector5 = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
			array3[num2] = new Vector2(vector5.x, vector5.y);
			array3[num2 + 1] = new Vector2(vector5.x, vector5.y);
			array3[num2 + 2] = new Vector2(vector5.x, vector5.y);
			array3[num2 + 3] = new Vector2(vector5.x, vector5.y);
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
}
