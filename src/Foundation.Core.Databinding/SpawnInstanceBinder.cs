using System;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	public class SpawnInstanceBinder : BindingBase
	{
		public Transform Target;

		protected GameObject instance;

		[HideInInspector]
		public BindingBase.BindingInfo ValueBinding = new BindingBase.BindingInfo
		{
			BindingName = "Prefab"
		};

		public bool DisplayMode;

		public bool MakeChild;

		protected override void InitBinding()
		{
			this.ValueBinding.Action = new Action<object>(this.UpdateState);
			this.ValueBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ValueBinding.FilterTypes = new Type[]
			{
				typeof(GameObject)
			};
		}

		private void UpdateState(object arg)
		{
			if (this.instance != null)
			{
				Object.Destroy(this.instance);
			}
			if (arg != null)
			{
				if (this.Target != null)
				{
					this.instance = (GameObject)Object.Instantiate((GameObject)arg, this.Target.get_position(), this.Target.get_rotation());
					if (this.MakeChild)
					{
						this.instance.get_transform().set_parent(this.Target.get_transform());
					}
				}
				else
				{
					this.instance = (GameObject)Object.Instantiate((GameObject)arg, Vector3.get_zero(), Quaternion.get_identity());
				}
				if (this.DisplayMode)
				{
					this.instance.SendMessage("DisplayMode", 1);
				}
			}
		}
	}
}
