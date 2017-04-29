using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_WrapModeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(WrapMode));
		L.RegVar("Once", new LuaCSFunction(UnityEngine_WrapModeWrap.get_Once), null);
		L.RegVar("Loop", new LuaCSFunction(UnityEngine_WrapModeWrap.get_Loop), null);
		L.RegVar("PingPong", new LuaCSFunction(UnityEngine_WrapModeWrap.get_PingPong), null);
		L.RegVar("Default", new LuaCSFunction(UnityEngine_WrapModeWrap.get_Default), null);
		L.RegVar("ClampForever", new LuaCSFunction(UnityEngine_WrapModeWrap.get_ClampForever), null);
		L.RegVar("Clamp", new LuaCSFunction(UnityEngine_WrapModeWrap.get_Clamp), null);
		L.RegFunction("IntToEnum", new LuaCSFunction(UnityEngine_WrapModeWrap.IntToEnum));
		L.EndEnum();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Once(IntPtr L)
	{
		ToLua.Push(L, 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Loop(IntPtr L)
	{
		ToLua.Push(L, 2);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_PingPong(IntPtr L)
	{
		ToLua.Push(L, 4);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Default(IntPtr L)
	{
		ToLua.Push(L, 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ClampForever(IntPtr L)
	{
		ToLua.Push(L, 8);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Clamp(IntPtr L)
	{
		ToLua.Push(L, 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		WrapMode wrapMode = num;
		ToLua.Push(L, wrapMode);
		return 1;
	}
}
