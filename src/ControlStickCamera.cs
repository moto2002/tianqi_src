using System;
using UnityEngine;

public class ControlStickCamera : BaseUIBehaviour
{
	private static ControlStickCamera m_instance;

	public static Vector2 Direction2Camera = Vector2.get_zero();

	private int _FingerId = -100;

	public bool IsCameraMoved;

	private static bool m_IsDraging = false;

	private Rect cc_RectBaseBottomL1 = default(Rect);

	public static Vector2 m_beginTouchBaseBottomL = new Vector2(0f, 0f);

	public static ControlStickCamera Instance
	{
		get
		{
			return ControlStickCamera.m_instance;
		}
	}

	public int FingerId
	{
		get
		{
			return this._FingerId;
		}
		set
		{
			this._FingerId = value;
		}
	}

	public static bool IsDraging
	{
		get
		{
			return ControlStickCamera.m_IsDraging;
		}
		set
		{
			ControlStickCamera.m_IsDraging = value;
		}
	}

	private void Awake()
	{
		ControlStickCamera.m_instance = this;
		this.AddListenersOfSelf();
		this.OnForbiddenStick(true);
		base.Invoke("DelayInit", 0.1f);
	}

	private void DelayInit()
	{
		ControlStickCamera.IsDraging = false;
		this.SetTouchRect();
	}

	private void AddListenersOfSelf()
	{
		EventDispatcher.AddListener<bool>("ControlStick.ForbiddenStick", new Callback<bool>(this.OnForbiddenStick));
		EventDispatcher.AddListener("ControlStick.InterruptStick", new Callback(this.OnInterruptStick));
		EventDispatcher.AddListener("ControlStick.HardwareResolutionChange", new Callback(this.OnHardwareResolutionChange));
	}

	private void OnForbiddenStick(bool forbidden)
	{
		if (this != null)
		{
			if (forbidden)
			{
				base.set_enabled(false);
			}
			else if (UIManagerControl.Instance.IsOpen("TownUI") || UIManagerControl.Instance.IsOpen("BattleUI"))
			{
				base.set_enabled(true);
			}
		}
	}

	private void OnInterruptStick()
	{
		if (this != null && base.get_enabled() && base.get_gameObject() != null && base.get_gameObject().get_activeSelf())
		{
			this.TouchEndReset();
		}
	}

	private void OnHardwareResolutionChange()
	{
		this.SetTouchRect();
	}

	public void UpdateSelf()
	{
		if (!CamerasMgr.CameraUI.get_enabled())
		{
			ControlStickCamera.Direction2Camera = Vector2.get_zero();
			return;
		}
		if (!ControlStickCamera.IsDraging)
		{
			this.CheckTouchBegin();
			if (ControlStickCamera.IsDraging)
			{
				EventDispatcher.Broadcast("UIStateSystem.ResetFPSSleep");
			}
			ControlStickCamera.Direction2Camera = Vector2.get_zero();
			return;
		}
		if (InputManager.InputIsTouchScreen())
		{
			if (InputManager.IsFingerMove(this.FingerId))
			{
				this.IsCameraMoved = true;
			}
		}
		else if (ControlStickCamera.Direction2Camera != Vector2.get_zero())
		{
			this.IsCameraMoved = true;
		}
		if (this.TouchEnd())
		{
			this.TouchEndReset();
			return;
		}
		ControlStickCamera.Direction2Camera = ControlStickCamera.GetDirection();
	}

	public void CheckTouchBegin()
	{
		if (ControlStick.Instance == null || ControlStickCamera.Instance == null)
		{
			return;
		}
		if (InputManager.InputIsTouchScreen())
		{
			if (Input.get_touchCount() > 0)
			{
				for (int i = 0; i < Input.get_touchCount(); i++)
				{
					Touch touch = Input.GetTouch(i);
					if (touch.get_fingerId() != ControlStick.Instance.FingerId && touch.get_fingerId() != ControlStickCamera.Instance.FingerId)
					{
						if (touch.get_phase() == null && this.IsTouchBeginValid(touch.get_position(), touch.get_fingerId()))
						{
							this.FingerId = touch.get_fingerId();
							this.IsCameraMoved = false;
							ControlStickCamera.Instance.TouchBeginSuccess(touch.get_position());
							return;
						}
					}
				}
			}
		}
		else if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
		{
			if (ControlStick.Instance.IsDraging || ControlStickCamera.IsDraging)
			{
				return;
			}
			if (this.IsTouchBeginValid(InputManager.GetTouchPositionBaseBottomL(this.FingerId), -100))
			{
				this.IsCameraMoved = false;
				ControlStickCamera.Instance.TouchBeginSuccess(InputManager.GetTouchPositionBaseBottomL(this.FingerId));
			}
		}
	}

	private bool IsTouchBeginValid(Vector2 vTouchPosBaseBottomL, int pointId = -100)
	{
		return !(ControlStick.Instance == null) && !(ControlStickCamera.Instance == null) && !InputManager.IsTriggerUI(pointId) && (!ControlStick.Instance.TouchInValidRect(vTouchPosBaseBottomL) && this.TouchInValidRect(vTouchPosBaseBottomL));
	}

	private void TouchBeginSuccess(Vector2 vTouchPosBaseBottomL)
	{
		ControlStickCamera.m_beginTouchBaseBottomL = vTouchPosBaseBottomL;
		ControlStickCamera.Direction2Camera = ControlStickCamera.GetDirection();
		ControlStickCamera.IsDraging = true;
	}

	private void SetTouchRect()
	{
		this.cc_RectBaseBottomL1 = new Rect(0f, 0f, (float)CamerasMgr.CameraMain.get_pixelWidth(), (float)CamerasMgr.CameraMain.get_pixelHeight());
	}

	public bool TouchInValidRect(Vector2 touchPosition)
	{
		return this.cc_RectBaseBottomL1.Contains(touchPosition);
	}

	private bool TouchEnd()
	{
		if (InputManager.InputIsTouchScreen())
		{
			if (Input.get_touchCount() > 0)
			{
				for (int i = 0; i < Input.get_touchCount(); i++)
				{
					Touch touch = Input.GetTouch(i);
					if (touch.get_fingerId() == this.FingerId && touch.get_phase() == 3)
					{
						return true;
					}
				}
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			return true;
		}
		return false;
	}

	private void TouchEndReset()
	{
		this.FingerId = -100;
		ControlStickCamera.IsDraging = false;
		ControlStickCamera.Direction2Camera = Vector2.get_zero();
	}

	public static Vector2 GetDirection()
	{
		if (ControlStickCamera.Instance == null)
		{
			return Vector2.get_zero();
		}
		Vector2 touchPositionBaseBottomL = InputManager.GetTouchPositionBaseBottomL(ControlStickCamera.Instance.FingerId);
		Vector2 result = new Vector2(touchPositionBaseBottomL.x, touchPositionBaseBottomL.y) - new Vector2(ControlStickCamera.m_beginTouchBaseBottomL.x, ControlStickCamera.m_beginTouchBaseBottomL.y);
		ControlStickCamera.m_beginTouchBaseBottomL = touchPositionBaseBottomL;
		return result;
	}
}
