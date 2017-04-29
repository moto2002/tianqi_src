using System;
using UnityEngine;

public class NcUvAnimation : NcEffectAniBehaviour
{
	public bool m_isShaderMaterial;

	public float m_fScrollSpeedX = 1f;

	public float m_fScrollSpeedY;

	public float m_fTilingX = 1f;

	public float m_fTilingY = 1f;

	public float m_fOffsetX;

	public float m_fOffsetY;

	public bool m_bFixedTileSize;

	public bool m_bRepeat = true;

	public bool m_bAutoDestruct;

	protected Vector3 m_OriginalScale = default(Vector3);

	protected Vector2 m_OriginalTiling = default(Vector2);

	protected Vector2 m_EndOffset = default(Vector2);

	protected Vector2 m_RepeatOffset = default(Vector2);

	protected Renderer m_Renderer;

	private Material m_Material;

	private bool SetMaterial()
	{
		if (this.m_Renderer == null || this.m_Renderer.get_sharedMaterial() == null || this.m_Renderer.get_sharedMaterial().get_mainTexture() == null)
		{
			base.set_enabled(false);
			return false;
		}
		if (this.m_isShaderMaterial)
		{
			this.m_Material = this.m_Renderer.get_sharedMaterial();
		}
		else
		{
			this.m_Material = this.m_Renderer.get_material();
		}
		return true;
	}

	private Material GetMaterial()
	{
		if (this.m_Renderer == null)
		{
			return null;
		}
		return this.m_Material;
	}

	private void Start()
	{
		this.m_Renderer = base.GetComponent<Renderer>();
		if (this.SetMaterial())
		{
			this.GetMaterial().set_mainTextureScale(new Vector2(this.m_fTilingX, this.m_fTilingY));
			float num = this.m_fOffsetX + this.m_fTilingX;
			this.m_RepeatOffset.x = num - (float)((int)num);
			if (this.m_RepeatOffset.x < 0f)
			{
				this.m_RepeatOffset.x = this.m_RepeatOffset.x + 1f;
			}
			num = this.m_fOffsetY + this.m_fTilingY;
			this.m_RepeatOffset.y = num - (float)((int)num);
			if (this.m_RepeatOffset.y < 0f)
			{
				this.m_RepeatOffset.y = this.m_RepeatOffset.y + 1f;
			}
			this.m_EndOffset.x = 1f - (this.m_fTilingX - (float)((int)this.m_fTilingX) + (float)((this.m_fTilingX - (float)((int)this.m_fTilingX) >= 0f) ? 0 : 1));
			this.m_EndOffset.y = 1f - (this.m_fTilingY - (float)((int)this.m_fTilingY) + (float)((this.m_fTilingY - (float)((int)this.m_fTilingY) >= 0f) ? 0 : 1));
			base.InitAnimationTimer();
		}
	}

	private void Update()
	{
		if (this.GetMaterial() == null)
		{
			return;
		}
		if (this.m_bFixedTileSize)
		{
			if (this.m_fScrollSpeedX != 0f && this.m_OriginalScale.x != 0f)
			{
				this.m_fTilingX = this.m_OriginalTiling.x * (base.get_transform().get_lossyScale().x / this.m_OriginalScale.x);
			}
			if (this.m_fScrollSpeedY != 0f && this.m_OriginalScale.y != 0f)
			{
				this.m_fTilingY = this.m_OriginalTiling.y * (base.get_transform().get_lossyScale().y / this.m_OriginalScale.y);
			}
			this.GetMaterial().set_mainTextureScale(new Vector2(this.m_fTilingX, this.m_fTilingY));
		}
		this.m_fOffsetX += this.m_Timer.GetDeltaTime() * this.m_fScrollSpeedX;
		this.m_fOffsetY += this.m_Timer.GetDeltaTime() * this.m_fScrollSpeedY;
		bool flag = false;
		if (!this.m_bRepeat)
		{
			this.m_RepeatOffset.x = this.m_RepeatOffset.x + this.m_Timer.GetDeltaTime() * this.m_fScrollSpeedX;
			if (this.m_RepeatOffset.x < 0f || 1f < this.m_RepeatOffset.x)
			{
				this.m_fOffsetX = this.m_EndOffset.x;
				base.set_enabled(false);
				flag = true;
			}
			this.m_RepeatOffset.y = this.m_RepeatOffset.y + this.m_Timer.GetDeltaTime() * this.m_fScrollSpeedY;
			if (this.m_RepeatOffset.y < 0f || 1f < this.m_RepeatOffset.y)
			{
				this.m_fOffsetY = this.m_EndOffset.y;
				base.set_enabled(false);
				flag = true;
			}
		}
		this.GetMaterial().set_mainTextureOffset(new Vector2(this.m_fOffsetX, this.m_fOffsetY));
		if (flag)
		{
			base.OnEndAnimation();
			if (this.m_bAutoDestruct)
			{
				Object.DestroyObject(base.get_gameObject());
			}
		}
	}

	public void SetFixedTileSize(bool bFixedTileSize)
	{
		this.m_bFixedTileSize = bFixedTileSize;
	}

	public override int GetAnimationState()
	{
		if (!this.m_bRepeat)
		{
			if (!base.get_enabled() || !NcEffectBehaviour.IsActive(base.get_gameObject()) || !base.IsEndAnimation())
			{
			}
		}
		return -1;
	}

	public override void ResetAnimation()
	{
		if (!base.get_enabled())
		{
			base.set_enabled(true);
		}
		this.Start();
	}

	public override void OnUpdateEffectSpeed(float fSpeedRate, bool bRuntime)
	{
		this.m_fScrollSpeedX *= fSpeedRate;
		this.m_fScrollSpeedY *= fSpeedRate;
	}

	public override void OnUpdateToolData()
	{
		this.m_OriginalScale = base.get_transform().get_lossyScale();
		this.m_OriginalTiling.x = this.m_fTilingX;
		this.m_OriginalTiling.y = this.m_fTilingY;
	}
}
