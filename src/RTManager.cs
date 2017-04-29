using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RTManager : MonoBehaviour
{
	public class EventNames
	{
		public const string ENABLE_PROJECTION_TYPE = "RTManager.ENABLE_PROJECTION_TYPE";

		public const string ACTSELF_CHANGE_WEAPON = "EquipCustomization.ACTSELF_CHANGE_WEAPON";
	}

	public enum RtType
	{
		None,
		LocalHeatDistortion,
		Player,
		ActorModel1,
		InstanceBall,
		ElementBall
	}

	private RenderTexture m_RTCommon;

	private Material m_RTMat;

	private Material m_RTNoAlphaMat;

	public static RTManager Instance;

	private Vector3 playerTmpPosition;

	public static RTManager.RtType CurrentRtType = RTManager.RtType.LocalHeatDistortion;

	private Dictionary<RTManager.RtType, int> CullingMasks = new Dictionary<RTManager.RtType, int>();

	[SetProperty("AimTarget"), SerializeField]
	private Transform _AimTarget;

	private Quaternion AimTargetInitRotate;

	[SetProperty("CamProjRotateRevise"), SerializeField]
	private Vector3 _CamProjRotateRevise = Vector3.get_zero();

	[SetProperty("AimTargetOffsetY"), SerializeField]
	private float _AimTargetOffsetY;

	[SetProperty("CurrentOffsetAngle"), SerializeField]
	private Vector2 _CurrentOffsetAngle = new Vector2(0f, 0f);

	private bool EnableRotateX;

	private bool EnableRotateY;

	private RawImage CurrentRawImageOfProjection;

	public RenderTexture RTCommon
	{
		get
		{
			if (this.m_RTCommon == null)
			{
				RTManager.CreateRenderTexture(ref this.m_RTCommon, "Buffer2RTCommon");
				Camera camera2RTCommon = CamerasMgr.Camera2RTCommon;
				camera2RTCommon.set_targetTexture(this.m_RTCommon);
			}
			this.FixedCamera2RTCommon();
			return this.m_RTCommon;
		}
	}

	public Material RTMat
	{
		get
		{
			if (this.m_RTMat == null)
			{
				this.m_RTMat = new Material(ShaderManager.Find("Hsh(Mobile)/UI/UIRT"));
			}
			return this.m_RTMat;
		}
	}

	public Material RTNoAlphaMat
	{
		get
		{
			if (this.m_RTNoAlphaMat == null)
			{
				this.m_RTNoAlphaMat = new Material(ShaderManager.Find("Hsh(Mobile)/UI/UIRT_NoAlpha"));
			}
			return this.m_RTNoAlphaMat;
		}
	}

	public Transform AimTarget
	{
		get
		{
			return this._AimTarget;
		}
		set
		{
			this._AimTarget = value;
			this.AimTargetInitRotate = this._AimTarget.get_rotation();
			if (this._AimTarget != null)
			{
				this.SetCameraPosition(RTManager.CurrentRtType, true);
				this.RefreshRotation(RTManager.CurrentRtType, true);
			}
		}
	}

	public Vector3 CamProjRotateRevise
	{
		get
		{
			return this._CamProjRotateRevise;
		}
		set
		{
			this._CamProjRotateRevise = value;
		}
	}

	public float AimTargetOffsetY
	{
		get
		{
			return this._AimTargetOffsetY;
		}
		set
		{
			this._AimTargetOffsetY = value;
		}
	}

	public Vector2 CurrentOffsetAngle
	{
		get
		{
			return this._CurrentOffsetAngle;
		}
		set
		{
			this._CurrentOffsetAngle = value;
		}
	}

	public void FixedCamera2RTCommon()
	{
		if (CamerasMgr.IsCamera2RTCommonTargetNull())
		{
			Debug.LogError("CamerasMgr.Camera2RTCommon.targetTexture == null");
			if (this.m_RTCommon != null)
			{
				CamerasMgr.Camera2RTCommon.set_targetTexture(this.m_RTCommon);
			}
			if (this.CurrentRawImageOfProjection != null)
			{
				RTManager.SetRT(this.CurrentRawImageOfProjection, this.RTCommon);
			}
		}
	}

	public bool RTIsUI()
	{
		return RTManager.CurrentRtType == RTManager.RtType.Player || RTManager.CurrentRtType == RTManager.RtType.ActorModel1 || RTManager.CurrentRtType == RTManager.RtType.InstanceBall || RTManager.CurrentRtType == RTManager.RtType.ElementBall;
	}

	private void Awake()
	{
		RTManager.Instance = this;
		EventDispatcher.AddListener<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", new Callback<bool, RTManager.RtType>(this.EnableProjectionType));
		EventDispatcher.AddListener("EquipCustomization.ACTSELF_CHANGE_WEAPON", new Callback(this.ActSelfChangeWeapon));
	}

	public void EnableProjectionType(bool bOn, RTManager.RtType type)
	{
		if (bOn && type == RTManager.RtType.LocalHeatDistortion && RTManager.CurrentRtType != RTManager.RtType.None && RTManager.CurrentRtType != RTManager.RtType.LocalHeatDistortion)
		{
			return;
		}
		this.SetCameraParentNode(bOn && type != RTManager.RtType.LocalHeatDistortion);
		this.SetCameraRenderType(type, bOn);
		this.SetCameraPosition(type, bOn);
		this.RefreshRotation(type, bOn);
	}

	private void ActSelfChangeWeapon()
	{
		if (EntityWorld.Instance.ActSelf != null && this.CullingMasks.ContainsKey(RTManager.RtType.Player))
		{
			ShadowController.ShowShadow(EntityWorld.Instance.EntSelf.ID, EntityWorld.Instance.ActSelf, true, 0);
		}
	}

	private void SetCameraRenderType(RTManager.RtType type, bool typeOn)
	{
		this.ResetRotation();
		switch (type)
		{
		case RTManager.RtType.LocalHeatDistortion:
		{
			string[] array = new string[]
			{
				"FX_Distortion"
			};
			int mask = LayerMask.GetMask(array);
			this.SetCameraCullingMask(type, typeOn, mask);
			break;
		}
		case RTManager.RtType.Player:
		{
			if (EntityWorld.Instance.ActSelf != null)
			{
				ShadowController.ShowShadow(EntityWorld.Instance.EntSelf.ID, EntityWorld.Instance.ActSelf, typeOn, 0);
			}
			string[] layerNames = new string[]
			{
				"CameraRange"
			};
			int mask2 = LayerSystem.GetMask(layerNames);
			this.SetCameraCullingMask(type, typeOn, mask2);
			if (typeOn)
			{
				this.AimTargetOffsetY = ModelDisplayManager.GetPlayerAM().camProjPosRevise;
				if (ModelDisplayManager.GetPlayerAM().camProjRotRevise.get_Count() >= 2)
				{
					RTManager.Instance.CamProjRotateRevise = new Vector3(0f, ModelDisplayManager.GetPlayerAM().camProjRotRevise.get_Item(0), ModelDisplayManager.GetPlayerAM().camProjRotRevise.get_Item(1));
				}
			}
			break;
		}
		case RTManager.RtType.ActorModel1:
			this.SetCameraCullingMask(type, typeOn, ModelDisplayManager.ModelFXMask);
			if (!typeOn)
			{
				this.HideModel();
				CamerasMgr.SetRTCClippingPlanes(0f);
			}
			else
			{
				CamerasMgr.SetRTCClippingPlanes(100000f);
			}
			break;
		case RTManager.RtType.InstanceBall:
		{
			string[] layerNames2 = new string[]
			{
				"BallItem",
				"BallObject"
			};
			int mask3 = LayerSystem.GetMask(layerNames2);
			this.SetCameraCullingMask(type, typeOn, mask3);
			if (!typeOn)
			{
				this.HideModel();
			}
			break;
		}
		case RTManager.RtType.ElementBall:
		{
			string[] layerNames3 = new string[]
			{
				"BallItem",
				"BallObject"
			};
			int mask4 = LayerSystem.GetMask(layerNames3);
			this.SetCameraCullingMask(type, typeOn, mask4);
			if (!typeOn)
			{
				this.HideModel();
			}
			break;
		}
		}
	}

	private void SetCameraCullingMask(RTManager.RtType type, bool typeOn, int mask = 2)
	{
		if (typeOn)
		{
			CamerasMgr.EnableRTC(true);
			this.CullingMasks.set_Item(type, mask);
			RTManager.CurrentRtType = type;
		}
		else
		{
			this.CullingMasks.Remove(type);
			if (this.CullingMasks.get_Count() == 0)
			{
				CamerasMgr.EnableRTC(false);
				RTManager.CurrentRtType = RTManager.RtType.None;
			}
			else
			{
				RTManager.CurrentRtType = RTManager.RtType.LocalHeatDistortion;
			}
		}
		this.SetSLightRotation();
		if (RTManager.CurrentRtType == RTManager.RtType.None)
		{
			CamerasMgr.SetRTCCullingMask(0);
		}
		else
		{
			CamerasMgr.SetRTCCullingMask(this.CullingMasks.get_Item(RTManager.CurrentRtType));
			if (CamerasMgr.Camera2RTCommon != null)
			{
				CamerasMgr.Camera2RTCommon.set_backgroundColor(new Color(0f, 0f, 0f, 0f));
			}
		}
	}

	public void SetCameraPosition(RTManager.RtType type, bool bEnable)
	{
		if (CamerasMgr.Camera2RTCommon == null)
		{
			Debug.LogError("CamerasMgr.Camera2RTCommon == null***********************");
			return;
		}
		if (!bEnable)
		{
			return;
		}
		switch (type)
		{
		case RTManager.RtType.LocalHeatDistortion:
			CamerasMgr.Camera2RTCommon.get_transform().set_localPosition(Vector3.get_zero());
			CamerasMgr.Camera2RTCommon.get_transform().set_localScale(Vector3.get_one());
			CamerasMgr.Camera2RTCommon.get_transform().set_localRotation(Quaternion.get_identity());
			break;
		case RTManager.RtType.Player:
		{
			if (this.AimTarget == null)
			{
				Debug.LogError("AimTarget is null");
				return;
			}
			Quaternion rotation = this.AimTarget.get_rotation();
			this.playerTmpPosition = this.AimTarget.get_position();
			Vector3 vector = rotation * RTManager.Instance.CamProjRotateRevise;
			Vector3 position = vector + RTManager.Instance.AimTarget.get_transform().get_position();
			Vector3 position2 = this.AimTarget.get_transform().get_position();
			position2.y += this.AimTargetOffsetY;
			CamerasMgr.Camera2RTCommon.get_transform().set_position(position);
			CamerasMgr.Camera2RTCommon.get_transform().LookAt(position2);
			break;
		}
		case RTManager.RtType.ActorModel1:
			if (this.AimTarget != null)
			{
				ModelDisplayManager.CameraSettingOfActorModel(CamerasMgr.Camera2RTCommon, this.CamProjRotateRevise, this.AimTargetOffsetY, 0f);
			}
			break;
		case RTManager.RtType.InstanceBall:
			if (this.AimTarget != null && ModelDisplayManager.Instance.goTerrestrialGlobe != null)
			{
				Vector3 position3 = ModelDisplayManager.Instance.goTerrestrialGlobe.get_transform().FindChild("CameraLookAtPlace").get_position();
				Vector3 position4 = ModelDisplayManager.Instance.goTerrestrialGlobe.get_transform().FindChild("CameraPlace").get_position();
				CamerasMgr.Camera2RTCommon.get_transform().set_position(position4);
				CamerasMgr.Camera2RTCommon.get_transform().LookAt(position3);
			}
			break;
		case RTManager.RtType.ElementBall:
			if (this.AimTarget != null && ModelDisplayManager.Instance.elementBall != null)
			{
				Vector3 position5 = ModelDisplayManager.Instance.elementBall.get_transform().FindChild("CameraLookAtPlace").get_position();
				Vector3 position6 = ModelDisplayManager.Instance.elementBall.get_transform().FindChild("CameraPlace").get_position();
				CamerasMgr.Camera2RTCommon.get_transform().set_position(position6);
				CamerasMgr.Camera2RTCommon.get_transform().LookAt(position5);
				if (BallElement.Instance.distanceVisable == 0f)
				{
					BallElement.Instance.CalDistance();
				}
			}
			break;
		}
	}

	private void SetCameraParentNode(bool isProj)
	{
		if (CamerasMgr.Camera2RTCommon == null)
		{
			return;
		}
		if (isProj)
		{
			CamerasMgr.Camera2RTCommon.get_transform().set_parent(null);
		}
		else
		{
			UGUITools.ResetTransform(CamerasMgr.Camera2RTCommon.get_transform(), CamerasMgr.MainCameraRoot);
		}
	}

	private void SetSLightRotation()
	{
		switch (RTManager.CurrentRtType)
		{
		case RTManager.RtType.None:
		case RTManager.RtType.LocalHeatDistortion:
			Utils.SetRoleLightRotation(true, 0f);
			break;
		case RTManager.RtType.Player:
			if (EntityWorld.Instance.ActSelf != null)
			{
				Utils.SetRoleLightRotation(false, EntityWorld.Instance.ActSelf.get_transform().get_localEulerAngles().y - 90f);
			}
			break;
		case RTManager.RtType.ActorModel1:
			Utils.SetRoleLightRotation(false, -90f);
			break;
		}
	}

	private void Update()
	{
		if (RTManager.CurrentRtType == RTManager.RtType.Player && !this.playerTmpPosition.Equals(this.AimTarget.get_position()))
		{
			this.SetCameraPosition(RTManager.CurrentRtType, true);
		}
	}

	public void SetRotate(bool enableRotateX, bool enableRotateY)
	{
		this.EnableRotateX = enableRotateX;
		this.EnableRotateY = enableRotateY;
	}

	public void SetModelRawImage1(RawImage actorRawImage, bool Is3DScene2Bg = false)
	{
		if (actorRawImage != null)
		{
			float num = 470f;
			RectTransform rectTransform = actorRawImage.get_transform() as RectTransform;
			if (rectTransform != null)
			{
				num = rectTransform.get_sizeDelta().y;
			}
			float num2 = (float)((int)((float)CamerasMgr.CameraMain.get_pixelWidth() * num / (float)CamerasMgr.CameraMain.get_pixelHeight()));
			actorRawImage.get_rectTransform().set_sizeDelta(new Vector2(num2, num));
			this.CurrentRawImageOfProjection = actorRawImage;
			RTManager.SetRT(actorRawImage, this.RTCommon);
			if (Is3DScene2Bg)
			{
				actorRawImage.set_material(this.RTMat);
			}
			else
			{
				actorRawImage.set_material(this.RTNoAlphaMat);
			}
		}
	}

	public void OnDragImageTouchPlace1(PointerEventData eventData)
	{
		Vector2 delta = eventData.get_delta();
		if (this.EnableRotateX)
		{
			this.CurrentOffsetAngle += new Vector2(delta.x, 0f);
		}
		if (this.EnableRotateY)
		{
			this.CurrentOffsetAngle += new Vector2(0f, delta.y);
		}
		this.RefreshRotation(RTManager.CurrentRtType, true);
	}

	public void ResetRotation()
	{
		this.CurrentOffsetAngle = Vector2.get_zero();
	}

	private void RefreshRotation(RTManager.RtType type, bool bEnable)
	{
		if (CamerasMgr.Camera2RTCommon == null)
		{
			return;
		}
		if (!bEnable)
		{
			return;
		}
		switch (type)
		{
		case RTManager.RtType.Player:
			this.RotateBaseCamera();
			break;
		case RTManager.RtType.ActorModel1:
			this.RotateBaseModel();
			break;
		case RTManager.RtType.InstanceBall:
			this.RotateInstanceBall();
			break;
		}
	}

	private void RotateInstanceBall()
	{
	}

	private void RotateBaseCamera()
	{
		if (this.AimTarget != null)
		{
			Quaternion quaternion = this.AimTargetInitRotate;
			quaternion *= Quaternion.Euler(0f, this.CurrentOffsetAngle.x, 0f);
			quaternion *= Quaternion.Euler(this.CurrentOffsetAngle.y, 0f, 0f);
			Vector3 vector = quaternion * RTManager.Instance.CamProjRotateRevise;
			Vector3 position = vector + RTManager.Instance.AimTarget.get_transform().get_position();
			Vector3 position2 = this.AimTarget.get_transform().get_position();
			position2.y += this.AimTargetOffsetY;
			CamerasMgr.Camera2RTCommon.get_transform().set_position(position);
			CamerasMgr.Camera2RTCommon.get_transform().LookAt(position2);
		}
	}

	private void RotateBaseModel()
	{
		if (this.AimTarget != null)
		{
			Quaternion quaternion = this.AimTargetInitRotate;
			quaternion *= Quaternion.Euler(0f, -this.CurrentOffsetAngle.x, 0f);
			quaternion *= Quaternion.Euler(-this.CurrentOffsetAngle.y, 0f, 0f);
			this.AimTarget.get_transform().set_rotation(quaternion);
		}
	}

	private void HideModel()
	{
		ModelDisplayManager.Instance.DeleteModel();
		ModelDisplayManager.Instance.ShowTerrestrialGlobe(false);
		ModelDisplayManager.Instance.ShowElementBall(false);
	}

	public static void CreateRenderTexture(ref RenderTexture renderTexture, string name)
	{
		int pixelWidth = CamerasMgr.CameraMain.get_pixelWidth();
		int pixelHeight = CamerasMgr.CameraMain.get_pixelHeight();
		UIConst.GetRealScreenSize(out pixelWidth, out pixelHeight);
		ShaderEffectUtils.SafeCreateRenderTexture(ref renderTexture, name, 16, 7, 1, pixelWidth, pixelHeight);
	}

	public static void SetRT(RawImage actorRawImage, Texture rt)
	{
		if (actorRawImage == null)
		{
			return;
		}
		if (actorRawImage.get_transform() != null)
		{
			UIImageRef component = actorRawImage.GetComponent<UIImageRef>();
			if (component != null)
			{
				component.sprite_name = "#";
			}
		}
		actorRawImage.set_texture(rt);
	}
}
