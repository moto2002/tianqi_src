using System;

namespace LuaInterface
{
	public class LuaThread : LuaBaseRef
	{
		private object[] objs;

		public LuaThread(int reference, LuaState state)
		{
			this.luaState = state;
			this.reference = reference;
		}

		public int Resume(params object[] args)
		{
			this.objs = null;
			this.luaState.Push(this);
			IntPtr intPtr = this.luaState.LuaToThread(-1);
			this.luaState.LuaPop(1);
			int narg = 0;
			if (args != null)
			{
				narg = args.Length;
				for (int i = 0; i < args.Length; i++)
				{
					ToLua.Push(intPtr, args[i]);
				}
			}
			int num = LuaDLL.lua_resume(intPtr, narg);
			int num2;
			if (num > 1)
			{
				num2 = LuaDLL.lua_gettop(intPtr);
				LuaDLL.tolua_pushtraceback(intPtr);
				LuaDLL.lua_pushthread(intPtr);
				LuaDLL.lua_pushvalue(intPtr, -3);
				if (LuaDLL.lua_pcall(intPtr, 2, -1, 0) != 0)
				{
					LuaDLL.lua_settop(intPtr, num2);
				}
				string msg = LuaDLL.lua_tostring(intPtr, -1);
				LuaDLL.lua_settop(intPtr, 0);
				throw new LuaException(msg, null, 1);
			}
			num2 = LuaDLL.lua_gettop(intPtr);
			if (num2 > 0)
			{
				this.objs = new object[num2];
				for (int j = 0; j < num2; j++)
				{
					this.objs[j] = ToLua.ToVarObject(intPtr, j + 1);
				}
			}
			if (num == 0)
			{
				this.Dispose();
			}
			return num;
		}

		public object[] GetResult()
		{
			return this.objs;
		}
	}
}
