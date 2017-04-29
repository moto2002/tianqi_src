using GameData;
using System;
using UnityEngine;
using XEngineActor;

public class XInputManager : MonoBehaviour
{
	protected static XInputManager instance;

	protected static bool enabledLogic = true;

	protected static bool isDragging;

	protected ActorParent actor;

	protected Transform mainCamera;

	protected Vector3 currentDragDirection;

	protected float tiltAroundX;

	protected float tiltAroundZ;

	protected float angle;

	protected XDict<KeyCode, int> skillKey = new XDict<KeyCode, int>();

	protected XDict<KeyCode, bool> skillState = new XDict<KeyCode, bool>();

	protected KeyCode tempKeyCode;

	protected float attackBufferTime = 0.1f;

	protected DateTime lastKeyPressedTime;

	protected int preKeyPressedIndex;

	protected int curKeyPressedIndex;

	public static XInputManager Instance
	{
		get
		{
			return XInputManager.instance;
		}
	}

	public static bool EnabledLogic
	{
		get
		{
			return XInputManager.enabledLogic;
		}
		set
		{
			XInputManager.enabledLogic = value;
		}
	}

	public static bool IsDragging
	{
		get
		{
			return XInputManager.isDragging;
		}
		protected set
		{
			XInputManager.isDragging = value;
		}
	}

	public ActorParent Actor
	{
		get
		{
			return this.actor;
		}
		set
		{
			this.actor = value;
		}
	}

	protected Transform MainCamera
	{
		get
		{
			if (this.mainCamera == null)
			{
				this.mainCamera = CamerasMgr.MainCameraRoot;
			}
			return this.mainCamera;
		}
	}

	public Vector3 CurrentDragDirection
	{
		get
		{
			return this.currentDragDirection;
		}
	}

	private void Awake()
	{
		XInputManager.instance = this;
		this.InitSkillKeyInput();
		this.attackBufferTime = float.Parse(DataReader<GlobalParams>.Get("attack_cache_i").value);
	}

	protected void InitSkillKeyInput()
	{
		this.skillKey.Clear();
		this.skillState.Clear();
		this.skillKey.Add(107, 1);
		this.skillKey.Add(267, 7);
		this.skillKey.Add(104, 8);
		this.skillKey.Add(106, 11);
		this.skillKey.Add(117, 12);
		this.skillKey.Add(105, 13);
		this.skillKey.Add(98, 21);
		this.skillKey.Add(110, 22);
		this.skillKey.Add(109, 23);
		this.skillState.Add(107, false);
		this.skillState.Add(267, false);
		this.skillState.Add(104, false);
		this.skillState.Add(106, false);
		this.skillState.Add(117, false);
		this.skillState.Add(105, false);
		this.skillState.Add(98, false);
		this.skillState.Add(110, false);
		this.skillState.Add(109, false);
	}

	private void Update()
	{
		if (!XInputManager.EnabledLogic)
		{
			return;
		}
		if (RTManager.Instance.RTIsUI())
		{
			return;
		}
		if (!MySceneManager.Instance.IsSceneExist)
		{
			return;
		}
		if (!this.Actor)
		{
			return;
		}
		if (this.Actor.GetEntity() == null)
		{
			return;
		}
		if (this.Actor.GetEntity().IsStatic || this.Actor.GetEntity().IsDizzy || this.Actor.GetEntity().IsWeak || this.Actor.GetEntity().IsFusing || this.Actor.GetEntity().IsHitMoving || this.Actor.GetEntity().IsAssault)
		{
			return;
		}
		if (GlobalBattleNetwork.Instance.IsServerEnable)
		{
			this.tiltAroundX = ControlStick.Direction2ControlStick.x;
			this.tiltAroundZ = ControlStick.Direction2ControlStick.y;
		}
		else
		{
			this.tiltAroundX = 0f;
			this.tiltAroundZ = 0f;
		}
		if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
		{
			this.tiltAroundX = Input.GetAxis("Horizontal");
			this.tiltAroundZ = Input.GetAxis("Vertical");
		}
		if (this.tiltAroundX != 0f || this.tiltAroundZ != 0f)
		{
			this.currentDragDirection = (this.Actor.get_transform().get_position() - this.MainCamera.get_transform().get_position()).get_normalized();
			this.currentDragDirection.y = 0f;
			this.angle = Mathf.Atan2(this.tiltAroundZ, this.tiltAroundX) * 57.29578f;
			Quaternion quaternion = Quaternion.AngleAxis(-this.angle, Vector3.get_up());
			this.currentDragDirection = quaternion * CamerasMgr.MainCameraRoot.get_right();
			this.currentDragDirection.y = 0f;
			this.Actor.MovingSpeed = this.Actor.RealMoveSpeed;
			this.Actor.MovingDirection = this.CurrentDragDirection;
			this.Actor.ApplyMovingDirAsForward();
			if (!XInputManager.IsDragging)
			{
				SelfAIControlManager.Instance.OnBeginDrag();
			}
			if (this.Actor.CanChangeActionTo("run", true, 0, false))
			{
				if (EntityWorld.Instance.EntSelf.CheckCancelNavToNPC())
				{
					MainTaskManager.Instance.StopToNPC(true);
				}
				if (RadarManager.Instance.CheckIsNavByRadar())
				{
					RadarManager.Instance.StopNav();
				}
				if (GuildWarManager.Instance.CheckIsNavToMine())
				{
					GuildWarManager.Instance.StopNavToMine();
				}
			}
			this.Actor.CastAction("run", true, 1f, 0, 0, string.Empty);
			XInputManager.IsDragging = true;
		}
		else
		{
			if (this.Actor.IsClearTargetPosition)
			{
				this.Actor.MovingSpeed = 0f;
				if (XUtility.StartsWith(this.Actor.CurActionStatus, "run"))
				{
					this.Actor.CastAction("idle", true, 1f, 0, 0, string.Empty);
				}
				if (XInputManager.IsDragging)
				{
					this.Actor.CheckSendPrecisePosOnReleaseDrag();
				}
			}
			if (XInputManager.IsDragging)
			{
				SelfAIControlManager.Instance.OnFinishDrag();
			}
			XInputManager.IsDragging = false;
		}
		this.CheckSkillKeyInput();
	}

	protected void CheckSkillKeyInput()
	{
		for (int i = 0; i < this.skillState.Count; i++)
		{
			this.tempKeyCode = this.skillState.ElementKeyAt(i);
			if (Input.GetKey(this.tempKeyCode))
			{
				EventDispatcher.Broadcast("UIStateSystem.ResetFPSSleep");
				if (!this.skillState[this.tempKeyCode])
				{
					this.skillState[this.tempKeyCode] = true;
					this.OnSkillBtnDown(this.skillKey[this.tempKeyCode], true);
				}
			}
			else if (this.skillState[this.tempKeyCode])
			{
				this.skillState[this.tempKeyCode] = false;
				this.OnSkillBtnUp(this.skillKey[this.tempKeyCode]);
			}
		}
	}

	public void OnSkillBtnDown(int index, bool isFromUI = true)
	{
		this.lastKeyPressedTime = DateTime.get_Now();
		this.preKeyPressedIndex = this.curKeyPressedIndex;
		this.curKeyPressedIndex = index;
		if (isFromUI)
		{
			SelfAIControlManager.Instance.OnButtonDown();
		}
		EventDispatcher.Broadcast<int, bool>(XInputManagerEvent.PressSkillKey, this.curKeyPressedIndex, this.curKeyPressedIndex == this.preKeyPressedIndex);
	}

	public void OnSkillBtnUp(int index)
	{
		SelfAIControlManager.Instance.OnButtonUp();
	}

	public void ComboFramePressSkillKey()
	{
		EventDispatcher.Broadcast<bool, bool>(XInputManagerEvent.PressSkillKeyWithCushionCheck, this.curKeyPressedIndex == this.preKeyPressedIndex, (DateTime.get_Now() - this.lastKeyPressedTime).get_TotalMilliseconds() <= (double)this.attackBufferTime);
	}

	public void StartSendPosAndDir()
	{
		this.Actor.EnableSendPos = true;
		this.Actor.EnableSendDir = true;
	}

	public void StopSendPosAndDir()
	{
		this.Actor.EnableSendPos = false;
		this.Actor.EnableSendDir = false;
	}
}
