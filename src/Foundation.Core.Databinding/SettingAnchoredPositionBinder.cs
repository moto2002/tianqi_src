using System;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	public class SettingAnchoredPositionBinder : BindingBase
	{
		[HideInInspector]
		public BindingBase.BindingInfo AnchoredPositionBinding = new BindingBase.BindingInfo
		{
			BindingName = "AnchoredPosition"
		};

		protected override void InitBinding()
		{
			this.AnchoredPositionBinding.Action = new Action<object>(this.SetAnchoredPosition);
			this.AnchoredPositionBinding.Filters = BindingBase.BindingFilter.Properties;
			this.AnchoredPositionBinding.FilterTypes = new Type[]
			{
				typeof(Vector2)
			};
		}

		private void SetAnchoredPosition(object arg)
		{
			if (arg is Vector2)
			{
				if (arg != null && base.get_transform() is RectTransform)
				{
					(base.get_transform() as RectTransform).set_anchoredPosition((Vector2)arg);
				}
			}
			else
			{
				Debug.LogError("arg is not Vector2");
			}
		}
	}
}
