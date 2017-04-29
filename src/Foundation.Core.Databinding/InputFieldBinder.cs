using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(InputField))]
	public class InputFieldBinder : BindingBase
	{
		protected InputField Target;

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
			this.Target = base.GetComponent<InputField>();
			this.Target.get_onEndEdit().AddListener(new UnityAction<string>(this.SubmitText));
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

		public void SetCharacterLimit(int limit)
		{
			if (this.Target == null)
			{
				this.Target = base.GetComponent<InputField>();
			}
			this.Target.set_characterLimit(limit);
		}

		public void SetContentType(InputField.ContentType type)
		{
			if (this.Target == null)
			{
				this.Target = base.GetComponent<InputField>();
			}
			this.Target.set_contentType(type);
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
					this.Target.set_text(this.oldText = arg.ToString());
				}
				else
				{
					this.Target.set_text(this.oldText = string.Empty);
				}
			}
		}

		private void Update()
		{
			if (this.Target.get_text() != this.oldText)
			{
				if (this.Target.get_text().Contains("\t"))
				{
					this.Target.set_text(this.Target.get_text().Replace("\t", string.Empty));
					return;
				}
				this.UpdateText(this.Target.get_text());
			}
		}
	}
}
