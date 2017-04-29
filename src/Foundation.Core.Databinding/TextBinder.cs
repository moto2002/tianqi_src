using System;
using UnityEngine;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(Text))]
	public class TextBinder : BindingBase
	{
		protected Text Label;

		[HideInInspector]
		public BindingBase.BindingInfo LabelBinding = new BindingBase.BindingInfo
		{
			BindingName = "Label"
		};

		[HideInInspector]
		public BindingBase.BindingInfo ColorBinding = new BindingBase.BindingInfo
		{
			BindingName = "Color"
		};

		public bool SetHeight;

		public string FormatString = string.Empty;

		protected override void InitBinding()
		{
			this.Label = base.GetComponentInChildren<Text>();
			this.LabelBinding.Action = new Action<object>(this.UpdateLabel);
			this.LabelBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ColorBinding.Action = new Action<object>(this.UpdateColor);
			this.ColorBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ColorBinding.FilterTypes = new Type[]
			{
				typeof(Color)
			};
		}

		private void UpdateLabel(object arg)
		{
			string text = (arg != null) ? arg.ToString() : string.Empty;
			if (this.Label)
			{
				if (string.IsNullOrEmpty(this.FormatString))
				{
					this.Label.set_text(text);
				}
				else
				{
					this.Label.set_text(string.Format(this.FormatString, text));
				}
				if (this.SetHeight)
				{
					RectTransform rectTransform = this.Label.get_transform() as RectTransform;
					if (rectTransform != null)
					{
						rectTransform.set_sizeDelta(new Vector2(rectTransform.get_sizeDelta().x, this.Label.get_preferredHeight()));
					}
				}
			}
		}

		private void UpdateColor(object arg)
		{
			if (this.Label)
			{
				this.Label.set_color((Color)arg);
			}
		}
	}
}
