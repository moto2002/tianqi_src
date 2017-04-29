using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(Button))]
	public class ButtonCustomBinder : BindingBase
	{
		private const float DOWN_TIME = 0.5f;

		protected Button Button;

		protected ButtonParamater Paramater;

		public bool IsDownSuccession;

		[HideInInspector]
		public BindingBase.BindingInfo EnabledBinding = new BindingBase.BindingInfo
		{
			BindingName = "Enabled"
		};

		[HideInInspector]
		public BindingBase.BindingInfo OnClickBinding = new BindingBase.BindingInfo
		{
			BindingName = "OnClick"
		};

		[HideInInspector]
		public BindingBase.BindingInfo OnDownBinding = new BindingBase.BindingInfo
		{
			BindingName = "OnButtonDown"
		};

		[HideInInspector]
		public BindingBase.BindingInfo OnUpBinding = new BindingBase.BindingInfo
		{
			BindingName = "OnButtonUp"
		};

		public List<Image> TargetImages = new List<Image>();

		private bool isDownState;

		private bool isDowned;

		private float downTime;

		protected override void InitBinding()
		{
			this.Paramater = base.GetComponent<ButtonParamater>();
			this.Button = base.GetComponent<Button>();
			this.OnClickBinding.Filters = BindingBase.BindingFilter.Commands;
			this.OnDownBinding.Filters = BindingBase.BindingFilter.Commands;
			this.OnUpBinding.Filters = BindingBase.BindingFilter.Commands;
			this.EnabledBinding.Action = new Action<object>(this.UpdateState);
			this.EnabledBinding.Filters = BindingBase.BindingFilter.Properties;
			this.EnabledBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
			EventTriggerListener expr_88 = EventTriggerListener.Get(base.get_gameObject());
			expr_88.onDown = (EventTriggerListener.VoidDelegateGameObject)Delegate.Combine(expr_88.onDown, new EventTriggerListener.VoidDelegateGameObject(this.OnButtonDown));
			EventTriggerListener expr_B4 = EventTriggerListener.Get(base.get_gameObject());
			expr_B4.onUp = (EventTriggerListener.VoidDelegateGameObject)Delegate.Combine(expr_B4.onUp, new EventTriggerListener.VoidDelegateGameObject(this.OnButtonUp));
		}

		private void Call()
		{
			if (UIStateSystem.IsEventSystemLock(false))
			{
				return;
			}
			if (!this.Button.IsInteractable())
			{
				return;
			}
			SoundManager.PlayUI(base.get_gameObject());
			base.SetValue(this.OnClickBinding.MemberName, (!(this.Paramater == null)) ? this.Paramater.GetValue() : null);
			UIStateSystem.LockOfClickInterval(0u);
		}

		private void OnButtonDown(GameObject go)
		{
			if (!this.Button.IsInteractable())
			{
				return;
			}
			this.ResetDown(true);
		}

		private void OnButtonUp(GameObject go)
		{
			if (!this.Button.IsInteractable())
			{
				return;
			}
			if (!this.isDowned)
			{
				this.DoClick();
			}
			else
			{
				this.DoUp();
			}
			this.ResetDown(false);
		}

		private void DoDown()
		{
			base.SetValue(this.OnDownBinding.MemberName, base.get_gameObject());
		}

		private void DoUp()
		{
			base.SetValue(this.OnUpBinding.MemberName, (!(this.Paramater == null)) ? this.Paramater.GetValue() : null);
		}

		private void DoClick()
		{
			this.Call();
		}

		private void UpdateState(object arg)
		{
			this.Button.set_enabled((bool)arg);
			if (this.Button.get_targetGraphic() != null)
			{
				Image component = this.Button.get_targetGraphic().GetComponent<Image>();
				ImageColorMgr.SetImageColor(component, !(bool)arg);
			}
			for (int i = 0; i < this.TargetImages.get_Count(); i++)
			{
				Image image = this.TargetImages.get_Item(i);
				if (image != null)
				{
					ImageColorMgr.SetImageColor(image, !(bool)arg);
				}
			}
		}

		private void Update()
		{
			if (EventSystem.get_current() == null || EventSystem.get_current().get_currentSelectedGameObject() == null)
			{
				this.ResetDown(false);
			}
			if (this.isDownState && InputManager.IsInputDownState())
			{
				this.downTime += Time.get_deltaTime();
				if (this.downTime >= 0.5f && this.IsCanDown())
				{
					this.DoDown();
					this.isDowned = true;
				}
			}
		}

		private void ResetDown(bool down)
		{
			this.isDownState = down;
			this.isDowned = false;
			this.downTime = 0f;
		}

		private bool IsCanDown()
		{
			return !this.isDowned || this.IsDownSuccession;
		}
	}
}
