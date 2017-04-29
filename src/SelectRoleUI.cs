using Foundation.Core.Databinding;
using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using XEngineActor;

public class SelectRoleUI : UIBase
{
	private const int kCareerPrimary = 3;

	private const int kCareerSecondary = 3;

	private static Dictionary<int, int> createMapIDs;

	private static Transform[] camerapoint1;

	private static Transform[] camerapoint2;

	private Transform[,] foot_fx_node = new Transform[3, 3];

	public InputFieldCustom inputField;

	private bool isRandom;

	private bool isUpdateReady;

	private int careerPrimaryCurr;

	private int careerSecondaryCurr;

	private Vector3 positionTo;

	private Quaternion rotationTo;

	private float moveSpeedPerSec;

	private float rotateSpeedPerSec;

	private bool isCameraMove;

	private float selfRotateSign = 1f;

	private float selfRotateSpeedPerSec = 2f;

	private float selfRotateAngle = 10f;

	private Transform SceneSystem;

	private Coroutine coroutine;

	private ActorModel roleModel;

	private GameObject ImageTouchPlace;

	private RawImage ImageActor;

	private int careerPrimaryLast;

	private int careerSecondaryLast;

	private string CurrentName;

	private bool isWait = true;

	private uint timerID;

	private bool isStop = true;

	static SelectRoleUI()
	{
		// 注意: 此类型已标记为 'beforefieldinit'.
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		dictionary.Add(0, 4);
		dictionary.Add(1, 7);
		dictionary.Add(2, 8);
		SelectRoleUI.createMapIDs = dictionary;
		SelectRoleUI.camerapoint1 = new Transform[3];
		SelectRoleUI.camerapoint2 = new Transform[3];
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ImageTouchPlace = base.FindTransform("ImageTouchPlace").get_gameObject();
		this.ImageActor = base.FindTransform("RawImageActor").GetComponent<RawImage>();
	}

	private void Start()
	{
		RTManager.Instance.SetModelRawImage1(this.ImageActor, false);
		EventTriggerListener expr_1C = EventTriggerListener.Get(this.ImageTouchPlace);
		expr_1C.onDrag = (EventTriggerListener.VoidDelegateData)Delegate.Combine(expr_1C.onDrag, new EventTriggerListener.VoidDelegateData(RTManager.Instance.OnDragImageTouchPlace1));
		this.ImageActor.GetComponent<RectTransform>().set_sizeDelta(new Vector2(1280f, (float)(1280 * Screen.get_height() / Screen.get_width())));
	}

	protected override void OnEnable()
	{
		LoginManager.Instance.ResetClickRoleTime();
		this.isUpdateReady = true;
		base.SetAsFirstSibling();
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", true, RTManager.RtType.ActorModel1);
		RTManager.Instance.SetRotate(true, false);
		CamerasMgr.Camera2RTCommon.GetComponent<Camera>().set_fieldOfView(32f);
		this.CurrentName = string.Empty;
		this.Init();
		SoundManager.Instance.PlayBGMByID(118);
	}

	private void InitFx()
	{
		base.get_transform().Find("group").get_gameObject().SetActive(true);
		base.StartCoroutine(this.MoveInputField());
	}

	private void Init()
	{
		this.InitButtonEvent();
		this.InitFx();
		this.OnClickRandom(null);
		this.ShowModel(0, 1);
		this.SetOneCareer(0, 1);
		this.timerID = TimerHeap.AddTimer(7000u, 0, new Action(this.ResetButtonTime));
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		PostProcessManager.Instance.EnablePostProcessToSelectRole(false);
		EventDispatcher.Broadcast<bool, RTManager.RtType>("RTManager.ENABLE_PROJECTION_TYPE", false, RTManager.RtType.ActorModel1);
		if (this.timerID != 0u)
		{
			TimerHeap.DelTimer(this.timerID);
		}
		this.isWait = false;
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			this.ResetRoleModel();
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.CLOSE_SELECTCRATEPANEL, new Callback(this.CloseSelectPanel));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.CLOSE_SELECTCRATEPANEL, new Callback(this.CloseSelectPanel));
	}

	private void CloseSelectPanel()
	{
		Debug.Log("CloseSelectPanel");
		UIManagerControl.Instance.UnLoadUIPrefab("SelectRoleUI");
	}

	private void OnFocuse(GameObject go)
	{
		if (this.inputField.get_isFocused() && this.isRandom)
		{
			this.inputField.set_text(string.Empty);
			this.isRandom = false;
		}
	}

	private void OnClickImgCareer(GameObject sender)
	{
		if (this.CurrentName != null && this.CurrentName.Equals(sender.get_name()))
		{
			return;
		}
		if (this.isWait)
		{
			return;
		}
		this.isWait = true;
		if (this.timerID != 0u)
		{
			TimerHeap.DelTimer(this.timerID);
		}
		this.timerID = TimerHeap.AddTimer(7000u, 0, new Action(this.ResetButtonTime));
		this.CurrentName = sender.get_name();
		this.careerPrimaryLast = this.careerPrimaryCurr;
		this.careerSecondaryLast = this.careerSecondaryCurr;
		string[] array = sender.get_name().Split(new char[]
		{
			'_'
		});
		this.careerPrimaryCurr = int.Parse(array[1]) - 1;
		this.careerSecondaryCurr = int.Parse(array[2]) - 1;
		LoginManager.Instance.AddClickRoleTime(SelectRoleUI.createMapIDs.get_Item(this.careerPrimaryCurr));
		this.ShowModel(this.careerPrimaryCurr, this.careerSecondaryCurr);
		this.SetOneCareer(this.careerPrimaryCurr, this.careerSecondaryCurr);
		if (SelectRoleUI.camerapoint1[this.careerPrimaryCurr] != null)
		{
			this.positionTo = SelectRoleUI.camerapoint1[this.careerPrimaryCurr].get_position();
			Vector3 vector = SelectRoleUI.camerapoint2[this.careerPrimaryCurr].get_position() - SelectRoleUI.camerapoint1[this.careerPrimaryCurr].get_position();
			this.rotationTo = Quaternion.LookRotation(vector);
			this.moveSpeedPerSec = Vector3.Distance(CamerasMgr.MainCameraRoot.get_position(), this.positionTo) / 0.5f;
			this.rotateSpeedPerSec = Quaternion.Angle(CamerasMgr.MainCameraRoot.get_rotation(), this.rotationTo) / 0.5f;
			this.isCameraMove = true;
			this.OnClickRandom(null);
			string keyJointName = this.GetKeyJointName(this.careerPrimaryCurr, 0);
			XuanJiaoPeiZhi xuanJiaoPeiZhi = DataReader<XuanJiaoPeiZhi>.Get(keyJointName);
			this.selfRotateAngle = (float)xuanJiaoPeiZhi.rotationAngle;
		}
	}

	private void ResetButtonTime()
	{
		if (this.timerID != 0u)
		{
			TimerHeap.DelTimer(this.timerID);
		}
		this.isWait = false;
	}

	private void OnClickCreate(GameObject go)
	{
		string empty = string.Empty;
		string text = this.inputField.get_text();
		if (string.IsNullOrEmpty(text))
		{
			UIManagerControl.Instance.ShowToastText("别闹了，你没有输入名字", 2f, 2f);
			return;
		}
		if (text.IndexOf(" ") > -1)
		{
			UIManagerControl.Instance.ShowToastText("名字中不能带有空格哦", 2f, 2f);
			return;
		}
		if (WordFilter.filter(text, out empty, 3, true, true, "*"))
		{
			UIManagerControl.Instance.ShowToastText("名字含有敏感词", 2f, 2f);
			return;
		}
		if (text.get_Length() > 6)
		{
			UIManagerControl.Instance.ShowToastText("名字长度不能超过六个字符", 2f, 2f);
			return;
		}
		WaitUI.OpenUI(10000u);
		LoginManager.Instance.CreateRole(SelectRoleUI.createMapIDs.get_Item(this.careerPrimaryCurr), text);
	}

	[DebuggerHidden]
	private IEnumerator MoveInputField()
	{
		SelectRoleUI.<MoveInputField>c__Iterator44 <MoveInputField>c__Iterator = new SelectRoleUI.<MoveInputField>c__Iterator44();
		<MoveInputField>c__Iterator.<>f__this = this;
		return <MoveInputField>c__Iterator;
	}

	private void FindFbxNode(Transform parent, ref Transform node, string name)
	{
		if (node != null)
		{
			return;
		}
		if (parent == null)
		{
			return;
		}
		if (parent.get_name().TrimEnd(new char[]
		{
			' '
		}) == name)
		{
			node = parent;
			return;
		}
		for (int i = 0; i < parent.get_childCount(); i++)
		{
			Transform child = parent.GetChild(i);
			this.FindFbxNode(child, ref node, name);
		}
	}

	private void Update()
	{
		if (this.isStop)
		{
			return;
		}
		if (!this.isUpdateReady)
		{
			return;
		}
		if (SelectRoleUI.camerapoint1[this.careerPrimaryCurr] == null)
		{
			return;
		}
		if (this.isCameraMove)
		{
			if (CamerasMgr.MainCameraRoot.get_position() == this.positionTo)
			{
				this.isCameraMove = false;
			}
			else
			{
				CamerasMgr.MainCameraRoot.set_position(Vector3.MoveTowards(CamerasMgr.MainCameraRoot.get_position(), this.positionTo, this.moveSpeedPerSec * Time.get_deltaTime()));
				CamerasMgr.MainCameraRoot.set_rotation(Quaternion.RotateTowards(CamerasMgr.MainCameraRoot.get_rotation(), this.rotationTo, this.rotateSpeedPerSec * Time.get_deltaTime()));
			}
		}
		else
		{
			Vector3 t = SelectRoleUI.camerapoint1[this.careerPrimaryCurr].get_position() - SelectRoleUI.camerapoint2[this.careerPrimaryCurr].get_position();
			Quaternion quaternion = Quaternion.AngleAxis(this.selfRotateSign * this.selfRotateSpeedPerSec * Time.get_deltaTime(), Vector3.get_up());
			Vector3 vector = CamerasMgr.MainCameraRoot.get_position() - SelectRoleUI.camerapoint2[this.careerPrimaryCurr].get_position();
			CamerasMgr.MainCameraRoot.set_position(SelectRoleUI.camerapoint2[this.careerPrimaryCurr].get_position() + quaternion * vector);
			Transform expr_14F = CamerasMgr.MainCameraRoot;
			expr_14F.set_rotation(expr_14F.get_rotation() * quaternion);
			Vector3 t2 = CamerasMgr.MainCameraRoot.get_position() - SelectRoleUI.camerapoint2[this.careerPrimaryCurr].get_position();
			float num = Vector3.Angle(t.AssignYZero(), t2.AssignYZero());
			if (num > this.selfRotateAngle)
			{
				this.selfRotateSign = Mathf.Sign(-Vector3.Cross(t.AssignYZero(), t2.AssignYZero()).y);
			}
		}
	}

	private string GetKeyJointName(int i, int j)
	{
		return i + 1 + "_" + (j + 1);
	}

	private string GetButtonJointName(int i, int j)
	{
		return "role_" + this.GetKeyJointName(i, j);
	}

	private string GetFbxJointName(int i, int j)
	{
		return "role_" + this.GetKeyJointName(i, j) + "_lg";
	}

	private void InitButtonEvent()
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				string buttonJointName = this.GetButtonJointName(i, j);
				Transform transform = base.FindTransform(buttonJointName);
				if (transform != null)
				{
					ButtonCustom expr_31 = transform.GetComponent<ButtonCustom>();
					expr_31.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_31.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickImgCareer));
				}
			}
		}
		Button component = base.get_transform().Find("btnBack").GetComponent<Button>();
		component.get_onClick().RemoveAllListeners();
		component.get_onClick().AddListener(delegate
		{
			this.Show(false);
			if (ClientApp.Instance != null)
			{
				ClientApp.Instance.ReInit();
			}
			SoundManager.Instance.PlayBGMByID(119);
			UIManagerControl.Instance.OpenUI("LoginUI", null, false, UIType.NonPush);
		});
		ButtonCustom expr_B3 = base.FindTransform("btnCreateRole").GetComponent<ButtonCustom>();
		expr_B3.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_B3.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickCreate));
		this.inputField.onClickCustom = new Action<GameObject>(this.OnFocuse);
		base.FindTransform("random").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRandom);
		string keyJointName = this.GetKeyJointName(this.careerPrimaryCurr, 0);
		XuanJiaoPeiZhi xuanJiaoPeiZhi = DataReader<XuanJiaoPeiZhi>.Get(keyJointName);
		this.selfRotateAngle = (float)xuanJiaoPeiZhi.rotationAngle;
	}

	private void SetOneCareer(int careerPrimary, int careerSecondary)
	{
		this.SetCareerIcon(careerPrimary, careerSecondary);
		this.SetCareerDesc(careerPrimary, careerSecondary);
		this.SetCareerAttr(careerPrimary, careerSecondary);
	}

	private void SetCareerHolyLight(int careerPrimary, int careerSecondary)
	{
		if (this.careerPrimaryLast != this.careerPrimaryCurr || this.careerSecondaryLast == this.careerSecondaryCurr)
		{
			return;
		}
		Vector3 position = this.foot_fx_node[careerPrimary, careerSecondary].get_position();
		Debug.LogError("SetCareerHolyLight=" + position);
		int num = FXManager.Instance.PlayFXOfDisplay(1920, null, position, Quaternion.get_identity(), 1f, 1f, 0, false, null, null);
		ActorFX actorByID = FXManager.Instance.GetActorByID(num);
		Debug.LogError(string.Concat(new object[]
		{
			"fxId=",
			num,
			" aFx=",
			actorByID
		}));
		LayerSystem.SetGameObjectLayer(actorByID.get_gameObject(), "CameraRange", 1);
	}

	private void SetCareerGroup(int careerPrimary, int careerSecondary)
	{
		for (int i = 0; i < 3; i++)
		{
			bool active = i == careerPrimary;
			base.FindTransform("group_" + (i + 1)).get_gameObject().SetActive(active);
		}
	}

	private void SetCareerIcon(int careerPrimary, int careerSecondary)
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				string buttonJointName = this.GetButtonJointName(i, j);
				Transform transform = base.FindTransform(buttonJointName);
				if (!(transform == null))
				{
					Image component = transform.GetComponent<Image>();
					string keyJointName = this.GetKeyJointName(i, j);
					XuanJiaoPeiZhi xuanJiaoPeiZhi = DataReader<XuanJiaoPeiZhi>.Get(keyJointName);
					if (i == careerPrimary && j == careerSecondary)
					{
						ResourceManager.SetSprite(component, GameDataUtils.GetIcon(xuanJiaoPeiZhi.iconHightLight));
					}
					else
					{
						ResourceManager.SetSprite(component, GameDataUtils.GetIcon(xuanJiaoPeiZhi.icon));
					}
				}
			}
		}
	}

	[DebuggerHidden]
	private IEnumerator MoveCareerDesc(int careerPrimary, int careerSecondary)
	{
		SelectRoleUI.<MoveCareerDesc>c__Iterator45 <MoveCareerDesc>c__Iterator = new SelectRoleUI.<MoveCareerDesc>c__Iterator45();
		<MoveCareerDesc>c__Iterator.careerPrimary = careerPrimary;
		<MoveCareerDesc>c__Iterator.careerSecondary = careerSecondary;
		<MoveCareerDesc>c__Iterator.<$>careerPrimary = careerPrimary;
		<MoveCareerDesc>c__Iterator.<$>careerSecondary = careerSecondary;
		<MoveCareerDesc>c__Iterator.<>f__this = this;
		return <MoveCareerDesc>c__Iterator;
	}

	private void SetCareerDesc(int careerPrimary, int careerSecondary)
	{
		if (this.coroutine != null)
		{
			base.StopCoroutine(this.coroutine);
		}
		this.coroutine = base.StartCoroutine(this.MoveCareerDesc(careerPrimary, careerSecondary));
	}

	private void SetCareerAttr(int careerPrimary, int careerSecondary)
	{
		string keyJointName = this.GetKeyJointName(careerPrimary, careerSecondary);
		XuanJiaoPeiZhi xuanJiaoPeiZhi = DataReader<XuanJiaoPeiZhi>.Get(keyJointName);
		for (int i = 0; i < xuanJiaoPeiZhi.capability.get_Count(); i++)
		{
			string transformName = "yellowBg" + xuanJiaoPeiZhi.capability.get_Item(i).key.ToString();
			Transform transform = base.FindTransform(transformName);
			if (transform != null)
			{
				int value = xuanJiaoPeiZhi.capability.get_Item(i).value;
				float num = (float)value / 100f;
				transform.GetComponent<RectTransform>().set_localScale(new Vector3(num, 1f, 1f));
			}
		}
	}

	private void OnClickRandom(GameObject go)
	{
		this.inputField.set_text(this.GetRandomName());
		this.isRandom = true;
	}

	private string GetRandomName()
	{
		List<Xing> dataList = DataReader<Xing>.DataList;
		string chineseContent = GameDataUtils.GetChineseContent(Random.Range(0, dataList.get_Count()) + 630001, false);
		string text = string.Empty;
		if (this.careerPrimaryCurr == 0)
		{
			List<NanMing> dataList2 = DataReader<NanMing>.DataList;
			text = GameDataUtils.GetChineseContent(Random.Range(0, dataList2.get_Count()) + 631001, false);
		}
		else if (this.careerPrimaryCurr == 1)
		{
			List<NvMing> dataList3 = DataReader<NvMing>.DataList;
			text = GameDataUtils.GetChineseContent(Random.Range(0, dataList3.get_Count()) + 632001, false);
		}
		else
		{
			if (this.careerPrimaryCurr != 2)
			{
				return string.Empty;
			}
			List<NvMing> dataList4 = DataReader<NvMing>.DataList;
			text = GameDataUtils.GetChineseContent(Random.Range(0, dataList4.get_Count()) + 632001, false);
		}
		string text2 = (chineseContent + text).Trim();
		if (WordFilter.filter(text2, out text, 3, true, true, "*"))
		{
			Debuger.Info("===[" + text2 + "]===有敏感字===", new object[0]);
			return this.GetRandomName();
		}
		return text2;
	}

	public static void PreLoadModel(Action action)
	{
		string key = "1_2";
		int modelID = SelectRoleUI.GetModelID(key);
		ModelDisplayManager.Instance.LoadModel(modelID, delegate(bool isSuccess)
		{
			if (action != null)
			{
				action.Invoke();
			}
		});
	}

	private static void PreLoadModelInBackground()
	{
		ModelDisplayManager.Instance.LoadModel(SelectRoleUI.GetModelID("2_3"), null);
		ModelDisplayManager.Instance.LoadModel(SelectRoleUI.GetModelID("3_3"), null);
	}

	public static void PreOneLoadModel(string key, Action action)
	{
		ModelDisplayManager.Instance.LoadModel(SelectRoleUI.GetModelID(key), delegate(bool isSuccess)
		{
			if (action != null)
			{
				action.Invoke();
			}
		});
	}

	public static void PreAllLoadModel(Action action)
	{
		SelectRoleUI.PreOneLoadModel("1_2", delegate
		{
			RoleLoadingUI.SetProgress(0.6f);
			SelectRoleUI.PreOneLoadModel("2_3", delegate
			{
				RoleLoadingUI.SetProgress(0.9f);
				SelectRoleUI.PreOneLoadModel("3_3", delegate
				{
					if (action != null)
					{
						action.Invoke();
					}
				});
			});
		});
	}

	protected void ShowModel(int careerPrimary, int careerSecondary)
	{
		ModelDisplayManager.Instance.DeleteModel();
		int modelID = SelectRoleUI.GetModelID(this.GetKeyJointName(careerPrimary, careerSecondary));
		ModelDisplayManager.Instance.ShowModel(modelID, true, ModelDisplayManager.OFFSET_TO_ROLESHOWUI, delegate(int uid)
		{
			ActorModel uIModel = ModelDisplayManager.Instance.GetUIModel(uid);
			this.roleModel = uIModel;
			if (this.roleModel != null)
			{
				this.roleModel.get_gameObject().SetActive(true);
				this.roleModel.get_gameObject().get_transform().set_localScale(new Vector3(0.7f, 0.7f, 0.7f));
				LayerSystem.SetGameObjectLayer(this.roleModel.get_gameObject(), "CameraRange", 2);
				Animator animator = this.roleModel.get_gameObject().GetComponentsInChildren<Animator>(true)[0];
				animator.set_cullingMode(0);
				this.ModelAction(uIModel);
				this.ResetButtonTime();
			}
		});
	}

	private static int GetModelID(string key)
	{
		XuanJiaoPeiZhi xuanJiaoPeiZhi = DataReader<XuanJiaoPeiZhi>.Get(key);
		if (xuanJiaoPeiZhi == null)
		{
			return 0;
		}
		string[] array = xuanJiaoPeiZhi.modelId.Split(new char[]
		{
			'.'
		});
		return int.Parse(array[0]);
	}

	private void ModelAction(ActorModel model)
	{
		if (model != null)
		{
			model.SetBornAction("born", null);
		}
	}

	public void ResetRoleModel()
	{
		if (this.roleModel != null && this.roleModel.get_gameObject() != null)
		{
			Object.Destroy(this.roleModel.get_gameObject());
		}
	}
}
