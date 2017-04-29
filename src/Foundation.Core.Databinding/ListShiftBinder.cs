using System;
using UnityEngine;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	public class ListShiftBinder : BindingBase
	{
		[HideInInspector]
		public BindingBase.BindingInfo ShiftBinding = new BindingBase.BindingInfo
		{
			BindingName = "Shift"
		};

		public Transform Target2ScrollRect;

		private ScrollRectCustom m_thisScrollRectCustom;

		private ScrollRectCustom thisScrollRectCustom
		{
			get
			{
				if (this.m_thisScrollRectCustom == null)
				{
					if (this.Target2ScrollRect == null)
					{
						this.m_thisScrollRectCustom = base.get_transform().get_parent().GetComponent<ScrollRectCustom>();
					}
					else
					{
						this.m_thisScrollRectCustom = this.Target2ScrollRect.GetComponent<ScrollRectCustom>();
					}
				}
				return this.m_thisScrollRectCustom;
			}
		}

		protected override void InitBinding()
		{
			this.ShiftBinding.Action = new Action<object>(this.UpdateShift);
			this.ShiftBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ShiftBinding.FilterTypes = new Type[]
			{
				typeof(int)
			};
		}

		private void UpdateShift(object arg)
		{
			ListShifts.SetShift(this.thisScrollRectCustom, arg);
		}
	}
}
