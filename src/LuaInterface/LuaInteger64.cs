using System;

namespace LuaInterface
{
	[NoToLua]
	public struct LuaInteger64
	{
		public long i64;

		public LuaInteger64(long i64)
		{
			this.i64 = i64;
		}

		public ulong ToUInt64()
		{
			return (ulong)this.i64;
		}

		public override string ToString()
		{
			return Convert.ToString(this.i64);
		}

		public static implicit operator LuaInteger64(long i64)
		{
			return new LuaInteger64(i64);
		}

		public static implicit operator long(LuaInteger64 self)
		{
			return self.i64;
		}
	}
}
