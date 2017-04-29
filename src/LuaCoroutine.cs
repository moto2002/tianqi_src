using LuaInterface;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public static class LuaCoroutine
{
	private static MonoBehaviour mb;

	private static string strCo = "\n        local _WaitForSeconds, _WaitForFixedUpdate, _WaitForEndOfFrame, _Yield = WaitForSeconds, WaitForFixedUpdate, WaitForEndOfFrame, Yield        \n        local comap = {}\n        setmetatable(comap, {__mode = 'kv'})\n\n        function WaitForSeconds(t)\n            local co = coroutine.running()\n            local resume = function()    \n                if comap[co] then\n                    return coroutine.resume(co)\n                end                            \n            end\n            \n            comap[co] = true\n            _WaitForSeconds(t, resume)\n            return coroutine.yield()\n        end\n\n        function WaitForFixedUpdate()\n            local co = coroutine.running()\n            local resume = function()          \n                if comap[co] then      \n                    return coroutine.resume(co)\n                end\n            end\n        \n            comap[co] = true\n            _WaitForFixedUpdate(resume)\n            return coroutine.yield()\n        end\n\n        function WaitForEndOfFrame()\n            local co = coroutine.running()\n            local resume = function()        \n                if comap[co] then        \n                    return coroutine.resume(co)\n                end\n            end\n        \n            comap[co] = true\n            _WaitForEndOfFrame(resume)\n            return coroutine.yield()\n        end\n\n        function Yield(o)\n            local co = coroutine.running()\n            local resume = function()        \n                if comap[co] then        \n                    return coroutine.resume(co)\n                end\n            end\n        \n            comap[co] = true\n            _Yield(o, resume)\n            return coroutine.yield()\n        end\n\n        function StartCoroutine(func)\n            local co = coroutine.create(func)                       \n            coroutine.resume(co)\n            return co\n        end\n\n        function StopCoroutine(co)\n            comap[co] = false\n        end\n        ";

	public static void Register(LuaState state, MonoBehaviour behaviour)
	{
		state.BeginModule(null);
		state.RegFunction("WaitForSeconds", new LuaCSFunction(LuaCoroutine.WaitForSeconds));
		state.RegFunction("WaitForFixedUpdate", new LuaCSFunction(LuaCoroutine.WaitForFixedUpdate));
		state.RegFunction("WaitForEndOfFrame", new LuaCSFunction(LuaCoroutine.WaitForEndOfFrame));
		state.RegFunction("Yield", new LuaCSFunction(LuaCoroutine.Yield));
		state.EndModule();
		state.DoString(LuaCoroutine.strCo, "LuaCoroutine");
		LuaCoroutine.mb = behaviour;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WaitForSeconds(IntPtr L)
	{
		int result;
		try
		{
			float sec = (float)LuaDLL.luaL_checknumber(L, 1);
			LuaFunction func = ToLua.ToLuaFunction(L, 2);
			LuaCoroutine.mb.StartCoroutine(LuaCoroutine.CoWaitForSeconds(sec, func));
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[DebuggerHidden]
	private static IEnumerator CoWaitForSeconds(float sec, LuaFunction func)
	{
		LuaCoroutine.<CoWaitForSeconds>c__Iterator0 <CoWaitForSeconds>c__Iterator = new LuaCoroutine.<CoWaitForSeconds>c__Iterator0();
		<CoWaitForSeconds>c__Iterator.sec = sec;
		<CoWaitForSeconds>c__Iterator.func = func;
		<CoWaitForSeconds>c__Iterator.<$>sec = sec;
		<CoWaitForSeconds>c__Iterator.<$>func = func;
		return <CoWaitForSeconds>c__Iterator;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WaitForFixedUpdate(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.ToLuaFunction(L, 1);
			LuaCoroutine.mb.StartCoroutine(LuaCoroutine.CoWaitForFixedUpdate(func));
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[DebuggerHidden]
	private static IEnumerator CoWaitForFixedUpdate(LuaFunction func)
	{
		LuaCoroutine.<CoWaitForFixedUpdate>c__Iterator1 <CoWaitForFixedUpdate>c__Iterator = new LuaCoroutine.<CoWaitForFixedUpdate>c__Iterator1();
		<CoWaitForFixedUpdate>c__Iterator.func = func;
		<CoWaitForFixedUpdate>c__Iterator.<$>func = func;
		return <CoWaitForFixedUpdate>c__Iterator;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WaitForEndOfFrame(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.ToLuaFunction(L, 1);
			LuaCoroutine.mb.StartCoroutine(LuaCoroutine.CoWaitForEndOfFrame(func));
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[DebuggerHidden]
	private static IEnumerator CoWaitForEndOfFrame(LuaFunction func)
	{
		LuaCoroutine.<CoWaitForEndOfFrame>c__Iterator2 <CoWaitForEndOfFrame>c__Iterator = new LuaCoroutine.<CoWaitForEndOfFrame>c__Iterator2();
		<CoWaitForEndOfFrame>c__Iterator.func = func;
		<CoWaitForEndOfFrame>c__Iterator.<$>func = func;
		return <CoWaitForEndOfFrame>c__Iterator;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Yield(IntPtr L)
	{
		int result;
		try
		{
			object o = ToLua.ToVarObject(L, 1);
			LuaFunction func = ToLua.ToLuaFunction(L, 2);
			LuaCoroutine.mb.StartCoroutine(LuaCoroutine.CoYield(o, func));
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[DebuggerHidden]
	private static IEnumerator CoYield(object o, LuaFunction func)
	{
		LuaCoroutine.<CoYield>c__Iterator3 <CoYield>c__Iterator = new LuaCoroutine.<CoYield>c__Iterator3();
		<CoYield>c__Iterator.o = o;
		<CoYield>c__Iterator.func = func;
		<CoYield>c__Iterator.<$>o = o;
		<CoYield>c__Iterator.<$>func = func;
		return <CoYield>c__Iterator;
	}
}
