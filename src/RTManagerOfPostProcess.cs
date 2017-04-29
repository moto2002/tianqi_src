using System;
using System.Collections.Generic;
using UnityEngine;

public class RTManagerOfPostProcess : MonoBehaviour
{
	public class EventNames
	{
		public const string ENABLE_POSTPROCESS_TYPE = "RTManager.ENABLE_POSTPROCESS_TYPE";
	}

	public enum PostProcessType
	{
		None,
		MobileBlurBlurry
	}

	private RenderTexture m_RTPostProcess;

	private Camera CullingCamera;

	public static RTManagerOfPostProcess Instance;

	private List<RTManagerOfPostProcess.PostProcessType> m_listOn = new List<RTManagerOfPostProcess.PostProcessType>();

	public RenderTexture RTPostProcess
	{
		get
		{
			if (this.m_RTPostProcess == null)
			{
				Camera cameraMain = CamerasMgr.CameraMain;
				Camera cullingCamera = this.CullingCamera;
				int pixelWidth = cameraMain.get_pixelWidth();
				int pixelHeight = cameraMain.get_pixelHeight();
				UIConst.GetRealScreenSize(out pixelWidth, out pixelHeight);
				ShaderEffectUtils.SafeCreateRenderTexture(ref this.m_RTPostProcess, "Buffer2RTCommon", 16, 7, 1, pixelWidth, pixelHeight);
				cullingCamera.set_targetTexture(this.m_RTPostProcess);
			}
			return this.m_RTPostProcess;
		}
	}

	private void SetCullingCamera()
	{
		Camera cameraMain = CamerasMgr.CameraMain;
		if (this.CullingCamera == null)
		{
			string text = "Camera2MotionBlurCulling";
			GameObject gameObject = GameObject.Find(text);
			if (null == gameObject)
			{
				this.CullingCamera = new GameObject(text, new Type[]
				{
					typeof(Camera)
				}).GetComponent<Camera>();
			}
			else
			{
				this.CullingCamera = gameObject.GetComponent<Camera>();
			}
			this.CullingCamera.get_transform().set_parent(cameraMain.get_transform());
			this.CullingCamera.CopyFrom(cameraMain);
			this.CullingCamera.set_hideFlags(52);
			this.CullingCamera.get_transform().set_localPosition(Vector3.get_zero());
			this.CullingCamera.get_transform().set_localRotation(Quaternion.get_identity());
			this.CullingCamera.get_transform().set_localScale(Vector3.get_one());
			this.CullingCamera.set_backgroundColor(Color.get_black());
			this.CullingCamera.set_depthTextureMode(0);
			this.CullingCamera.set_clearFlags(1);
			this.CullingCamera.set_depth(cameraMain.get_depth() - 1f);
			this.CullingCamera.set_cullingMask(this.GetExcludeLayers());
			this.CullingCamera.set_targetTexture(this.RTPostProcess);
		}
		this.CullingCamera.set_enabled(true);
	}

	private int GetExcludeLayers()
	{
		return Utils.GetCullingMask(4);
	}

	private void Awake()
	{
		RTManagerOfPostProcess.Instance = this;
		EventDispatcher.AddListener<bool, RTManagerOfPostProcess.PostProcessType>("RTManager.ENABLE_POSTPROCESS_TYPE", new Callback<bool, RTManagerOfPostProcess.PostProcessType>(this.SetPostProcessType));
	}

	private void SetPostProcessType(bool bOn, RTManagerOfPostProcess.PostProcessType type)
	{
		if (bOn)
		{
			if (!this.m_listOn.Contains(type))
			{
				this.m_listOn.Add(type);
			}
		}
		else
		{
			this.m_listOn.Remove(type);
		}
		if (this.m_listOn.get_Count() > 0)
		{
			this.SetCullingCamera();
			this.CullingCamera.set_enabled(true);
		}
		else
		{
			this.CullingCamera.set_enabled(false);
		}
	}
}
