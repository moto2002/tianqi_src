using System;
using System.Collections.Generic;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	public class VisibilityBinder : BindingBase
	{
		private List<GameObject> _Targets;

		private GameObject _Target;

		private GameObject _InverseTarget;

		[HideInInspector]
		public BindingBase.BindingInfo ValueBinding = new BindingBase.BindingInfo
		{
			BindingName = "Value"
		};

		public List<GameObject> Targets
		{
			get
			{
				if (this._Targets == null)
				{
					this._Targets = new List<GameObject>();
				}
				return this._Targets;
			}
		}

		public GameObject Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				this._Target = value;
			}
		}

		public GameObject InverseTarget
		{
			get
			{
				return this._InverseTarget;
			}
			set
			{
				this._InverseTarget = value;
			}
		}

		public bool TargetSelf
		{
			get
			{
				return (this._Targets == null || this._Targets.get_Count() == 0) && this.Target == null && this._InverseTarget == null;
			}
		}

		protected override void InitBinding()
		{
			this.ValueBinding.Action = new Action<object>(this.UpdateState);
			this.ValueBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ValueBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
		}

		private void UpdateState(object arg)
		{
			bool flag = arg != null && (bool)arg;
			if (this.TargetSelf)
			{
				base.get_gameObject().SetActive(flag);
			}
			else
			{
				if (this._Target != null)
				{
					this._Target.SetActive(flag);
				}
				if (this._Targets != null && this._Targets.get_Count() > 0)
				{
					for (int i = 0; i < this._Targets.get_Count(); i++)
					{
						if (this._Targets.get_Item(i) != null)
						{
							this._Targets.get_Item(i).SetActive(flag);
						}
					}
				}
			}
			if (this._InverseTarget != null)
			{
				this._InverseTarget.SetActive(!flag);
			}
		}
	}
}
