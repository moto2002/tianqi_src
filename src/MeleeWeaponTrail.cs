using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponTrail : MonoBehaviour
{
	[Serializable]
	public class Point
	{
		public float timeCreated;

		public Vector3 basePosition;

		public Vector3 tipPosition;
	}

	private bool _emitOfInit = true;

	private float _emitTimeOfInit;

	[SerializeField]
	private bool _emit = true;

	private bool _use = true;

	[SerializeField]
	private float _emitTime;

	[SerializeField]
	private Material _material;

	[SerializeField]
	private float _lifeTime = 1f;

	[SerializeField]
	private Color[] _colors;

	[SerializeField]
	private float[] _sizes;

	[SerializeField]
	private float _minVertexDistance = 0.1f;

	[SerializeField]
	private float _maxVertexDistance = 10f;

	private float _minVertexDistanceSqr;

	private float _maxVertexDistanceSqr;

	[SerializeField]
	private float _maxAngle = 3f;

	[SerializeField]
	private bool _autoDestruct;

	[SerializeField]
	private int subdivisions = 4;

	[SerializeField]
	private Transform _base;

	[SerializeField]
	private Transform _tip;

	private List<MeleeWeaponTrail.Point> _points = new List<MeleeWeaponTrail.Point>();

	private List<MeleeWeaponTrail.Point> _smoothedPoints = new List<MeleeWeaponTrail.Point>();

	private GameObject _trailObject;

	private Mesh _trailMesh;

	private Vector3 _lastPosition;

	private List<MeleeWeaponTrail.Point> m_listRemove = new List<MeleeWeaponTrail.Point>();

	public bool Emit
	{
		set
		{
			this._emit = value;
		}
	}

	public bool Use
	{
		set
		{
			this._use = value;
		}
	}

	private void Awake()
	{
		this._emitOfInit = this._emit;
		this._emitTimeOfInit = this._emitTime;
	}

	private void OnEnable()
	{
		this._emit = this._emitOfInit;
		this._emitTime = this._emitTimeOfInit;
		this._lastPosition = base.get_transform().get_position();
		this._trailObject = new GameObject("Trail");
		this._trailObject.set_layer(LayerSystem.NameToLayer("FX"));
		this._trailObject.get_transform().set_parent(null);
		this._trailObject.get_transform().set_position(Vector3.get_zero());
		this._trailObject.get_transform().set_rotation(Quaternion.get_identity());
		this._trailObject.get_transform().set_localScale(Vector3.get_one());
		this._trailObject.AddComponent(typeof(MeshFilter));
		this._trailObject.AddComponent(typeof(MeshRenderer));
		this._trailObject.GetComponent<Renderer>().set_material(this._material);
		this._trailMesh = new Mesh();
		this._trailMesh.set_name(base.get_name() + "TrailMesh");
		this._trailObject.GetComponent<MeshFilter>().set_mesh(this._trailMesh);
		this._minVertexDistanceSqr = this._minVertexDistance * this._minVertexDistance;
		this._maxVertexDistanceSqr = this._maxVertexDistance * this._maxVertexDistance;
	}

	private void OnDisable()
	{
		Object.Destroy(this._trailObject);
	}

	private void Update()
	{
		if (!this._use)
		{
			return;
		}
		if (this._emit && this._emitTime != 0f)
		{
			this._emitTime -= Time.get_deltaTime();
			if (this._emitTime == 0f)
			{
				this._emitTime = -1f;
			}
			if (this._emitTime < 0f)
			{
				this._emit = false;
			}
		}
		if (!this._emit && this._points.get_Count() == 0 && this._autoDestruct)
		{
			Object.Destroy(this._trailObject);
			Object.Destroy(base.get_gameObject());
		}
		float sqrMagnitude = (this._lastPosition - base.get_transform().get_position()).get_sqrMagnitude();
		if (this._emit)
		{
			if (sqrMagnitude > this._minVertexDistanceSqr)
			{
				bool flag = false;
				if (this._points.get_Count() < 3)
				{
					flag = true;
				}
				else
				{
					Vector3 vector = this._points.get_Item(this._points.get_Count() - 2).tipPosition - this._points.get_Item(this._points.get_Count() - 3).tipPosition;
					Vector3 vector2 = this._points.get_Item(this._points.get_Count() - 1).tipPosition - this._points.get_Item(this._points.get_Count() - 2).tipPosition;
					if (Vector3.Angle(vector, vector2) > this._maxAngle || sqrMagnitude > this._maxVertexDistanceSqr)
					{
						flag = true;
					}
				}
				if (flag)
				{
					MeleeWeaponTrail.Point point = new MeleeWeaponTrail.Point();
					point.basePosition = this._base.get_position();
					point.tipPosition = this._tip.get_position();
					point.timeCreated = Time.get_time();
					this._points.Add(point);
					this._lastPosition = base.get_transform().get_position();
					if (this._points.get_Count() == 1)
					{
						this._smoothedPoints.Add(point);
					}
					else if (this._points.get_Count() > 1)
					{
						for (int i = 0; i < 1 + this.subdivisions; i++)
						{
							this._smoothedPoints.Add(point);
						}
					}
					if (this._points.get_Count() >= 4)
					{
						IEnumerable<Vector3> enumerable = Interpolate.NewCatmullRom(new Vector3[]
						{
							this._points.get_Item(this._points.get_Count() - 4).tipPosition,
							this._points.get_Item(this._points.get_Count() - 3).tipPosition,
							this._points.get_Item(this._points.get_Count() - 2).tipPosition,
							this._points.get_Item(this._points.get_Count() - 1).tipPosition
						}, this.subdivisions, false);
						IEnumerable<Vector3> enumerable2 = Interpolate.NewCatmullRom(new Vector3[]
						{
							this._points.get_Item(this._points.get_Count() - 4).basePosition,
							this._points.get_Item(this._points.get_Count() - 3).basePosition,
							this._points.get_Item(this._points.get_Count() - 2).basePosition,
							this._points.get_Item(this._points.get_Count() - 1).basePosition
						}, this.subdivisions, false);
						List<Vector3> list = new List<Vector3>(enumerable);
						List<Vector3> list2 = new List<Vector3>(enumerable2);
						float timeCreated = this._points.get_Item(this._points.get_Count() - 4).timeCreated;
						float timeCreated2 = this._points.get_Item(this._points.get_Count() - 1).timeCreated;
						for (int j = 0; j < list.get_Count(); j++)
						{
							int num = this._smoothedPoints.get_Count() - (list.get_Count() - j);
							if (num > -1 && num < this._smoothedPoints.get_Count())
							{
								MeleeWeaponTrail.Point point2 = new MeleeWeaponTrail.Point();
								point2.basePosition = list2.get_Item(j);
								point2.tipPosition = list.get_Item(j);
								point2.timeCreated = Mathf.Lerp(timeCreated, timeCreated2, (float)j / (float)list.get_Count());
								this._smoothedPoints.set_Item(num, point2);
							}
						}
					}
				}
				else
				{
					if (this._points.get_Count() > 0)
					{
						this._points.get_Item(this._points.get_Count() - 1).basePosition = this._base.get_position();
						this._points.get_Item(this._points.get_Count() - 1).tipPosition = this._tip.get_position();
					}
					if (this._smoothedPoints.get_Count() > 0)
					{
						this._smoothedPoints.get_Item(this._smoothedPoints.get_Count() - 1).basePosition = this._base.get_position();
						this._smoothedPoints.get_Item(this._smoothedPoints.get_Count() - 1).tipPosition = this._tip.get_position();
					}
				}
			}
			else
			{
				if (this._points.get_Count() > 0)
				{
					this._points.get_Item(this._points.get_Count() - 1).basePosition = this._base.get_position();
					this._points.get_Item(this._points.get_Count() - 1).tipPosition = this._tip.get_position();
				}
				if (this._smoothedPoints.get_Count() > 0)
				{
					this._smoothedPoints.get_Item(this._smoothedPoints.get_Count() - 1).basePosition = this._base.get_position();
					this._smoothedPoints.get_Item(this._smoothedPoints.get_Count() - 1).tipPosition = this._tip.get_position();
				}
			}
		}
		this.RemoveOldPoints(this._points);
		if (this._points.get_Count() == 0)
		{
			this._trailMesh.Clear();
		}
		this.RemoveOldPoints(this._smoothedPoints);
		if (this._smoothedPoints.get_Count() == 0)
		{
			this._trailMesh.Clear();
		}
		List<MeleeWeaponTrail.Point> smoothedPoints = this._smoothedPoints;
		if (smoothedPoints.get_Count() > 1)
		{
			Vector3[] array = new Vector3[smoothedPoints.get_Count() * 2];
			Vector2[] array2 = new Vector2[smoothedPoints.get_Count() * 2];
			int[] array3 = new int[(smoothedPoints.get_Count() - 1) * 6];
			Color[] array4 = new Color[smoothedPoints.get_Count() * 2];
			for (int k = 0; k < smoothedPoints.get_Count(); k++)
			{
				MeleeWeaponTrail.Point point3 = smoothedPoints.get_Item(k);
				float num2 = (Time.get_time() - point3.timeCreated) / this._lifeTime;
				Color color = Color.Lerp(Color.get_white(), Color.get_clear(), num2);
				if (this._colors != null && this._colors.Length > 0)
				{
					float num3 = num2 * (float)(this._colors.Length - 1);
					float num4 = Mathf.Floor(num3);
					float num5 = Mathf.Clamp(Mathf.Ceil(num3), 1f, (float)(this._colors.Length - 1));
					float num6 = Mathf.InverseLerp(num4, num5, num3);
					if (num4 >= (float)this._colors.Length)
					{
						num4 = (float)(this._colors.Length - 1);
					}
					if (num4 < 0f)
					{
						num4 = 0f;
					}
					if (num5 >= (float)this._colors.Length)
					{
						num5 = (float)(this._colors.Length - 1);
					}
					if (num5 < 0f)
					{
						num5 = 0f;
					}
					color = Color.Lerp(this._colors[(int)num4], this._colors[(int)num5], num6);
				}
				float num7 = 0f;
				if (this._sizes != null && this._sizes.Length > 0)
				{
					float num8 = num2 * (float)(this._sizes.Length - 1);
					float num9 = Mathf.Floor(num8);
					float num10 = Mathf.Clamp(Mathf.Ceil(num8), 1f, (float)(this._sizes.Length - 1));
					float num11 = Mathf.InverseLerp(num9, num10, num8);
					if (num9 >= (float)this._sizes.Length)
					{
						num9 = (float)(this._sizes.Length - 1);
					}
					if (num9 < 0f)
					{
						num9 = 0f;
					}
					if (num10 >= (float)this._sizes.Length)
					{
						num10 = (float)(this._sizes.Length - 1);
					}
					if (num10 < 0f)
					{
						num10 = 0f;
					}
					num7 = Mathf.Lerp(this._sizes[(int)num9], this._sizes[(int)num10], num11);
				}
				Vector3 vector3 = point3.tipPosition - point3.basePosition;
				array[k * 2] = point3.basePosition - vector3 * (num7 * 0.5f);
				array[k * 2 + 1] = point3.tipPosition + vector3 * (num7 * 0.5f);
				array4[k * 2] = (array4[k * 2 + 1] = color);
				float num12 = (float)k / (float)smoothedPoints.get_Count();
				array2[k * 2] = new Vector2(num12, 0f);
				array2[k * 2 + 1] = new Vector2(num12, 1f);
				if (k > 0)
				{
					array3[(k - 1) * 6] = k * 2 - 2;
					array3[(k - 1) * 6 + 1] = k * 2 - 1;
					array3[(k - 1) * 6 + 2] = k * 2;
					array3[(k - 1) * 6 + 3] = k * 2 + 1;
					array3[(k - 1) * 6 + 4] = k * 2;
					array3[(k - 1) * 6 + 5] = k * 2 - 1;
				}
			}
			this._trailMesh.Clear();
			this._trailMesh.set_vertices(array);
			this._trailMesh.set_colors(array4);
			this._trailMesh.set_uv(array2);
			this._trailMesh.set_triangles(array3);
		}
	}

	private void RemoveOldPoints(List<MeleeWeaponTrail.Point> pointList)
	{
		this.m_listRemove.Clear();
		using (List<MeleeWeaponTrail.Point>.Enumerator enumerator = pointList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MeleeWeaponTrail.Point current = enumerator.get_Current();
				if (Time.get_time() - current.timeCreated > this._lifeTime)
				{
					this.m_listRemove.Add(current);
				}
			}
		}
		using (List<MeleeWeaponTrail.Point>.Enumerator enumerator2 = this.m_listRemove.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				MeleeWeaponTrail.Point current2 = enumerator2.get_Current();
				pointList.Remove(current2);
			}
		}
	}
}
