using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_InputWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("Input");
		L.RegFunction("GetAxis", new LuaCSFunction(UnityEngine_InputWrap.GetAxis));
		L.RegFunction("GetAxisRaw", new LuaCSFunction(UnityEngine_InputWrap.GetAxisRaw));
		L.RegFunction("GetButton", new LuaCSFunction(UnityEngine_InputWrap.GetButton));
		L.RegFunction("GetButtonDown", new LuaCSFunction(UnityEngine_InputWrap.GetButtonDown));
		L.RegFunction("GetButtonUp", new LuaCSFunction(UnityEngine_InputWrap.GetButtonUp));
		L.RegFunction("GetKey", new LuaCSFunction(UnityEngine_InputWrap.GetKey));
		L.RegFunction("GetKeyDown", new LuaCSFunction(UnityEngine_InputWrap.GetKeyDown));
		L.RegFunction("GetKeyUp", new LuaCSFunction(UnityEngine_InputWrap.GetKeyUp));
		L.RegFunction("GetJoystickNames", new LuaCSFunction(UnityEngine_InputWrap.GetJoystickNames));
		L.RegFunction("GetMouseButton", new LuaCSFunction(UnityEngine_InputWrap.GetMouseButton));
		L.RegFunction("GetMouseButtonDown", new LuaCSFunction(UnityEngine_InputWrap.GetMouseButtonDown));
		L.RegFunction("GetMouseButtonUp", new LuaCSFunction(UnityEngine_InputWrap.GetMouseButtonUp));
		L.RegFunction("ResetInputAxes", new LuaCSFunction(UnityEngine_InputWrap.ResetInputAxes));
		L.RegFunction("GetAccelerationEvent", new LuaCSFunction(UnityEngine_InputWrap.GetAccelerationEvent));
		L.RegFunction("GetTouch", new LuaCSFunction(UnityEngine_InputWrap.GetTouch));
		L.RegVar("compensateSensors", new LuaCSFunction(UnityEngine_InputWrap.get_compensateSensors), new LuaCSFunction(UnityEngine_InputWrap.set_compensateSensors));
		L.RegVar("gyro", new LuaCSFunction(UnityEngine_InputWrap.get_gyro), null);
		L.RegVar("mousePosition", new LuaCSFunction(UnityEngine_InputWrap.get_mousePosition), null);
		L.RegVar("mouseScrollDelta", new LuaCSFunction(UnityEngine_InputWrap.get_mouseScrollDelta), null);
		L.RegVar("mousePresent", new LuaCSFunction(UnityEngine_InputWrap.get_mousePresent), null);
		L.RegVar("simulateMouseWithTouches", new LuaCSFunction(UnityEngine_InputWrap.get_simulateMouseWithTouches), new LuaCSFunction(UnityEngine_InputWrap.set_simulateMouseWithTouches));
		L.RegVar("anyKey", new LuaCSFunction(UnityEngine_InputWrap.get_anyKey), null);
		L.RegVar("anyKeyDown", new LuaCSFunction(UnityEngine_InputWrap.get_anyKeyDown), null);
		L.RegVar("inputString", new LuaCSFunction(UnityEngine_InputWrap.get_inputString), null);
		L.RegVar("acceleration", new LuaCSFunction(UnityEngine_InputWrap.get_acceleration), null);
		L.RegVar("accelerationEvents", new LuaCSFunction(UnityEngine_InputWrap.get_accelerationEvents), null);
		L.RegVar("accelerationEventCount", new LuaCSFunction(UnityEngine_InputWrap.get_accelerationEventCount), null);
		L.RegVar("touches", new LuaCSFunction(UnityEngine_InputWrap.get_touches), null);
		L.RegVar("touchCount", new LuaCSFunction(UnityEngine_InputWrap.get_touchCount), null);
		L.RegVar("touchPressureSupported", new LuaCSFunction(UnityEngine_InputWrap.get_touchPressureSupported), null);
		L.RegVar("stylusTouchSupported", new LuaCSFunction(UnityEngine_InputWrap.get_stylusTouchSupported), null);
		L.RegVar("touchSupported", new LuaCSFunction(UnityEngine_InputWrap.get_touchSupported), null);
		L.RegVar("multiTouchEnabled", new LuaCSFunction(UnityEngine_InputWrap.get_multiTouchEnabled), new LuaCSFunction(UnityEngine_InputWrap.set_multiTouchEnabled));
		L.RegVar("location", new LuaCSFunction(UnityEngine_InputWrap.get_location), null);
		L.RegVar("compass", new LuaCSFunction(UnityEngine_InputWrap.get_compass), null);
		L.RegVar("deviceOrientation", new LuaCSFunction(UnityEngine_InputWrap.get_deviceOrientation), null);
		L.RegVar("imeCompositionMode", new LuaCSFunction(UnityEngine_InputWrap.get_imeCompositionMode), new LuaCSFunction(UnityEngine_InputWrap.set_imeCompositionMode));
		L.RegVar("compositionString", new LuaCSFunction(UnityEngine_InputWrap.get_compositionString), null);
		L.RegVar("imeIsSelected", new LuaCSFunction(UnityEngine_InputWrap.get_imeIsSelected), null);
		L.RegVar("compositionCursorPos", new LuaCSFunction(UnityEngine_InputWrap.get_compositionCursorPos), new LuaCSFunction(UnityEngine_InputWrap.set_compositionCursorPos));
		L.RegVar("backButtonLeavesApp", new LuaCSFunction(UnityEngine_InputWrap.get_backButtonLeavesApp), new LuaCSFunction(UnityEngine_InputWrap.set_backButtonLeavesApp));
		L.EndStaticLibs();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAxis(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			float axis = Input.GetAxis(text);
			LuaDLL.lua_pushnumber(L, (double)axis);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAxisRaw(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			float axisRaw = Input.GetAxisRaw(text);
			LuaDLL.lua_pushnumber(L, (double)axisRaw);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetButton(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			bool button = Input.GetButton(text);
			LuaDLL.lua_pushboolean(L, button);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetButtonDown(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			bool buttonDown = Input.GetButtonDown(text);
			LuaDLL.lua_pushboolean(L, buttonDown);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetButtonUp(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string text = ToLua.CheckString(L, 1);
			bool buttonUp = Input.GetButtonUp(text);
			LuaDLL.lua_pushboolean(L, buttonUp);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetKey(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(KeyCode)))
			{
				KeyCode keyCode = (int)ToLua.ToObject(L, 1);
				bool key = Input.GetKey(keyCode);
				LuaDLL.lua_pushboolean(L, key);
				result = 1;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				bool key2 = Input.GetKey(text);
				LuaDLL.lua_pushboolean(L, key2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Input.GetKey");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetKeyDown(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(KeyCode)))
			{
				KeyCode keyCode = (int)ToLua.ToObject(L, 1);
				bool keyDown = Input.GetKeyDown(keyCode);
				LuaDLL.lua_pushboolean(L, keyDown);
				result = 1;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				bool keyDown2 = Input.GetKeyDown(text);
				LuaDLL.lua_pushboolean(L, keyDown2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Input.GetKeyDown");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetKeyUp(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(KeyCode)))
			{
				KeyCode keyCode = (int)ToLua.ToObject(L, 1);
				bool keyUp = Input.GetKeyUp(keyCode);
				LuaDLL.lua_pushboolean(L, keyUp);
				result = 1;
			}
			else if (num == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string text = ToLua.ToString(L, 1);
				bool keyUp2 = Input.GetKeyUp(text);
				LuaDLL.lua_pushboolean(L, keyUp2);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Input.GetKeyUp");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetJoystickNames(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 0);
			string[] joystickNames = Input.GetJoystickNames();
			ToLua.Push(L, joystickNames);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetMouseButton(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			int num = (int)LuaDLL.luaL_checknumber(L, 1);
			bool mouseButton = Input.GetMouseButton(num);
			LuaDLL.lua_pushboolean(L, mouseButton);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetMouseButtonDown(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			int num = (int)LuaDLL.luaL_checknumber(L, 1);
			bool mouseButtonDown = Input.GetMouseButtonDown(num);
			LuaDLL.lua_pushboolean(L, mouseButtonDown);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetMouseButtonUp(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			int num = (int)LuaDLL.luaL_checknumber(L, 1);
			bool mouseButtonUp = Input.GetMouseButtonUp(num);
			LuaDLL.lua_pushboolean(L, mouseButtonUp);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResetInputAxes(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 0);
			Input.ResetInputAxes();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAccelerationEvent(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			int num = (int)LuaDLL.luaL_checknumber(L, 1);
			AccelerationEvent accelerationEvent = Input.GetAccelerationEvent(num);
			ToLua.PushValue(L, accelerationEvent);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTouch(IntPtr L)
	{
		int result;
		try
		{
			int num = (int)LuaDLL.luaL_checknumber(L, 1);
			int flag = LuaDLL.luaL_optinteger(L, 2, 7);
			Touch touch = Input.GetTouch(num);
			ToLua.Push(L, touch, flag);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_compensateSensors(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_compensateSensors());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_gyro(IntPtr L)
	{
		ToLua.PushObject(L, Input.get_gyro());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mousePosition(IntPtr L)
	{
		ToLua.Push(L, Input.get_mousePosition());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mouseScrollDelta(IntPtr L)
	{
		ToLua.Push(L, Input.get_mouseScrollDelta());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mousePresent(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_mousePresent());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_simulateMouseWithTouches(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_simulateMouseWithTouches());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anyKey(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_anyKey());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anyKeyDown(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_anyKeyDown());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_inputString(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Input.get_inputString());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_acceleration(IntPtr L)
	{
		ToLua.Push(L, Input.get_acceleration());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_accelerationEvents(IntPtr L)
	{
		ToLua.Push(L, Input.get_accelerationEvents());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_accelerationEventCount(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, Input.get_accelerationEventCount());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_touches(IntPtr L)
	{
		ToLua.Push(L, Input.get_touches());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_touchCount(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, Input.get_touchCount());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_touchPressureSupported(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_touchPressureSupported());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_stylusTouchSupported(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_stylusTouchSupported());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_touchSupported(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_touchSupported());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_multiTouchEnabled(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_multiTouchEnabled());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_location(IntPtr L)
	{
		ToLua.PushObject(L, Input.get_location());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_compass(IntPtr L)
	{
		ToLua.PushObject(L, Input.get_compass());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_deviceOrientation(IntPtr L)
	{
		ToLua.Push(L, Input.get_deviceOrientation());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_imeCompositionMode(IntPtr L)
	{
		ToLua.Push(L, Input.get_imeCompositionMode());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_compositionString(IntPtr L)
	{
		LuaDLL.lua_pushstring(L, Input.get_compositionString());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_imeIsSelected(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_imeIsSelected());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_compositionCursorPos(IntPtr L)
	{
		ToLua.Push(L, Input.get_compositionCursorPos());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_backButtonLeavesApp(IntPtr L)
	{
		LuaDLL.lua_pushboolean(L, Input.get_backButtonLeavesApp());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_compensateSensors(IntPtr L)
	{
		int result;
		try
		{
			bool compensateSensors = LuaDLL.luaL_checkboolean(L, 2);
			Input.set_compensateSensors(compensateSensors);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_simulateMouseWithTouches(IntPtr L)
	{
		int result;
		try
		{
			bool simulateMouseWithTouches = LuaDLL.luaL_checkboolean(L, 2);
			Input.set_simulateMouseWithTouches(simulateMouseWithTouches);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_multiTouchEnabled(IntPtr L)
	{
		int result;
		try
		{
			bool multiTouchEnabled = LuaDLL.luaL_checkboolean(L, 2);
			Input.set_multiTouchEnabled(multiTouchEnabled);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_imeCompositionMode(IntPtr L)
	{
		int result;
		try
		{
			IMECompositionMode imeCompositionMode = (int)ToLua.CheckObject(L, 2, typeof(IMECompositionMode));
			Input.set_imeCompositionMode(imeCompositionMode);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_compositionCursorPos(IntPtr L)
	{
		int result;
		try
		{
			Vector2 compositionCursorPos = ToLua.ToVector2(L, 2);
			Input.set_compositionCursorPos(compositionCursorPos);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_backButtonLeavesApp(IntPtr L)
	{
		int result;
		try
		{
			bool backButtonLeavesApp = LuaDLL.luaL_checkboolean(L, 2);
			Input.set_backButtonLeavesApp(backButtonLeavesApp);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
