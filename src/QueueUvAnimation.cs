using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class QueueUvAnimation : MonoBehaviour
{
	public int RowsFadeIn = 4;

	public int ColumnsFadeIn = 4;

	public int RowsLoop = 4;

	public int ColumnsLoop = 4;

	public float Fps = 20f;

	public bool IsBump;

	public Material NextMaterial;

	private int index;

	private int count;

	private int allCount;

	private float deltaTime;

	private bool isVisible;

	private bool isFadeHandle;

	private void Start()
	{
		this.deltaTime = 1f / this.Fps;
		this.InitDefaultTex(this.RowsFadeIn, this.ColumnsFadeIn);
	}

	private void InitDefaultTex(int rows, int colums)
	{
		this.count = rows * colums;
		this.index += colums - 1;
		Vector2 vector = new Vector2(1f / (float)colums, 1f / (float)rows);
		base.GetComponent<Renderer>().get_material().SetTextureScale("_MainTex", vector);
		if (this.IsBump)
		{
			base.GetComponent<Renderer>().get_material().SetTextureScale("_BumpMap", vector);
		}
	}

	private void OnBecameVisible()
	{
		this.isVisible = true;
		base.StartCoroutine(this.UpdateTiling());
	}

	private void OnBecameInvisible()
	{
		this.isVisible = false;
	}

	[DebuggerHidden]
	private IEnumerator UpdateTiling()
	{
		QueueUvAnimation.<UpdateTiling>c__Iterator25 <UpdateTiling>c__Iterator = new QueueUvAnimation.<UpdateTiling>c__Iterator25();
		<UpdateTiling>c__Iterator.<>f__this = this;
		return <UpdateTiling>c__Iterator;
	}
}
