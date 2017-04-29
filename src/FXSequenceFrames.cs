using System;
using UnityEngine;
using XEngineActor;

public class FXSequenceFrames : MonoBehaviour
{
	public bool IsLoop = true;

	public int SizeY = 1;

	public int SizeX = 1;

	public float LoopTime = 1f;

	public int Circle = 1;

	private Renderer m_renderer;

	private Material m_material;

	private bool _IsPlay = true;

	private bool IsInit;

	private float FrameTime;

	private int FrameSum;

	private float escapeTime;

	private int index;

	private bool IsPlay
	{
		get
		{
			return this._IsPlay;
		}
		set
		{
			this._IsPlay = value;
			base.GetComponent<Renderer>().set_enabled(value);
		}
	}

	private void InitSelf()
	{
		if (this.IsInit)
		{
			return;
		}
		this.IsInit = true;
		this.m_renderer = base.GetComponent<Renderer>();
		if (this.m_renderer == null)
		{
			base.set_enabled(false);
			Debug.LogError("m_renderer is null");
			return;
		}
		this.m_material = this.m_renderer.get_material();
		if (this.m_material == null)
		{
			base.set_enabled(false);
			Debug.LogError("m_material is null");
			return;
		}
		if (!this.CheckIsSequence(this.m_material.get_shader().get_name()))
		{
			base.set_enabled(false);
			ActorFX componentInParent = base.get_transform().GetComponentInParent<ActorFX>();
			if (componentInParent != null)
			{
				Debug.LogError("shader is illegal, root = + " + componentInParent.get_transform().get_name() + ", transform = " + base.get_transform().get_name());
			}
			else
			{
				Debug.LogError("shader is illegal, transform = " + base.get_transform().get_name());
			}
			return;
		}
		this.FrameSum = this.SizeY * this.SizeY * this.Circle;
		this.FrameTime = this.LoopTime / (float)this.FrameSum;
		this.m_material.SetFloat(ShaderPIDManager._SizeY, (float)this.SizeY);
		this.m_material.SetFloat(ShaderPIDManager._SizeX, (float)this.SizeX);
	}

	private void OnEnable()
	{
		this.InitSelf();
		this.index = 0;
		this.SetIndex();
		this.IsPlay = true;
	}

	private void Update()
	{
		if (!this.IsPlay)
		{
			return;
		}
		if (this.m_material == null)
		{
			return;
		}
		if (!this.m_renderer.get_enabled())
		{
			return;
		}
		if (!this.CheckIsSequence(this.m_material.get_shader().get_name()))
		{
			return;
		}
		this.escapeTime += Time.get_deltaTime();
		if (this.escapeTime > this.FrameTime)
		{
			this.index++;
			this.escapeTime -= this.FrameTime;
			if (this.index >= this.FrameSum)
			{
				this.index = 0;
				this.SetIndex();
				if (!this.IsLoop)
				{
					this.IsPlay = false;
				}
			}
			else
			{
				this.SetIndex();
			}
		}
	}

	private void SetIndex()
	{
		int num = this.index / this.SizeX;
		int num2 = this.index - num * this.SizeX;
		this.m_material.SetFloat(ShaderPIDManager._IndexY, (float)(this.SizeY - num - 1));
		this.m_material.SetFloat(ShaderPIDManager._IndexX, (float)num2);
	}

	private bool CheckIsSequence(string shader_name)
	{
		return this.m_material.get_shader().get_name().Contains("Hsh(Mobile)/FX/SequenceFramesAdd") || this.m_material.get_shader().get_name().Contains("Hsh(Mobile)/FX/SequenceFramesAlphaBlended");
	}
}
