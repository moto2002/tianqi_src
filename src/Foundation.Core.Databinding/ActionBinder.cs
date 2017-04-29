using System;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	public class ActionBinder : BindingBase
	{
		[HideInInspector]
		public BindingBase.BindingInfo CallActionOfBoolBinding = new BindingBase.BindingInfo
		{
			BindingName = "CallActionOfBoolBinding"
		};

		[HideInInspector]
		public BindingBase.BindingInfo CallActionOfVec3Binding = new BindingBase.BindingInfo
		{
			BindingName = "CallActionOfVecBinding"
		};

		public Action<bool> actoncall_bool;

		public Action<Vector3> actoncall_vec3;

		protected override void InitBinding()
		{
			this.CallActionOfBoolBinding.Action = new Action<object>(this.CallActionOfBool);
			this.CallActionOfBoolBinding.Filters = BindingBase.BindingFilter.Properties;
			this.CallActionOfBoolBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
			this.CallActionOfVec3Binding.Action = new Action<object>(this.CallActionOfVec3);
			this.CallActionOfVec3Binding.Filters = BindingBase.BindingFilter.Properties;
			this.CallActionOfVec3Binding.FilterTypes = new Type[]
			{
				typeof(Vector2)
			};
		}

		private void CallActionOfBool(object arg)
		{
			if (arg != null && this.actoncall_bool != null)
			{
				this.actoncall_bool.Invoke((bool)arg);
			}
		}

		private void CallActionOfVec3(object arg)
		{
			if (arg != null && this.actoncall_vec3 != null)
			{
				this.actoncall_vec3.Invoke((Vector3)arg);
			}
		}
	}
}
