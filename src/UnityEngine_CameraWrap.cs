using LuaInterface;
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class UnityEngine_CameraWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Camera), typeof(Behaviour), null);
		L.RegFunction("SetTargetBuffers", new LuaCSFunction(UnityEngine_CameraWrap.SetTargetBuffers));
		L.RegFunction("ResetWorldToCameraMatrix", new LuaCSFunction(UnityEngine_CameraWrap.ResetWorldToCameraMatrix));
		L.RegFunction("ResetProjectionMatrix", new LuaCSFunction(UnityEngine_CameraWrap.ResetProjectionMatrix));
		L.RegFunction("ResetAspect", new LuaCSFunction(UnityEngine_CameraWrap.ResetAspect));
		L.RegFunction("ResetFieldOfView", new LuaCSFunction(UnityEngine_CameraWrap.ResetFieldOfView));
		L.RegFunction("SetStereoViewMatrices", new LuaCSFunction(UnityEngine_CameraWrap.SetStereoViewMatrices));
		L.RegFunction("ResetStereoViewMatrices", new LuaCSFunction(UnityEngine_CameraWrap.ResetStereoViewMatrices));
		L.RegFunction("SetStereoProjectionMatrices", new LuaCSFunction(UnityEngine_CameraWrap.SetStereoProjectionMatrices));
		L.RegFunction("ResetStereoProjectionMatrices", new LuaCSFunction(UnityEngine_CameraWrap.ResetStereoProjectionMatrices));
		L.RegFunction("WorldToScreenPoint", new LuaCSFunction(UnityEngine_CameraWrap.WorldToScreenPoint));
		L.RegFunction("WorldToViewportPoint", new LuaCSFunction(UnityEngine_CameraWrap.WorldToViewportPoint));
		L.RegFunction("ViewportToWorldPoint", new LuaCSFunction(UnityEngine_CameraWrap.ViewportToWorldPoint));
		L.RegFunction("ScreenToWorldPoint", new LuaCSFunction(UnityEngine_CameraWrap.ScreenToWorldPoint));
		L.RegFunction("ScreenToViewportPoint", new LuaCSFunction(UnityEngine_CameraWrap.ScreenToViewportPoint));
		L.RegFunction("ViewportToScreenPoint", new LuaCSFunction(UnityEngine_CameraWrap.ViewportToScreenPoint));
		L.RegFunction("ViewportPointToRay", new LuaCSFunction(UnityEngine_CameraWrap.ViewportPointToRay));
		L.RegFunction("ScreenPointToRay", new LuaCSFunction(UnityEngine_CameraWrap.ScreenPointToRay));
		L.RegFunction("GetAllCameras", new LuaCSFunction(UnityEngine_CameraWrap.GetAllCameras));
		L.RegFunction("Render", new LuaCSFunction(UnityEngine_CameraWrap.Render));
		L.RegFunction("RenderWithShader", new LuaCSFunction(UnityEngine_CameraWrap.RenderWithShader));
		L.RegFunction("SetReplacementShader", new LuaCSFunction(UnityEngine_CameraWrap.SetReplacementShader));
		L.RegFunction("ResetReplacementShader", new LuaCSFunction(UnityEngine_CameraWrap.ResetReplacementShader));
		L.RegFunction("RenderDontRestore", new LuaCSFunction(UnityEngine_CameraWrap.RenderDontRestore));
		L.RegFunction("SetupCurrent", new LuaCSFunction(UnityEngine_CameraWrap.SetupCurrent));
		L.RegFunction("RenderToCubemap", new LuaCSFunction(UnityEngine_CameraWrap.RenderToCubemap));
		L.RegFunction("CopyFrom", new LuaCSFunction(UnityEngine_CameraWrap.CopyFrom));
		L.RegFunction("AddCommandBuffer", new LuaCSFunction(UnityEngine_CameraWrap.AddCommandBuffer));
		L.RegFunction("RemoveCommandBuffer", new LuaCSFunction(UnityEngine_CameraWrap.RemoveCommandBuffer));
		L.RegFunction("RemoveCommandBuffers", new LuaCSFunction(UnityEngine_CameraWrap.RemoveCommandBuffers));
		L.RegFunction("RemoveAllCommandBuffers", new LuaCSFunction(UnityEngine_CameraWrap.RemoveAllCommandBuffers));
		L.RegFunction("GetCommandBuffers", new LuaCSFunction(UnityEngine_CameraWrap.GetCommandBuffers));
		L.RegFunction("CalculateObliqueMatrix", new LuaCSFunction(UnityEngine_CameraWrap.CalculateObliqueMatrix));
		L.RegFunction("New", new LuaCSFunction(UnityEngine_CameraWrap._CreateUnityEngine_Camera));
		L.RegFunction("__eq", new LuaCSFunction(UnityEngine_CameraWrap.op_Equality));
		L.RegFunction("__tostring", new LuaCSFunction(UnityEngine_CameraWrap.Lua_ToString));
		L.RegVar("onPreCull", new LuaCSFunction(UnityEngine_CameraWrap.get_onPreCull), new LuaCSFunction(UnityEngine_CameraWrap.set_onPreCull));
		L.RegVar("onPreRender", new LuaCSFunction(UnityEngine_CameraWrap.get_onPreRender), new LuaCSFunction(UnityEngine_CameraWrap.set_onPreRender));
		L.RegVar("onPostRender", new LuaCSFunction(UnityEngine_CameraWrap.get_onPostRender), new LuaCSFunction(UnityEngine_CameraWrap.set_onPostRender));
		L.RegVar("fieldOfView", new LuaCSFunction(UnityEngine_CameraWrap.get_fieldOfView), new LuaCSFunction(UnityEngine_CameraWrap.set_fieldOfView));
		L.RegVar("nearClipPlane", new LuaCSFunction(UnityEngine_CameraWrap.get_nearClipPlane), new LuaCSFunction(UnityEngine_CameraWrap.set_nearClipPlane));
		L.RegVar("farClipPlane", new LuaCSFunction(UnityEngine_CameraWrap.get_farClipPlane), new LuaCSFunction(UnityEngine_CameraWrap.set_farClipPlane));
		L.RegVar("renderingPath", new LuaCSFunction(UnityEngine_CameraWrap.get_renderingPath), new LuaCSFunction(UnityEngine_CameraWrap.set_renderingPath));
		L.RegVar("actualRenderingPath", new LuaCSFunction(UnityEngine_CameraWrap.get_actualRenderingPath), null);
		L.RegVar("hdr", new LuaCSFunction(UnityEngine_CameraWrap.get_hdr), new LuaCSFunction(UnityEngine_CameraWrap.set_hdr));
		L.RegVar("orthographicSize", new LuaCSFunction(UnityEngine_CameraWrap.get_orthographicSize), new LuaCSFunction(UnityEngine_CameraWrap.set_orthographicSize));
		L.RegVar("orthographic", new LuaCSFunction(UnityEngine_CameraWrap.get_orthographic), new LuaCSFunction(UnityEngine_CameraWrap.set_orthographic));
		L.RegVar("opaqueSortMode", new LuaCSFunction(UnityEngine_CameraWrap.get_opaqueSortMode), new LuaCSFunction(UnityEngine_CameraWrap.set_opaqueSortMode));
		L.RegVar("transparencySortMode", new LuaCSFunction(UnityEngine_CameraWrap.get_transparencySortMode), new LuaCSFunction(UnityEngine_CameraWrap.set_transparencySortMode));
		L.RegVar("depth", new LuaCSFunction(UnityEngine_CameraWrap.get_depth), new LuaCSFunction(UnityEngine_CameraWrap.set_depth));
		L.RegVar("aspect", new LuaCSFunction(UnityEngine_CameraWrap.get_aspect), new LuaCSFunction(UnityEngine_CameraWrap.set_aspect));
		L.RegVar("cullingMask", new LuaCSFunction(UnityEngine_CameraWrap.get_cullingMask), new LuaCSFunction(UnityEngine_CameraWrap.set_cullingMask));
		L.RegVar("eventMask", new LuaCSFunction(UnityEngine_CameraWrap.get_eventMask), new LuaCSFunction(UnityEngine_CameraWrap.set_eventMask));
		L.RegVar("backgroundColor", new LuaCSFunction(UnityEngine_CameraWrap.get_backgroundColor), new LuaCSFunction(UnityEngine_CameraWrap.set_backgroundColor));
		L.RegVar("rect", new LuaCSFunction(UnityEngine_CameraWrap.get_rect), new LuaCSFunction(UnityEngine_CameraWrap.set_rect));
		L.RegVar("pixelRect", new LuaCSFunction(UnityEngine_CameraWrap.get_pixelRect), new LuaCSFunction(UnityEngine_CameraWrap.set_pixelRect));
		L.RegVar("targetTexture", new LuaCSFunction(UnityEngine_CameraWrap.get_targetTexture), new LuaCSFunction(UnityEngine_CameraWrap.set_targetTexture));
		L.RegVar("pixelWidth", new LuaCSFunction(UnityEngine_CameraWrap.get_pixelWidth), null);
		L.RegVar("pixelHeight", new LuaCSFunction(UnityEngine_CameraWrap.get_pixelHeight), null);
		L.RegVar("cameraToWorldMatrix", new LuaCSFunction(UnityEngine_CameraWrap.get_cameraToWorldMatrix), null);
		L.RegVar("worldToCameraMatrix", new LuaCSFunction(UnityEngine_CameraWrap.get_worldToCameraMatrix), new LuaCSFunction(UnityEngine_CameraWrap.set_worldToCameraMatrix));
		L.RegVar("projectionMatrix", new LuaCSFunction(UnityEngine_CameraWrap.get_projectionMatrix), new LuaCSFunction(UnityEngine_CameraWrap.set_projectionMatrix));
		L.RegVar("velocity", new LuaCSFunction(UnityEngine_CameraWrap.get_velocity), null);
		L.RegVar("clearFlags", new LuaCSFunction(UnityEngine_CameraWrap.get_clearFlags), new LuaCSFunction(UnityEngine_CameraWrap.set_clearFlags));
		L.RegVar("stereoEnabled", new LuaCSFunction(UnityEngine_CameraWrap.get_stereoEnabled), null);
		L.RegVar("stereoSeparation", new LuaCSFunction(UnityEngine_CameraWrap.get_stereoSeparation), new LuaCSFunction(UnityEngine_CameraWrap.set_stereoSeparation));
		L.RegVar("stereoConvergence", new LuaCSFunction(UnityEngine_CameraWrap.get_stereoConvergence), new LuaCSFunction(UnityEngine_CameraWrap.set_stereoConvergence));
		L.RegVar("cameraType", new LuaCSFunction(UnityEngine_CameraWrap.get_cameraType), new LuaCSFunction(UnityEngine_CameraWrap.set_cameraType));
		L.RegVar("stereoMirrorMode", new LuaCSFunction(UnityEngine_CameraWrap.get_stereoMirrorMode), new LuaCSFunction(UnityEngine_CameraWrap.set_stereoMirrorMode));
		L.RegVar("targetDisplay", new LuaCSFunction(UnityEngine_CameraWrap.get_targetDisplay), new LuaCSFunction(UnityEngine_CameraWrap.set_targetDisplay));
		L.RegVar("main", new LuaCSFunction(UnityEngine_CameraWrap.get_main), null);
		L.RegVar("current", new LuaCSFunction(UnityEngine_CameraWrap.get_current), null);
		L.RegVar("allCameras", new LuaCSFunction(UnityEngine_CameraWrap.get_allCameras), null);
		L.RegVar("allCamerasCount", new LuaCSFunction(UnityEngine_CameraWrap.get_allCamerasCount), null);
		L.RegVar("useOcclusionCulling", new LuaCSFunction(UnityEngine_CameraWrap.get_useOcclusionCulling), new LuaCSFunction(UnityEngine_CameraWrap.set_useOcclusionCulling));
		L.RegVar("layerCullDistances", new LuaCSFunction(UnityEngine_CameraWrap.get_layerCullDistances), new LuaCSFunction(UnityEngine_CameraWrap.set_layerCullDistances));
		L.RegVar("layerCullSpherical", new LuaCSFunction(UnityEngine_CameraWrap.get_layerCullSpherical), new LuaCSFunction(UnityEngine_CameraWrap.set_layerCullSpherical));
		L.RegVar("depthTextureMode", new LuaCSFunction(UnityEngine_CameraWrap.get_depthTextureMode), new LuaCSFunction(UnityEngine_CameraWrap.set_depthTextureMode));
		L.RegVar("clearStencilAfterLightingPass", new LuaCSFunction(UnityEngine_CameraWrap.get_clearStencilAfterLightingPass), new LuaCSFunction(UnityEngine_CameraWrap.set_clearStencilAfterLightingPass));
		L.RegVar("commandBufferCount", new LuaCSFunction(UnityEngine_CameraWrap.get_commandBufferCount), null);
		L.RegFunction("CameraCallback", new LuaCSFunction(UnityEngine_CameraWrap.UnityEngine_Camera_CameraCallback));
		L.EndClass();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateUnityEngine_Camera(IntPtr L)
	{
		int result;
		try
		{
			if (LuaDLL.lua_gettop(L) == 0)
			{
				Camera obj = new Camera();
				ToLua.Push(L, obj);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.Camera.New");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetTargetBuffers(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Camera), typeof(RenderBuffer[]), typeof(RenderBuffer)))
			{
				Camera camera = (Camera)ToLua.ToObject(L, 1);
				RenderBuffer[] array = ToLua.CheckObjectArray<RenderBuffer>(L, 2);
				RenderBuffer renderBuffer = (RenderBuffer)ToLua.ToObject(L, 3);
				camera.SetTargetBuffers(array, renderBuffer);
				result = 0;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Camera), typeof(RenderBuffer), typeof(RenderBuffer)))
			{
				Camera camera2 = (Camera)ToLua.ToObject(L, 1);
				RenderBuffer renderBuffer2 = (RenderBuffer)ToLua.ToObject(L, 2);
				RenderBuffer renderBuffer3 = (RenderBuffer)ToLua.ToObject(L, 3);
				camera2.SetTargetBuffers(renderBuffer2, renderBuffer3);
				result = 0;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Camera.SetTargetBuffers");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResetWorldToCameraMatrix(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			camera.ResetWorldToCameraMatrix();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResetProjectionMatrix(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			camera.ResetProjectionMatrix();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResetAspect(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			camera.ResetAspect();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResetFieldOfView(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			camera.ResetFieldOfView();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetStereoViewMatrices(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Matrix4x4 matrix4x = (Matrix4x4)ToLua.CheckObject(L, 2, typeof(Matrix4x4));
			Matrix4x4 matrix4x2 = (Matrix4x4)ToLua.CheckObject(L, 3, typeof(Matrix4x4));
			camera.SetStereoViewMatrices(matrix4x, matrix4x2);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResetStereoViewMatrices(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			camera.ResetStereoViewMatrices();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetStereoProjectionMatrices(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Matrix4x4 matrix4x = (Matrix4x4)ToLua.CheckObject(L, 2, typeof(Matrix4x4));
			Matrix4x4 matrix4x2 = (Matrix4x4)ToLua.CheckObject(L, 3, typeof(Matrix4x4));
			camera.SetStereoProjectionMatrices(matrix4x, matrix4x2);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResetStereoProjectionMatrices(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			camera.ResetStereoProjectionMatrices();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WorldToScreenPoint(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Vector3 v = camera.WorldToScreenPoint(vector);
			ToLua.Push(L, v);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WorldToViewportPoint(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Vector3 v = camera.WorldToViewportPoint(vector);
			ToLua.Push(L, v);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ViewportToWorldPoint(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Vector3 v = camera.ViewportToWorldPoint(vector);
			ToLua.Push(L, v);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ScreenToWorldPoint(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Vector3 v = camera.ScreenToWorldPoint(vector);
			ToLua.Push(L, v);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ScreenToViewportPoint(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Vector3 v = camera.ScreenToViewportPoint(vector);
			ToLua.Push(L, v);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ViewportToScreenPoint(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Vector3 v = camera.ViewportToScreenPoint(vector);
			ToLua.Push(L, v);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ViewportPointToRay(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Ray ray = camera.ViewportPointToRay(vector);
			ToLua.Push(L, ray);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ScreenPointToRay(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Vector3 vector = ToLua.ToVector3(L, 2);
			Ray ray = camera.ScreenPointToRay(vector);
			ToLua.Push(L, ray);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAllCameras(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera[] array = ToLua.CheckObjectArray<Camera>(L, 1);
			int allCameras = Camera.GetAllCameras(array);
			LuaDLL.lua_pushinteger(L, allCameras);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Render(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			camera.Render();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RenderWithShader(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Shader shader = (Shader)ToLua.CheckUnityObject(L, 2, typeof(Shader));
			string text = ToLua.CheckString(L, 3);
			camera.RenderWithShader(shader, text);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetReplacementShader(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Shader shader = (Shader)ToLua.CheckUnityObject(L, 2, typeof(Shader));
			string text = ToLua.CheckString(L, 3);
			camera.SetReplacementShader(shader, text);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ResetReplacementShader(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			camera.ResetReplacementShader();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RenderDontRestore(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			camera.RenderDontRestore();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetupCurrent(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckUnityObject(L, 1, typeof(Camera));
			Camera.SetupCurrent(camera);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RenderToCubemap(IntPtr L)
	{
		int result;
		try
		{
			int num = LuaDLL.lua_gettop(L);
			if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Camera), typeof(RenderTexture)))
			{
				Camera camera = (Camera)ToLua.ToObject(L, 1);
				RenderTexture renderTexture = (RenderTexture)ToLua.ToObject(L, 2);
				bool value = camera.RenderToCubemap(renderTexture);
				LuaDLL.lua_pushboolean(L, value);
				result = 1;
			}
			else if (num == 2 && TypeChecker.CheckTypes(L, 1, typeof(Camera), typeof(Cubemap)))
			{
				Camera camera2 = (Camera)ToLua.ToObject(L, 1);
				Cubemap cubemap = (Cubemap)ToLua.ToObject(L, 2);
				bool value2 = camera2.RenderToCubemap(cubemap);
				LuaDLL.lua_pushboolean(L, value2);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Camera), typeof(RenderTexture), typeof(int)))
			{
				Camera camera3 = (Camera)ToLua.ToObject(L, 1);
				RenderTexture renderTexture2 = (RenderTexture)ToLua.ToObject(L, 2);
				int num2 = (int)LuaDLL.lua_tonumber(L, 3);
				bool value3 = camera3.RenderToCubemap(renderTexture2, num2);
				LuaDLL.lua_pushboolean(L, value3);
				result = 1;
			}
			else if (num == 3 && TypeChecker.CheckTypes(L, 1, typeof(Camera), typeof(Cubemap), typeof(int)))
			{
				Camera camera4 = (Camera)ToLua.ToObject(L, 1);
				Cubemap cubemap2 = (Cubemap)ToLua.ToObject(L, 2);
				int num3 = (int)LuaDLL.lua_tonumber(L, 3);
				bool value4 = camera4.RenderToCubemap(cubemap2, num3);
				LuaDLL.lua_pushboolean(L, value4);
				result = 1;
			}
			else
			{
				result = LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.Camera.RenderToCubemap");
			}
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CopyFrom(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Camera camera2 = (Camera)ToLua.CheckUnityObject(L, 2, typeof(Camera));
			camera.CopyFrom(camera2);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddCommandBuffer(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			CameraEvent cameraEvent = (int)ToLua.CheckObject(L, 2, typeof(CameraEvent));
			CommandBuffer commandBuffer = (CommandBuffer)ToLua.CheckObject(L, 3, typeof(CommandBuffer));
			camera.AddCommandBuffer(cameraEvent, commandBuffer);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveCommandBuffer(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			CameraEvent cameraEvent = (int)ToLua.CheckObject(L, 2, typeof(CameraEvent));
			CommandBuffer commandBuffer = (CommandBuffer)ToLua.CheckObject(L, 3, typeof(CommandBuffer));
			camera.RemoveCommandBuffer(cameraEvent, commandBuffer);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveCommandBuffers(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			CameraEvent cameraEvent = (int)ToLua.CheckObject(L, 2, typeof(CameraEvent));
			camera.RemoveCommandBuffers(cameraEvent);
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveAllCommandBuffers(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			camera.RemoveAllCommandBuffers();
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetCommandBuffers(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			CameraEvent cameraEvent = (int)ToLua.CheckObject(L, 2, typeof(CameraEvent));
			CommandBuffer[] commandBuffers = camera.GetCommandBuffers(cameraEvent);
			ToLua.Push(L, commandBuffers);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CalculateObliqueMatrix(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Camera camera = (Camera)ToLua.CheckObject(L, 1, typeof(Camera));
			Vector4 vector = ToLua.ToVector4(L, 2);
			Matrix4x4 matrix4x = camera.CalculateObliqueMatrix(vector);
			ToLua.PushValue(L, matrix4x);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int op_Equality(IntPtr L)
	{
		int result;
		try
		{
			ToLua.CheckArgsCount(L, 2);
			Object @object = (Object)ToLua.ToObject(L, 1);
			Object object2 = (Object)ToLua.ToObject(L, 2);
			bool value = @object == object2;
			LuaDLL.lua_pushboolean(L, value);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_ToString(IntPtr L)
	{
		object obj = ToLua.ToObject(L, 1);
		if (obj != null)
		{
			LuaDLL.lua_pushstring(L, obj.ToString());
		}
		else
		{
			LuaDLL.lua_pushnil(L);
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_onPreCull(IntPtr L)
	{
		ToLua.Push(L, Camera.onPreCull);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_onPreRender(IntPtr L)
	{
		ToLua.Push(L, Camera.onPreRender);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_onPostRender(IntPtr L)
	{
		ToLua.Push(L, Camera.onPostRender);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fieldOfView(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float fieldOfView = camera.get_fieldOfView();
			LuaDLL.lua_pushnumber(L, (double)fieldOfView);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index fieldOfView on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_nearClipPlane(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float nearClipPlane = camera.get_nearClipPlane();
			LuaDLL.lua_pushnumber(L, (double)nearClipPlane);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index nearClipPlane on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_farClipPlane(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float farClipPlane = camera.get_farClipPlane();
			LuaDLL.lua_pushnumber(L, (double)farClipPlane);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index farClipPlane on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_renderingPath(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			RenderingPath renderingPath = camera.get_renderingPath();
			ToLua.Push(L, renderingPath);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index renderingPath on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_actualRenderingPath(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			RenderingPath actualRenderingPath = camera.get_actualRenderingPath();
			ToLua.Push(L, actualRenderingPath);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index actualRenderingPath on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_hdr(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool hdr = camera.get_hdr();
			LuaDLL.lua_pushboolean(L, hdr);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index hdr on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_orthographicSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float orthographicSize = camera.get_orthographicSize();
			LuaDLL.lua_pushnumber(L, (double)orthographicSize);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index orthographicSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_orthographic(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool orthographic = camera.get_orthographic();
			LuaDLL.lua_pushboolean(L, orthographic);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index orthographic on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_opaqueSortMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			OpaqueSortMode opaqueSortMode = camera.get_opaqueSortMode();
			ToLua.Push(L, opaqueSortMode);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index opaqueSortMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_transparencySortMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			TransparencySortMode transparencySortMode = camera.get_transparencySortMode();
			ToLua.Push(L, transparencySortMode);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index transparencySortMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_depth(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float depth = camera.get_depth();
			LuaDLL.lua_pushnumber(L, (double)depth);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index depth on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_aspect(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float aspect = camera.get_aspect();
			LuaDLL.lua_pushnumber(L, (double)aspect);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index aspect on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cullingMask(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			int cullingMask = camera.get_cullingMask();
			LuaDLL.lua_pushinteger(L, cullingMask);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index cullingMask on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_eventMask(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			int eventMask = camera.get_eventMask();
			LuaDLL.lua_pushinteger(L, eventMask);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index eventMask on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_backgroundColor(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Color backgroundColor = camera.get_backgroundColor();
			ToLua.Push(L, backgroundColor);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index backgroundColor on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rect(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Rect rect = camera.get_rect();
			ToLua.PushValue(L, rect);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rect on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pixelRect(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Rect pixelRect = camera.get_pixelRect();
			ToLua.PushValue(L, pixelRect);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index pixelRect on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_targetTexture(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			RenderTexture targetTexture = camera.get_targetTexture();
			ToLua.Push(L, targetTexture);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index targetTexture on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pixelWidth(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			int pixelWidth = camera.get_pixelWidth();
			LuaDLL.lua_pushinteger(L, pixelWidth);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index pixelWidth on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pixelHeight(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			int pixelHeight = camera.get_pixelHeight();
			LuaDLL.lua_pushinteger(L, pixelHeight);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index pixelHeight on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cameraToWorldMatrix(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Matrix4x4 cameraToWorldMatrix = camera.get_cameraToWorldMatrix();
			ToLua.PushValue(L, cameraToWorldMatrix);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index cameraToWorldMatrix on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldToCameraMatrix(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Matrix4x4 worldToCameraMatrix = camera.get_worldToCameraMatrix();
			ToLua.PushValue(L, worldToCameraMatrix);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index worldToCameraMatrix on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_projectionMatrix(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Matrix4x4 projectionMatrix = camera.get_projectionMatrix();
			ToLua.PushValue(L, projectionMatrix);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index projectionMatrix on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_velocity(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Vector3 velocity = camera.get_velocity();
			ToLua.Push(L, velocity);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index velocity on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clearFlags(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			CameraClearFlags clearFlags = camera.get_clearFlags();
			ToLua.Push(L, clearFlags);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index clearFlags on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_stereoEnabled(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool stereoEnabled = camera.get_stereoEnabled();
			LuaDLL.lua_pushboolean(L, stereoEnabled);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index stereoEnabled on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_stereoSeparation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float stereoSeparation = camera.get_stereoSeparation();
			LuaDLL.lua_pushnumber(L, (double)stereoSeparation);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index stereoSeparation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_stereoConvergence(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float stereoConvergence = camera.get_stereoConvergence();
			LuaDLL.lua_pushnumber(L, (double)stereoConvergence);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index stereoConvergence on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cameraType(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			CameraType cameraType = camera.get_cameraType();
			ToLua.Push(L, cameraType);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index cameraType on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_stereoMirrorMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool stereoMirrorMode = camera.get_stereoMirrorMode();
			LuaDLL.lua_pushboolean(L, stereoMirrorMode);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index stereoMirrorMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_targetDisplay(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			int targetDisplay = camera.get_targetDisplay();
			LuaDLL.lua_pushinteger(L, targetDisplay);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index targetDisplay on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_main(IntPtr L)
	{
		ToLua.Push(L, Camera.get_main());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_current(IntPtr L)
	{
		ToLua.Push(L, Camera.get_current());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_allCameras(IntPtr L)
	{
		ToLua.Push(L, Camera.get_allCameras());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_allCamerasCount(IntPtr L)
	{
		LuaDLL.lua_pushinteger(L, Camera.get_allCamerasCount());
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useOcclusionCulling(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool useOcclusionCulling = camera.get_useOcclusionCulling();
			LuaDLL.lua_pushboolean(L, useOcclusionCulling);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useOcclusionCulling on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_layerCullDistances(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float[] layerCullDistances = camera.get_layerCullDistances();
			ToLua.Push(L, layerCullDistances);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index layerCullDistances on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_layerCullSpherical(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool layerCullSpherical = camera.get_layerCullSpherical();
			LuaDLL.lua_pushboolean(L, layerCullSpherical);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index layerCullSpherical on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_depthTextureMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			DepthTextureMode depthTextureMode = camera.get_depthTextureMode();
			ToLua.Push(L, depthTextureMode);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index depthTextureMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clearStencilAfterLightingPass(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool clearStencilAfterLightingPass = camera.get_clearStencilAfterLightingPass();
			LuaDLL.lua_pushboolean(L, clearStencilAfterLightingPass);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index clearStencilAfterLightingPass on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_commandBufferCount(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			int commandBufferCount = camera.get_commandBufferCount();
			LuaDLL.lua_pushinteger(L, commandBufferCount);
			result = 1;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index commandBufferCount on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_onPreCull(IntPtr L)
	{
		int result;
		try
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 2);
			Camera.CameraCallback onPreCull;
			if (luaTypes != LuaTypes.LUA_TFUNCTION)
			{
				onPreCull = (Camera.CameraCallback)ToLua.CheckObject(L, 2, typeof(Camera.CameraCallback));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				onPreCull = (DelegateFactory.CreateDelegate(typeof(Camera.CameraCallback), func) as Camera.CameraCallback);
			}
			Camera.onPreCull = onPreCull;
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_onPreRender(IntPtr L)
	{
		int result;
		try
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 2);
			Camera.CameraCallback onPreRender;
			if (luaTypes != LuaTypes.LUA_TFUNCTION)
			{
				onPreRender = (Camera.CameraCallback)ToLua.CheckObject(L, 2, typeof(Camera.CameraCallback));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				onPreRender = (DelegateFactory.CreateDelegate(typeof(Camera.CameraCallback), func) as Camera.CameraCallback);
			}
			Camera.onPreRender = onPreRender;
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_onPostRender(IntPtr L)
	{
		int result;
		try
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 2);
			Camera.CameraCallback onPostRender;
			if (luaTypes != LuaTypes.LUA_TFUNCTION)
			{
				onPostRender = (Camera.CameraCallback)ToLua.CheckObject(L, 2, typeof(Camera.CameraCallback));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				onPostRender = (DelegateFactory.CreateDelegate(typeof(Camera.CameraCallback), func) as Camera.CameraCallback);
			}
			Camera.onPostRender = onPostRender;
			result = 0;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fieldOfView(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float fieldOfView = (float)LuaDLL.luaL_checknumber(L, 2);
			camera.set_fieldOfView(fieldOfView);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index fieldOfView on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_nearClipPlane(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float nearClipPlane = (float)LuaDLL.luaL_checknumber(L, 2);
			camera.set_nearClipPlane(nearClipPlane);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index nearClipPlane on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_farClipPlane(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float farClipPlane = (float)LuaDLL.luaL_checknumber(L, 2);
			camera.set_farClipPlane(farClipPlane);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index farClipPlane on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_renderingPath(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			RenderingPath renderingPath = (int)ToLua.CheckObject(L, 2, typeof(RenderingPath));
			camera.set_renderingPath(renderingPath);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index renderingPath on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_hdr(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool hdr = LuaDLL.luaL_checkboolean(L, 2);
			camera.set_hdr(hdr);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index hdr on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_orthographicSize(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float orthographicSize = (float)LuaDLL.luaL_checknumber(L, 2);
			camera.set_orthographicSize(orthographicSize);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index orthographicSize on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_orthographic(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool orthographic = LuaDLL.luaL_checkboolean(L, 2);
			camera.set_orthographic(orthographic);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index orthographic on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_opaqueSortMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			OpaqueSortMode opaqueSortMode = (int)ToLua.CheckObject(L, 2, typeof(OpaqueSortMode));
			camera.set_opaqueSortMode(opaqueSortMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index opaqueSortMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_transparencySortMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			TransparencySortMode transparencySortMode = (int)ToLua.CheckObject(L, 2, typeof(TransparencySortMode));
			camera.set_transparencySortMode(transparencySortMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index transparencySortMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_depth(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float depth = (float)LuaDLL.luaL_checknumber(L, 2);
			camera.set_depth(depth);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index depth on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_aspect(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float aspect = (float)LuaDLL.luaL_checknumber(L, 2);
			camera.set_aspect(aspect);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index aspect on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cullingMask(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			int cullingMask = (int)LuaDLL.luaL_checknumber(L, 2);
			camera.set_cullingMask(cullingMask);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index cullingMask on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_eventMask(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			int eventMask = (int)LuaDLL.luaL_checknumber(L, 2);
			camera.set_eventMask(eventMask);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index eventMask on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_backgroundColor(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Color backgroundColor = ToLua.ToColor(L, 2);
			camera.set_backgroundColor(backgroundColor);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index backgroundColor on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rect(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Rect rect = (Rect)ToLua.CheckObject(L, 2, typeof(Rect));
			camera.set_rect(rect);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index rect on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_pixelRect(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Rect pixelRect = (Rect)ToLua.CheckObject(L, 2, typeof(Rect));
			camera.set_pixelRect(pixelRect);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index pixelRect on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_targetTexture(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			RenderTexture targetTexture = (RenderTexture)ToLua.CheckUnityObject(L, 2, typeof(RenderTexture));
			camera.set_targetTexture(targetTexture);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index targetTexture on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_worldToCameraMatrix(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Matrix4x4 worldToCameraMatrix = (Matrix4x4)ToLua.CheckObject(L, 2, typeof(Matrix4x4));
			camera.set_worldToCameraMatrix(worldToCameraMatrix);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index worldToCameraMatrix on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_projectionMatrix(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			Matrix4x4 projectionMatrix = (Matrix4x4)ToLua.CheckObject(L, 2, typeof(Matrix4x4));
			camera.set_projectionMatrix(projectionMatrix);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index projectionMatrix on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_clearFlags(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			CameraClearFlags clearFlags = (int)ToLua.CheckObject(L, 2, typeof(CameraClearFlags));
			camera.set_clearFlags(clearFlags);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index clearFlags on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_stereoSeparation(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float stereoSeparation = (float)LuaDLL.luaL_checknumber(L, 2);
			camera.set_stereoSeparation(stereoSeparation);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index stereoSeparation on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_stereoConvergence(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float stereoConvergence = (float)LuaDLL.luaL_checknumber(L, 2);
			camera.set_stereoConvergence(stereoConvergence);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index stereoConvergence on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cameraType(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			CameraType cameraType = (int)ToLua.CheckObject(L, 2, typeof(CameraType));
			camera.set_cameraType(cameraType);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index cameraType on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_stereoMirrorMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool stereoMirrorMode = LuaDLL.luaL_checkboolean(L, 2);
			camera.set_stereoMirrorMode(stereoMirrorMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index stereoMirrorMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_targetDisplay(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			int targetDisplay = (int)LuaDLL.luaL_checknumber(L, 2);
			camera.set_targetDisplay(targetDisplay);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index targetDisplay on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useOcclusionCulling(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool useOcclusionCulling = LuaDLL.luaL_checkboolean(L, 2);
			camera.set_useOcclusionCulling(useOcclusionCulling);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index useOcclusionCulling on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_layerCullDistances(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			float[] layerCullDistances = ToLua.CheckNumberArray<float>(L, 2);
			camera.set_layerCullDistances(layerCullDistances);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index layerCullDistances on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_layerCullSpherical(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool layerCullSpherical = LuaDLL.luaL_checkboolean(L, 2);
			camera.set_layerCullSpherical(layerCullSpherical);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index layerCullSpherical on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_depthTextureMode(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			DepthTextureMode depthTextureMode = (int)ToLua.CheckObject(L, 2, typeof(DepthTextureMode));
			camera.set_depthTextureMode(depthTextureMode);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index depthTextureMode on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_clearStencilAfterLightingPass(IntPtr L)
	{
		object obj = null;
		int result;
		try
		{
			obj = ToLua.ToObject(L, 1);
			Camera camera = (Camera)obj;
			bool clearStencilAfterLightingPass = LuaDLL.luaL_checkboolean(L, 2);
			camera.set_clearStencilAfterLightingPass(clearStencilAfterLightingPass);
			result = 0;
		}
		catch (Exception ex)
		{
			result = LuaDLL.toluaL_exception(L, ex, (obj != null) ? ex.get_Message() : "attempt to index clearStencilAfterLightingPass on a nil value");
		}
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnityEngine_Camera_CameraCallback(IntPtr L)
	{
		int result;
		try
		{
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);
			Delegate ev = DelegateFactory.CreateDelegate(typeof(Camera.CameraCallback), func);
			ToLua.Push(L, ev);
			result = 1;
		}
		catch (Exception e)
		{
			result = LuaDLL.toluaL_exception(L, e, null);
		}
		return result;
	}
}
