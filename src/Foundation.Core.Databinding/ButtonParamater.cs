using System;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	public class ButtonParamater : BindingBase
	{
		public enum ParamaterTypeEnum
		{
			Context,
			Static,
			Binding,
			GameObject
		}

		[HideInInspector]
		public BindingBase.BindingInfo ParameterBinding = new BindingBase.BindingInfo
		{
			BindingName = "Parameter"
		};

		public ButtonParamater.ParamaterTypeEnum ParamaterType;

		public string StaticParamater;

		protected override void InitBinding()
		{
			this.ParameterBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ParameterBinding.ShouldShow = new Func<bool>(this.HasParamaterBinding);
		}

		public object GetValue()
		{
			switch (this.ParamaterType)
			{
			case ButtonParamater.ParamaterTypeEnum.Context:
				return this.Context.DataInstance;
			case ButtonParamater.ParamaterTypeEnum.Static:
				return this.StaticParamater;
			case ButtonParamater.ParamaterTypeEnum.Binding:
				return base.GetValue(this.ParameterBinding.MemberName);
			case ButtonParamater.ParamaterTypeEnum.GameObject:
				return base.get_gameObject();
			default:
				return null;
			}
		}

		private bool HasParamaterBinding()
		{
			return this.ParamaterType == ButtonParamater.ParamaterTypeEnum.Binding;
		}
	}
}
