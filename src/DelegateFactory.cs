using LuaInterface;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class DelegateFactory
{
	private class System_Action_Event : LuaDelegate
	{
		public System_Action_Event(LuaFunction func) : base(func)
		{
		}

		public void Call()
		{
			this.func.Call();
		}
	}

	private class UnityEngine_Events_UnityAction_Event : LuaDelegate
	{
		public UnityEngine_Events_UnityAction_Event(LuaFunction func) : base(func)
		{
		}

		public void Call()
		{
			this.func.Call();
		}
	}

	private class TestEventListener_OnClick_Event : LuaDelegate
	{
		public TestEventListener_OnClick_Event(LuaFunction func) : base(func)
		{
		}

		public void Call(GameObject param0)
		{
			this.func.BeginPCall();
			this.func.Push(param0);
			this.func.PCall();
			this.func.EndPCall();
		}
	}

	private class TestEventListener_VoidDelegate_Event : LuaDelegate
	{
		public TestEventListener_VoidDelegate_Event(LuaFunction func) : base(func)
		{
		}

		public void Call(GameObject param0)
		{
			this.func.BeginPCall();
			this.func.Push(param0);
			this.func.PCall();
			this.func.EndPCall();
		}
	}

	private class UnityEngine_Application_LogCallback_Event : LuaDelegate
	{
		public UnityEngine_Application_LogCallback_Event(LuaFunction func) : base(func)
		{
		}

		public void Call(string param0, string param1, LogType param2)
		{
			this.func.BeginPCall();
			this.func.Push(param0);
			this.func.Push(param1);
			this.func.Push(param2);
			this.func.PCall();
			this.func.EndPCall();
		}
	}

	private class UnityEngine_Application_AdvertisingIdentifierCallback_Event : LuaDelegate
	{
		public UnityEngine_Application_AdvertisingIdentifierCallback_Event(LuaFunction func) : base(func)
		{
		}

		public void Call(string param0, bool param1, string param2)
		{
			this.func.BeginPCall();
			this.func.Push(param0);
			this.func.Push(param1);
			this.func.Push(param2);
			this.func.PCall();
			this.func.EndPCall();
		}
	}

	private class UnityEngine_Camera_CameraCallback_Event : LuaDelegate
	{
		public UnityEngine_Camera_CameraCallback_Event(LuaFunction func) : base(func)
		{
		}

		public void Call(Camera param0)
		{
			this.func.BeginPCall();
			this.func.Push(param0);
			this.func.PCall();
			this.func.EndPCall();
		}
	}

	private class UnityEngine_AudioClip_PCMReaderCallback_Event : LuaDelegate
	{
		public UnityEngine_AudioClip_PCMReaderCallback_Event(LuaFunction func) : base(func)
		{
		}

		public void Call(float[] param0)
		{
			this.func.BeginPCall();
			this.func.Push(param0);
			this.func.PCall();
			this.func.EndPCall();
		}
	}

	private class UnityEngine_AudioClip_PCMSetPositionCallback_Event : LuaDelegate
	{
		public UnityEngine_AudioClip_PCMSetPositionCallback_Event(LuaFunction func) : base(func)
		{
		}

		public void Call(int param0)
		{
			this.func.BeginPCall();
			this.func.Push((double)param0);
			this.func.PCall();
			this.func.EndPCall();
		}
	}

	private class UnityEngine_RectTransform_ReapplyDrivenProperties_Event : LuaDelegate
	{
		public UnityEngine_RectTransform_ReapplyDrivenProperties_Event(LuaFunction func) : base(func)
		{
		}

		public void Call(RectTransform param0)
		{
			this.func.BeginPCall();
			this.func.Push(param0);
			this.func.PCall();
			this.func.EndPCall();
		}
	}

	private class System_Action_UnityEngine_Objects_Event : LuaDelegate
	{
		public System_Action_UnityEngine_Objects_Event(LuaFunction func) : base(func)
		{
		}

		public void Call(Object[] param0)
		{
			this.func.BeginPCall();
			this.func.Push(param0);
			this.func.PCall();
			this.func.EndPCall();
		}
	}

	private delegate Delegate DelegateValue(LuaFunction func);

	private static Dictionary<Type, DelegateFactory.DelegateValue> dict;

	static DelegateFactory()
	{
		DelegateFactory.dict = new Dictionary<Type, DelegateFactory.DelegateValue>();
		DelegateFactory.Register();
	}

	[NoToLua]
	public static void Register()
	{
		DelegateFactory.dict.Clear();
		DelegateFactory.dict.Add(typeof(Action), new DelegateFactory.DelegateValue(DelegateFactory.System_Action));
		DelegateFactory.dict.Add(typeof(UnityAction), new DelegateFactory.DelegateValue(DelegateFactory.UnityEngine_Events_UnityAction));
		DelegateFactory.dict.Add(typeof(Application.LogCallback), new DelegateFactory.DelegateValue(DelegateFactory.UnityEngine_Application_LogCallback));
		DelegateFactory.dict.Add(typeof(Application.AdvertisingIdentifierCallback), new DelegateFactory.DelegateValue(DelegateFactory.UnityEngine_Application_AdvertisingIdentifierCallback));
		DelegateFactory.dict.Add(typeof(Camera.CameraCallback), new DelegateFactory.DelegateValue(DelegateFactory.UnityEngine_Camera_CameraCallback));
		DelegateFactory.dict.Add(typeof(AudioClip.PCMReaderCallback), new DelegateFactory.DelegateValue(DelegateFactory.UnityEngine_AudioClip_PCMReaderCallback));
		DelegateFactory.dict.Add(typeof(AudioClip.PCMSetPositionCallback), new DelegateFactory.DelegateValue(DelegateFactory.UnityEngine_AudioClip_PCMSetPositionCallback));
		DelegateFactory.dict.Add(typeof(RectTransform.ReapplyDrivenProperties), new DelegateFactory.DelegateValue(DelegateFactory.UnityEngine_RectTransform_ReapplyDrivenProperties));
		DelegateFactory.dict.Add(typeof(Action<Object[]>), new DelegateFactory.DelegateValue(DelegateFactory.System_Action_UnityEngine_Objects));
	}

	[NoToLua]
	public static Delegate CreateDelegate(Type t, LuaFunction func = null)
	{
		DelegateFactory.DelegateValue delegateValue = null;
		if (!DelegateFactory.dict.TryGetValue(t, ref delegateValue))
		{
			throw new LuaException(string.Format("Delegate {0} not register", LuaMisc.GetTypeName(t)), null, 1);
		}
		return delegateValue(func);
	}

	[NoToLua]
	public static Delegate RemoveDelegate(Delegate obj, LuaFunction func)
	{
		LuaState luaState = func.GetLuaState();
		Delegate[] invocationList = obj.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			LuaDelegate luaDelegate = invocationList[i].get_Target() as LuaDelegate;
			if (luaDelegate != null && luaDelegate.func == func)
			{
				obj = Delegate.Remove(obj, invocationList[i]);
				luaState.DelayDispose(luaDelegate.func);
				break;
			}
		}
		return obj;
	}

	public static Delegate System_Action(LuaFunction func)
	{
		if (func == null)
		{
			return delegate
			{
			};
		}
		return new Action(new DelegateFactory.System_Action_Event(func).Call);
	}

	public static Delegate UnityEngine_Events_UnityAction(LuaFunction func)
	{
		if (func == null)
		{
			return delegate
			{
			};
		}
		return new UnityAction(new DelegateFactory.UnityEngine_Events_UnityAction_Event(func).Call);
	}

	public static Delegate UnityEngine_Application_LogCallback(LuaFunction func)
	{
		if (func == null)
		{
			return delegate
			{
			};
		}
		return new Application.LogCallback(new DelegateFactory.UnityEngine_Application_LogCallback_Event(func).Call);
	}

	public static Delegate UnityEngine_Application_AdvertisingIdentifierCallback(LuaFunction func)
	{
		if (func == null)
		{
			return delegate
			{
			};
		}
		return new Application.AdvertisingIdentifierCallback(new DelegateFactory.UnityEngine_Application_AdvertisingIdentifierCallback_Event(func).Call);
	}

	public static Delegate UnityEngine_Camera_CameraCallback(LuaFunction func)
	{
		if (func == null)
		{
			return delegate
			{
			};
		}
		return new Camera.CameraCallback(new DelegateFactory.UnityEngine_Camera_CameraCallback_Event(func).Call);
	}

	public static Delegate UnityEngine_AudioClip_PCMReaderCallback(LuaFunction func)
	{
		if (func == null)
		{
			return delegate
			{
			};
		}
		return new AudioClip.PCMReaderCallback(new DelegateFactory.UnityEngine_AudioClip_PCMReaderCallback_Event(func).Call);
	}

	public static Delegate UnityEngine_AudioClip_PCMSetPositionCallback(LuaFunction func)
	{
		if (func == null)
		{
			return delegate
			{
			};
		}
		return new AudioClip.PCMSetPositionCallback(new DelegateFactory.UnityEngine_AudioClip_PCMSetPositionCallback_Event(func).Call);
	}

	public static Delegate UnityEngine_RectTransform_ReapplyDrivenProperties(LuaFunction func)
	{
		if (func == null)
		{
			return delegate
			{
			};
		}
		return new RectTransform.ReapplyDrivenProperties(new DelegateFactory.UnityEngine_RectTransform_ReapplyDrivenProperties_Event(func).Call);
	}

	public static Delegate System_Action_UnityEngine_Objects(LuaFunction func)
	{
		if (func == null)
		{
			return delegate
			{
			};
		}
		return new Action<Object[]>(new DelegateFactory.System_Action_UnityEngine_Objects_Event(func).Call);
	}
}
