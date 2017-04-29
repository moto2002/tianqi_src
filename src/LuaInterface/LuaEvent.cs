using System;

namespace LuaInterface
{
	public class LuaEvent : IDisposable
	{
		protected LuaState luaState;

		protected bool beDisposed;

		private LuaTable self;

		private LuaFunction _add;

		private LuaFunction _remove;

		public LuaEvent(LuaTable table)
		{
			this.self = table;
			this.luaState = table.GetLuaState();
			this.self.AddRef();
			this._add = this.self.GetLuaFunction("Add");
			this._remove = this.self.GetLuaFunction("Remove");
		}

		public void Dispose()
		{
			this.self.Dispose();
			this._add.Dispose();
			this._remove.Dispose();
			this.Clear();
		}

		private void Clear()
		{
			this._add = null;
			this._remove = null;
			this.self = null;
			this.luaState = null;
		}

		public void Dispose(bool disposeManagedResources)
		{
			if (!this.beDisposed)
			{
				this.beDisposed = true;
				if (this._add != null)
				{
					this._add.Dispose(disposeManagedResources);
					this._add = null;
				}
				if (this._remove != null)
				{
					this._remove.Dispose(disposeManagedResources);
					this._remove = null;
				}
				if (this.self != null)
				{
					this.self.Dispose(disposeManagedResources);
				}
				this.Clear();
			}
		}

		public void Add(LuaFunction func, LuaTable obj)
		{
			if (func == null)
			{
				return;
			}
			this._add.BeginPCall();
			this._add.Push(this.self);
			this._add.Push(func);
			this._add.Push(obj);
			this._add.PCall();
			this._add.EndPCall();
		}

		public void Remove(LuaFunction func, LuaTable obj)
		{
			if (func == null)
			{
				return;
			}
			this._remove.BeginPCall();
			this._remove.Push(this.self);
			this._remove.Push(func);
			this._remove.Push(obj);
			this._remove.PCall();
			this._remove.EndPCall();
		}
	}
}
