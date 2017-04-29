using LuaFramework;
using LuaInterface;
using System;

public class LuaFramework_ByteBufferWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ByteBuffer), typeof(object), null);
		L.RegFunction("Close", new LuaCSFunction(LuaFramework_ByteBufferWrap.Close));
		L.RegFunction("WriteByte", new LuaCSFunction(LuaFramework_ByteBufferWrap.WriteByte));
		L.RegFunction("WriteInt", new LuaCSFunction(LuaFramework_ByteBufferWrap.WriteInt));
		L.RegFunction("WriteShort", new LuaCSFunction(LuaFramework_ByteBufferWrap.WriteShort));
		L.RegFunction("WriteLong", new LuaCSFunction(LuaFramework_ByteBufferWrap.WriteLong));
		L.RegFunction("WriteFloat", new LuaCSFunction(LuaFramework_ByteBufferWrap.WriteFloat));
		L.RegFunction("WriteDouble", new LuaCSFunction(LuaFramework_ByteBufferWrap.WriteDouble));
		L.RegFunction("WriteString", new LuaCSFunction(LuaFramework_ByteBufferWrap.WriteString));
		L.RegFunction("WriteBytes", new LuaCSFunction(LuaFramework_ByteBufferWrap.WriteBytes));
		L.RegFunction("WriteBuffer", new LuaCSFunction(LuaFramework_ByteBufferWrap.WriteBuffer));
		L.RegFunction("ReadByte", new LuaCSFunction(LuaFramework_ByteBufferWrap.ReadByte));
		L.RegFunction("ReadInt", new LuaCSFunction(LuaFramework_ByteBufferWrap.ReadInt));
		L.RegFunction("ReadShort", new LuaCSFunction(LuaFramework_ByteBufferWrap.ReadShort));
		L.RegFunction("ReadLong", new LuaCSFunction(LuaFramework_ByteBufferWrap.ReadLong));
		L.RegFunction("ReadFloat", new LuaCSFunction(LuaFramework_ByteBufferWrap.ReadFloat));
		L.RegFunction("ReadDouble", new LuaCSFunction(LuaFramework_ByteBufferWrap.ReadDouble));
		L.RegFunction("ReadString", new LuaCSFunction(LuaFramework_ByteBufferWrap.ReadString));
		L.RegFunction("ReadBytes", new LuaCSFunction(LuaFramework_ByteBufferWrap.ReadBytes));
		L.RegFunction("ReadBuffer", new LuaCSFunction(LuaFramework_ByteBufferWrap.ReadBuffer));
		L.RegFunction("ToBytes", new LuaCSFunction(LuaFramework_ByteBufferWrap.ToBytes));
		L.RegFunction("Flush", new LuaCSFunction(LuaFramework_ByteBufferWrap.Flush));
		L.RegFunction("New", new LuaCSFunction(LuaFramework_ByteBufferWrap._CreateLuaFramework_ByteBuffer));
		L.RegFunction("__tostring", new LuaCSFunction(LuaFramework_ByteBufferWrap.Lua_ToString));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateLuaFramework_ByteBuffer(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 0)
			{
				ByteBuffer o = new ByteBuffer();
				ToLua.PushObject(L, o);
				result = 1;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(byte[])))
			{
				byte[] data = ToLua.CheckByteBuffer(L, 1);
				ByteBuffer o2 = new ByteBuffer(data);
				ToLua.PushObject(L, o2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: LuaFramework.ByteBuffer.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Close(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			byteBuffer.Close();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteByte(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			byte v = (byte)LuaDLL.luaL_checknumber(L, 2);
			byteBuffer.WriteByte(v);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteInt(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			int v = (int)LuaDLL.luaL_checknumber(L, 2);
			byteBuffer.WriteInt(v);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteShort(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			ushort v = (ushort)LuaDLL.luaL_checknumber(L, 2);
			byteBuffer.WriteShort(v);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteLong(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			long v = (long)LuaDLL.luaL_checknumber(L, 2);
			byteBuffer.WriteLong(v);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteFloat(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			float v = (float)LuaDLL.luaL_checknumber(L, 2);
			byteBuffer.WriteFloat(v);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteDouble(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			double v = LuaDLL.luaL_checknumber(L, 2);
			byteBuffer.WriteDouble(v);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteString(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			string v = ToLua.CheckString(L, 2);
			byteBuffer.WriteString(v);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteBytes(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			byte[] v = ToLua.CheckByteBuffer(L, 2);
			byteBuffer.WriteBytes(v);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WriteBuffer(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			LuaByteBuffer strBuffer = new LuaByteBuffer(ToLua.CheckByteBuffer(L, 2));
			byteBuffer.WriteBuffer(strBuffer);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadByte(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			byte b = byteBuffer.ReadByte();
			LuaDLL.lua_pushnumber(L, (double)b);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadInt(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			int n = byteBuffer.ReadInt();
			LuaDLL.lua_pushinteger(L, n);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadShort(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			ushort num = byteBuffer.ReadShort();
			LuaDLL.lua_pushnumber(L, (double)num);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadLong(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			long num = byteBuffer.ReadLong();
			LuaDLL.lua_pushnumber(L, (double)num);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadFloat(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			float num = byteBuffer.ReadFloat();
			LuaDLL.lua_pushnumber(L, (double)num);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadDouble(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			double number = byteBuffer.ReadDouble();
			LuaDLL.lua_pushnumber(L, number);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadString(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			string str = byteBuffer.ReadString();
			LuaDLL.lua_pushstring(L, str);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadBytes(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			byte[] array = byteBuffer.ReadBytes();
			ToLua.Push(L, array);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReadBuffer(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			LuaByteBuffer bb = byteBuffer.ReadBuffer();
			ToLua.Push(L, bb);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToBytes(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			byte[] array = byteBuffer.ToBytes();
			ToLua.Push(L, array);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Flush(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ByteBuffer byteBuffer = (ByteBuffer)ToLua.CheckObject(L, 1, typeof(ByteBuffer));
			byteBuffer.Flush();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_ToString(IntPtr L)
	{
		object obj = ToLua.ToObject(L, 1);
		if (obj != null)
		{
			LuaDLL.lua_pushstring(L, obj.ToString());
		}
		else
		{
			LuaDLL.lua_pushnil(L);
		}
		return 1;
	}
}
