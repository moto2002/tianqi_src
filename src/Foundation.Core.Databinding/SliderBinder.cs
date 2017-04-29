using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(Slider))]
	public class SliderBinder : BindingBase
	{
		protected Slider Target;

		[HideInInspector]
		public BindingBase.BindingInfo ValueBinding = new BindingBase.BindingInfo
		{
			BindingName = "Value"
		};

		[HideInInspector]
		public BindingBase.BindingInfo EnabledBinding = new BindingBase.BindingInfo
		{
			BindingName = "Enabled"
		};

		[HideInInspector]
		public BindingBase.BindingInfo MinValue = new BindingBase.BindingInfo
		{
			BindingName = "MinValue"
		};

		[HideInInspector]
		public BindingBase.BindingInfo MaxValue = new BindingBase.BindingInfo
		{
			BindingName = "MaxValue"
		};

		public float SoundLag = 0.5f;

		public bool ReadOnly;

		protected float NextSwipe;

		protected override void InitBinding()
		{
			this.NextSwipe = Time.get_time() + this.SoundLag;
			this.Target = base.GetComponent<Slider>();
			if (!this.ReadOnly)
			{
				this.Target.get_onValueChanged().AddListener(new UnityAction<float>(this.HandleChange));
			}
			this.ValueBinding.Action = new Action<object>(this.UpdateSlider);
			this.ValueBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ValueBinding.FilterTypes = new Type[]
			{
				typeof(float)
			};
			this.MinValue.Action = new Action<object>(this.UpdateMin);
			this.MinValue.Filters = BindingBase.BindingFilter.Properties;
			this.MinValue.FilterTypes = new Type[]
			{
				typeof(float)
			};
			this.MaxValue.Action = new Action<object>(this.UpdateMax);
			this.MaxValue.Filters = BindingBase.BindingFilter.Properties;
			this.MaxValue.FilterTypes = new Type[]
			{
				typeof(float)
			};
			this.EnabledBinding.Action = new Action<object>(this.UpdateEnabled);
			this.EnabledBinding.Filters = BindingBase.BindingFilter.Properties;
			this.EnabledBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
		}

		private void UpdateEnabled(object arg)
		{
			this.Target.set_interactable((bool)arg);
		}

		private void HandleChange(float arg)
		{
			if (UIStateSystem.IsEventSystemLock(false))
			{
				return;
			}
			if (this.NextSwipe < Time.get_time())
			{
				SoundManager.PlayUI(base.get_gameObject());
				this.NextSwipe = Time.get_time() + this.SoundLag;
			}
			base.SetValue(this.ValueBinding.MemberName, arg);
		}

		private void UpdateSlider(object arg)
		{
			if (this.Target)
			{
				this.Target.set_value((float)arg);
			}
		}

		private void UpdateMin(object arg)
		{
			if (this.Target)
			{
				this.Target.set_minValue((float)arg);
			}
		}

		private void UpdateMax(object arg)
		{
			if (this.Target)
			{
				this.Target.set_maxValue((float)arg);
			}
		}
	}
}
