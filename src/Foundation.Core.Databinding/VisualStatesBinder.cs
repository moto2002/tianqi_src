using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	public class VisualStatesBinder : BindingBase
	{
		[Serializable]
		public class StateValue
		{
			public GameObject Target;

			public string Value;
		}

		public List<VisualStatesBinder.StateValue> Targets = new List<VisualStatesBinder.StateValue>();

		public bool IntConvert;

		public CompareConditionEnum CompareCondition;

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
			for (int i = 0; i < this.Targets.get_Count(); i++)
			{
				VisualStatesBinder.StateValue stateValue = this.Targets.get_Item(i);
				if (this.IntConvert)
				{
					int num = (arg != null) ? ((int)arg) : 0;
					bool active = false;
					switch (this.CompareCondition)
					{
					case CompareConditionEnum.Equal:
						active = (int.Parse(stateValue.Value) == num);
						break;
					case CompareConditionEnum.Greater:
						active = (int.Parse(stateValue.Value) > num);
						break;
					case CompareConditionEnum.GreaterOrEqual:
						active = (int.Parse(stateValue.Value) >= num);
						break;
					case CompareConditionEnum.Less:
						active = (int.Parse(stateValue.Value) < num);
						break;
					case CompareConditionEnum.LessOrEqual:
						active = (int.Parse(stateValue.Value) <= num);
						break;
					}
					stateValue.Target.SetActive(active);
				}
				else
				{
					bool active2 = arg != null && stateValue.Value.ToLower() == arg.ToString().ToLower();
					stateValue.Target.SetActive(active2);
				}
			}
		}
	}
}
