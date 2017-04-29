using System;
using XEngine.AssetLoader;

public class FXSpinePool : ActorPool<ActorFXSpine>
{
	public static readonly FXSpinePool Instance = new FXSpinePool();

	protected override void SetPipeline()
	{
		this.pipeline = new FXSpinePipeline();
	}

	protected override ObjectPool GetPool()
	{
		if (this.m_pool == null)
		{
			this.m_pool = new ObjectPoolOfFXSpine();
		}
		return this.m_pool;
	}

	public override void Clear()
	{
		this.GetPool().DestroyAll();
	}
}
