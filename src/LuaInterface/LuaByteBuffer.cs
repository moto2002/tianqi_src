using System;
using System.Runtime.InteropServices;

namespace LuaInterface
{
	public class LuaByteBuffer
	{
		public byte[] buffer;

		public LuaByteBuffer(IntPtr source, int len)
		{
			this.buffer = new byte[len];
			Marshal.Copy(source, this.buffer, 0, len);
		}

		public LuaByteBuffer(byte[] buf)
		{
			this.buffer = buf;
		}

		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			LuaByteBuffer luaByteBuffer = o as LuaByteBuffer;
			return luaByteBuffer != null && luaByteBuffer.buffer == this.buffer;
		}

		public override int GetHashCode()
		{
			return this.buffer.GetHashCode();
		}

		public static bool operator ==(LuaByteBuffer a, LuaByteBuffer b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (a == null && b != null)
			{
				return b.buffer == null;
			}
			if (a != null && b == null)
			{
				return a.buffer == null;
			}
			return a.buffer == b.buffer;
		}

		public static bool operator !=(LuaByteBuffer a, LuaByteBuffer b)
		{
			return !(a == b);
		}
	}
}
