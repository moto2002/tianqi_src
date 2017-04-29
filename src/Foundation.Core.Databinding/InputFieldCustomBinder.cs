using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(InputFieldCustom0))]
	public class InputFieldCustomBinder : BindingBase
	{
		protected InputFieldCustom0 Target;

		[HideInInspector]
		public BindingBase.BindingInfo TextBinding = new BindingBase.BindingInfo
		{
			BindingName = "Text"
		};

		[HideInInspector]
		public BindingBase.BindingInfo SubmitBinding = new BindingBase.BindingInfo
		{
			BindingName = "Submit"
		};

		[HideInInspector]
		public BindingBase.BindingInfo EnabledBinding = new BindingBase.BindingInfo
		{
			BindingName = "Enabled"
		};

		protected string oldText;

		protected override void InitBinding()
		{
			this.Target = base.GetComponent<InputFieldCustom0>();
			this.Target.onEndEdit.AddListener(new UnityAction<string>(this.SubmitText));
			this.TextBinding.Action = new Action<object>(this.UpdateText);
			this.TextBinding.Filters = BindingBase.BindingFilter.Properties;
			this.TextBinding.FilterTypes = new Type[]
			{
				typeof(string)
			};
			this.SubmitBinding.Filters = BindingBase.BindingFilter.Commands;
			this.EnabledBinding.Action = new Action<object>(this.UpdateState);
			this.EnabledBinding.Filters = BindingBase.BindingFilter.Properties;
			this.EnabledBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
		}

		private void SubmitText(object text)
		{
			base.SetValue(this.SubmitBinding.MemberName, null);
		}

		private void UpdateState(object arg)
		{
			this.Target.set_enabled((bool)arg);
		}

		private void UpdateText(string text)
		{
			this.oldText = text;
			base.SetValue(this.TextBinding.MemberName, text);
		}

		private void UpdateText(object arg)
		{
			if (this.Target)
			{
				if (arg != null)
				{
					this.Target.text = (this.oldText = arg.ToString());
				}
				else
				{
					this.Target.text = (this.oldText = string.Empty);
				}
			}
		}

		private void Update()
		{
			if (this.Target.text != this.oldText)
			{
				if (this.Target.text.Contains("\t"))
				{
					this.Target.text = this.Target.text.Replace("\t", string.Empty);
					return;
				}
				this.UpdateText(this.Target.text);
			}
		}
	}
}
