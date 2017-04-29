using System;
using UnityEngine;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(Image))]
	public class TweenColorBinder : BindingBase
	{
		[HideInInspector]
		public BindingBase.BindingInfo ValueBinding = new BindingBase.BindingInfo
		{
			BindingName = "Value"
		};

		public Color MinColor = Color.get_red();

		public Color MaxColor = Color.get_green();

		public Image image;

		protected override void InitBinding()
		{
			this.image = base.GetComponent<Image>();
			this.ValueBinding.Action = new Action<object>(this.UpdateColor);
			this.ValueBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ValueBinding.FilterTypes = new Type[]
			{
				typeof(float)
			};
		}

		private void UpdateColor(object arg)
		{
			float num = (float)arg;
			this.image.set_color(Color.Lerp(this.MinColor, this.MaxColor, num));
		}
	}
}
