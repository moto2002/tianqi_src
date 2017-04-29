using System;
using System.Collections.Generic;

namespace LuaInterface
{
	public class LuaTable : LuaBaseRef
	{
		public object this[string key]
		{
			get
			{
				int newTop = this.luaState.LuaGetTop();
				object result;
				try
				{
					this.luaState.Push(this);
					this.luaState.Push(key);
					this.luaState.LuaGetTable(-2);
					object obj = this.luaState.ToVariant(-1);
					this.luaState.LuaSetTop(newTop);
					result = obj;
				}
				catch (Exception ex)
				{
					this.luaState.LuaSetTop(newTop);
					throw ex;
				}
				return result;
			}
			set
			{
				int newTop = this.luaState.LuaGetTop();
				try
				{
					this.luaState.Push(this);
					this.luaState.Push(key);
					this.luaState.Push(value);
					this.luaState.LuaSetTable(-3);
					this.luaState.LuaSetTop(newTop);
				}
				catch (Exception ex)
				{
					this.luaState.LuaSetTop(newTop);
					throw ex;
				}
			}
		}

		public object this[int key]
		{
			get
			{
				if (key < 1)
				{
					throw new LuaException(string.Format("array index out of bounds: {0}", key), null, 1);
				}
				int newTop = this.luaState.LuaGetTop();
				object result;
				try
				{
					this.luaState.Push(this);
					this.luaState.LuaRawGetI(-1, key);
					object obj = this.luaState.ToVariant(-1);
					this.luaState.LuaSetTop(newTop);
					result = obj;
				}
				catch (Exception ex)
				{
					this.luaState.LuaSetTop(newTop);
					throw ex;
				}
				return result;
			}
			set
			{
				if (key < 1)
				{
					throw new LuaException("array index out of bounds: " + key, null, 1);
				}
				int newTop = this.luaState.LuaGetTop();
				try
				{
					this.luaState.Push(this);
					this.luaState.Push(value);
					this.luaState.LuaRawSetI(-2, key);
					this.luaState.LuaSetTop(newTop);
				}
				catch (Exception ex)
				{
					this.luaState.LuaSetTop(newTop);
					throw ex;
				}
			}
		}

		public int Length
		{
			get
			{
				this.luaState.Push(this);
				int result = this.luaState.LuaObjLen(-1);
				this.luaState.LuaPop(1);
				return result;
			}
		}

		public LuaTable(int reference, LuaState state)
		{
			this.reference = reference;
			this.luaState = state;
		}

		public LuaFunction RawGetLuaFunction(string key)
		{
			int newTop = this.luaState.LuaGetTop();
			LuaFunction result;
			try
			{
				this.luaState.Push(this);
				this.luaState.Push(key);
				this.luaState.LuaRawGet(-2);
				LuaFunction luaFunction = this.luaState.CheckLuaFunction(-1);
				this.luaState.LuaSetTop(newTop);
				if (luaFunction != null)
				{
					luaFunction.name = this.name + "." + key;
				}
				result = luaFunction;
			}
			catch (Exception ex)
			{
				this.luaState.LuaSetTop(newTop);
				throw ex;
			}
			return result;
		}

		public LuaFunction GetLuaFunction(string key)
		{
			int newTop = this.luaState.LuaGetTop();
			LuaFunction result;
			try
			{
				this.luaState.Push(this);
				this.luaState.Push(key);
				this.luaState.LuaGetTable(-2);
				LuaFunction luaFunction = this.luaState.CheckLuaFunction(-1);
				this.luaState.LuaSetTop(newTop);
				if (luaFunction != null)
				{
					luaFunction.name = this.name + "." + key;
				}
				result = luaFunction;
			}
			catch (Exception ex)
			{
				this.luaState.LuaSetTop(newTop);
				throw ex;
			}
			return result;
		}

		public string GetStringField(string name)
		{
			int newTop = this.luaState.LuaGetTop();
			string result;
			try
			{
				this.luaState.Push(this);
				this.luaState.LuaGetField(-1, name);
				string text = this.luaState.CheckString(-1);
				this.luaState.LuaSetTop(newTop);
				result = text;
			}
			catch (LuaException ex)
			{
				this.luaState.LuaSetTop(newTop);
				throw ex;
			}
			return result;
		}

		public void AddTable(string name)
		{
			int newTop = this.luaState.LuaGetTop();
			try
			{
				this.luaState.Push(this);
				this.luaState.Push(name);
				this.luaState.LuaCreateTable(0, 0);
				this.luaState.LuaRawSet(-3);
				this.luaState.LuaSetTop(newTop);
			}
			catch (Exception ex)
			{
				this.luaState.LuaSetTop(newTop);
				throw ex;
			}
		}

		public object[] ToArray()
		{
			int newTop = this.luaState.LuaGetTop();
			object[] result;
			try
			{
				this.luaState.Push(this);
				int num = this.luaState.LuaObjLen(-1);
				List<object> list = new List<object>(num + 1);
				int i = 1;
				while (i <= num)
				{
					this.luaState.LuaRawGetI(-1, i++);
					object obj = this.luaState.ToVariant(-1);
					this.luaState.LuaPop(1);
					list.Add(obj);
				}
				this.luaState.LuaSetTop(newTop);
				result = list.ToArray();
			}
			catch (Exception ex)
			{
				this.luaState.LuaSetTop(newTop);
				throw ex;
			}
			return result;
		}

		public override string ToString()
		{
			this.luaState.Push(this);
			IntPtr intPtr = this.luaState.LuaToPointer(-1);
			this.luaState.LuaPop(1);
			return string.Format("table:0x{0}", intPtr.ToString("X"));
		}

		public LuaArrayTable ToArrayTable()
		{
			return new LuaArrayTable(this);
		}

		public LuaDictTable ToDictTable()
		{
			return new LuaDictTable(this);
		}

		public LuaTable GetMetaTable()
		{
			int newTop = this.luaState.LuaGetTop();
			LuaTable result;
			try
			{
				LuaTable luaTable = null;
				this.luaState.Push(this);
				if (this.luaState.LuaGetMetaTable(-1) != 0)
				{
					luaTable = this.luaState.CheckLuaTable(-1);
				}
				this.luaState.LuaSetTop(newTop);
				result = luaTable;
			}
			catch (Exception ex)
			{
				this.luaState.LuaSetTop(newTop);
				throw ex;
			}
			return result;
		}
	}
}
