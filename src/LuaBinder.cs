using LuaInterface;
using System;
using UnityEngine;
using UnityEngine.Events;

public static class LuaBinder
{
	public static void Bind(LuaState L)
	{
		float realtimeSinceStartup = Time.get_realtimeSinceStartup();
		L.BeginModule(null);
		DebuggerWrap.Register(L);
		ViewWrap.Register(L);
		BaseWrap.Register(L);
		ManagerWrap.Register(L);
		L.BeginModule("UnityEngine");
		UnityEngine_ComponentWrap.Register(L);
		UnityEngine_BehaviourWrap.Register(L);
		UnityEngine_MonoBehaviourWrap.Register(L);
		UnityEngine_GameObjectWrap.Register(L);
		UnityEngine_TransformWrap.Register(L);
		UnityEngine_TrackedReferenceWrap.Register(L);
		UnityEngine_ApplicationWrap.Register(L);
		UnityEngine_PhysicsWrap.Register(L);
		UnityEngine_ColliderWrap.Register(L);
		UnityEngine_TimeWrap.Register(L);
		UnityEngine_TextureWrap.Register(L);
		UnityEngine_Texture2DWrap.Register(L);
		UnityEngine_ShaderWrap.Register(L);
		UnityEngine_MaterialWrap.Register(L);
		UnityEngine_RendererWrap.Register(L);
		UnityEngine_WWWWrap.Register(L);
		UnityEngine_ScreenWrap.Register(L);
		UnityEngine_CameraWrap.Register(L);
		UnityEngine_CameraClearFlagsWrap.Register(L);
		UnityEngine_AudioClipWrap.Register(L);
		UnityEngine_AudioSourceWrap.Register(L);
		UnityEngine_AssetBundleWrap.Register(L);
		UnityEngine_ParticleSystemWrap.Register(L);
		UnityEngine_AsyncOperationWrap.Register(L);
		UnityEngine_LightWrap.Register(L);
		UnityEngine_LightTypeWrap.Register(L);
		UnityEngine_SleepTimeoutWrap.Register(L);
		UnityEngine_AnimatorWrap.Register(L);
		UnityEngine_InputWrap.Register(L);
		UnityEngine_KeyCodeWrap.Register(L);
		UnityEngine_SkinnedMeshRendererWrap.Register(L);
		UnityEngine_SpaceWrap.Register(L);
		UnityEngine_AnimationBlendModeWrap.Register(L);
		UnityEngine_QueueModeWrap.Register(L);
		UnityEngine_PlayModeWrap.Register(L);
		UnityEngine_WrapModeWrap.Register(L);
		UnityEngine_QualitySettingsWrap.Register(L);
		UnityEngine_RenderSettingsWrap.Register(L);
		UnityEngine_RectTransformWrap.Register(L);
		L.BeginModule("UI");
		UnityEngine_UI_TextWrap.Register(L);
		L.EndModule();
		L.BeginModule("Experimental");
		L.BeginModule("Director");
		UnityEngine_Experimental_Director_DirectorPlayerWrap.Register(L);
		L.EndModule();
		L.EndModule();
		L.BeginModule("Events");
		L.RegFunction("UnityAction", new LuaCSFunction(LuaBinder.UnityEngine_Events_UnityAction));
		L.EndModule();
		L.EndModule();
		L.BeginModule("LuaFramework");
		LuaFramework_UtilWrap.Register(L);
		LuaFramework_AppConstWrap.Register(L);
		LuaFramework_LuaHelperWrap.Register(L);
		LuaFramework_ByteBufferWrap.Register(L);
		LuaFramework_LuaBehaviourWrap.Register(L);
		LuaFramework_LuaManagerWrap.Register(L);
		LuaFramework_PanelManagerWrap.Register(L);
		LuaFramework_SoundManagerWrap.Register(L);
		LuaFramework_TimerManagerWrap.Register(L);
		LuaFramework_NetworkManagerWrap.Register(L);
		L.EndModule();
		L.BeginModule("System");
		L.RegFunction("Action", new LuaCSFunction(LuaBinder.System_Action));
		L.RegFunction("Action_UnityEngine_Objects", new LuaCSFunction(LuaBinder.System_Action_UnityEngine_Objects));
		L.EndModule();
		L.EndModule();
		L.BeginPreLoad();
		L.AddPreLoad("UnityEngine.MeshRenderer", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_MeshRenderer), typeof(MeshRenderer));
		L.AddPreLoad("UnityEngine.ParticleEmitter", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_ParticleEmitter), typeof(ParticleEmitter));
		L.AddPreLoad("UnityEngine.ParticleRenderer", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_ParticleRenderer), typeof(ParticleRenderer));
		L.AddPreLoad("UnityEngine.ParticleAnimator", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_ParticleAnimator), typeof(ParticleAnimator));
		L.AddPreLoad("UnityEngine.BoxCollider", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_BoxCollider), typeof(BoxCollider));
		L.AddPreLoad("UnityEngine.MeshCollider", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_MeshCollider), typeof(MeshCollider));
		L.AddPreLoad("UnityEngine.SphereCollider", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_SphereCollider), typeof(SphereCollider));
		L.AddPreLoad("UnityEngine.CharacterController", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_CharacterController), typeof(CharacterController));
		L.AddPreLoad("UnityEngine.CapsuleCollider", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_CapsuleCollider), typeof(CapsuleCollider));
		L.AddPreLoad("UnityEngine.Animation", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_Animation), typeof(Animation));
		L.AddPreLoad("UnityEngine.AnimationClip", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_AnimationClip), typeof(AnimationClip));
		L.AddPreLoad("UnityEngine.AnimationState", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_AnimationState), typeof(AnimationState));
		L.AddPreLoad("UnityEngine.BlendWeights", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_BlendWeights), typeof(BlendWeights));
		L.AddPreLoad("UnityEngine.RenderTexture", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_RenderTexture), typeof(RenderTexture));
		L.AddPreLoad("UnityEngine.Rigidbody", new LuaCSFunction(LuaBinder.LuaOpen_UnityEngine_Rigidbody), typeof(Rigidbody));
		L.EndPreLoad();
		Debugger.Log("Register lua type cost time: {0}", Time.get_realtimeSinceStartup() - realtimeSinceStartup);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnityEngine_Events_UnityAction(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate ev = DelegateFactory.CreateDelegate(typeof(UnityAction), func);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int System_Action(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate ev = DelegateFactory.CreateDelegate(typeof(Action), func);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int System_Action_UnityEngine_Objects(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate ev = DelegateFactory.CreateDelegate(typeof(Action<Object[]>), func);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_MeshRenderer(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_MeshRendererWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_ParticleEmitter(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_ParticleEmitterWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_ParticleRenderer(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_ParticleRendererWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_ParticleAnimator(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_ParticleAnimatorWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_BoxCollider(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_BoxColliderWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_MeshCollider(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_MeshColliderWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_SphereCollider(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_SphereColliderWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_CharacterController(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_CharacterControllerWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_CapsuleCollider(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_CapsuleColliderWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_Animation(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_AnimationWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_AnimationClip(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_AnimationClipWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_AnimationState(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_AnimationStateWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_BlendWeights(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_BlendWeightsWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_RenderTexture(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_RenderTextureWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LuaOpen_UnityEngine_Rigidbody(IntPtr L)
	{
		int result;
		try
		{
			int top = LuaDLL.lua_gettop(L);
			LuaState luaState = LuaState.Get(L);
			int top2 = luaState.BeginPreModule("UnityEngine");
			UnityEngine_RigidbodyWrap.Register(luaState);
			luaState.EndPreModule(top2);
			LuaDLL.lua_settop(L, top);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
