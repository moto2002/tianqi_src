using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace LuaInterface
{
	public class LuaState : IDisposable
	{
		public ObjectTranslator translator = new ObjectTranslator();

		protected IntPtr L;

		private Dictionary<string, WeakReference> funcMap = new Dictionary<string, WeakReference>();

		private Dictionary<int, WeakReference> funcRefMap = new Dictionary<int, WeakReference>();

		private List<GCRef> gcList = new List<GCRef>();

		private List<LuaFunction> subList = new List<LuaFunction>();

		private Dictionary<Type, int> metaMap = new Dictionary<Type, int>();

		private Dictionary<Enum, object> enumMap = new Dictionary<Enum, object>();

		private Dictionary<Type, LuaCSFunction> preLoadMap = new Dictionary<Type, LuaCSFunction>();

		private Dictionary<int, Type> typeMap = new Dictionary<int, Type>();

		private static LuaState mainState = null;

		private static Dictionary<IntPtr, LuaState> stateMap = new Dictionary<IntPtr, LuaState>();

		private int beginCount;

		private bool beLogGC;

		public int ArrayMetatable
		{
			get;
			private set;
		}

		public int DelegateMetatable
		{
			get;
			private set;
		}

		public int TypeMetatable
		{
			get;
			private set;
		}

		public int EnumMetatable
		{
			get;
			private set;
		}

		public int IterMetatable
		{
			get;
			private set;
		}

		public int OutMetatable
		{
			get;
			private set;
		}

		public int EventMetatable
		{
			get;
			private set;
		}

		public int PackBounds
		{
			get;
			private set;
		}

		public int UnpackBounds
		{
			get;
			private set;
		}

		public int PackRay
		{
			get;
			private set;
		}

		public int UnpackRay
		{
			get;
			private set;
		}

		public int PackRaycastHit
		{
			get;
			private set;
		}

		public int PackTouch
		{
			get;
			private set;
		}

		public bool LogGC
		{
			get
			{
				return this.beLogGC;
			}
			set
			{
				this.beLogGC = value;
				this.translator.LogGC = value;
			}
		}

		public object this[string fullPath]
		{
			get
			{
				int top = LuaDLL.lua_gettop(this.L);
				int num = fullPath.LastIndexOf('.');
				object result;
				if (num > 0)
				{
					string fullPath2 = fullPath.Substring(0, num);
					if (!this.PushLuaTable(fullPath2, true))
					{
						LuaDLL.lua_settop(this.L, top);
						return null;
					}
					string str = fullPath.Substring(num + 1);
					LuaDLL.lua_pushstring(this.L, str);
					LuaDLL.lua_rawget(this.L, -2);
					result = this.ToVariant(-1);
				}
				else
				{
					LuaDLL.lua_getglobal(this.L, fullPath);
					result = this.ToVariant(-1);
				}
				LuaDLL.lua_settop(this.L, top);
				return result;
			}
			set
			{
				int top = LuaDLL.lua_gettop(this.L);
				int num = fullPath.LastIndexOf('.');
				if (num > 0)
				{
					string fname = fullPath.Substring(0, num);
					IntPtr intPtr = LuaDLL.luaL_findtable(this.L, LuaIndexes.LUA_GLOBALSINDEX, fname, 1);
					if (!(intPtr == IntPtr.Zero))
					{
						LuaDLL.lua_settop(this.L, top);
						int len = LuaDLL.tolua_strlen(intPtr);
						string text = LuaDLL.lua_ptrtostring(intPtr, len);
						throw new LuaException(string.Format("{0} not a Lua table", text), null, 1);
					}
					string str = fullPath.Substring(num + 1);
					LuaDLL.lua_pushstring(this.L, str);
					this.Push(value);
					LuaDLL.lua_settable(this.L, -3);
				}
				else
				{
					this.Push(value);
					LuaDLL.lua_setglobal(this.L, fullPath);
				}
				LuaDLL.lua_settop(this.L, top);
			}
		}

		public LuaState()
		{
			if (LuaState.mainState == null)
			{
				LuaState.mainState = this;
			}
			LuaException.Init();
			this.L = LuaDLL.luaL_newstate();
			LuaDLL.tolua_openlibs(this.L);
			LuaStatic.OpenLibs(this.L);
			LuaState.stateMap.Add(this.L, this);
			this.OpenBaseLibs();
			LuaDLL.lua_settop(this.L, 0);
		}

		private void OpenBaseLibs()
		{
			this.BeginModule(null);
			this.BeginModule("System");
			System_ObjectWrap.Register(this);
			System_NullObjectWrap.Register(this);
			System_StringWrap.Register(this);
			System_DelegateWrap.Register(this);
			System_EnumWrap.Register(this);
			System_ArrayWrap.Register(this);
			System_TypeWrap.Register(this);
			LuaInterface_LuaOutWrap.Register(this);
			LuaInterface_EventObjectWrap.Register(this);
			this.BeginModule("Collections");
			System_Collections_IEnumeratorWrap.Register(this);
			this.EndModule();
			this.EndModule();
			this.BeginModule("UnityEngine");
			UnityEngine_ObjectWrap.Register(this);
			this.EndModule();
			this.EndModule();
			ToLua.OpenLibs(this.L);
			LuaUnityLibs.OpenLibs(this.L);
			this.ArrayMetatable = this.metaMap.get_Item(typeof(Array));
			this.TypeMetatable = this.metaMap.get_Item(typeof(Type));
			this.DelegateMetatable = this.metaMap.get_Item(typeof(Delegate));
			this.EnumMetatable = this.metaMap.get_Item(typeof(Enum));
			this.IterMetatable = this.metaMap.get_Item(typeof(IEnumerator));
			this.EventMetatable = this.metaMap.get_Item(typeof(EventObject));
		}

		private void OpenBaseLuaLibs()
		{
			this.DoFile("tolua.lua");
			LuaUnityLibs.OpenLuaLibs(this.L);
		}

		public void Start()
		{
			Debugger.Log("LuaState start");
			this.OpenBaseLuaLibs();
			this.PackBounds = this.GetFuncRef("Bounds.New");
			this.UnpackBounds = this.GetFuncRef("Bounds.Get");
			this.PackRay = this.GetFuncRef("Ray.New");
			this.UnpackRay = this.GetFuncRef("Ray.Get");
			this.PackRaycastHit = this.GetFuncRef("RaycastHit.New");
			this.PackTouch = this.GetFuncRef("Touch.New");
		}

		public int OpenLibs(LuaCSFunction open)
		{
			return open(this.L);
		}

		public void BeginPreLoad()
		{
			this.LuaGetGlobal("package");
			this.LuaGetField(-1, "preload");
		}

		public void AddPreLoad(string name, LuaCSFunction func, Type type)
		{
			if (!this.preLoadMap.ContainsKey(type))
			{
				LuaDLL.tolua_pushcfunction(this.L, func);
				LuaDLL.lua_setfield(this.L, -2, name);
				this.preLoadMap.set_Item(type, func);
			}
		}

		public void EndPreLoad()
		{
			this.LuaPop(2);
		}

		public int BeginPreModule(string name)
		{
			int result = LuaDLL.lua_gettop(this.L);
			if (LuaDLL.tolua_createtable(this.L, name, 0))
			{
				this.beginCount++;
				return result;
			}
			throw new LuaException(string.Format("create table {0} fail", name), null, 1);
		}

		public void EndPreModule(int top)
		{
			this.beginCount--;
			LuaDLL.lua_settop(this.L, top);
		}

		public void BindPreModule(Type t, LuaCSFunction func)
		{
			this.preLoadMap.set_Item(t, func);
		}

		public LuaCSFunction GetPreModule(Type t)
		{
			LuaCSFunction result = null;
			this.preLoadMap.TryGetValue(t, ref result);
			return result;
		}

		public bool BeginModule(string name)
		{
			if (LuaDLL.tolua_beginmodule(this.L, name))
			{
				this.beginCount++;
				return true;
			}
			LuaDLL.lua_settop(this.L, 0);
			throw new LuaException(string.Format("create table {0} fail", name), null, 1);
		}

		public void EndModule()
		{
			this.beginCount--;
			LuaDLL.lua_pop(this.L, 1);
		}

		private void BindTypeRef(int reference, Type t)
		{
			this.metaMap.Add(t, reference);
			this.typeMap.Add(reference, t);
		}

		public Type GetClassType(int reference)
		{
			Type result = null;
			this.typeMap.TryGetValue(reference, ref result);
			return result;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int Collect(IntPtr L)
		{
			int num = LuaDLL.tolua_rawnetobj(L, 1);
			if (num != -1)
			{
				ObjectTranslator objectTranslator = LuaState.GetTranslator(L);
				objectTranslator.RemoveObject(num);
			}
			return 0;
		}

		public int BeginClass(Type t, Type baseType, string name = null)
		{
			if (this.beginCount == 0)
			{
				throw new LuaException("must call BeginModule first", null, 1);
			}
			int num = 0;
			int num2 = 0;
			if (name == null)
			{
				name = t.get_Name();
			}
			if (baseType != null && !this.metaMap.TryGetValue(baseType, ref num))
			{
				LuaDLL.lua_createtable(this.L, 0, 0);
				num = LuaDLL.luaL_ref(this.L, LuaIndexes.LUA_REGISTRYINDEX);
				this.BindTypeRef(num, baseType);
			}
			if (this.metaMap.TryGetValue(t, ref num2))
			{
				LuaDLL.tolua_beginclass(this.L, name, num, num2);
				this.RegFunction("__gc", new LuaCSFunction(LuaState.Collect));
			}
			else
			{
				num2 = LuaDLL.tolua_beginclass(this.L, name, num, 0);
				this.RegFunction("__gc", new LuaCSFunction(LuaState.Collect));
				this.BindTypeRef(num2, t);
			}
			return num2;
		}

		public void EndClass()
		{
			LuaDLL.tolua_endclass(this.L);
		}

		public int BeginEnum(Type t)
		{
			if (this.beginCount == 0)
			{
				throw new LuaException("must call BeginModule first", null, 1);
			}
			int num = LuaDLL.tolua_beginenum(this.L, t.get_Name());
			this.RegFunction("__gc", new LuaCSFunction(LuaState.Collect));
			this.BindTypeRef(num, t);
			return num;
		}

		public void EndEnum()
		{
			LuaDLL.tolua_endenum(this.L);
		}

		public void BeginStaticLibs(string name)
		{
			if (this.beginCount == 0)
			{
				throw new LuaException("must call BeginModule first", null, 1);
			}
			LuaDLL.tolua_beginstaticclass(this.L, name);
		}

		public void EndStaticLibs()
		{
			LuaDLL.tolua_endstaticclass(this.L);
		}

		public void RegFunction(string name, LuaCSFunction func)
		{
			IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(func);
			LuaDLL.tolua_function(this.L, name, functionPointerForDelegate);
		}

		public void RegVar(string name, LuaCSFunction get, LuaCSFunction set)
		{
			IntPtr get2 = IntPtr.Zero;
			IntPtr set2 = IntPtr.Zero;
			if (get != null)
			{
				get2 = Marshal.GetFunctionPointerForDelegate(get);
			}
			if (set != null)
			{
				set2 = Marshal.GetFunctionPointerForDelegate(set);
			}
			LuaDLL.tolua_variable(this.L, name, get2, set2);
		}

		public void RegConstant(string name, double d)
		{
			LuaDLL.tolua_constant(this.L, name, d);
		}

		private int GetFuncRef(string name)
		{
			if (this.PushLuaFunction(name, false))
			{
				return LuaDLL.luaL_ref(this.L, LuaIndexes.LUA_REGISTRYINDEX);
			}
			throw new LuaException("get lua function reference failed: " + name, null, 1);
		}

		public static LuaState Get(IntPtr ptr)
		{
			return LuaState.mainState;
		}

		public static ObjectTranslator GetTranslator(IntPtr ptr)
		{
			return LuaState.mainState.translator;
		}

		public object[] DoString(string chunk, string chunkName = "LuaState.DoString")
		{
			byte[] bytes = Encoding.get_UTF8().GetBytes(chunk);
			return this.LuaLoadBuffer(bytes, chunkName);
		}

		public object[] DoFile(string fileName)
		{
			if (!Path.HasExtension(fileName))
			{
				fileName += ".lua";
			}
			byte[] array = LuaFileUtils.Instance.ReadFile(fileName);
			if (array == null)
			{
				throw new LuaException(string.Format("Load lua file failed: {0}", fileName), null, 1);
			}
			return this.LuaLoadBuffer(array, fileName);
		}

		public void Require(string fileName)
		{
			int top = LuaDLL.lua_gettop(this.L);
			string text = null;
			if (this.LuaRequire(fileName) != 0)
			{
				text = LuaDLL.lua_tostring(this.L, -1);
			}
			LuaDLL.lua_settop(this.L, top);
			if (text != null)
			{
				throw new LuaException(text, null, 1);
			}
		}

		public void AddSearchPath(string fullPath)
		{
			if (!Directory.Exists(fullPath))
			{
				string msg = string.Format("Lua config path not exists: {0}", fullPath);
				throw new LuaException(msg, null, 1);
			}
			if (!Path.IsPathRooted(fullPath))
			{
				throw new LuaException(fullPath + " is not a full path", null, 1);
			}
			fullPath.Replace('\\', '/');
			if (LuaFileUtils.Instance.AddSearchPath(fullPath, false))
			{
				LuaDLL.lua_getglobal(this.L, "package");
				LuaDLL.lua_getfield(this.L, -1, "path");
				string text = LuaDLL.lua_tostring(this.L, -1);
				string str = string.Format("{0};{1}/?.lua", text, fullPath);
				LuaDLL.lua_pushstring(this.L, str);
				LuaDLL.lua_setfield(this.L, -3, "path");
				LuaDLL.lua_pop(this.L, 2);
			}
		}

		public void RemoveSeachPath(string fullPath)
		{
			if (!Path.IsPathRooted(fullPath))
			{
				throw new LuaException(fullPath + " is not a full path", null, 1);
			}
			fullPath.Replace('\\', '/');
			LuaFileUtils.Instance.RemoveSearchPath(fullPath);
			LuaDLL.lua_getglobal(this.L, "package");
			LuaDLL.lua_getfield(this.L, -1, "path");
			string text = LuaDLL.lua_tostring(this.L, -1);
			text = text.Replace(fullPath + "/?.lua", string.Empty);
			LuaDLL.lua_pushstring(this.L, text);
			LuaDLL.lua_setfield(this.L, -3, "path");
			LuaDLL.lua_pop(this.L, 2);
		}

		public int BeginPCall(int reference)
		{
			return LuaDLL.tolua_beginpcall(this.L, reference);
		}

		public void PCall(int args, int oldTop)
		{
			if (LuaDLL.lua_pcall(this.L, args, LuaDLL.LUA_MULTRET, oldTop) != 0)
			{
				string msg = LuaDLL.lua_tostring(this.L, -1);
				Exception luaStack = LuaException.luaStack;
				LuaException.luaStack = null;
				throw new LuaException(msg, luaStack, 1);
			}
		}

		public void EndPCall(int oldTop)
		{
			LuaDLL.lua_settop(this.L, oldTop - 1);
		}

		public void PushArgs(object[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				this.Push(args[i]);
			}
		}

		private bool PushLuaTable(string fullPath, bool checkMap = true)
		{
			if (checkMap)
			{
				WeakReference weakReference = null;
				if (this.funcMap.TryGetValue(fullPath, ref weakReference))
				{
					if (weakReference.get_IsAlive())
					{
						LuaTable lbr = weakReference.get_Target() as LuaTable;
						this.Push(lbr);
						return true;
					}
					this.funcMap.Remove(fullPath);
				}
			}
			return LuaDLL.tolua_pushluatable(this.L, fullPath);
		}

		private bool PushLuaFunction(string fullPath, bool checkMap = true)
		{
			if (checkMap)
			{
				WeakReference weakReference = null;
				if (this.funcMap.TryGetValue(fullPath, ref weakReference))
				{
					if (weakReference.get_IsAlive())
					{
						LuaFunction luaFunction = weakReference.get_Target() as LuaFunction;
						if (luaFunction.IsAlive())
						{
							luaFunction.AddRef();
							return true;
						}
					}
					this.funcMap.Remove(fullPath);
				}
			}
			int num = LuaDLL.lua_gettop(this.L);
			int num2 = fullPath.LastIndexOf('.');
			if (num2 > 0)
			{
				string fullPath2 = fullPath.Substring(0, num2);
				if (this.PushLuaTable(fullPath2, true))
				{
					string str = fullPath.Substring(num2 + 1);
					LuaDLL.lua_pushstring(this.L, str);
					LuaDLL.lua_rawget(this.L, -2);
					LuaTypes luaTypes = LuaDLL.lua_type(this.L, -1);
					if (luaTypes == LuaTypes.LUA_TFUNCTION)
					{
						LuaDLL.lua_insert(this.L, num + 1);
						LuaDLL.lua_settop(this.L, num + 1);
						return true;
					}
				}
				LuaDLL.lua_settop(this.L, num);
				return false;
			}
			LuaDLL.lua_getglobal(this.L, fullPath);
			LuaTypes luaTypes2 = LuaDLL.lua_type(this.L, -1);
			if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
			{
				LuaDLL.lua_settop(this.L, num);
				return false;
			}
			return true;
		}

		private void RemoveFromGCList(int reference)
		{
			List<GCRef> list = this.gcList;
			lock (list)
			{
				int num = this.gcList.FindIndex((GCRef gc) => gc.reference == reference);
				if (num >= 0)
				{
					this.gcList.RemoveAt(num);
				}
			}
		}

		public LuaFunction GetFunction(string name, bool beLogMiss = true)
		{
			WeakReference weakReference = null;
			if (this.funcMap.TryGetValue(name, ref weakReference))
			{
				if (weakReference.get_IsAlive())
				{
					LuaFunction luaFunction = weakReference.get_Target() as LuaFunction;
					if (luaFunction.IsAlive())
					{
						luaFunction.AddRef();
						this.RemoveFromGCList(luaFunction.GetReference());
						return luaFunction;
					}
				}
				this.funcMap.Remove(name);
			}
			if (this.PushLuaFunction(name, false))
			{
				int num = LuaDLL.toluaL_ref(this.L);
				if (this.funcRefMap.TryGetValue(num, ref weakReference))
				{
					if (weakReference.get_IsAlive())
					{
						LuaFunction luaFunction2 = weakReference.get_Target() as LuaFunction;
						if (luaFunction2.IsAlive())
						{
							this.funcMap.Add(name, weakReference);
							luaFunction2.AddRef();
							this.RemoveFromGCList(num);
							return luaFunction2;
						}
					}
					this.funcRefMap.Remove(num);
				}
				LuaFunction luaFunction3 = new LuaFunction(num, this);
				luaFunction3.name = name;
				this.funcMap.Add(name, new WeakReference(luaFunction3));
				this.funcRefMap.Add(num, new WeakReference(luaFunction3));
				this.RemoveFromGCList(num);
				if (this.LogGC)
				{
					Debugger.Log("Alloc LuaFunction name {0}, id {1}", name, num);
				}
				return luaFunction3;
			}
			if (beLogMiss)
			{
				Debugger.Log("Lua function {0} not exists", name);
			}
			return null;
		}

		private LuaBaseRef TryGetLuaRef(int reference)
		{
			WeakReference weakReference = null;
			if (this.funcRefMap.TryGetValue(reference, ref weakReference))
			{
				if (weakReference.get_IsAlive())
				{
					LuaBaseRef luaBaseRef = (LuaBaseRef)weakReference.get_Target();
					if (luaBaseRef.IsAlive())
					{
						luaBaseRef.AddRef();
						return luaBaseRef;
					}
				}
				this.funcRefMap.Remove(reference);
			}
			return null;
		}

		public LuaFunction GetFunction(int reference)
		{
			LuaFunction luaFunction = this.TryGetLuaRef(reference) as LuaFunction;
			if (luaFunction == null)
			{
				luaFunction = new LuaFunction(reference, this);
				this.funcRefMap.Add(reference, new WeakReference(luaFunction));
				if (this.LogGC)
				{
					Debugger.Log("Alloc LuaFunction name , id {0}", reference);
				}
			}
			this.RemoveFromGCList(reference);
			return luaFunction;
		}

		public LuaTable GetTable(string fullPath, bool beLogMiss = true)
		{
			WeakReference weakReference = null;
			if (this.funcMap.TryGetValue(fullPath, ref weakReference))
			{
				if (weakReference.get_IsAlive())
				{
					LuaTable luaTable = weakReference.get_Target() as LuaTable;
					if (luaTable.IsAlive())
					{
						luaTable.AddRef();
						this.RemoveFromGCList(luaTable.GetReference());
						return luaTable;
					}
				}
				this.funcMap.Remove(fullPath);
			}
			if (this.PushLuaTable(fullPath, false))
			{
				int num = LuaDLL.toluaL_ref(this.L);
				LuaTable luaTable2;
				if (this.funcRefMap.TryGetValue(num, ref weakReference))
				{
					if (weakReference.get_IsAlive())
					{
						luaTable2 = (weakReference.get_Target() as LuaTable);
						if (luaTable2.IsAlive())
						{
							this.funcMap.Add(fullPath, weakReference);
							luaTable2.AddRef();
							this.RemoveFromGCList(num);
							return luaTable2;
						}
					}
					this.funcRefMap.Remove(num);
				}
				luaTable2 = new LuaTable(num, this);
				luaTable2.name = fullPath;
				this.funcMap.Add(fullPath, new WeakReference(luaTable2));
				this.funcRefMap.Add(num, new WeakReference(luaTable2));
				if (this.LogGC)
				{
					Debugger.Log("Alloc LuaTable name {0}, id {1}", fullPath, num);
				}
				this.RemoveFromGCList(num);
				return luaTable2;
			}
			if (beLogMiss)
			{
				Debugger.LogWarning("Lua table {0} not exists", fullPath);
			}
			return null;
		}

		public LuaTable GetTable(int reference)
		{
			LuaTable luaTable = this.TryGetLuaRef(reference) as LuaTable;
			if (luaTable == null)
			{
				luaTable = new LuaTable(reference, this);
				this.funcRefMap.Add(reference, new WeakReference(luaTable));
			}
			this.RemoveFromGCList(reference);
			return luaTable;
		}

		public LuaThread GetLuaThread(int reference)
		{
			LuaThread luaThread = this.TryGetLuaRef(reference) as LuaThread;
			if (luaThread == null)
			{
				luaThread = new LuaThread(reference, this);
				this.funcRefMap.Add(reference, new WeakReference(luaThread));
			}
			this.RemoveFromGCList(reference);
			return luaThread;
		}

		public bool CheckTop()
		{
			int num = LuaDLL.lua_gettop(this.L);
			if (num != 0)
			{
				Debugger.LogWarning("Lua stack top is {0}", num);
				return false;
			}
			return true;
		}

		public void Push(bool b)
		{
			LuaDLL.lua_pushboolean(this.L, b);
		}

		public void Push(double d)
		{
			LuaDLL.lua_pushnumber(this.L, d);
		}

		public void PushInt64(LuaInteger64 n)
		{
			LuaDLL.tolua_pushint64(this.L, n);
		}

		public void Push(string str)
		{
			LuaDLL.lua_pushstring(this.L, str);
		}

		public void Push(IntPtr p)
		{
			LuaDLL.lua_pushlightuserdata(this.L, p);
		}

		public void Push(Vector3 v3)
		{
			LuaDLL.tolua_pushvec3(this.L, v3.x, v3.y, v3.z);
		}

		public void Push(Vector2 v2)
		{
			LuaDLL.tolua_pushvec2(this.L, v2.x, v2.y);
		}

		public void Push(Vector4 v4)
		{
			LuaDLL.tolua_pushvec4(this.L, v4.x, v4.y, v4.z, v4.w);
		}

		public void Push(Color clr)
		{
			LuaDLL.tolua_pushclr(this.L, clr.r, clr.g, clr.b, clr.a);
		}

		public void Push(Quaternion q)
		{
			LuaDLL.tolua_pushquat(this.L, q.x, q.y, q.z, q.w);
		}

		public void Push(Ray ray)
		{
			ToLua.Push(this.L, ray);
		}

		public void Push(Bounds bound)
		{
			ToLua.Push(this.L, bound);
		}

		public void Push(RaycastHit hit)
		{
			ToLua.Push(this.L, hit);
		}

		public void Push(Touch touch)
		{
			ToLua.Push(this.L, touch);
		}

		public void PushLayerMask(LayerMask mask)
		{
			LuaDLL.tolua_pushlayermask(this.L, mask.get_value());
		}

		public void Push(LuaByteBuffer bb)
		{
			LuaDLL.lua_pushlstring(this.L, bb.buffer, bb.buffer.Length);
		}

		public void PushByteBuffer(byte[] buffer)
		{
			LuaDLL.lua_pushlstring(this.L, buffer, buffer.Length);
		}

		public void Push(LuaBaseRef lbr)
		{
			if (lbr == null)
			{
				LuaDLL.lua_pushnil(this.L);
			}
			else
			{
				LuaDLL.lua_getref(this.L, lbr.GetReference());
			}
		}

		private void PushUserData(object o, int reference)
		{
			int index;
			if (this.translator.Getudata(o, out index) && LuaDLL.tolua_pushudata(this.L, index))
			{
				return;
			}
			index = this.translator.AddObject(o);
			LuaDLL.tolua_pushnewudata(this.L, reference, index);
		}

		public void Push(Array array)
		{
			if (array == null)
			{
				LuaDLL.lua_pushnil(this.L);
			}
			else
			{
				this.PushUserData(array, this.ArrayMetatable);
			}
		}

		public void Push(Type t)
		{
			if (t == null)
			{
				LuaDLL.lua_pushnil(this.L);
			}
			else
			{
				this.PushUserData(t, this.TypeMetatable);
			}
		}

		public void Push(Delegate ev)
		{
			if (ev == null)
			{
				LuaDLL.lua_pushnil(this.L);
			}
			else
			{
				this.PushUserData(ev, this.DelegateMetatable);
			}
		}

		public object GetEnumObj(Enum e)
		{
			object obj = null;
			if (!this.enumMap.TryGetValue(e, ref obj))
			{
				obj = e;
				this.enumMap.Add(e, obj);
			}
			return obj;
		}

		public void Push(Enum e)
		{
			if (e == null)
			{
				LuaDLL.lua_pushnil(this.L);
			}
			else
			{
				object enumObj = this.GetEnumObj(e);
				this.PushUserData(enumObj, this.EnumMetatable);
			}
		}

		public void Push(IEnumerator iter)
		{
			ToLua.Push(this.L, iter);
		}

		public void Push(Object obj)
		{
			ToLua.Push(this.L, obj);
		}

		public void Push(TrackedReference tracker)
		{
			ToLua.Push(this.L, tracker);
		}

		public void PushValue(ValueType vt)
		{
			ToLua.PushValue(this.L, vt);
		}

		public void Push(object obj)
		{
			ToLua.Push(this.L, obj);
		}

		public void PushObject(object obj)
		{
			ToLua.PushObject(this.L, obj);
		}

		public double CheckNumber(int stackPos)
		{
			return LuaDLL.luaL_checknumber(this.L, stackPos);
		}

		public bool CheckBoolean(int stackPos)
		{
			return LuaDLL.luaL_checkboolean(this.L, stackPos);
		}

		private Vector3 ToVector3(int stackPos)
		{
			float num;
			float num2;
			float num3;
			LuaDLL.tolua_getvec3(this.L, stackPos, out num, out num2, out num3);
			return new Vector3(num, num2, num3);
		}

		public Vector3 CheckVector3(int stackPos)
		{
			LuaValueType luaValueType = LuaDLL.tolua_getvaluetype(this.L, stackPos);
			if (luaValueType != LuaValueType.Vector3)
			{
				LuaDLL.luaL_typerror(this.L, stackPos, "Vector3", luaValueType.ToString());
				return Vector3.get_zero();
			}
			float num;
			float num2;
			float num3;
			LuaDLL.tolua_getvec3(this.L, stackPos, out num, out num2, out num3);
			return new Vector3(num, num2, num3);
		}

		public Quaternion CheckQuaternion(int stackPos)
		{
			LuaValueType luaValueType = LuaDLL.tolua_getvaluetype(this.L, stackPos);
			if (luaValueType != LuaValueType.Vector4)
			{
				LuaDLL.luaL_typerror(this.L, stackPos, "Quaternion", luaValueType.ToString());
				return Quaternion.get_identity();
			}
			float num;
			float num2;
			float num3;
			float num4;
			LuaDLL.tolua_getquat(this.L, stackPos, out num, out num2, out num3, out num4);
			return new Quaternion(num, num2, num3, num4);
		}

		public Vector2 CheckVector2(int stackPos)
		{
			LuaValueType luaValueType = LuaDLL.tolua_getvaluetype(this.L, stackPos);
			if (luaValueType != LuaValueType.Vector2)
			{
				LuaDLL.luaL_typerror(this.L, stackPos, "Vector2", luaValueType.ToString());
				return Vector2.get_zero();
			}
			float num;
			float num2;
			LuaDLL.tolua_getvec2(this.L, stackPos, out num, out num2);
			return new Vector2(num, num2);
		}

		public Vector4 CheckVector4(int stackPos)
		{
			LuaValueType luaValueType = LuaDLL.tolua_getvaluetype(this.L, stackPos);
			if (luaValueType != LuaValueType.Vector4)
			{
				LuaDLL.luaL_typerror(this.L, stackPos, "Vector4", luaValueType.ToString());
				return Vector4.get_zero();
			}
			float num;
			float num2;
			float num3;
			float num4;
			LuaDLL.tolua_getvec4(this.L, stackPos, out num, out num2, out num3, out num4);
			return new Vector4(num, num2, num3, num4);
		}

		public Color CheckColor(int stackPos)
		{
			LuaValueType luaValueType = LuaDLL.tolua_getvaluetype(this.L, stackPos);
			if (luaValueType != LuaValueType.Color)
			{
				LuaDLL.luaL_typerror(this.L, stackPos, "Color", luaValueType.ToString());
				return Color.get_black();
			}
			float num;
			float num2;
			float num3;
			float num4;
			LuaDLL.tolua_getclr(this.L, stackPos, out num, out num2, out num3, out num4);
			return new Color(num, num2, num3, num4);
		}

		public Ray CheckRay(int stackPos)
		{
			LuaValueType luaValueType = LuaDLL.tolua_getvaluetype(this.L, stackPos);
			if (luaValueType != LuaValueType.Ray)
			{
				LuaDLL.luaL_typerror(this.L, stackPos, "Ray", luaValueType.ToString());
				return default(Ray);
			}
			int num = this.BeginPCall(this.UnpackRay);
			LuaDLL.lua_pushvalue(this.L, stackPos);
			Ray result;
			try
			{
				this.PCall(1, num);
				Vector3 vector = this.ToVector3(num + 1);
				Vector3 vector2 = this.ToVector3(num + 2);
				this.EndPCall(num);
				result = new Ray(vector, vector2);
			}
			catch (Exception ex)
			{
				this.EndPCall(num);
				throw ex;
			}
			return result;
		}

		public Bounds CheckBounds(int stackPos)
		{
			LuaValueType luaValueType = LuaDLL.tolua_getvaluetype(this.L, stackPos);
			if (luaValueType != LuaValueType.Bounds)
			{
				LuaDLL.luaL_typerror(this.L, stackPos, "Bounds", luaValueType.ToString());
				return default(Bounds);
			}
			int num = this.BeginPCall(this.UnpackBounds);
			LuaDLL.lua_pushvalue(this.L, stackPos);
			Bounds result;
			try
			{
				this.PCall(1, num);
				Vector3 vector = this.ToVector3(num + 1);
				Vector3 vector2 = this.ToVector3(num + 2);
				this.EndPCall(num);
				result = new Bounds(vector, vector2);
			}
			catch (Exception ex)
			{
				this.EndPCall(num);
				throw ex;
			}
			return result;
		}

		public LayerMask CheckLayerMask(int stackPos)
		{
			LuaValueType luaValueType = LuaDLL.tolua_getvaluetype(this.L, stackPos);
			if (luaValueType != LuaValueType.LayerMask)
			{
				LuaDLL.luaL_typerror(this.L, stackPos, "LayerMask", luaValueType.ToString());
				return 0;
			}
			return LuaDLL.tolua_getlayermask(this.L, stackPos);
		}

		public LuaInteger64 CheckInteger64(int stackPos)
		{
			return ToLua.CheckLuaInteger64(this.L, stackPos);
		}

		public string CheckString(int stackPos)
		{
			return ToLua.CheckString(this.L, stackPos);
		}

		public Delegate CheckDelegate(int stackPos)
		{
			int num = LuaDLL.tolua_rawnetobj(this.L, stackPos);
			if (num != -1)
			{
				object @object = this.translator.GetObject(num);
				if (@object != null)
				{
					Type type = @object.GetType();
					if (type.IsSubclassOf(typeof(MulticastDelegate)))
					{
						return (Delegate)@object;
					}
					LuaDLL.luaL_typerror(this.L, stackPos, "Delegate", type.get_FullName());
				}
				return null;
			}
			if (LuaDLL.lua_isnil(this.L, stackPos))
			{
				return null;
			}
			LuaDLL.luaL_typerror(this.L, stackPos, "Delegate", null);
			return null;
		}

		public char[] CheckCharBuffer(int stackPos)
		{
			return ToLua.CheckCharBuffer(this.L, stackPos);
		}

		public byte[] CheckByteBuffer(int stackPos)
		{
			return ToLua.CheckByteBuffer(this.L, stackPos);
		}

		public T[] CheckNumberArray<T>(int stackPos)
		{
			return ToLua.CheckNumberArray<T>(this.L, stackPos);
		}

		public object CheckObject(int stackPos, Type type)
		{
			return ToLua.CheckObject(this.L, stackPos, type);
		}

		public object[] CheckObjects(int oldTop)
		{
			int num = LuaDLL.lua_gettop(this.L);
			if (oldTop == num)
			{
				return null;
			}
			List<object> list = new List<object>();
			for (int i = oldTop + 1; i <= num; i++)
			{
				list.Add(this.ToVariant(i));
			}
			return list.ToArray();
		}

		public LuaFunction CheckLuaFunction(int stackPos)
		{
			return ToLua.CheckLuaFunction(this.L, stackPos);
		}

		public LuaTable CheckLuaTable(int stackPos)
		{
			return ToLua.CheckLuaTable(this.L, stackPos);
		}

		public LuaThread CheckLuaThread(int stackPos)
		{
			return ToLua.CheckLuaThread(this.L, stackPos);
		}

		public object ToVariant(int stackPos)
		{
			return ToLua.ToVarObject(this.L, stackPos);
		}

		public void CollectRef(int reference, string name, bool isGCThread = false)
		{
			if (!isGCThread)
			{
				this.Collect(reference, name, false);
			}
			else
			{
				List<GCRef> list = this.gcList;
				lock (list)
				{
					this.gcList.Add(new GCRef(reference, name));
				}
			}
		}

		public void DelayDispose(LuaFunction func)
		{
			this.subList.Add(func);
		}

		public int Collect()
		{
			int count = this.gcList.get_Count();
			if (count > 0)
			{
				List<GCRef> list = this.gcList;
				lock (list)
				{
					for (int i = 0; i < this.gcList.get_Count(); i++)
					{
						int reference = this.gcList.get_Item(i).reference;
						string name = this.gcList.get_Item(i).name;
						this.Collect(reference, name, true);
					}
					this.gcList.Clear();
					return count;
				}
			}
			for (int j = 0; j < this.subList.get_Count(); j++)
			{
				this.subList.get_Item(j).Dispose();
			}
			this.subList.Clear();
			this.translator.Collect();
			return 0;
		}

		public void ReLoad(string moduleFileName)
		{
			LuaDLL.lua_getglobal(this.L, "package");
			LuaDLL.lua_getfield(this.L, -1, "loaded");
			LuaDLL.lua_pushstring(this.L, moduleFileName);
			LuaDLL.lua_gettable(this.L, -2);
			if (!LuaDLL.lua_isnil(this.L, -1))
			{
				LuaDLL.lua_pushstring(this.L, moduleFileName);
				LuaDLL.lua_pushnil(this.L);
				LuaDLL.lua_settable(this.L, -4);
			}
			LuaDLL.lua_pop(this.L, 3);
			string chunk = string.Format("require '{0}'", moduleFileName);
			this.DoString(chunk, "ReLoad");
		}

		public int GetMetaReference(Type t)
		{
			int result = 0;
			this.metaMap.TryGetValue(t, ref result);
			return result;
		}

		public int LuaGetTop()
		{
			return LuaDLL.lua_gettop(this.L);
		}

		public void LuaSetTop(int newTop)
		{
			LuaDLL.lua_settop(this.L, newTop);
		}

		public void LuaRawGet(int idx)
		{
			LuaDLL.lua_rawget(this.L, idx);
		}

		public void LuaRawSet(int idx)
		{
			LuaDLL.lua_rawset(this.L, idx);
		}

		public void LuaRawGetI(int tableIndex, int index)
		{
			LuaDLL.lua_rawgeti(this.L, tableIndex, index);
		}

		public void LuaRawSetI(int tableIndex, int index)
		{
			LuaDLL.lua_rawseti(this.L, tableIndex, index);
		}

		public void LuaRemove(int index)
		{
			LuaDLL.lua_remove(this.L, index);
		}

		public void LuaInsert(int idx)
		{
			LuaDLL.lua_insert(this.L, idx);
		}

		public void LuaReplace(int idx)
		{
			LuaDLL.lua_replace(this.L, idx);
		}

		public void LuaRawGlobal(string name)
		{
			LuaDLL.lua_pushstring(this.L, name);
			LuaDLL.lua_rawget(this.L, LuaIndexes.LUA_GLOBALSINDEX);
		}

		public void LuaGetGlobal(string name)
		{
			LuaDLL.lua_pushstring(this.L, name);
			LuaDLL.lua_gettable(this.L, LuaIndexes.LUA_GLOBALSINDEX);
		}

		public void LuaSetGlobal(string name)
		{
			LuaDLL.lua_setglobal(this.L, name);
		}

		public LuaTypes LuaType(int stackPos)
		{
			return LuaDLL.lua_type(this.L, stackPos);
		}

		public IntPtr LuaToThread(int stackPos)
		{
			return LuaDLL.lua_tothread(this.L, stackPos);
		}

		public bool LuaNext(int index)
		{
			return LuaDLL.lua_next(this.L, index) != 0;
		}

		public void LuaPushNil()
		{
			LuaDLL.lua_pushnil(this.L);
		}

		public void LuaPop(int amount)
		{
			LuaDLL.lua_pop(this.L, amount);
		}

		public int LuaObjLen(int index)
		{
			return LuaDLL.tolua_objlen(this.L, index);
		}

		public bool LuaCheckStack(int args)
		{
			return LuaDLL.lua_checkstack(this.L, args) != 0;
		}

		public void LuaGetTable(int index)
		{
			LuaDLL.lua_gettable(this.L, index);
		}

		public void LuaSetTable(int index)
		{
			LuaDLL.lua_settable(this.L, index);
		}

		public void LuaGetField(int index, string key)
		{
			LuaDLL.lua_getfield(this.L, index, key);
		}

		public void LuaSetField(int index, string name)
		{
			LuaDLL.lua_setfield(this.L, index, name);
		}

		public int LuaRequire(string fileName)
		{
			return LuaDLL.tolua_require(this.L, fileName);
		}

		public string LuaToString(int index)
		{
			return LuaDLL.lua_tostring(this.L, index);
		}

		public int LuaToInteger(int index)
		{
			return LuaDLL.lua_tointeger(this.L, index);
		}

		public void LuaGetRef(int reference)
		{
			LuaDLL.lua_getref(this.L, reference);
		}

		public int ToLuaRef()
		{
			return LuaDLL.toluaL_ref(this.L);
		}

		public void LuaCreateTable(int narr = 0, int nec = 0)
		{
			LuaDLL.lua_createtable(this.L, narr, nec);
		}

		public int LuaGetMetaTable(int idx)
		{
			return LuaDLL.lua_getmetatable(this.L, idx);
		}

		public IntPtr LuaToPointer(int idx)
		{
			return LuaDLL.lua_topointer(this.L, idx);
		}

		public void ToLuaException(Exception e)
		{
			if (LuaException.InstantiateCount > 0 || LuaException.SendMsgCount > 0)
			{
				LuaDLL.toluaL_exception(this.L, e, null);
				return;
			}
			throw e;
		}

		public void LuaGC(LuaGCOptions what, int data = 0)
		{
			LuaDLL.lua_gc(this.L, what, data);
		}

		public int LuaUpdate(float delta, float unscaled)
		{
			return LuaDLL.tolua_update(this.L, delta, unscaled);
		}

		public int LuaLateUpdate()
		{
			return LuaDLL.tolua_lateupdate(this.L);
		}

		public int LuaFixedUpdate(float fixedTime)
		{
			return LuaDLL.tolua_fixedupdate(this.L, fixedTime);
		}

		private void CloseBaseRef()
		{
			LuaDLL.lua_unref(this.L, this.PackBounds);
			LuaDLL.lua_unref(this.L, this.UnpackBounds);
			LuaDLL.lua_unref(this.L, this.PackRay);
			LuaDLL.lua_unref(this.L, this.UnpackRay);
			LuaDLL.lua_unref(this.L, this.PackRaycastHit);
			LuaDLL.lua_unref(this.L, this.PackTouch);
		}

		public void Dispose()
		{
			if (IntPtr.Zero != this.L)
			{
				LuaDLL.lua_gc(this.L, LuaGCOptions.LUA_GCCOLLECT, 0);
				this.Collect();
				using (Dictionary<Type, int>.Enumerator enumerator = this.metaMap.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<Type, int> current = enumerator.get_Current();
						LuaDLL.lua_unref(this.L, current.get_Value());
					}
				}
				List<LuaBaseRef> list = new List<LuaBaseRef>();
				using (Dictionary<int, WeakReference>.Enumerator enumerator2 = this.funcRefMap.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<int, WeakReference> current2 = enumerator2.get_Current();
						if (current2.get_Value().get_IsAlive())
						{
							list.Add((LuaBaseRef)current2.get_Value().get_Target());
						}
					}
				}
				for (int i = 0; i < list.get_Count(); i++)
				{
					list.get_Item(i).Dispose(true);
				}
				this.CloseBaseRef();
				this.funcRefMap.Clear();
				this.funcMap.Clear();
				this.metaMap.Clear();
				this.typeMap.Clear();
				this.enumMap.Clear();
				this.preLoadMap.Clear();
				LuaState.stateMap.Remove(this.L);
				LuaDLL.lua_close(this.L);
				this.translator.Dispose();
				this.translator = null;
				this.L = IntPtr.Zero;
				Debugger.Log("LuaState quit");
			}
			if (LuaState.mainState == this)
			{
				LuaState.mainState = null;
			}
			LuaFileUtils.Instance.Dispose();
			GC.SuppressFinalize(this);
		}

		public virtual void Dispose(bool dispose)
		{
		}

		public override int GetHashCode()
		{
			return this.L.GetHashCode();
		}

		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			LuaState luaState = o as LuaState;
			return luaState != null && luaState.L == this.L;
		}

		public void PrintTable(string name)
		{
			LuaTable table = this.GetTable(name, true);
			LuaDictTable luaDictTable = table.ToDictTable();
			table.Dispose();
			IEnumerator<DictionaryEntry> enumerator = luaDictTable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string arg_44_0 = "map item, k,v is {0}:{1}";
				DictionaryEntry current = enumerator.get_Current();
				object arg_44_1 = current.get_Key();
				DictionaryEntry current2 = enumerator.get_Current();
				Debugger.Log(arg_44_0, arg_44_1, current2.get_Value());
			}
			enumerator.Dispose();
			luaDictTable.Dispose();
		}

		protected void Collect(int reference, string name, bool beThread)
		{
			if (beThread)
			{
				WeakReference weakReference = null;
				if (name != null)
				{
					this.funcMap.TryGetValue(name, ref weakReference);
					if (weakReference != null && !weakReference.get_IsAlive())
					{
						this.funcMap.Remove(name);
						weakReference = null;
					}
				}
				this.funcRefMap.TryGetValue(reference, ref weakReference);
				if (weakReference != null && !weakReference.get_IsAlive())
				{
					LuaDLL.toluaL_unref(this.L, reference);
					this.funcRefMap.Remove(reference);
					if (this.LogGC)
					{
						Debugger.Log("collect lua reference name {0}, id {1} in thread", name, reference);
					}
				}
			}
			else
			{
				if (name != null)
				{
					WeakReference weakReference2 = null;
					this.funcMap.TryGetValue(name, ref weakReference2);
					if (weakReference2 != null && weakReference2.get_IsAlive())
					{
						LuaBaseRef luaBaseRef = (LuaBaseRef)weakReference2.get_Target();
						if (reference == luaBaseRef.GetReference())
						{
							this.funcMap.Remove(name);
						}
					}
				}
				LuaDLL.toluaL_unref(this.L, reference);
				this.funcRefMap.Remove(reference);
				if (this.LogGC)
				{
					Debugger.Log("collect lua reference name {0}, id {1} in main", name, reference);
				}
			}
		}

		protected object[] LuaLoadBuffer(byte[] buffer, string chunkName)
		{
			LuaDLL.tolua_pushtraceback(this.L);
			int num = LuaDLL.lua_gettop(this.L);
			if (LuaDLL.luaL_loadbuffer(this.L, buffer, buffer.Length, chunkName) == 0 && LuaDLL.lua_pcall(this.L, 0, LuaDLL.LUA_MULTRET, num) == 0)
			{
				object[] result = this.CheckObjects(num);
				LuaDLL.lua_settop(this.L, num - 1);
				return result;
			}
			string msg = LuaDLL.lua_tostring(this.L, -1);
			LuaDLL.lua_settop(this.L, num - 1);
			throw new LuaException(msg, null, 1);
		}

		public static bool operator ==(LuaState a, LuaState b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (a == null && b != null)
			{
				return b.L == IntPtr.Zero;
			}
			if (a != null && b == null)
			{
				return a.L == IntPtr.Zero;
			}
			return a.L == b.L;
		}

		public static bool operator !=(LuaState a, LuaState b)
		{
			return !(a == b);
		}
	}
}
