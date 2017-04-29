using System;
using UnityEngine;

public class PlayerOutLine : MonoBehaviour
{
	public Color OutLineColor = Color.get_green();

	private GameObject _cameraGameObject;

	private Camera _camera;

	private Camera _mainCamera;

	public RenderTexture _renderTextureDepth;

	public RenderTexture _renderTextureOcclusion;

	public RenderTexture _renderTextureStretch;

	public Material _materialOcclusion;

	public Material _materialStretch;

	public Material _materialMix;

	public Shader _depthShader;

	private void Start()
	{
		this._mainCamera = Camera.get_main();
		this._mainCamera.set_depthTextureMode(1);
		this._depthShader = ShaderManager.Find("OutLine/Depth");
		this._cameraGameObject = new GameObject();
		this._cameraGameObject.get_transform().set_parent(this._mainCamera.get_transform());
		this._cameraGameObject.get_transform().set_localPosition(Vector3.get_zero());
		this._cameraGameObject.get_transform().set_localScale(Vector3.get_one());
		this._cameraGameObject.get_transform().set_localRotation(Quaternion.get_identity());
		this._camera = this._cameraGameObject.AddComponent<Camera>();
		this._camera.set_aspect(this._mainCamera.get_aspect());
		this._camera.set_fieldOfView(this._mainCamera.get_fieldOfView());
		this._camera.set_orthographic(false);
		this._camera.set_nearClipPlane(this._mainCamera.get_nearClipPlane());
		this._camera.set_farClipPlane(this._mainCamera.get_farClipPlane());
		this._camera.set_rect(this._mainCamera.get_rect());
		this._camera.set_depthTextureMode(0);
		this._camera.set_cullingMask(1 << LayerMask.NameToLayer("Player"));
		this._camera.set_enabled(false);
		this._materialOcclusion = new Material(ShaderManager.Find("OutLine/Occlusion"));
		this._materialStretch = new Material(ShaderManager.Find("OutLine/Stretch"));
		this._materialMix = new Material(ShaderManager.Find("OutLine/Mix"));
		Shader.SetGlobalColor(ShaderPIDManager._OutLineColor, this.OutLineColor);
		if (!this._depthShader.get_isSupported() || !this._materialMix.get_shader().get_isSupported() || !this._materialMix.get_shader().get_isSupported() || !this._materialOcclusion.get_shader().get_isSupported())
		{
			Debug.LogError("no isSupported");
			return;
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this._renderTextureDepth = RenderTexture.GetTemporary(Screen.get_width(), Screen.get_height(), 24, 1);
		this._renderTextureOcclusion = RenderTexture.GetTemporary(Screen.get_width(), Screen.get_height(), 0);
		this._renderTextureStretch = RenderTexture.GetTemporary(Screen.get_width(), Screen.get_height(), 0);
		this._camera.set_targetTexture(this._renderTextureDepth);
		this._camera.set_fieldOfView(this._mainCamera.get_fieldOfView());
		this._camera.set_aspect(this._mainCamera.get_aspect());
		this._camera.RenderWithShader(this._depthShader, string.Empty);
		Graphics.Blit(this._renderTextureDepth, this._renderTextureOcclusion, this._materialOcclusion);
		Vector4 vector = new Vector4(1f / (float)Screen.get_width(), 1f / (float)Screen.get_height(), 0f, 0f);
		this._materialStretch.SetVector(ShaderPIDManager._ScreenSize, vector);
		Graphics.Blit(this._renderTextureOcclusion, this._renderTextureStretch, this._materialStretch, 0);
		Graphics.Blit(this._renderTextureStretch, this._renderTextureStretch, this._materialStretch, 1);
		this._materialMix.SetTexture(ShaderPIDManager._OcclusionTex, this._renderTextureOcclusion);
		this._materialMix.SetTexture(ShaderPIDManager._StretchTex, this._renderTextureStretch);
		Graphics.Blit(source, destination, this._materialMix);
		RenderTexture.ReleaseTemporary(this._renderTextureDepth);
		RenderTexture.ReleaseTemporary(this._renderTextureOcclusion);
		RenderTexture.ReleaseTemporary(this._renderTextureStretch);
	}
}
