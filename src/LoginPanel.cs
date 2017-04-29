using Foundation.Core.Databinding;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : UIBase
{
	public static LoginPanel Instance;

	private GameObject m_goSwitchAccout;

	private GameObject m_goSDK;

	private GameObject m_goGetServer;

	private Text serverName;

	private Text localVersion;

	private RectTransform birdTrans;

	private RectTransform birdTrans2;

	private int fxBackgroudID;

	private int fxBirdID;

	private int fxGuangID;

	private int fxBirdID2;

	public float birdFlySpeed = 150f;

	private bool isStart;

	public Vector2 startPos;

	public Vector2 endPos;

	private bool isStart2;

	public Vector2 startPos2;

	public Vector2 endPos2;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = true;
		this.isInterruptStick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		LoginPanel.Instance = this;
	}

	protected override void InitUI()
	{
		this.m_goSwitchAccout = base.FindTransform("SwitchAccout").get_gameObject();
		this.ShowSwitchAccount();
		this.m_goSDK = base.FindTransform("SDK").get_gameObject();
		this.birdTrans = (base.FindTransform("niaoObj") as RectTransform);
		this.birdTrans2 = (base.FindTransform("niaoObj2") as RectTransform);
		this.birdFlySpeed = 120f;
		this.startPos = new Vector2(120f, 170f);
		this.endPos = new Vector2(1200f, 1000f);
		this.startPos2 = new Vector2(200f, 120f);
		this.endPos2 = new Vector2(850f, 600f);
		this.serverName = base.FindTransform("Name").GetComponent<Text>();
		string[] localVersions = GameManager.Instance.GetLocalVersions();
		this.localVersion = base.FindTransform("LocalVersion").GetComponent<Text>();
		string text = string.Join(".", localVersions);
		this.localVersion.set_text(text);
		Debug.Log(this.localVersion.get_text());
	}

	private void Start()
	{
		base.FindTransform("Goto").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickEnter);
		this.m_goSwitchAccout.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickAccout);
		base.FindTransform("SNameImg").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSelect);
		this.m_goGetServer = base.FindTransform("GetServer").get_gameObject();
		this.m_goGetServer.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGetServer);
		base.FindTransform("Announcement").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickOpenAnnouncementUI);
		base.FindTransform("ButtonWeixin").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonWeixin);
		base.FindTransform("ButtonQQ").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonQQ);
		base.FindTransform("ButtonGuest").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickButtonGuest);
		base.StartCoroutine(this.FadinAlpha());
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		LoginManager.Instance.TrySetCurrentServer();
		this.ShowLoginSDKIfNoLogin();
		this.AddFX();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.ShowLoginSDK(false);
		this.DeleteFX();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			LoginPanel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<string>(EventNames.Update_CurrentServer_Name, new Callback<string>(this.OnUpdateServerName));
		EventDispatcher.AddListener<int, int>(EventNames.ServerQueueLoginNty, new Callback<int, int>(this.OnServerQueueLoginNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<string>(EventNames.Update_CurrentServer_Name, new Callback<string>(this.OnUpdateServerName));
		EventDispatcher.RemoveListener<int, int>(EventNames.ServerQueueLoginNty, new Callback<int, int>(this.OnServerQueueLoginNty));
	}

	private void OnClickGetServer(GameObject go)
	{
		NetworkManager.Instance.ConnectLoginOuterServer();
		NetworkManager.Instance.ConnectLoginInnerServer();
	}

	private void OnClickOpenAnnouncementUI(GameObject go)
	{
		LoginManager.Instance.OpenAnnouncementUI();
	}

	private void OnClickAccout(GameObject go)
	{
		if (SDKManager.Instance.HasSDK())
		{
			if (SDKManager.Instance.IsAndroidYSDK())
			{
				SDKManager.Instance.Logout();
				this.ShowLoginSDK(true);
			}
			else if (SDKManager.Instance.GetSDKType() == 59)
			{
				SDKManager.Instance.OpenUserCenter();
			}
			else
			{
				SDKManager.Instance.Logout();
			}
		}
		else
		{
			LoginManager.Instance.OpenAccountUI();
		}
	}

	private void OnClickSelect(GameObject go)
	{
		LoginManager.Instance.OpenServerUI();
		if (ServerPanel.Instance != null)
		{
			ServerPanel.Instance.UpdateList();
		}
	}

	private void OnClickEnter(GameObject go)
	{
		LoadingUIView.IsFromLogin = true;
		NativeCallManager.QueryUpdate();
		LoginManager.Instance.LoginProcess(this.serverName.get_text());
	}

	private void OnUpdateServerName(string name)
	{
		this.serverName.set_text(name);
		Debug.Log("已选定服务器:" + name);
	}

	private void OnServerQueueLoginNty(int count, int minute)
	{
		DialogBoxUIViewModel.Instance.ShowAsConfirm("登录排队", string.Format("当前服务器人数已满\n正在等待进入，队列位置：{0}\n\n预计时间：{1}", count, TimeConverter.GetTime(minute * 60, TimeFormat.HHMM_Chinese)), new Action(this.SendCancelLoginReq), new Action(this.SendCancelLoginReq), false, "取消排队", "button_orange_1", null);
	}

	private void SendCancelLoginReq()
	{
		LoginManager.Instance.SendCancelLoginReq();
	}

	private void OnClickButtonWeixin(GameObject go)
	{
		SDKManager.Instance.Login(new OnSDKResultCallback(LoginManager.Instance.OnGetLoginResp), 1);
	}

	private void OnClickButtonQQ(GameObject go)
	{
		SDKManager.Instance.Login(new OnSDKResultCallback(LoginManager.Instance.OnGetLoginResp), 2);
	}

	private void OnClickButtonGuest(GameObject go)
	{
	}

	[DebuggerHidden]
	private IEnumerator FadinAlpha()
	{
		LoginPanel.<FadinAlpha>c__Iterator47 <FadinAlpha>c__Iterator = new LoginPanel.<FadinAlpha>c__Iterator47();
		<FadinAlpha>c__Iterator.<>f__this = this;
		return <FadinAlpha>c__Iterator;
	}

	private void ShowLoginSDKIfNoLogin()
	{
		if (!SDKManager.Instance.IsLogin())
		{
			this.ShowLoginSDK(true);
		}
	}

	public void ShowLoginSDK(bool isShow)
	{
		if (this.m_goSDK == null)
		{
			return;
		}
		if (!SDKManager.Instance.IsAndroidYSDK())
		{
			this.m_goSDK.SetActive(false);
		}
		else
		{
			this.m_goSDK.SetActive(isShow);
		}
	}

	private void ShowSwitchAccount()
	{
		if (SDKManager.Instance.HasSDK())
		{
			this.m_goSwitchAccout.SetActive(SDKManager.Instance.IsUserCenterOn());
		}
		else
		{
			this.m_goSwitchAccout.SetActive(true);
		}
	}

	public void SetGetServerButton()
	{
		if (this.m_goGetServer == null)
		{
			return;
		}
		this.m_goGetServer.SetActive(SystemConfig.IsDebugInfoOn);
	}

	private void AddFX()
	{
		this.fxBackgroudID = FXSpineManager.Instance.ReplaySpine(this.fxBackgroudID, 1230, base.FindTransform("spineObj"), "LoginUI", 2002, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fxGuangID = FXSpineManager.Instance.ReplaySpine(this.fxGuangID, 1232, base.FindTransform("spineObj"), "LoginUI", 2003, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fxBirdID = FXSpineManager.Instance.ReplaySpine(this.fxBirdID, 1231, this.birdTrans, "LoginUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.fxBirdID2 = FXSpineManager.Instance.ReplaySpine(this.fxBirdID2, 1231, this.birdTrans2, "LoginUI", 2001, null, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		this.ResetBirdSpine(1);
		this.ResetBirdSpine(2);
	}

	private void DeleteFX()
	{
		FXSpineManager.Instance.DeleteSpine(this.fxBackgroudID, true);
		FXSpineManager.Instance.DeleteSpine(this.fxBirdID, true);
		FXSpineManager.Instance.DeleteSpine(this.fxGuangID, true);
		FXSpineManager.Instance.DeleteSpine(this.fxBirdID2, true);
	}

	public void OpenFXMeshRender()
	{
		GameObject gameObject = GameObject.Find("denglujiemian(Clone)");
		if (gameObject != null)
		{
			gameObject.GetComponent<MeshRenderer>().set_enabled(true);
		}
		GameObject gameObject2 = GameObject.Find("niao(Clone)");
		if (gameObject2 != null)
		{
			gameObject2.GetComponent<MeshRenderer>().set_enabled(true);
		}
		GameObject gameObject3 = GameObject.Find("denglujiemianguang(Clone)");
		if (gameObject3 != null)
		{
			gameObject3.GetComponent<MeshRenderer>().set_enabled(true);
		}
		GameObject gameObject4 = this.birdTrans2.GetChild(0).get_gameObject();
		if (gameObject4 != null)
		{
			gameObject4.GetComponent<MeshRenderer>().set_enabled(true);
		}
	}

	private void Update()
	{
		this.SetGetServerButton();
		float num = 0.005f;
		float num2 = Time.get_deltaTime() * this.birdFlySpeed;
		int num3 = Random.Range(1, 500);
		if (num3 < 50 && !this.isStart)
		{
			this.ResetBirdSpine(1);
			this.isStart = true;
		}
		if (num3 < 100 && num3 > 50 && !this.isStart2)
		{
			this.ResetBirdSpine(2);
			this.isStart2 = true;
		}
		if (this.isStart)
		{
			if (this.birdTrans.get_anchoredPosition().x > 700f || this.birdTrans.get_anchoredPosition().y > 500f)
			{
				this.isStart = false;
			}
			else
			{
				if (this.birdTrans.get_localScale().x < 0.5f)
				{
					RectTransform expr_E5 = this.birdTrans;
					expr_E5.set_localScale(expr_E5.get_localScale() + new Vector3(num, num, num));
				}
				else
				{
					this.birdTrans.set_localScale(new Vector3(0.5f, 0.5f, 0.5f));
				}
				this.birdTrans.set_anchoredPosition(Vector2.MoveTowards(this.birdTrans.get_anchoredPosition(), this.endPos, num2));
			}
		}
		if (this.isStart2)
		{
			if (this.birdTrans2.get_anchoredPosition().x > 830f || this.birdTrans2.get_anchoredPosition().y > 550f)
			{
				this.isStart2 = false;
			}
			else
			{
				if (this.birdTrans2.get_localScale().x < 0.5f)
				{
					RectTransform expr_1BA = this.birdTrans2;
					expr_1BA.set_localScale(expr_1BA.get_localScale() + new Vector3(num, num, num));
				}
				else
				{
					this.birdTrans2.set_localScale(new Vector3(0.5f, 0.5f, 0.5f));
				}
				this.birdTrans2.set_anchoredPosition(Vector2.MoveTowards(this.birdTrans2.get_anchoredPosition(), this.endPos2, num2));
			}
		}
	}

	private void ResetBirdSpine(int bird)
	{
		if (bird == 1)
		{
			this.birdTrans.set_anchoredPosition(this.startPos);
			this.birdTrans.set_localScale(Vector3.get_zero());
		}
		else if (bird == 2)
		{
			this.birdTrans2.set_anchoredPosition(this.startPos2);
			this.birdTrans2.set_localScale(Vector3.get_zero());
		}
	}
}
