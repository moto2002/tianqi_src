using System;
using UnityEngine;

public class PolygenBackCtrl : MonoBehaviour
{
	public Material m_material;

	private static float RANGE = 53f;

	private static float Height = 1.7f;

	private static float Width = 2f;

	public static Vector3 PolygenPoint1 = new Vector3(PolygenBackCtrl.RANGE * -1f, PolygenBackCtrl.RANGE * PolygenBackCtrl.Height, 0f);

	public static Vector3 PolygenPoint2 = new Vector3(PolygenBackCtrl.RANGE * 1f, PolygenBackCtrl.RANGE * PolygenBackCtrl.Height, 0f);

	public static Vector3 PolygenPoint3 = new Vector3(PolygenBackCtrl.RANGE * PolygenBackCtrl.Width, PolygenBackCtrl.RANGE * 0f, 0f);

	public static Vector3 PolygenPoint4 = new Vector3(PolygenBackCtrl.RANGE * 1f, PolygenBackCtrl.RANGE * -PolygenBackCtrl.Height, 0f);

	public static Vector3 PolygenPoint5 = new Vector3(PolygenBackCtrl.RANGE * -1f, PolygenBackCtrl.RANGE * -PolygenBackCtrl.Height, 0f);

	public static Vector3 PolygenPoint6 = new Vector3(PolygenBackCtrl.RANGE * -PolygenBackCtrl.Width, PolygenBackCtrl.RANGE * 0f, 0f);

	public static Vector3 PolygenPointEnd = new Vector2(0f, 0f);

	private Vector3[] m_vertices;

	private MeshFilter m_meshFilter;

	private MeshRenderer m_meshRenderer;

	public static Vector2 uvPoint1 = new Vector2(0.25f, 1f);

	public static Vector2 uvPoint2 = new Vector2(0.75f, 1f);

	public static Vector2 uvPoint3 = new Vector2(1f, 0.5f);

	public static Vector2 uvPoint4 = new Vector2(0.75f, 0f);

	public static Vector2 uvPoint5 = new Vector2(0.25f, 0f);

	public static Vector2 uvPoint6 = new Vector2(0f, 0.5f);

	public static Vector2 uvPointOrigin = new Vector2(0.5f, 0.5f);

	public static int[] PolygenTriangles;

	private void Start()
	{
		this.m_vertices = new Vector3[]
		{
			PolygenBackCtrl.PolygenPoint1,
			PolygenBackCtrl.PolygenPoint2,
			PolygenBackCtrl.PolygenPoint3,
			PolygenBackCtrl.PolygenPoint4,
			PolygenBackCtrl.PolygenPoint5,
			PolygenBackCtrl.PolygenPoint6,
			PolygenBackCtrl.PolygenPointEnd
		};
		this.SetMeshFilter();
	}

	private void SetMeshFilter()
	{
		if (this.m_meshFilter == null)
		{
			this.m_meshFilter = (MeshFilter)base.get_gameObject().AddComponent(typeof(MeshFilter));
		}
		if (this.m_meshRenderer == null)
		{
			this.m_meshRenderer = (MeshRenderer)base.get_gameObject().AddComponent(typeof(MeshRenderer));
			this.m_meshRenderer.set_material(this.m_material);
		}
		this.m_meshFilter.get_mesh().set_vertices(this.m_vertices);
		this.m_meshFilter.get_mesh().set_uv(this.CalNewUV());
		this.m_meshFilter.get_mesh().set_triangles(PolygenBackCtrl.GetPoygenTriangles());
	}

	private Vector2[] CalNewUV()
	{
		return new Vector2[]
		{
			Vector2.Lerp(PolygenBackCtrl.uvPoint1, PolygenBackCtrl.uvPointOrigin, 0f),
			Vector2.Lerp(PolygenBackCtrl.uvPoint2, PolygenBackCtrl.uvPointOrigin, 0f),
			Vector2.Lerp(PolygenBackCtrl.uvPoint3, PolygenBackCtrl.uvPointOrigin, 0f),
			Vector2.Lerp(PolygenBackCtrl.uvPoint4, PolygenBackCtrl.uvPointOrigin, 0f),
			Vector2.Lerp(PolygenBackCtrl.uvPoint5, PolygenBackCtrl.uvPointOrigin, 0f),
			Vector2.Lerp(PolygenBackCtrl.uvPoint6, PolygenBackCtrl.uvPointOrigin, 0f),
			new Vector2(0.5f, 0.5f)
		};
	}

	public static int[] GetPoygenTriangles()
	{
		if (PolygenBackCtrl.PolygenTriangles != null)
		{
			return PolygenBackCtrl.PolygenTriangles;
		}
		PolygenBackCtrl.PolygenTriangles = new int[]
		{
			6,
			0,
			1,
			6,
			1,
			2,
			6,
			2,
			3,
			6,
			3,
			4,
			6,
			4,
			5,
			6,
			5,
			0
		};
		return PolygenBackCtrl.PolygenTriangles;
	}
}
