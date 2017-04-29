using Foundation.Core.Databinding;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ControlStick : UIBase
{
	private static ControlStick m_instance;

	private Image m_spBg;

	private Image m_spStick;

	private float m_fSceenRangeStick;

	private float m_fRangeStick = 100f;

	private static bool IsControlStickCanMove = true;

	public static Vector2 Direction2ControlStick = Vector2.get_zero();

	private int _FingerId = -100;

	private Rect cs_RectBaseBottomL1 = default(Rect);

	private bool m_IsDraging;

	private Vector3 default_worldpos = Vector3.get_zero();

	public static Vector2 m_beginTouchBaseBottomL = new Vector2(0f, 0f);

	public static ControlStick Instance
	{
		get
		{
			return ControlStick.m_instance;
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

	public bool IsDraging
	{
		get
		{
			return this.m_IsDraging;
		}
		set
		{
			this.m_IsDraging = value;
			this.SetBackgroundOfA(this.m_IsDraging);
		}
	}

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isEndNav = false;
		this.isIgnoreToSpine = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		ControlStick.m_instance = this;
		this.m_spBg = base.FindTransform("ControlStickBg").GetComponent<Image>();
		this.m_spStick = base.FindTransform("ControlStickDot").GetComponent<Image>();
		this.ShowBackground(false);
		this.SetBackgroundOfA(false);
		this.OnForbiddenStick(true);
		base.Invoke("DelayInit", 0.1f);
	}

	private void DelayInit()
	{
		this.m_fSceenRangeStick = this.m_fRangeStick * this.CalculateRate();
		this.IsDraging = false;
		this.SetTouchRect();
		this.CalculateDefaultWorldPos();
		this.TouchEndReset();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.ShowBackground(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		this.ShowBackground(false);
		this.TouchEndReset();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn && calledDestroy)
		{
			ControlStick.m_instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<bool>("ControlStick.ForbiddenStick", new Callback<bool>(this.OnForbiddenStick));
		EventDispatcher.AddListener("ControlStick.InterruptStick", new Callback(this.OnInterruptStick));
		EventDispatcher.AddListener("ControlStick.HardwareResolutionChange", new Callback(this.OnHardwareResolutionChange));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<bool>("ControlStick.ForbiddenStick", new Callback<bool>(this.OnForbiddenStick));
		EventDispatcher.RemoveListener("ControlStick.InterruptStick", new Callback(this.OnInterruptStick));
		EventDispatcher.RemoveListener("ControlStick.HardwareResolutionChange", new Callback(this.OnHardwareResolutionChange));
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
			ControlStick.Direction2ControlStick = Vector2.get_zero();
			return;
		}
		if (!this.IsDraging)
		{
			this.CheckTouchBegin();
			if (this.IsDraging)
			{
				EventDispatcher.Broadcast("UIStateSystem.ResetFPSSleep");
			}
			ControlStick.Direction2ControlStick = Vector2.get_zero();
			return;
		}
		if (this.TouchEnd())
		{
			this.TouchEndReset();
			return;
		}
		Vector2 offset = this.ChangeStickPositon();
		this.OffsetControlStickPos(offset);
		ControlStick.Direction2ControlStick = ControlStick.GetDirection();
	}

	private void OffsetControlStickPos(Vector2 offset)
	{
		if (ControlStick.IsControlStickCanMove)
		{
			ControlStick.m_beginTouchBaseBottomL += offset;
			this.ChangeControlStickPos(ControlStick.m_beginTouchBaseBottomL);
		}
	}

	private void ChangeControlStickPos(Vector2 screenPos)
	{
		Vector3 vector = CamerasMgr.CameraUI.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));
		this.m_myTransform.set_position(new Vector3(vector.x, vector.y, 0f));
		this.m_myTransform.set_localPosition(new Vector3(this.m_myTransform.get_localPosition().x, this.m_myTransform.get_localPosition().y, 0f));
	}

	private Vector2 ChangeStickPositon()
	{
		Vector2 touchPositionBaseBottomL = InputManager.GetTouchPositionBaseBottomL(this.FingerId);
		Vector2 vector = new Vector2(touchPositionBaseBottomL.x, touchPositionBaseBottomL.y) - new Vector2(ControlStick.m_beginTouchBaseBottomL.x, ControlStick.m_beginTouchBaseBottomL.y);
		float num = vector.get_magnitude() - this.m_fSceenRangeStick;
		if (num > 0f)
		{
			vector = vector.get_normalized();
			Vector2 vector2 = vector * this.m_fRangeStick;
			Vector2 result = vector * num;
			this.m_spStick.get_transform().set_localPosition(new Vector3(vector2.x, vector2.y, 0f));
			return result;
		}
		Vector2 touchPositionBaseBottomL2 = InputManager.GetTouchPositionBaseBottomL(this.FingerId);
		Vector3 vector3 = CamerasMgr.CameraUI.ScreenToWorldPoint(new Vector3(touchPositionBaseBottomL2.x, touchPositionBaseBottomL2.y, 0f));
		Transform transform = this.m_spStick.get_transform();
		transform.set_position(new Vector3(vector3.x, vector3.y, 0f));
		transform.set_localPosition(new Vector3(transform.get_localPosition().x, transform.get_localPosition().y, 0f));
		return new Vector2(0f, 0f);
	}

	private void SetTouchRect()
	{
		this.cs_RectBaseBottomL1 = new Rect(0f, 0f, (float)CamerasMgr.CameraMain.get_pixelWidth() * 0.35f, (float)CamerasMgr.CameraMain.get_pixelHeight() * 0.58f);
	}

	public bool TouchInValidRect(Vector2 touchPosition)
	{
		return this.cs_RectBaseBottomL1.Contains(touchPosition);
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
						if ((touch.get_phase() == null || touch.get_phase() == 1 || touch.get_phase() == 2) && this.IsTouchBeginValid(touch.get_position(), touch.get_fingerId()))
						{
							this.FingerId = touch.get_fingerId();
							ControlStick.Instance.TouchBeginSuccess(touch.get_position());
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
				ControlStick.Instance.TouchBeginSuccess(InputManager.GetTouchPositionBaseBottomL(this.FingerId));
			}
		}
	}

	private bool IsTouchBeginValid(Vector2 vTouchPosBaseBottomL, int pointId = -100)
	{
		return !(ControlStick.Instance == null) && !(ControlStickCamera.Instance == null) && ControlStick.Instance.TouchInValidRect(vTouchPosBaseBottomL) && !InputManager.IsTriggerUI(pointId);
	}

	private void TouchBeginSuccess(Vector2 vTouchPosBaseBottomL)
	{
		ControlStick.m_beginTouchBaseBottomL = vTouchPosBaseBottomL;
		this.ChangeControlStickPos(vTouchPosBaseBottomL);
		ControlStick.Direction2ControlStick = ControlStick.GetDirection();
		this.IsDraging = true;
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
		this.IsDraging = false;
		ControlStick.Direction2ControlStick = Vector2.get_zero();
		if (this.m_spStick != null)
		{
			this.m_spStick.get_transform().set_localPosition(Vector3.get_zero());
		}
	}

	private void SetBackgroundOfA(bool isShow)
	{
		if (this.m_spBg != null && this.m_spStick != null)
		{
			float alpha = 1f;
			if (!isShow)
			{
				alpha = 0.5f;
				this.SetToDefaultPos();
				this.m_spBg.get_transform().set_localPosition(Vector3.get_zero());
				this.m_spStick.get_transform().set_localPosition(Vector3.get_zero());
			}
			this.m_spBg.set_color(this.GetColor(alpha));
			this.m_spStick.set_color(this.GetColor(alpha));
		}
	}

	private Color GetColor(float alpha)
	{
		return new Color(1f, 1f, 1f, alpha);
	}

	private void ShowBackground(bool isShow)
	{
		this.SetToDefaultPos();
		if (this.m_spBg != null && this.m_spStick != null)
		{
			this.m_spBg.set_enabled(isShow);
			this.m_spStick.set_enabled(isShow);
		}
	}

	public void CalculateDefaultWorldPos()
	{
		Vector2 vector = new Vector2(140f, 130f);
		vector *= this.CalculateRate();
		this.default_worldpos = CamerasMgr.CameraUI.ScreenToWorldPoint(new Vector3(vector.x, vector.y, 0f));
	}

	private void SetToDefaultPos()
	{
		base.get_transform().set_position(this.default_worldpos);
		base.get_transform().set_localPosition(new Vector3(base.get_transform().get_localPosition().x, base.get_transform().get_localPosition().y, 0f));
	}

	private float CalculateRate()
	{
		float num = UIConst.UI_SIZE_WIDTH / UIConst.UI_SIZE_HEIGHT;
		float result;
		if (CamerasMgr.CameraUI.get_aspect() > num)
		{
			result = (float)Screen.get_height() / UIConst.UI_SIZE_HEIGHT;
		}
		else
		{
			result = (float)Screen.get_width() / UIConst.UI_SIZE_WIDTH;
		}
		return result;
	}

	public static Vector2 GetDirection()
	{
		if (ControlStick.Instance == null)
		{
			return Vector2.get_zero();
		}
		Vector2 touchPositionBaseBottomL = InputManager.GetTouchPositionBaseBottomL(ControlStick.Instance.FingerId);
		return (new Vector2(touchPositionBaseBottomL.x, touchPositionBaseBottomL.y) - new Vector2(ControlStick.m_beginTouchBaseBottomL.x, ControlStick.m_beginTouchBaseBottomL.y)).get_normalized();
	}
}
