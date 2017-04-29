using GameData;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TimelineHierarchy : MonoBehaviour
{
	private class FrameRow
	{
		public List<int> frames = new List<int>();

		public int idleFrameIndex = -1;
	}

	private class FrameSheet
	{
		public Dictionary<int, TimelineHierarchy.FrameRow> frameRows = new Dictionary<int, TimelineHierarchy.FrameRow>();
	}

	public static TimelineHierarchy instance;

	private bool m_out_system_lock;

	private Vector3 cameraMoveBegin;

	private Vector3 cameraMoveEnd;

	private float cameraMoveSpeed;

	private Vector3 cameraLookBegin;

	private Vector3 cameraLookEnd;

	private Vector3 cameraForwardBegin;

	private Vector3 cameraForwardEnd;

	private float cameraForwardSpeed;

	private bool isPause;

	private bool isManualContinue;

	private List<int> frameRowIds;

	private int frameLength;

	private float currTime;

	private int currFrameIndex;

	private int prevFrameIndex = -1;

	private Dictionary<int, TimelineHierarchy.FrameSheet> frameSheets = new Dictionary<int, TimelineHierarchy.FrameSheet>();

	private Vector3 fightCameraPosition;

	private Quaternion fightCameraRotation;

	private List<TimelineNpc> npcActionSeqs = new List<TimelineNpc>();

	private Dictionary<int, bool> manualDelSpineIds = new Dictionary<int, bool>();

	private Dictionary<int, bool> autoDelSpineIds = new Dictionary<int, bool>();

	private int spineZBuffer;

	private Dictionary<int, List<List<int>>> comics = new Dictionary<int, List<List<int>>>();

	private Dictionary<int, int> loopSpines = new Dictionary<int, int>();

	private int actionIdCurr;

	private List<int> comicIdsCurr = new List<int>();

	private int manualContinueSpineId;

	private bool out_system_lock
	{
		get
		{
			return this.m_out_system_lock;
		}
		set
		{
			if (value)
			{
				GuideManager.Instance.out_system_lock = true;
			}
			else if (this.m_out_system_lock)
			{
				GuideManager.Instance.out_system_lock = false;
			}
			this.m_out_system_lock = value;
		}
	}

	public object UIManager
	{
		get;
		private set;
	}

	private void Awake()
	{
		TimelineHierarchy.instance = this;
		this.Init();
	}

	private void OnEnable()
	{
	}

	private void Section0Begin()
	{
		CameraGlobal.cameraType = CameraType.Timeline;
		InstanceManager.IsPauseTimeEscape = true;
		TimelineGlobal.SetVisible(false);
		this.fightCameraPosition = CamerasMgr.MainCameraRoot.get_position();
		this.fightCameraRotation = CamerasMgr.MainCameraRoot.get_rotation();
	}

	private void Section0End()
	{
		InstanceManager.IsPauseTimeEscape = false;
		TimelineGlobal.timelineCallback.Invoke();
		TimelineGlobal.ClearNpcs();
		TimelineGlobal.SetVisible(true);
		CameraGlobal.cameraType = CameraType.Follow;
		CamerasMgr.MainCameraRoot.set_position(this.fightCameraPosition);
		CamerasMgr.MainCameraRoot.set_rotation(this.fightCameraRotation);
	}

	private void Section1Begin()
	{
		InstanceManager.IsPauseTimeEscape = true;
		FXManager.Instance.DeleteFXs();
		TimelineGlobal.SetVisible(false);
		Utils.SetCameraCullingMask(CamerasMgr.CameraMain, 6);
		CameraGlobal.cameraType = CameraType.None;
	}

	private void Section1End()
	{
		InstanceManager.IsPauseTimeEscape = false;
		TimelineGlobal.timelineCallback.Invoke();
		TimelineGlobal.ClearNpcs();
		TimelineGlobal.SetVisible(true);
		UIManagerControl.Instance.HideAll();
	}

	private void FrameStart()
	{
		DongHuaShiJianBiao dongHuaShiJianBiao = DataReader<DongHuaShiJianBiao>.Get(TimelineGlobal.timelineId);
		if (TimelineGlobal.timelineSection == 0)
		{
			this.frameLength = dongHuaShiJianBiao.beginEventTime;
			this.frameRowIds = dongHuaShiJianBiao.beginEventId;
			this.Section0Begin();
		}
		else
		{
			this.frameLength = dongHuaShiJianBiao.endEventTime;
			this.frameRowIds = dongHuaShiJianBiao.endEventId;
			this.Section1Begin();
		}
		Debug.LogError("frameLength=" + this.frameLength);
	}

	private void InitFrameSheets()
	{
		for (int i = 0; i < this.frameRowIds.get_Count(); i++)
		{
			ShiJianShiJianBiao shiJianShiJianBiao = DataReader<ShiJianShiJianBiao>.Get(this.frameRowIds.get_Item(i));
			List<int> list = new List<int>();
			for (int j = 0; j < this.frameLength; j++)
			{
				Type typeFromHandle = typeof(ShiJianShiJianBiao);
				PropertyInfo property = typeFromHandle.GetProperty("time" + j);
				int num = (int)property.GetValue(shiJianShiJianBiao, null);
				list.Add(num);
			}
			int eventType = shiJianShiJianBiao.eventType;
			int eventId = shiJianShiJianBiao.eventId;
			this.frameSheets.get_Item(eventType).frameRows.Add(eventId, new TimelineHierarchy.FrameRow
			{
				frames = list,
				idleFrameIndex = -1
			});
		}
	}

	private void CreateFrameSheets()
	{
		for (int i = 1; i < 10; i++)
		{
			this.frameSheets.set_Item(i, new TimelineHierarchy.FrameSheet());
		}
	}

	private void Init()
	{
		this.CreateFrameSheets();
		this.FrameStart();
		this.InitFrameSheets();
	}

	private bool IsGoOn()
	{
		return !this.isPause && !this.isManualContinue;
	}

	private void Update()
	{
		this.CheckContinue();
		if (!this.IsGoOn())
		{
			return;
		}
		this.currTime += Time.get_deltaTime();
		this.currFrameIndex = Mathf.FloorToInt(this.currTime);
		if (this.IsFrameEnd())
		{
			this.FrameEnd();
			this.out_system_lock = false;
			Object.Destroy(this);
			return;
		}
		this.DoPause();
		if (!this.IsGoOn())
		{
			return;
		}
		this.DoComic();
		if (!this.IsGoOn())
		{
			return;
		}
		this.DoSubtitle();
		if (!this.IsGoOn())
		{
			return;
		}
		this.DoDialogue();
		if (!this.IsGoOn())
		{
			return;
		}
		this.DoCamera();
		this.DoNpc();
		this.DoSound();
		this.DoFx();
		this.DoDel();
		this.SetPrevFrameIndex();
	}

	private void CheckContinue()
	{
		if (Input.GetMouseButton(0) || (Input.get_touchCount() > 0 && Input.GetTouch(0).get_phase() == 3))
		{
			if (this.isManualContinue && this.comicIdsCurr.get_Count() == 0)
			{
				this.isManualContinue = false;
				this.PlaySpineCallback(this.actionIdCurr);
				if (this.manualContinueSpineId != 0)
				{
					FXSpineManager.Instance.DeleteSpine(this.manualContinueSpineId, true);
					this.manualContinueSpineId = 0;
				}
			}
			using (Dictionary<int, int>.Enumerator enumerator = this.loopSpines.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, int> current = enumerator.get_Current();
					FXSpineManager.Instance.DeleteSpine(current.get_Key(), true);
				}
			}
			this.loopSpines.Clear();
		}
	}

	private void SetPrevFrameIndex()
	{
		this.prevFrameIndex = this.currFrameIndex;
	}

	private Vector3 List2Vector3(List<float> l)
	{
		return new Vector3(l.get_Item(0), l.get_Item(1), l.get_Item(2));
	}

	private int GetMoveToFrameIndex(TimelineHierarchy.FrameRow frameRow, int moveFromFrameIndex)
	{
		while (!this.IsFrameEnd(++moveFromFrameIndex))
		{
			int num = frameRow.frames.get_Item(moveFromFrameIndex);
			if (num != 0)
			{
				return moveFromFrameIndex;
			}
		}
		return -1;
	}

	private bool IsFrameNext()
	{
		return this.currFrameIndex > this.prevFrameIndex;
	}

	private bool IsFrameEnd()
	{
		return this.currFrameIndex >= this.frameLength;
	}

	private bool IsFrameEnd(int frameIndex)
	{
		return frameIndex >= this.frameLength;
	}

	private bool IsFrameIdle(TimelineHierarchy.FrameRow frameRow)
	{
		return this.currFrameIndex > frameRow.idleFrameIndex;
	}

	private void FrameEnd()
	{
		if (TimelineGlobal.timelineSection == 0)
		{
			this.Section0End();
		}
		else
		{
			this.Section1End();
		}
	}

	private Vector3 FloatToOrientation(float degree)
	{
		Quaternion quaternion = Quaternion.AngleAxis(degree, Vector3.get_up());
		return quaternion * Vector3.get_right();
	}

	private void DoCamera()
	{
		using (Dictionary<int, TimelineHierarchy.FrameRow>.Enumerator enumerator = this.frameSheets.get_Item(1).frameRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, TimelineHierarchy.FrameRow> current = enumerator.get_Current();
				TimelineHierarchy.FrameRow value = current.get_Value();
				if (this.currFrameIndex > value.idleFrameIndex)
				{
					int num = value.frames.get_Item(this.currFrameIndex);
					if (num != 0)
					{
						ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
						List<float> position = shiJianCanShuBiao.position;
						List<float> towardPoint = shiJianCanShuBiao.towardPoint;
						int moveToFrameIndex = this.GetMoveToFrameIndex(value, this.currFrameIndex);
						if (moveToFrameIndex >= 0)
						{
							value.idleFrameIndex = moveToFrameIndex;
							num = value.frames.get_Item(moveToFrameIndex);
							shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
							this.cameraMoveEnd = this.List2Vector3(shiJianCanShuBiao.position);
							this.cameraLookEnd = this.List2Vector3(shiJianCanShuBiao.towardPoint);
							this.cameraMoveBegin = this.List2Vector3(position);
							this.cameraLookBegin = this.List2Vector3(towardPoint);
							CamerasMgr.CameraMain.get_transform().set_position(this.cameraMoveBegin);
							CamerasMgr.CameraMain.get_transform().LookAt(this.cameraLookBegin);
							int num2 = moveToFrameIndex - this.currFrameIndex;
							this.cameraMoveSpeed = (this.cameraMoveEnd - this.cameraMoveBegin).get_magnitude() / (float)num2;
							this.cameraForwardBegin = (this.cameraLookBegin - this.cameraMoveBegin).get_normalized();
							this.cameraForwardEnd = (this.cameraLookEnd - this.cameraMoveEnd).get_normalized();
							this.cameraForwardSpeed = Vector3.Angle(this.cameraForwardBegin, this.cameraForwardEnd) * 0.0174532924f / (float)num2;
							break;
						}
					}
				}
			}
		}
		if (this.cameraMoveBegin != this.cameraMoveEnd || CamerasMgr.MainCameraRoot.get_forward() != this.cameraForwardEnd)
		{
			Vector3 position2 = CamerasMgr.MainCameraRoot.get_position();
			Vector3 vector = this.cameraMoveEnd;
			CamerasMgr.MainCameraRoot.set_position(Vector3.MoveTowards(position2, vector, this.cameraMoveSpeed * Time.get_deltaTime()));
			Vector3 forward = CamerasMgr.MainCameraRoot.get_forward();
			Vector3 vector2 = this.cameraForwardEnd;
			CamerasMgr.MainCameraRoot.set_forward(Vector3.RotateTowards(forward, vector2, this.cameraForwardSpeed * Time.get_deltaTime(), 0f));
			this.cameraMoveBegin = CamerasMgr.MainCameraRoot.get_position();
		}
	}

	private void DoNpc()
	{
		using (Dictionary<int, TimelineHierarchy.FrameRow>.Enumerator enumerator = this.frameSheets.get_Item(2).frameRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, TimelineHierarchy.FrameRow> current = enumerator.get_Current();
				TimelineHierarchy.FrameRow value = current.get_Value();
				if (this.currFrameIndex > value.idleFrameIndex)
				{
					int num = value.frames.get_Item(this.currFrameIndex);
					if (num != 0)
					{
						ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
						int num2 = shiJianCanShuBiao.modelId;
						string action = shiJianCanShuBiao.action;
						float time = 0f;
						Vector3 posA = this.List2Vector3(shiJianCanShuBiao.position);
						Vector3 towardA = this.FloatToOrientation((float)shiJianCanShuBiao.towardAngle);
						Vector3 posB = Vector3.get_zero();
						Vector3 towardB = Vector3.get_zero();
						if (action == "run" || action == "run_city")
						{
							int moveToFrameIndex = this.GetMoveToFrameIndex(value, this.currFrameIndex);
							if (moveToFrameIndex < 0)
							{
								continue;
							}
							int key = value.frames.get_Item(moveToFrameIndex);
							shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(key);
							time = (float)(moveToFrameIndex - this.currFrameIndex);
							value.idleFrameIndex = moveToFrameIndex;
							posB = this.List2Vector3(shiJianCanShuBiao.position);
							towardB = this.FloatToOrientation((float)shiJianCanShuBiao.towardAngle);
						}
						else
						{
							value.idleFrameIndex = this.currFrameIndex + 1;
						}
						if (num2 == 1)
						{
							num2 = EntityWorld.Instance.ActSelf.resGUID;
						}
						for (int i = 0; i < this.npcActionSeqs.get_Count(); i++)
						{
							TimelineNpc timelineNpc = this.npcActionSeqs.get_Item(i);
							if (timelineNpc.npcId == num2)
							{
								timelineNpc.SwitchingAction(action, num2, posA, towardA, posB, towardB, time);
								return;
							}
						}
						TimelineNpc timelineNpc2 = new TimelineNpc(action, num2, posA, towardA, posB, towardB, time);
						this.npcActionSeqs.Add(timelineNpc2);
					}
				}
			}
		}
	}

	private void DoDialogue()
	{
		using (Dictionary<int, TimelineHierarchy.FrameRow>.Enumerator enumerator = this.frameSheets.get_Item(3).frameRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, TimelineHierarchy.FrameRow> current = enumerator.get_Current();
				TimelineHierarchy.FrameRow value = current.get_Value();
				if (this.currFrameIndex > value.idleFrameIndex)
				{
					int num = value.frames.get_Item(this.currFrameIndex);
					if (num != 0)
					{
						this.isPause = true;
						value.idleFrameIndex = this.currFrameIndex + 1;
						ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
						List<int> diolague = shiJianCanShuBiao.diolague;
						MainTaskManager.Instance.OpenTalkUI(diolague, false, delegate
						{
							this.isPause = false;
						}, 0);
					}
				}
			}
		}
	}

	private void DoSubtitle()
	{
		using (Dictionary<int, TimelineHierarchy.FrameRow>.Enumerator enumerator = this.frameSheets.get_Item(4).frameRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, TimelineHierarchy.FrameRow> current = enumerator.get_Current();
				TimelineHierarchy.FrameRow value = current.get_Value();
				if (this.currFrameIndex > value.idleFrameIndex)
				{
					int num = value.frames.get_Item(this.currFrameIndex);
					if (num != 0)
					{
						this.isPause = true;
						value.idleFrameIndex = this.currFrameIndex + 1;
						List<string> list = new List<string>();
						ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
						if (shiJianCanShuBiao != null)
						{
							for (int i = 0; i < shiJianCanShuBiao.word.get_Count(); i++)
							{
								list.Add(GameDataUtils.GetChineseContent(shiJianCanShuBiao.word.get_Item(i), false));
							}
						}
						else
						{
							Debug.LogError("GameData.ShiJianCanShuBiao no exist, id = " + num);
						}
						SubtitlesUI subtitlesUI = UIManagerControl.Instance.OpenUI("SubtitlesUI", UINodesManager.T2RootOfSpecial, false, UIType.NonPush) as SubtitlesUI;
						subtitlesUI.Init(list, delegate
						{
							this.isPause = false;
						});
					}
				}
			}
		}
	}

	private void DoPause()
	{
		using (Dictionary<int, TimelineHierarchy.FrameRow>.Enumerator enumerator = this.frameSheets.get_Item(9).frameRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, TimelineHierarchy.FrameRow> current = enumerator.get_Current();
				TimelineHierarchy.FrameRow value = current.get_Value();
				if (this.currFrameIndex > value.idleFrameIndex)
				{
					int num = value.frames.get_Item(this.currFrameIndex);
					if (num != 0)
					{
						value.idleFrameIndex = this.currFrameIndex + 1;
						Debug.LogError("DoPause=" + num);
						this.isManualContinue = true;
					}
				}
			}
		}
	}

	private void DoComic()
	{
		using (Dictionary<int, TimelineHierarchy.FrameRow>.Enumerator enumerator = this.frameSheets.get_Item(5).frameRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, TimelineHierarchy.FrameRow> current = enumerator.get_Current();
				TimelineHierarchy.FrameRow value = current.get_Value();
				if (this.currFrameIndex > value.idleFrameIndex)
				{
					int num = value.frames.get_Item(this.currFrameIndex);
					if (num != 0)
					{
						value.idleFrameIndex = this.currFrameIndex + 1;
						ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
						Debug.LogError(string.Concat(new object[]
						{
							"DoComic anime=",
							shiJianCanShuBiao.anime,
							" anime2=",
							shiJianCanShuBiao.anime2
						}));
						if (shiJianCanShuBiao.anime == -1)
						{
							CamerasMgr.OpenEye();
						}
						else if (shiJianCanShuBiao.anime <= 0)
						{
							if (shiJianCanShuBiao.anime2 != string.Empty)
							{
								this.isPause = true;
								List<List<int>> list = new List<List<int>>();
								string[] array = shiJianCanShuBiao.anime2.Split(new char[]
								{
									';'
								});
								for (int i = 0; i < array.Length; i++)
								{
									List<int> list2 = new List<int>();
									string[] array2 = array[i].Split(new char[]
									{
										','
									});
									for (int j = 0; j < array2.Length; j++)
									{
										list2.Add(int.Parse(array2[j]));
									}
									list.Add(list2);
								}
								this.comics.set_Item(num, list);
								this.PlaySpineCallback(num);
							}
						}
						this.out_system_lock = true;
					}
				}
			}
		}
	}

	private void PlaySpineCallback(int actionId)
	{
		if (!this.comics.ContainsKey(actionId) || this.comics.get_Item(actionId).get_Count() == 0)
		{
			this.isPause = false;
			this.out_system_lock = false;
			return;
		}
		this.actionIdCurr = actionId;
		List<int> list = this.comics.get_Item(actionId).get_Item(0);
		this.comics.get_Item(actionId).RemoveAt(0);
		this.comicIdsCurr.AddRange(list);
		for (int i = 0; i < list.get_Count(); i++)
		{
			int templateId = list.get_Item(i);
			int spineId = 0;
			spineId = FXSpineManager.Instance.PlaySpine(templateId, UINodesManager.MiddleUIRoot, null, 2000 + ++this.spineZBuffer, delegate
			{
				ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(actionId);
				if (shiJianCanShuBiao.isManualContinue == 1)
				{
					this.isManualContinue = true;
					if (this.manualContinueSpineId == 0)
					{
						this.manualContinueSpineId = FXSpineManager.Instance.PlaySpine(3007, UINodesManager.MiddleUIRoot, null, 2000 + ++this.spineZBuffer, null, "UI", 500f, -300f, 1f, 1f, false, FXMaskLayer.MaskState.None);
					}
					this.loopSpines.set_Item(spineId, spineId);
					this.comicIdsCurr.RemoveAt(0);
				}
				else
				{
					FXSpineManager.Instance.DeleteSpine(spineId, true);
					this.PlaySpineCallback(actionId);
				}
			}, "UI", 0f, 0f, 1f, 1f, false, FXMaskLayer.MaskState.None);
		}
	}

	private void DoSound()
	{
		using (Dictionary<int, TimelineHierarchy.FrameRow>.Enumerator enumerator = this.frameSheets.get_Item(6).frameRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, TimelineHierarchy.FrameRow> current = enumerator.get_Current();
				TimelineHierarchy.FrameRow value = current.get_Value();
				if (this.currFrameIndex > value.idleFrameIndex)
				{
					int num = value.frames.get_Item(this.currFrameIndex);
					if (num != 0)
					{
						value.idleFrameIndex = this.currFrameIndex + 1;
						ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
						Debug.LogError("DoSound=" + shiJianCanShuBiao.voice);
						SoundManager.Instance.PlayPlayer(base.get_gameObject().GetComponent<AudioPlayer>(), shiJianCanShuBiao.voice);
					}
				}
			}
		}
	}

	private void DoFx()
	{
		using (Dictionary<int, TimelineHierarchy.FrameRow>.Enumerator enumerator = this.frameSheets.get_Item(7).frameRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, TimelineHierarchy.FrameRow> current = enumerator.get_Current();
				TimelineHierarchy.FrameRow value = current.get_Value();
				if (this.currFrameIndex > value.idleFrameIndex)
				{
					int num = value.frames.get_Item(this.currFrameIndex);
					if (num != 0)
					{
						value.idleFrameIndex = this.currFrameIndex + 1;
						ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
						int fx = shiJianCanShuBiao.fx;
						Vector3 position = this.List2Vector3(shiJianCanShuBiao.position);
						Vector3 vector = this.FloatToOrientation((float)shiJianCanShuBiao.towardAngle);
						Quaternion rotation = Quaternion.LookRotation(vector);
						FXManager.Instance.PlayFXOfDisplay(fx, null, position, rotation, 1f, 1f, 0, false, null, null);
					}
				}
			}
		}
	}

	private void DoDel()
	{
		using (Dictionary<int, TimelineHierarchy.FrameRow>.Enumerator enumerator = this.frameSheets.get_Item(8).frameRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<int, TimelineHierarchy.FrameRow> current = enumerator.get_Current();
				TimelineHierarchy.FrameRow value = current.get_Value();
				if (this.currFrameIndex > value.idleFrameIndex)
				{
					int num = value.frames.get_Item(this.currFrameIndex);
					if (num != 0)
					{
						value.idleFrameIndex = this.currFrameIndex + 1;
						ShiJianCanShuBiao shiJianCanShuBiao = DataReader<ShiJianCanShuBiao>.Get(num);
						Debug.LogError("DoDel=" + shiJianCanShuBiao.function);
						if (shiJianCanShuBiao.function == 1)
						{
							TimelineGlobal.DelNpc(shiJianCanShuBiao.modelId);
						}
					}
				}
			}
		}
	}
}
