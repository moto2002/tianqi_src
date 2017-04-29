using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngine;
using XEngineActor;

public class ModelDisplayManager
{
	public class EventNames
	{
	}

	private static ModelDisplayManager m_instance;

	public static bool IsAlwaysCombo = false;

	public static readonly Vector3 ModelPoolPosition = new Vector3(-1000f, -1000f, 0f);

	private Dictionary<int, ActorModel> m_models = new Dictionary<int, ActorModel>();

	private int m_counter;

	private static int CurrentActorModelUID = 0;

	private static int CurrentActorPetModelUID = 0;

	public static Vector2 OFFSET_TO_PETUI = new Vector2(0f, 0.35f);

	public static Vector3 OFFSET_TO_PETUI_FX = new Vector3(0f, -1.2f, 5.5f);

	public static Vector2 OFFSET_TO_ROLESHOWUI = new Vector2(0f, -0.1f);

	public static Vector2 OFFSET_TO_BOSSUI = new Vector2(0f, 0f);

	private static AvatarModel PlayerAM;

	public string ballScrollItem = "null";

	public string tmpBallScrollItem = "null";

	public GameObject goTerrestrialGlobe;

	public GameObject elementBall;

	private static int modelFXMask = -1;

	public static ModelDisplayManager Instance
	{
		get
		{
			if (ModelDisplayManager.m_instance == null)
			{
				ModelDisplayManager.m_instance = new ModelDisplayManager();
			}
			return ModelDisplayManager.m_instance;
		}
	}

	public static int ModelFXMask
	{
		get
		{
			if (ModelDisplayManager.modelFXMask < 0)
			{
				string[] layerNames = new string[]
				{
					"CameraRange",
					"FX",
					"FX_Distortion"
				};
				ModelDisplayManager.modelFXMask = LayerSystem.GetMask(layerNames);
			}
			return ModelDisplayManager.modelFXMask;
		}
	}

	public void CreateUIModel(ref int uid, int modelId, AvatarModel dataAM, bool model_active, string model_layer, Vector2 offsetlocalPos, Action successCallback)
	{
		uid = ++this.m_counter;
		this.m_models.set_Item(uid, null);
		int temp_uid = uid;
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelId);
		if (avatarModel == null)
		{
			return;
		}
		AssetManager.LoadAssetWithPool(avatarModel.path, delegate(bool isSuccess)
		{
			if (this.m_models.ContainsKey(temp_uid))
			{
				this.JustCreateUIModel(temp_uid, modelId, dataAM, model_active, model_layer, offsetlocalPos);
				if (successCallback != null)
				{
					successCallback.Invoke();
				}
			}
		});
	}

	public ActorModel JustCreateUIModel(int uid, int modelId, AvatarModel dataAM, bool model_active, string model_layer, Vector2 offsetlocalPos)
	{
		ActorModel actorModel = ModelPool.Instance.Get(modelId);
		if (actorModel == null)
		{
			return null;
		}
		this.m_models.set_Item(uid, actorModel);
		actorModel.ModelType = ActorModelType.UI;
		actorModel.ModelLayer = model_layer;
		NavMeshAgent component = actorModel.GetComponent<NavMeshAgent>();
		if (component != null)
		{
			component.set_enabled(false);
		}
		ModelDisplayManager.RefreshInitLocalPosition(actorModel, offsetlocalPos);
		actorModel.get_transform().set_localEulerAngles(new Vector3(0f, dataAM.modelProjRotateRevise, 0f));
		actorModel.get_transform().set_localScale(Vector3.get_one() * dataAM.scale);
		actorModel.ShowShadow(false, modelId);
		actorModel.ChangeToIdle();
		actorModel.get_gameObject().SetActive(model_active);
		return actorModel;
	}

	public void LoadModel(int modelId, Action<bool> action)
	{
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelId);
		if (avatarModel == null)
		{
			return;
		}
		AssetManager.LoadAssetWithPool(avatarModel.path, delegate(bool isSuccess)
		{
			if (action != null)
			{
				action.Invoke(isSuccess);
			}
		});
	}

	public void DeleteUIModel(int uid)
	{
		if (this.m_models.ContainsKey(uid))
		{
			if (this.m_models.get_Item(uid) != null)
			{
				this.m_models.get_Item(uid).Destroy();
			}
			this.m_models.Remove(uid);
		}
	}

	public ActorModel GetUIModel(int uid)
	{
		if (this.m_models.ContainsKey(uid))
		{
			return this.m_models.get_Item(uid);
		}
		return null;
	}

	public void ShowModel(int modelId, bool model_active, Vector2 offsetlocalPos, Action<int> successCallback)
	{
		AvatarModel dataAM = DataReader<AvatarModel>.Get(modelId);
		if (dataAM == null)
		{
			return;
		}
		int uid = 0;
		this.CreateUIModel(ref uid, modelId, dataAM, model_active, "CameraRange", offsetlocalPos, delegate
		{
			this.DeleteModel();
			this.CurrentModelSetting(uid, dataAM, this.GetUIModel(uid));
			if (successCallback != null)
			{
				successCallback.Invoke(uid);
			}
		});
	}

	public ActorModel CurrentModelSetting(int uid, AvatarModel dataAM, ActorModel actorModel)
	{
		ModelDisplayManager.CurrentActorModelUID = uid;
		ModelDisplayManager.SetRTManager(dataAM, actorModel);
		return actorModel;
	}

	public void HideModel(bool hide)
	{
		if (ModelDisplayManager.CurrentActorModelUID != 0)
		{
			ActorModel uIModel = this.GetUIModel(ModelDisplayManager.CurrentActorModelUID);
			if (uIModel != null && uIModel.get_gameObject() != null)
			{
				uIModel.get_gameObject().SetActive(!hide);
			}
		}
	}

	public void DeleteModel()
	{
		if (ModelDisplayManager.CurrentActorModelUID > 0)
		{
			RTManager.Instance.ResetRotation();
			this.DeleteUIModel(ModelDisplayManager.CurrentActorModelUID);
			ModelDisplayManager.CurrentActorModelUID = 0;
		}
	}

	public void ShowPetModel(int modelId, bool model_active, Vector2 offsetlocalPos, Action<int> successCallback)
	{
		AvatarModel dataAM = DataReader<AvatarModel>.Get(modelId);
		if (dataAM == null)
		{
			return;
		}
		int uid = 0;
		this.CreateUIModel(ref uid, modelId, dataAM, model_active, "CameraRange", offsetlocalPos, delegate
		{
			this.DeletePetModel();
			this.CurrentPetModelSetting(uid, dataAM, this.GetUIModel(uid));
			if (successCallback != null)
			{
				successCallback.Invoke(uid);
			}
		});
	}

	public ActorModel CurrentPetModelSetting(int uid, AvatarModel dataAM, ActorModel actorModel)
	{
		ModelDisplayManager.CurrentActorPetModelUID = uid;
		ModelDisplayManager.SetRTManager(dataAM, actorModel);
		return actorModel;
	}

	public void HidePetModel(bool hide)
	{
		if (ModelDisplayManager.CurrentActorPetModelUID != 0)
		{
			ActorModel uIModel = this.GetUIModel(ModelDisplayManager.CurrentActorPetModelUID);
			if (uIModel != null && uIModel.get_gameObject() != null)
			{
				uIModel.get_gameObject().SetActive(!hide);
			}
		}
	}

	public void DeletePetModel()
	{
		if (ModelDisplayManager.CurrentActorPetModelUID > 0)
		{
			RTManager.Instance.ResetRotation();
			this.DeleteUIModel(ModelDisplayManager.CurrentActorPetModelUID);
			ModelDisplayManager.CurrentActorPetModelUID = 0;
		}
	}

	public static AvatarModel GetPlayerAM()
	{
		if (ModelDisplayManager.PlayerAM == null)
		{
			ModelDisplayManager.PlayerAM = DataReader<AvatarModel>.Get(EntityWorld.Instance.EntSelf.FixModelID);
		}
		return ModelDisplayManager.PlayerAM;
	}

	public static void ShowSkill(int model_uid, int skillId, Action skillEnd = null)
	{
		ActorModel uIModel = ModelDisplayManager.Instance.GetUIModel(model_uid);
		if (uIModel != null)
		{
			ModelDisplayManager.ShowSkill(uIModel, skillId, skillEnd);
		}
		else if (skillEnd != null)
		{
			skillEnd.Invoke();
		}
	}

	public static void ShowSkill(ActorModel actorModel, int skillId, Action skillEnd = null)
	{
		if (actorModel != null)
		{
			Skill skill = DataReader<Skill>.Get(skillId);
			if (skill != null)
			{
				actorModel.PlaySkillImmediate(skill, skillEnd);
				return;
			}
		}
		if (skillEnd != null)
		{
			skillEnd.Invoke();
		}
	}

	public static void ShowAction(ActorModel actorModel, string actionName, Action actionEnd = null)
	{
		if (actorModel != null)
		{
			actorModel.PlayActionImmediate(actionName, actionEnd);
			return;
		}
		if (actionEnd != null)
		{
			actionEnd.Invoke();
		}
	}

	private static void RefreshInitLocalPosition(ActorModel actorModel, Vector2 offsetlocalPos)
	{
		if (actorModel != null && actorModel.resGUID > 0)
		{
			AvatarModel avatarModel = DataReader<AvatarModel>.Get(actorModel.resGUID);
			if (avatarModel != null)
			{
				actorModel.InitLocalPosition = new Vector3(offsetlocalPos.x, avatarModel.modelProjYRevise + offsetlocalPos.y, 0f);
			}
		}
	}

	private static void SetRTManager(AvatarModel dataAM, ActorModel actorModel)
	{
		if (actorModel == null)
		{
			return;
		}
		if (dataAM.camProjRotRevise.get_Count() >= 2)
		{
			RTManager.Instance.CamProjRotateRevise = new Vector3(0f, dataAM.camProjRotRevise.get_Item(0), dataAM.camProjRotRevise.get_Item(1));
		}
		RTManager.Instance.AimTargetOffsetY = dataAM.camProjPosRevise;
		RTManager.Instance.AimTarget = actorModel.get_transform();
	}

	public static ActorModel SetRawImage(RawImage rawImage, int modelId, Vector2 offsetlocalPos, ref GameObject goModel, ref GameObject goCamera)
	{
		AvatarModel avatarModel = DataReader<AvatarModel>.Get(modelId);
		if (avatarModel == null)
		{
			return null;
		}
		RenderTexture renderTexture = null;
		RTManager.CreateRenderTexture(ref renderTexture, "temp_render_texture");
		rawImage.GetComponent<RectTransform>().set_sizeDelta(new Vector2((float)renderTexture.get_width(), (float)renderTexture.get_height()));
		RTManager.SetRT(rawImage, renderTexture);
		ActorModel actorModel = ModelDisplayManager.Instance.JustCreateUIModel(0, modelId, avatarModel, true, "CameraRange", offsetlocalPos);
		Camera camera = ModelDisplayManager.CreateCameraToModel(renderTexture);
		ModelDisplayManager.CameraSettingOfActorModel(camera, avatarModel, offsetlocalPos.x);
		goModel = actorModel.get_gameObject();
		goCamera = camera.get_gameObject();
		return actorModel;
	}

	public static Camera CreateCameraToModel(RenderTexture renderTexture)
	{
		GameObject gameObject = new GameObject("ui_temp_camera");
		Camera camera = gameObject.AddComponent<Camera>();
		camera.set_clearFlags(2);
		camera.set_backgroundColor(Vector4.get_zero());
		camera.set_targetTexture(renderTexture);
		camera.set_cullingMask(ModelDisplayManager.ModelFXMask);
		camera.set_orthographic(false);
		camera.set_fieldOfView(50f);
		camera.set_useOcclusionCulling(false);
		return camera;
	}

	private static void CameraSettingOfActorModel(Camera cameraRT, AvatarModel dataAM, float modelOffset)
	{
		if (dataAM.camProjRotRevise.get_Count() >= 2)
		{
			ModelDisplayManager.CameraSettingOfActorModel(cameraRT, new Vector3(0f, dataAM.camProjRotRevise.get_Item(0), dataAM.camProjRotRevise.get_Item(1)), dataAM.camProjPosRevise, modelOffset);
		}
	}

	public static void CameraSettingOfActorModel(Camera cameraRT, Vector3 camProjRotateRevise, float aimTargetOffsetY, float modelOffset)
	{
		Quaternion identity = Quaternion.get_identity();
		Vector3 vector = identity * camProjRotateRevise;
		Vector3 vector2 = vector + ModelDisplayManager.GetModelPosition(modelOffset);
		Vector3 modelPosition = ModelDisplayManager.GetModelPosition(modelOffset);
		modelPosition.y += aimTargetOffsetY;
		cameraRT.get_transform().set_position(new Vector3(ModelDisplayManager.GetModelPosition(modelOffset).x, vector2.y, vector2.z));
		cameraRT.get_transform().LookAt(modelPosition);
	}

	private static Vector3 GetModelPosition(float modelOffset)
	{
		return ModelDisplayManager.ModelPoolPosition + new Vector3(modelOffset, 0f, 0f);
	}

	public void TryToScrollBall()
	{
	}

	public void ShowTerrestrialGlobe(bool isShow)
	{
		if (isShow)
		{
			if (this.goTerrestrialGlobe == null)
			{
				GameObject gameObject = AssetManager.AssetOfNoPool.LoadAssetNowNoAB("Envi/Model/qiumian/BallPrefab", typeof(Object)) as GameObject;
				this.goTerrestrialGlobe = Object.Instantiate<GameObject>(gameObject);
				this.goTerrestrialGlobe.get_transform().set_parent(ModelPool.Instance.root.get_transform());
				this.goTerrestrialGlobe.get_transform().set_localPosition(Vector3.get_zero());
				this.goTerrestrialGlobe.get_transform().set_localRotation(Quaternion.get_identity());
			}
			this.goTerrestrialGlobe.get_transform().FindChild("Ball").get_transform().set_rotation(Quaternion.Euler(300.1149f, 68.77782f, 0f));
			this.goTerrestrialGlobe.SetActive(true);
			this.TryToScrollBall();
			RTManager.Instance.CamProjRotateRevise = new Vector3(0.9f, 0.9f, 0.9f);
			RTManager.Instance.AimTargetOffsetY = 0.18f;
			RTManager.Instance.AimTarget = this.goTerrestrialGlobe.get_transform().FindChild("Ball").get_transform();
			CamerasMgr.SetRTCFieldOfView(30f);
		}
		else
		{
			if (this.goTerrestrialGlobe != null)
			{
				this.goTerrestrialGlobe.SetActive(false);
			}
			CamerasMgr.SetRTCFieldOfView(0f);
		}
	}

	public void ShowElementBall(bool isShow)
	{
		if (isShow)
		{
			if (this.elementBall == null)
			{
				GameObject gameObject = AssetManager.AssetOfNoPool.LoadAssetNowNoAB(FileSystem.GetPathOfPrefab("BallPrefab"), typeof(Object)) as GameObject;
				if (gameObject != null)
				{
					this.elementBall = Object.Instantiate<GameObject>(gameObject);
					this.elementBall.get_transform().set_parent(ModelPool.Instance.root.get_transform());
					this.elementBall.get_transform().set_localPosition(Vector3.get_zero());
					this.elementBall.get_transform().set_localRotation(Quaternion.get_identity());
				}
			}
			this.elementBall.get_transform().FindChild("ball").get_transform().set_rotation(Quaternion.Euler(0f, 0f, 0f));
			this.elementBall.SetActive(true);
			RTManager.Instance.CamProjRotateRevise = new Vector3(0.9f, 0.9f, 0.9f);
			RTManager.Instance.AimTargetOffsetY = 0.18f;
			RTManager.Instance.AimTarget = this.elementBall.get_transform().FindChild("ball").get_transform();
			CamerasMgr.SetRTCFieldOfView((float)DataReader<YWanFaSheZhi>.Get("cameraFieldOfView").num);
		}
		else
		{
			if (this.elementBall != null)
			{
				this.elementBall.SetActive(false);
			}
			CamerasMgr.SetRTCFieldOfView(0f);
		}
	}

	public void ShowPetScene(bool isShow)
	{
	}
}
