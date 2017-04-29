using System;

namespace LuaInterface
{
	public abstract class LuaBaseRef : IDisposable
	{
		public string name;

		protected int reference;

		protected LuaState luaState;

		protected ObjectTranslator translator;

		protected bool beDisposed;

		protected int count;

		public LuaBaseRef()
		{
			this.count = 1;
		}

		~LuaBaseRef()
		{
			this.Dispose(false);
		}

		public virtual void Dispose()
		{
			this.count--;
			if (this.count > 0)
			{
				return;
			}
			this.Dispose(true);
		}

		public void AddRef()
		{
			this.count++;
		}

		public virtual void Dispose(bool disposeManagedResources)
		{
			if (!this.beDisposed)
			{
				this.beDisposed = true;
				if (this.reference > 0 && this.luaState != null)
				{
					this.luaState.CollectRef(this.reference, this.name, !disposeManagedResources);
				}
				this.reference = 0;
				this.luaState = null;
			}
		}

		public LuaState GetLuaState()
		{
			return this.luaState;
		}

		public void Push()
		{
			this.luaState.Push(this);
		}

		public override int GetHashCode()
		{
			return this.reference;
		}

		public virtual int GetReference()
		{
			return this.reference;
		}

		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			LuaBaseRef luaBaseRef = o as LuaBaseRef;
			return luaBaseRef != null && luaBaseRef.reference == this.reference;
		}

		public bool IsAlive()
		{
			return !this.beDisposed;
		}

		public static bool operator ==(LuaBaseRef a, LuaBaseRef b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (a == null)
			{
				return b == null || b.reference == 0;
			}
			if (b == null)
			{
				return a.reference == 0;
			}
			return b != null && a.reference == b.reference;
		}

		public static bool operator !=(LuaBaseRef a, LuaBaseRef b)
		{
			return !(a == b);
		}
	}
}
