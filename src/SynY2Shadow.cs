using System;
using UnityEngine;

public class SynY2Shadow : MonoBehaviour
{
	private Transform root;

	public static float dir_x;

	public static float dir_y;

	public static float dir_z;

	public float offset_y = 0.03f;

	public bool hideShadow;

	private int m_count;

	private float shadowY;

	public void Init(Transform tranRoot, bool bHideShadow = false)
	{
		this.root = tranRoot;
		this.hideShadow = bHideShadow;
		this.SetVerticalDirection();
	}

	private void Update()
	{
		this.SetShadowY();
		if (Input.GetKeyDown(282))
		{
			this.SetDirection(1f, 1f, -0.8f);
		}
		if (Input.GetKeyDown(283))
		{
			this.SetDirection(-0.2f, 0.8f, -1.5f);
		}
		if (Input.GetKeyDown(284))
		{
			this.SetDirection(SynY2Shadow.dir_x, SynY2Shadow.dir_y, SynY2Shadow.dir_z);
		}
		if (Input.GetKeyDown(13))
		{
			switch (this.m_count % 3)
			{
			case 0:
				this.SetDirection(1f, 1f, -0.8f);
				break;
			case 1:
				this.SetDirection(-0.2f, 0.8f, -1.5f);
				break;
			case 2:
				this.SetDirection(SynY2Shadow.dir_x, SynY2Shadow.dir_y, SynY2Shadow.dir_z);
				break;
			}
		}
	}

	private void SetVerticalDirection()
	{
		this.SetDirection(0f, 0f, 0f);
	}

	private void SetDirection(float x, float y, float z)
	{
		if (base.GetComponent<Renderer>() != null && Utils.GetShareMaterial(base.GetComponent<Renderer>()) != null)
		{
			Utils.GetShareMaterial(base.GetComponent<Renderer>()).SetFloat(ShaderPIDManager._LitDirX, x);
			Utils.GetShareMaterial(base.GetComponent<Renderer>()).SetFloat(ShaderPIDManager._LitDirY, y);
			Utils.GetShareMaterial(base.GetComponent<Renderer>()).SetFloat(ShaderPIDManager._LitDirZ, z);
		}
	}

	private void SetShadowY()
	{
		if (InstanceManager.CurrentInstanceType == InstanceType.None)
		{
			this.shadowY = this.root.get_localPosition().y + this.GetOffsetY();
		}
		else
		{
			this.shadowY = this.GetOffsetY();
		}
		if (base.GetComponent<Renderer>() != null && Utils.GetShareMaterial(base.GetComponent<Renderer>()) != null)
		{
			Utils.GetShareMaterial(base.GetComponent<Renderer>()).SetFloat(ShaderPIDManager._ShadowY, this.shadowY);
		}
	}

	private float GetOffsetY()
	{
		if (this.hideShadow)
		{
			return -10000f;
		}
		return this.offset_y;
	}
}
