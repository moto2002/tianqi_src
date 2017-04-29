using System;
using UnityEngine;
using UnityEngine.UI;

public class RTSyncBackground : MonoBehaviour
{
	private static GameObject RTCanvas;

	private GameObject m_goSync;

	private Image m_thisImage;

	private RawImage m_thisRawImage;

	private Image m_syncImage;

	private RawImage m_syncRawImage;

	private static Material _BackgroundMat;

	public static Material BackgroundMat
	{
		get
		{
			if (RTSyncBackground._BackgroundMat == null)
			{
				Shader shader = ShaderManager.Find("Hsh(Mobile)/FX/ParticleAlphaBlended");
				RTSyncBackground._BackgroundMat = new Material(shader);
			}
			return RTSyncBackground._BackgroundMat;
		}
	}

	private void Awake()
	{
		this.Create();
	}

	private void OnEnable()
	{
		this.Create();
	}

	private void OnDisable()
	{
		this.Release();
	}

	private void Update()
	{
		this.SyncImage();
	}

	private void SyncImage()
	{
		if (this.m_thisImage != null && this.m_syncImage != null)
		{
			this.m_syncImage.set_sprite(this.m_thisImage.get_sprite());
			return;
		}
		if (this.m_thisRawImage != null && this.m_syncRawImage != null)
		{
			RTManager.SetRT(this.m_syncRawImage, this.m_thisRawImage.get_texture());
			return;
		}
		if (this.m_syncImage != null)
		{
			ResourceManager.SetSprite(this.m_syncImage, ResourceManagerBase.GetNullSprite());
		}
		if (this.m_syncRawImage != null)
		{
			RTManager.SetRT(this.m_syncRawImage, ResourceManagerBase.GetNullTexture());
		}
	}

	public void Create()
	{
		if (CamerasMgr.Camera2RTCommon == null)
		{
			base.set_enabled(false);
			return;
		}
		if (RTSyncBackground.RTCanvas == null)
		{
			RTSyncBackground.RTCanvas = new GameObject("RTCanvas");
			RTSyncBackground.RTCanvas.get_transform().set_parent(CamerasMgr.Camera2RTCommon.get_transform());
			RTSyncBackground.RTCanvas.get_transform().set_localPosition(Vector3.get_zero());
			RTSyncBackground.RTCanvas.get_transform().set_localRotation(Quaternion.get_identity());
			RTSyncBackground.RTCanvas.get_transform().set_localScale(Vector3.get_one());
			RTSyncBackground.RTCanvas.set_layer(LayerSystem.NameToLayer("FX"));
			Canvas canvas = RTSyncBackground.RTCanvas.AddComponent<Canvas>();
			canvas.set_renderMode(1);
			canvas.set_pixelPerfect(false);
			canvas.set_worldCamera(CamerasMgr.Camera2RTCommon);
			CanvasScaler canvasScaler = RTSyncBackground.RTCanvas.AddComponent<CanvasScaler>();
			canvasScaler.set_uiScaleMode(0);
		}
		if (this.m_goSync != null)
		{
			this.m_goSync.SetActive(true);
			this.SyncImage();
		}
		else
		{
			this.m_goSync = new GameObject(base.get_gameObject().get_name());
			this.m_goSync.get_transform().set_parent(RTSyncBackground.RTCanvas.get_transform());
			this.m_goSync.get_transform().set_localPosition(Vector3.get_zero());
			this.m_goSync.get_transform().set_localRotation(Quaternion.get_identity());
			this.m_goSync.get_transform().set_localScale(Vector3.get_one());
			this.m_goSync.set_layer(LayerSystem.NameToLayer("FX"));
			this.m_thisImage = base.get_gameObject().GetComponent<Image>();
			if (this.m_thisImage != null)
			{
				this.m_syncImage = this.m_goSync.AddComponent<Image>();
				this.m_syncImage.set_material(RTSyncBackground.BackgroundMat);
			}
			else
			{
				this.m_thisRawImage = base.get_gameObject().GetComponent<RawImage>();
				if (this.m_thisRawImage != null)
				{
					this.m_syncRawImage = this.m_goSync.AddComponent<RawImage>();
					this.m_syncRawImage.set_material(RTSyncBackground.BackgroundMat);
				}
			}
			this.SyncImage();
			if (base.get_gameObject().GetComponent<UIMaskLayer>() != null)
			{
				this.m_goSync.AddComponent<UIMaskLayer>();
			}
		}
	}

	public void Release()
	{
		if (this.m_goSync != null)
		{
			Object.Destroy(this.m_goSync);
			this.m_goSync = null;
		}
	}
}
