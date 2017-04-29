using System;
using UnityEngine;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(RawImage))]
	public class RawImageBinder : BindingBase
	{
		protected RawImage Target;

		[HideInInspector]
		public BindingBase.BindingInfo SpriteBinding = new BindingBase.BindingInfo
		{
			BindingName = "Sprite"
		};

		[HideInInspector]
		public BindingBase.BindingInfo ColorBinding = new BindingBase.BindingInfo
		{
			BindingName = "Color"
		};

		protected override void InitBinding()
		{
			this.Target = base.GetComponent<RawImage>();
			this.SpriteBinding.Action = new Action<object>(this.UpdateLabel);
			this.SpriteBinding.Filters = BindingBase.BindingFilter.Properties;
			this.SpriteBinding.FilterTypes = new Type[]
			{
				typeof(Texture2D)
			};
			this.ColorBinding.Action = new Action<object>(this.UpdateColor);
			this.ColorBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ColorBinding.FilterTypes = new Type[]
			{
				typeof(Color)
			};
		}

		private void UpdateLabel(object arg)
		{
			if (arg == null)
			{
				return;
			}
			if (this.Target)
			{
				Texture2D texture = (Texture2D)arg;
				this.Target.set_texture(texture);
			}
		}

		private void UpdateColor(object arg)
		{
			if (this.Target)
			{
				this.Target.set_color((Color)arg);
			}
		}
	}
}
