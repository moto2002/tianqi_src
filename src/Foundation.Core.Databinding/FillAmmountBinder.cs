using System;
using UnityEngine;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(Image))]
	public class FillAmmountBinder : BindingBase
	{
		protected Image Target;

		[HideInInspector]
		public BindingBase.BindingInfo FillValueBinding = new BindingBase.BindingInfo
		{
			BindingName = "FillValue"
		};

		private float InitWidth;

		protected override void InitBinding()
		{
			this.Target = base.GetComponent<Image>();
			this.FillValueBinding.Action = new Action<object>(this.UpdateFill);
			this.FillValueBinding.Filters = BindingBase.BindingFilter.Properties;
			this.FillValueBinding.FilterTypes = new Type[]
			{
				typeof(float)
			};
			if (this.Target.get_type() != 3 && this.Target.get_transform() is RectTransform)
			{
				RectTransform rectTransform = this.Target.get_transform() as RectTransform;
				this.InitWidth = rectTransform.get_sizeDelta().x;
			}
		}

		private void UpdateFill(object arg)
		{
			float num = Mathf.Min(1f, (float)arg);
			if (this.Target)
			{
				if (this.Target.get_type() == 3)
				{
					this.Target.set_fillAmount(num);
				}
				else
				{
					RectTransform rectTransform = this.Target.get_transform() as RectTransform;
					if (rectTransform != null)
					{
						rectTransform.set_sizeDelta(new Vector2(this.InitWidth * num, rectTransform.get_sizeDelta().y));
					}
				}
			}
		}
	}
}
