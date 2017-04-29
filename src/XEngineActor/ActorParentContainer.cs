using System;

namespace XEngineActor
{
	public class ActorParentContainer<T> : ActorParent where T : EntityParent
	{
		private T m_theEntity;

		public T theEntity
		{
			get
			{
				return this.m_theEntity;
			}
			set
			{
				this.m_theEntity = value;
				this.ResetController();
			}
		}

		public override EntityParent GetEntity()
		{
			return this.theEntity;
		}
	}
}
