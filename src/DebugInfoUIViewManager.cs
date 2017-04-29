using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugInfoUIViewManager : MonoBehaviour
{
	private float m_fFPS;

	[HideInInspector]
	public Rect GUIDebugInfoRect = new Rect(5f, 5f, 500f, 500f);

	private bool IsShowSwitch;

	private float m_fDeltaTime;

	private float m_fFrameCount;

	private string _scale = string.Empty;

	private List<GameObject> goModels = new List<GameObject>();

	private bool IsFound;

	private void Awake()
	{
		EventDispatcher.AddListener<int>(SceneManagerEvent.LoadSceneEnd, new Callback<int>(this.OnSceneLoadEnd));
	}

	private void OnGUI()
	{
		SystemConfig.IsDebugPing = true;
		base.get_gameObject().AddUniqueComponent<PingDebug>();
		if (!SystemConfig.IsDebugInfoOn)
		{
			return;
		}
		this.GUIInfo();
		this.GUIButtons();
	}

	private void GUIInfo()
	{
		GUILayout.BeginArea(this.GUIDebugInfoRect);
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUI.set_color(Color.get_red());
		GUIStyle gUIStyle = new GUIStyle(GUI.get_skin().get_label());
		gUIStyle.set_fontSize(24);
		GUILayout.Label("FPS: " + this.m_fFPS, gUIStyle, new GUILayoutOption[0]);
		GUI.set_color(Color.get_white());
		if (GUI.Button(new Rect(200f, 200f, 100f, 60f), "SWITCH"))
		{
			this.IsShowSwitch = !this.IsShowSwitch;
			UIManagerControl.Instance.OpenUI("DebugUI", UINodesManager.T4RootOfSpecial, false, UIType.NonPush);
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	private void Update()
	{
		this.m_fDeltaTime += Time.get_deltaTime();
		this.m_fFrameCount += 1f;
		if (this.m_fDeltaTime > 1f)
		{
			this.m_fFPS = this.m_fFrameCount;
			this.m_fDeltaTime = 0f;
			this.m_fFrameCount = 0f;
		}
	}

	private void GUIOfTimeScale()
	{
		this._scale = GUI.TextField(new Rect(10f, 100f, 80f, 35f), this._scale);
		if (GUI.Button(new Rect(100f, 100f, 80f, 35f), "TimeScale"))
		{
			Time.set_timeScale(float.Parse(this._scale));
		}
	}

	private void GUIButtons()
	{
		if (!this.IsShowSwitch)
		{
			return;
		}
		int num = -1;
		this.Buttons1(ref num);
		this.Buttons2(ref num);
	}

	private void OnSceneLoadEnd(int sceneId)
	{
		this.IsFound = false;
	}

	private void Find()
	{
		if (!this.IsFound)
		{
			this.IsFound = true;
			this.goModels.Clear();
			this.goModels.Add(GameObject.Find("CityPlayerPool"));
			this.goModels.Add(GameObject.Find("PlayerPool"));
			this.goModels.Add(GameObject.Find("PetPool"));
			this.goModels.Add(GameObject.Find("MonsterPool"));
		}
	}

	private void Buttons1(ref int index)
	{
		index++;
		if (CamerasMgr.CameraUI != null && !CamerasMgr.CameraUI.get_enabled())
		{
			index++;
			if (GUI.Button(this.GetRect(index), "界面打开"))
			{
				UINodesManager.UIRoot.get_gameObject().SetActive(true);
				UIManagerControl.Instance.FakeHideAllUI(false, 7);
			}
		}
	}

	private void Buttons2(ref int index)
	{
	}

	private Rect GetRect(int index)
	{
		return new Rect((float)(20 + 160 * (index % 2)), (float)(150 + 60 * (index / 2)), 150f, 55f);
	}
}
