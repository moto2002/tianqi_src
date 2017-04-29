using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(Button))]
	public class ButtonValueBinder : BindingBase, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler
	{
		protected Button Button;

		protected ButtonParamater Paramater;

		[HideInInspector]
		public BindingBase.BindingInfo EnabledBinding = new BindingBase.BindingInfo
		{
			BindingName = "Enabled"
		};

		[HideInInspector]
		public BindingBase.BindingInfo IsPressedBinding = new BindingBase.BindingInfo
		{
			BindingName = "IsPressed"
		};

		protected override void InitBinding()
		{
			this.Paramater = base.GetComponent<ButtonParamater>();
			this.Button = base.GetComponent<Button>();
			this.IsPressedBinding.Filters = BindingBase.BindingFilter.Properties;
			this.IsPressedBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
			this.EnabledBinding.Action = new Action<object>(this.UpdateState);
			this.EnabledBinding.Filters = BindingBase.BindingFilter.Properties;
			this.EnabledBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
		}

		private void UpdateState(object arg)
		{
			this.Button.set_interactable((bool)arg);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			base.SetValue(this.IsPressedBinding.MemberName, true);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			base.SetValue(this.IsPressedBinding.MemberName, false);
		}
	}
}
