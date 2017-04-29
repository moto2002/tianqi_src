using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_BlendWeightsWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(BlendWeights));
		L.RegVar("OneBone", new LuaCSFunction(UnityEngine_BlendWeightsWrap.get_OneBone), null);
		L.RegVar("TwoBones", new LuaCSFunction(UnityEngine_BlendWeightsWrap.get_TwoBones), null);
		L.RegVar("FourBones", new LuaCSFunction(UnityEngine_BlendWeightsWrap.get_FourBones), null);
		L.RegFunction("IntToEnum", new LuaCSFunction(UnityEngine_BlendWeightsWrap.IntToEnum));
		L.EndEnum();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_OneBone(IntPtr L)
	{
		ToLua.Push(L, 1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_TwoBones(IntPtr L)
	{
		ToLua.Push(L, 2);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_FourBones(IntPtr L)
	{
		ToLua.Push(L, 4);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		BlendWeights blendWeights = num;
		ToLua.Push(L, blendWeights);
		return 1;
	}
}
