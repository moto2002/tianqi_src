using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaInterface
{
	public class LuaFunction : LuaBaseRef
	{
		protected class FuncData
		{
			public int oldTop = -1;

			public int stackPos = -1;

			public FuncData(int top, int stack)
			{
				this.oldTop = top;
				this.stackPos = stack;
			}
		}

		protected int oldTop = -1;

		private int argCount;

		private int stackPos = -1;

		private Stack<LuaFunction.FuncData> stack = new Stack<LuaFunction.FuncData>();

		public LuaFunction(int reference, LuaState state)
		{
			this.reference = reference;
			this.luaState = state;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		public virtual int BeginPCall()
		{
			if (this.luaState == null)
			{
				throw new LuaException("LuaFunction has been disposed", null, 1);
			}
			this.stack.Push(new LuaFunction.FuncData(this.oldTop, this.stackPos));
			this.oldTop = this.luaState.BeginPCall(this.reference);
			this.stackPos = -1;
			this.argCount = 0;
			return this.oldTop;
		}

		public void PCall()
		{
			this.stackPos = this.oldTop + 1;
			try
			{
				this.luaState.PCall(this.argCount, this.oldTop);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
		}

		public void EndPCall()
		{
			if (this.oldTop != -1)
			{
				this.luaState.EndPCall(this.oldTop);
				this.argCount = 0;
				LuaFunction.FuncData funcData = this.stack.Pop();
				this.oldTop = funcData.oldTop;
				this.stackPos = funcData.stackPos;
			}
		}

		public void Call()
		{
			this.BeginPCall();
			this.PCall();
			this.EndPCall();
		}

		public object[] Call(params object[] args)
		{
			this.BeginPCall();
			int num = (args != null) ? args.Length : 0;
			if (!this.luaState.LuaCheckStack(num + 6))
			{
				this.EndPCall();
				throw new LuaException("stack overflow", null, 1);
			}
			this.PushArgs(args);
			this.PCall();
			object[] result = this.luaState.CheckObjects(this.oldTop);
			this.EndPCall();
			return result;
		}

		public bool IsBegin()
		{
			return this.oldTop != -1;
		}

		public void Push(double num)
		{
			this.luaState.Push(num);
			this.argCount++;
		}

		public void PushInt64(LuaInteger64 n64)
		{
			this.luaState.PushInt64(n64);
			this.argCount++;
		}

		public void Push(bool b)
		{
			this.luaState.Push(b);
			this.argCount++;
		}

		public void Push(string str)
		{
			this.luaState.Push(str);
			this.argCount++;
		}

		public void Push(IntPtr ptr)
		{
			this.luaState.Push(ptr);
			this.argCount++;
		}

		public void Push(LuaBaseRef lbr)
		{
			this.luaState.Push(lbr);
			this.argCount++;
		}

		public void Push(object o)
		{
			this.luaState.Push(o);
			this.argCount++;
		}

		public void Push(Object o)
		{
			this.luaState.Push(o);
			this.argCount++;
		}

		public void Push(Type t)
		{
			this.luaState.Push(t);
			this.argCount++;
		}

		public void Push(Enum e)
		{
			this.luaState.Push(e);
			this.argCount++;
		}

		public void Push(Array array)
		{
			this.luaState.Push(array);
			this.argCount++;
		}

		public void Push(Vector3 v3)
		{
			this.luaState.Push(v3);
			this.argCount++;
		}

		public void Push(Vector2 v2)
		{
			this.luaState.Push(v2);
			this.argCount++;
		}

		public void Push(Vector4 v4)
		{
			this.luaState.Push(v4);
			this.argCount++;
		}

		public void Push(Quaternion quat)
		{
			this.luaState.Push(quat);
			this.argCount++;
		}

		public void Push(Color clr)
		{
			this.luaState.Push(clr);
			this.argCount++;
		}

		public void PushLayerMask(LayerMask mask)
		{
			this.luaState.PushLayerMask(mask);
			this.argCount++;
		}

		public void Push(Ray ray)
		{
			try
			{
				this.luaState.Push(ray);
				this.argCount++;
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
		}

		public void Push(Bounds bounds)
		{
			try
			{
				this.luaState.Push(bounds);
				this.argCount++;
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
		}

		public void Push(RaycastHit hit)
		{
			try
			{
				this.luaState.Push(hit);
				this.argCount++;
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
		}

		public void Push(Touch t)
		{
			try
			{
				this.luaState.Push(t);
				this.argCount++;
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
		}

		public void Push(LuaByteBuffer buffer)
		{
			this.luaState.Push(buffer);
			this.argCount++;
		}

		public void PushValue(ValueType value)
		{
			this.luaState.PushValue(value);
			this.argCount++;
		}

		public void PushObject(object o)
		{
			this.luaState.PushObject(o);
			this.argCount++;
		}

		public void PushArgs(object[] args)
		{
			if (args == null)
			{
				return;
			}
			this.argCount += args.Length;
			this.luaState.PushArgs(args);
		}

		public void PushByteBuffer(byte[] buffer)
		{
			this.luaState.PushByteBuffer(buffer);
			this.argCount++;
		}

		public double CheckNumber()
		{
			double result;
			try
			{
				result = this.luaState.CheckNumber(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public bool CheckBoolean()
		{
			bool result;
			try
			{
				result = this.luaState.CheckBoolean(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public string CheckString()
		{
			string result;
			try
			{
				result = this.luaState.CheckString(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public Vector3 CheckVector3()
		{
			Vector3 result;
			try
			{
				result = this.luaState.CheckVector3(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public Quaternion CheckQuaternion()
		{
			Quaternion result;
			try
			{
				result = this.luaState.CheckQuaternion(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public Vector2 CheckVector2()
		{
			Vector2 result;
			try
			{
				result = this.luaState.CheckVector2(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public Vector4 CheckVector4()
		{
			Vector4 result;
			try
			{
				result = this.luaState.CheckVector4(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public Color CheckColor()
		{
			Color result;
			try
			{
				result = this.luaState.CheckColor(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public Ray CheckRay()
		{
			Ray result;
			try
			{
				result = this.luaState.CheckRay(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public Bounds CheckBounds()
		{
			Bounds result;
			try
			{
				result = this.luaState.CheckBounds(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public LayerMask CheckLayerMask()
		{
			LayerMask result;
			try
			{
				result = this.luaState.CheckLayerMask(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public LuaInteger64 CheckInteger64()
		{
			LuaInteger64 result;
			try
			{
				result = this.luaState.CheckInteger64(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public Delegate CheckDelegate()
		{
			Delegate result;
			try
			{
				result = this.luaState.CheckDelegate(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public object CheckVariant()
		{
			return this.luaState.ToVariant(this.stackPos++);
		}

		public char[] CheckCharBuffer()
		{
			char[] result;
			try
			{
				result = this.luaState.CheckCharBuffer(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public byte[] CheckByteBuffer()
		{
			byte[] result;
			try
			{
				result = this.luaState.CheckByteBuffer(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public object CheckObject(Type t)
		{
			object result;
			try
			{
				result = this.luaState.CheckObject(this.stackPos++, t);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public LuaFunction CheckLuaFunction()
		{
			LuaFunction result;
			try
			{
				result = this.luaState.CheckLuaFunction(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public LuaTable CheckLuaTable()
		{
			LuaTable result;
			try
			{
				result = this.luaState.CheckLuaTable(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}

		public LuaThread CheckLuaThread()
		{
			LuaThread result;
			try
			{
				result = this.luaState.CheckLuaThread(this.stackPos++);
			}
			catch (Exception ex)
			{
				this.EndPCall();
				throw ex;
			}
			return result;
		}
	}
}
