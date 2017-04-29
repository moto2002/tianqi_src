using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
	public class EventNames
	{
		public const string ForbiddenStick = "ControlStick.ForbiddenStick";

		public const string InterruptStick = "ControlStick.InterruptStick";

		public const string HardwareResolutionChange = "ControlStick.HardwareResolutionChange";

		public const string SelectModel = "InputManager.SelectModel";
	}

	public const int DEFAULT_FINGER_ID = -100;

	private static bool UseSimulationTouchScreen = false;

	private static Vector2 mSelectTouchPositionBaseBottomL = Vector2.get_zero();

	public static Vector2 SelectTouchPositionBaseBottomL
	{
		get
		{
			return InputManager.mSelectTouchPositionBaseBottomL;
		}
		set
		{
			if (value != Vector2.get_zero())
			{
				InputManager.mSelectTouchPositionBaseBottomL = value;
				EventDispatcher.Broadcast("InputManager.SelectModel");
			}
		}
	}

	public static void UpdateInputManager()
	{
		if (ControlStick.Instance == null || ControlStickCamera.Instance == null)
		{
			return;
		}
		InputManager.UpdateSelectTouchPosition();
		ControlStick.Instance.UpdateSelf();
		ControlStickCamera.Instance.UpdateSelf();
	}

	public static int GetCurrentFingerID()
	{
		if (Input.get_touchCount() > 0)
		{
			return Input.GetTouch(0).get_fingerId();
		}
		return 0;
	}

	public static bool InputIsTouchScreen()
	{
		return Input.get_touchCount() > 0 || ((Application.get_platform() == 7 || Application.get_platform() == null) && InputManager.UseSimulationTouchScreen) || (Application.get_platform() == 8 || Application.get_platform() == 11);
	}

	public static bool IsInputDownState()
	{
		if (InputManager.InputIsTouchScreen())
		{
			if (Input.get_touchCount() > 0)
			{
				for (int i = 0; i < Input.get_touchCount(); i++)
				{
					Touch touch = Input.GetTouch(i);
					if (touch.get_phase() == null || touch.get_phase() == 1 || touch.get_phase() == 2)
					{
						return true;
					}
				}
			}
		}
		else if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
		{
			return true;
		}
		return false;
	}

	public static bool IsInputDownOn()
	{
		if (InputManager.InputIsTouchScreen())
		{
			if (Input.get_touchCount() > 0)
			{
				for (int i = 0; i < Input.get_touchCount(); i++)
				{
					if (Input.GetTouch(i).get_phase() == null)
					{
						EventDispatcher.Broadcast("UIStateSystem.ResetFPSSleep");
						return true;
					}
				}
			}
		}
		else if (Input.GetMouseButtonDown(0))
		{
			EventDispatcher.Broadcast("UIStateSystem.ResetFPSSleep");
			return true;
		}
		return false;
	}

	public static Vector2 GetTouchPositionBaseBottomL(int fingerId = 0)
	{
		Vector2 result = Vector2.get_zero();
		if (InputManager.InputIsTouchScreen())
		{
			if (Input.get_touchCount() > 0)
			{
				for (int i = 0; i < Input.get_touchCount(); i++)
				{
					Touch touch = Input.GetTouch(i);
					if (touch.get_fingerId() == fingerId)
					{
						result = touch.get_position();
						break;
					}
				}
			}
		}
		else
		{
			result = new Vector2(Input.get_mousePosition().x, Input.get_mousePosition().y);
		}
		return result;
	}

	public static bool IsFingerMove(int fingerId)
	{
		if (InputManager.InputIsTouchScreen() && Input.get_touchCount() > 0)
		{
			for (int i = 0; i < Input.get_touchCount(); i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.get_fingerId() == fingerId && touch.get_phase() == 1)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool IsTriggerUI(int pointId)
	{
		if (EventSystem.get_current() != null)
		{
			if (EventSystem.get_current().get_currentSelectedGameObject() != null)
			{
				return true;
			}
			if (pointId == -100)
			{
				if (EventSystem.get_current().IsPointerOverGameObject())
				{
					return true;
				}
			}
			else if (EventSystem.get_current().IsPointerOverGameObject(pointId))
			{
				return true;
			}
		}
		return false;
	}

	private static void UpdateSelectTouchPosition()
	{
		if (ControlStick.Instance == null || ControlStickCamera.Instance == null)
		{
			InputManager.SelectTouchPositionBaseBottomL = Vector2.get_zero();
			return;
		}
		if (InputManager.InputIsTouchScreen())
		{
			if (Input.get_touchCount() > 0)
			{
				for (int i = 0; i < Input.get_touchCount(); i++)
				{
					Touch touch = Input.GetTouch(i);
					if (touch.get_fingerId() != ControlStick.Instance.FingerId)
					{
						if (touch.get_fingerId() != ControlStickCamera.Instance.FingerId || !ControlStickCamera.Instance.IsCameraMoved)
						{
							if (touch.get_phase() == null && !InputManager.IsTriggerUI(touch.get_fingerId()))
							{
								InputManager.SelectTouchPositionBaseBottomL = touch.get_position();
							}
						}
					}
				}
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Vector2 vector = new Vector2(Input.get_mousePosition().x, Input.get_mousePosition().y);
			if (ControlStick.Instance.TouchInValidRect(vector))
			{
				InputManager.SelectTouchPositionBaseBottomL = Vector2.get_zero();
				return;
			}
			if (ControlStickCamera.Instance.IsCameraMoved)
			{
				InputManager.SelectTouchPositionBaseBottomL = Vector2.get_zero();
				return;
			}
			if (!InputManager.IsTriggerUI(-100))
			{
				InputManager.SelectTouchPositionBaseBottomL = vector;
			}
		}
		InputManager.SelectTouchPositionBaseBottomL = Vector2.get_zero();
	}
}
