using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LuaInterface
{
	public struct Lua_Debug
	{
		public int eventcode;

		public IntPtr _name;

		public IntPtr _namewhat;

		public IntPtr _what;

		public IntPtr _source;

		public int currentline;

		public int nups;

		public int linedefined;

		public int lastlinedefined;

		[MarshalAs(30, SizeConst = 128)]
		public byte[] _short_src;

		public int i_ci;

		public string namewhat
		{
			get
			{
				return this.tostring(this._namewhat);
			}
		}

		public string name
		{
			get
			{
				return this.tostring(this._name);
			}
		}

		public string what
		{
			get
			{
				return this.tostring(this._what);
			}
		}

		public string source
		{
			get
			{
				return this.tostring(this._source);
			}
		}

		public string short_src
		{
			get
			{
				if (this._short_src == null)
				{
					return string.Empty;
				}
				int shortSrcLen = this.GetShortSrcLen(this._short_src);
				return Encoding.get_UTF8().GetString(this._short_src, 0, shortSrcLen);
			}
		}

		private string tostring(IntPtr p)
		{
			if (p != IntPtr.Zero)
			{
				int len = LuaDLL.tolua_strlen(p);
				return LuaDLL.lua_ptrtostring(p, len);
			}
			return string.Empty;
		}

		private int GetShortSrcLen(byte[] str)
		{
			int i;
			for (i = 0; i < 128; i++)
			{
				if (str[i] == 0)
				{
					return i;
				}
			}
			return i;
		}
	}
}
