using System;
using UnityEngine;

public class PolygenFrontCtrl : MonoBehaviour
{
	public Material m_material;

	public Material m_materialLine;

	private MeshFilter m_meshFilter;

	private MeshRenderer m_meshRenderer;

	private LineRenderer m_lineRenderer;

	private Vector3[] newVertices;

	private Vector3[] newLineVertices;

	private Vector3 origin = Vector3.get_zero();

	private Vector2[] newUV;

	public float _value1;

	public float _value2;

	public float _value3;

	public float _value4;

	public float _value5;

	public float _value6;

	public void RefreshMesh()
	{
		this.CalNewVertices();
		this.CalNewUV();
		this.SetLineRenderer();
		this.SetMeshFilter();
	}

	private void SetMeshFilter()
	{
		if (this.m_meshFilter == null)
		{
			this.m_meshFilter = base.get_gameObject().AddUniqueComponent<MeshFilter>();
		}
		if (this.m_meshRenderer == null)
		{
			this.m_meshRenderer = base.get_gameObject().AddUniqueComponent<MeshRenderer>();
			this.m_meshRenderer.set_sortingOrder(2001);
			this.m_meshRenderer.set_material(this.m_material);
		}
		this.m_meshFilter.get_mesh().set_vertices(this.newVertices);
		this.m_meshFilter.get_mesh().set_uv(this.newUV);
		this.m_meshFilter.get_mesh().set_triangles(PolygenBackCtrl.GetPoygenTriangles());
	}

	private void SetLineRenderer()
	{
		if (this.m_lineRenderer == null)
		{
			this.m_lineRenderer = base.get_gameObject().AddUniqueComponent<LineRenderer>();
			this.m_lineRenderer.set_sortingOrder(2002);
			this.m_lineRenderer.set_useWorldSpace(false);
			this.m_lineRenderer.GetComponent<Renderer>().set_sharedMaterial(this.m_materialLine);
			this.m_lineRenderer.SetWidth(0.007f, 0.007f);
			this.m_lineRenderer.SetVertexCount(this.newLineVertices.Length);
		}
		for (int i = 0; i < this.newLineVertices.Length; i++)
		{
			this.m_lineRenderer.SetPosition(i, this.newLineVertices[i]);
		}
	}

	private void CalNewVertices()
	{
		Vector3 vector = new Vector3(PolygenBackCtrl.PolygenPoint1.x * (1f - this._value1), PolygenBackCtrl.PolygenPoint1.y * (1f - this._value1), 0f);
		Vector3 vector2 = new Vector3(PolygenBackCtrl.PolygenPoint2.x * (1f - this._value2), PolygenBackCtrl.PolygenPoint2.y * (1f - this._value2), 0f);
		Vector3 vector3 = new Vector3(PolygenBackCtrl.PolygenPoint3.x * (1f - this._value3), PolygenBackCtrl.PolygenPoint3.y * (1f - this._value3), 0f);
		Vector3 vector4 = new Vector3(PolygenBackCtrl.PolygenPoint4.x * (1f - this._value4), PolygenBackCtrl.PolygenPoint4.y * (1f - this._value4), 0f);
		Vector3 vector5 = new Vector3(PolygenBackCtrl.PolygenPoint5.x * (1f - this._value5), PolygenBackCtrl.PolygenPoint5.y * (1f - this._value5), 0f);
		Vector3 vector6 = new Vector3(PolygenBackCtrl.PolygenPoint6.x * (1f - this._value6), PolygenBackCtrl.PolygenPoint6.y * (1f - this._value6), 0f);
		this.newVertices = new Vector3[]
		{
			vector,
			vector2,
			vector3,
			vector4,
			vector5,
			vector6,
			this.origin
		};
		this.newLineVertices = new Vector3[]
		{
			vector,
			vector2,
			vector3,
			vector4,
			vector5,
			vector6,
			vector
		};
	}

	private void CalNewUV()
	{
		this.newUV = new Vector2[]
		{
			Vector2.Lerp(PolygenBackCtrl.uvPoint1, PolygenBackCtrl.uvPointOrigin, this._value1),
			Vector2.Lerp(PolygenBackCtrl.uvPoint2, PolygenBackCtrl.uvPointOrigin, this._value2),
			Vector2.Lerp(PolygenBackCtrl.uvPoint3, PolygenBackCtrl.uvPointOrigin, this._value3),
			Vector2.Lerp(PolygenBackCtrl.uvPoint4, PolygenBackCtrl.uvPointOrigin, this._value4),
			Vector2.Lerp(PolygenBackCtrl.uvPoint5, PolygenBackCtrl.uvPointOrigin, this._value5),
			Vector2.Lerp(PolygenBackCtrl.uvPoint6, PolygenBackCtrl.uvPointOrigin, this._value6),
			new Vector2(0.5f, 0.5f)
		};
	}
}
