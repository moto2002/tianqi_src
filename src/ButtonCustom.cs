using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCustom : Button
{
	public enum ButtonState
	{
		Normal,
		Selected,
		Diasbled
	}

	public delegate void VoidDelegateObj(GameObject go);

	public delegate void VoidDelegateEventData(PointerEventData eventData);

	public ButtonCustom.VoidDelegateObj onClickCustom;

	public ButtonCustom.VoidDelegateEventData onClickEventData;

	protected Selectable.SelectionState currentSeletionState;

	public Image imageNormalState;

	public Image imageSelectedState;

	public Image imageDisableState;

	public Image imageTextNormalState;

	public Image imageTextSelectedState;

	public Image imageTextDisableState;

	public Text textButton;

	public Color textColorNormalState;

	public Color textColorSelectedState;

	public Color textColorDisableState;

	public Image hideImage;

	public bool hideNormalState;

	public bool hideSelectedState;

	public bool hideDisableState;

	public override void OnPointerClick(PointerEventData eventData)
	{
		if (UIStateSystem.IsEventSystemLock(false))
		{
			return;
		}
		base.OnPointerClick(eventData);
		if (this.onClickCustom != null)
		{
			SoundManager.PlayUI(base.get_gameObject());
			this.onClickCustom(base.get_gameObject());
		}
		if (this.onClickEventData != null)
		{
			this.onClickEventData(eventData);
		}
		UIStateSystem.LockOfClickInterval(0u);
	}

	protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
	{
		if (this.currentSeletionState == 2 && state == 1 && (Input.get_touchCount() > 0 || Input.GetMouseButton(0)) && EventSystem.get_current() != null && EventSystem.get_current().get_currentSelectedGameObject() != null && EventSystem.get_current().get_currentSelectedGameObject() == base.get_gameObject())
		{
			this.currentSeletionState = state;
			return;
		}
		this.currentSeletionState = state;
		base.DoStateTransition(state, instant);
	}

	public void SetDisableColor(bool disable)
	{
		if (disable)
		{
			base.GetComponent<Image>().set_color(new Color(0.5f, 0.5f, 0.5f, 1f));
		}
		else
		{
			base.GetComponent<Image>().set_color(new Color(1f, 1f, 1f, 1f));
		}
	}

	public void SetState(ButtonCustom.ButtonState state)
	{
		switch (state)
		{
		case ButtonCustom.ButtonState.Normal:
			if (this.imageNormalState != null)
			{
				this.imageNormalState.get_gameObject().SetActive(true);
			}
			if (this.imageSelectedState != null)
			{
				this.imageSelectedState.get_gameObject().SetActive(false);
			}
			if (this.imageDisableState != null)
			{
				this.imageDisableState.get_gameObject().SetActive(false);
			}
			if (this.imageTextNormalState != null)
			{
				this.imageTextNormalState.get_gameObject().SetActive(true);
			}
			if (this.imageTextSelectedState != null)
			{
				this.imageTextSelectedState.get_gameObject().SetActive(false);
			}
			if (this.imageTextDisableState != null)
			{
				this.imageTextDisableState.get_gameObject().SetActive(false);
			}
			if (this.textButton != null)
			{
				this.textButton.set_color(this.textColorNormalState);
			}
			if (this.hideImage != null)
			{
				if (this.hideNormalState)
				{
					this.hideImage.get_gameObject().SetActive(false);
				}
				else
				{
					this.hideImage.get_gameObject().SetActive(true);
				}
			}
			break;
		case ButtonCustom.ButtonState.Selected:
			if (this.imageNormalState != null)
			{
				this.imageNormalState.get_gameObject().SetActive(false);
			}
			if (this.imageSelectedState != null)
			{
				this.imageSelectedState.get_gameObject().SetActive(true);
			}
			if (this.imageDisableState != null)
			{
				this.imageDisableState.get_gameObject().SetActive(false);
			}
			if (this.imageTextNormalState != null)
			{
				this.imageTextNormalState.get_gameObject().SetActive(false);
			}
			if (this.imageTextSelectedState != null)
			{
				this.imageTextSelectedState.get_gameObject().SetActive(true);
			}
			if (this.imageTextDisableState != null)
			{
				this.imageTextDisableState.get_gameObject().SetActive(false);
			}
			if (this.textButton != null)
			{
				this.textButton.set_color(this.textColorSelectedState);
			}
			if (this.hideImage != null)
			{
				if (this.hideSelectedState)
				{
					this.hideImage.get_gameObject().SetActive(false);
				}
				else
				{
					this.hideImage.get_gameObject().SetActive(true);
				}
			}
			break;
		case ButtonCustom.ButtonState.Diasbled:
			if (this.imageNormalState != null)
			{
				this.imageNormalState.get_gameObject().SetActive(false);
			}
			if (this.imageSelectedState != null)
			{
				this.imageSelectedState.get_gameObject().SetActive(false);
			}
			if (this.imageDisableState != null)
			{
				this.imageDisableState.get_gameObject().SetActive(true);
			}
			if (this.imageTextNormalState != null)
			{
				this.imageTextNormalState.get_gameObject().SetActive(false);
			}
			if (this.imageTextSelectedState != null)
			{
				this.imageTextSelectedState.get_gameObject().SetActive(false);
			}
			if (this.imageTextDisableState != null)
			{
				this.imageTextDisableState.get_gameObject().SetActive(true);
			}
			if (this.textButton != null)
			{
				this.textButton.set_color(this.textColorDisableState);
			}
			if (this.hideImage != null)
			{
				if (this.hideDisableState)
				{
					this.hideImage.get_gameObject().SetActive(false);
				}
				else
				{
					this.hideImage.get_gameObject().SetActive(true);
				}
			}
			break;
		}
	}

	public void SetImagesForButtonState(Image normalState, Image selectedState)
	{
		this.SetImagesForButtonState(normalState, selectedState, null);
	}

	public void SetImagesForButtonState(Image normalState, Image selectedState, Image disableState)
	{
		this.imageNormalState = normalState;
		this.imageSelectedState = selectedState;
		this.imageDisableState = disableState;
	}

	public void SetImageTextsForButtonState(Image normalState, Image selectedState)
	{
		this.SetImageTextsForButtonState(normalState, selectedState, null);
	}

	public void SetImageTextsForButtonState(Image normalState, Image selectedState, Image disableState)
	{
		this.imageTextNormalState = normalState;
		this.imageTextSelectedState = selectedState;
		this.imageTextDisableState = disableState;
	}

	public void SetTextColorForButtonState(Text text, Color normalState, Color selectedState, Color disableState)
	{
		this.textButton = text;
		this.textColorNormalState = normalState;
		this.textColorSelectedState = selectedState;
		this.textColorDisableState = disableState;
	}

	public void SetImageShouldHideForState(Image image, bool normalState, bool selectedState, bool disableState)
	{
		this.hideImage = image;
		this.hideNormalState = normalState;
		this.hideSelectedState = selectedState;
		this.hideDisableState = disableState;
	}
}
