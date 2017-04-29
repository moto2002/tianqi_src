using System;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	public class VisualStateBinder : BindingBase
	{
		public GameObject[] Targets;

		public string ValidState;

		[HideInInspector]
		public BindingBase.BindingInfo ValueBinding = new BindingBase.BindingInfo
		{
			BindingName = "State"
		};

		protected override void InitBinding()
		{
			this.ValueBinding.Action = new Action<object>(this.UpdateState);
			this.ValueBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ValueBinding.FilterTypes = new Type[]
			{
				typeof(bool),
				typeof(string),
				typeof(int),
				typeof(Enum)
			};
		}

		private void UpdateState(object arg)
		{
			bool active = arg != null && this.ValidState.ToLower() == arg.ToString().ToLower();
			for (int i = 0; i < this.Targets.Length; i++)
			{
				this.Targets[i].SetActive(active);
			}
		}
	}
}
