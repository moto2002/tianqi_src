using System;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	public class KeyBinder : BindingBase
	{
		[HideInInspector]
		public BindingBase.BindingInfo CommandBinding = new BindingBase.BindingInfo
		{
			BindingName = "Command"
		};

		public KeyCode Key;

		public bool RequireDouble;

		protected float lastHit;

		protected override void InitBinding()
		{
			this.CommandBinding.Filters = BindingBase.BindingFilter.Commands;
		}

		private void Call()
		{
			if (UIStateSystem.IsEventSystemLock(false))
			{
				return;
			}
			UIStateSystem.LockOfClickInterval(0u);
			SoundManager.PlayUI(base.get_gameObject());
			base.SetValue(this.CommandBinding.MemberName, null);
		}

		private void Update()
		{
			if (Input.GetKeyUp(this.Key))
			{
				if (this.RequireDouble)
				{
					if (this.lastHit + 0.2f > Time.get_time())
					{
						this.Call();
						this.lastHit = 0f;
					}
					this.lastHit = Time.get_time();
				}
				else
				{
					this.Call();
				}
			}
		}
	}
}
