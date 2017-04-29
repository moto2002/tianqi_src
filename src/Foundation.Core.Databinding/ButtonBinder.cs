using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(Button))]
	public class ButtonBinder : BindingBase
	{
		protected Button Button;

		protected ButtonParamater Paramater;

		public List<Image> TargetImages = new List<Image>();

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

		protected override void InitBinding()
		{
			this.Paramater = base.GetComponent<ButtonParamater>();
			this.Button = base.GetComponent<Button>();
			this.OnClickBinding.Filters = BindingBase.BindingFilter.Commands;
			this.EnabledBinding.Action = new Action<object>(this.UpdateState);
			this.EnabledBinding.Filters = BindingBase.BindingFilter.Properties;
			this.EnabledBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
			this.Button.get_onClick().AddListener(new UnityAction(this.Call));
		}

		private void OnDestroy()
		{
			this.Button = null;
			this.Paramater = null;
			this.TargetImages = null;
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
	}
}
