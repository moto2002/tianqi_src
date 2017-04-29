using System;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	public class RenameBinder : BindingBase
	{
		public string prefix;

		[HideInInspector]
		public BindingBase.BindingInfo RenameBinding = new BindingBase.BindingInfo
		{
			BindingName = "RenameBinding"
		};

		protected override void InitBinding()
		{
			this.RenameBinding.Action = new Action<object>(this.Rename);
			this.RenameBinding.Filters = BindingBase.BindingFilter.Properties;
			this.RenameBinding.FilterTypes = new Type[]
			{
				typeof(int)
			};
		}

		private void Rename(object arg)
		{
			if (arg != null)
			{
				int num = (int)arg;
				base.set_name(this.prefix + num);
			}
		}
	}
}
