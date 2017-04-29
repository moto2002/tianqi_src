using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(Toggle))]
	public class ToggleBinder : BindingBase, IEventSystemHandler, IPointerClickHandler
	{
		protected Toggle Target;

		public bool OffWhenDisable = true;

		private float ScaleOff = 1f;

		public float ScaleOn = 1f;

		[HideInInspector]
		public BindingBase.BindingInfo EnabledBinding = new BindingBase.BindingInfo
		{
			BindingName = "Enabled"
		};

		[HideInInspector]
		public BindingBase.BindingInfo ValueBinding = new BindingBase.BindingInfo
		{
			BindingName = "Value"
		};

		private ToggleBinderHideFlag[] myHideFlags;

		protected override void InitBinding()
		{
			this.myHideFlags = base.get_transform().GetComponentsInChildren<ToggleBinderHideFlag>();
			this.ScaleOff = base.get_transform().get_localScale().x;
			this.ValueBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ValueBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
			this.ValueBinding.Action = new Action<object>(this.UpdateValue);
			this.EnabledBinding.Action = new Action<object>(this.UpdateState);
			this.EnabledBinding.Filters = BindingBase.BindingFilter.Properties;
			this.EnabledBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
			this.Target = base.GetComponent<Toggle>();
			this.Target.set_group(base.get_transform().get_parent().GetComponent<ToggleGroup>());
			this.Target.onValueChanged.AddListener(new UnityAction<bool>(this.Call));
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (UIStateSystem.IsEventSystemLock(false))
			{
				return;
			}
			if (this.Target != null && this.Target.get_isOn())
			{
				this.Sound();
			}
		}

		private void Call(bool value)
		{
			if (UIStateSystem.IsEventSystemLock(false))
			{
				return;
			}
			base.SetValue(this.ValueBinding.MemberName, value);
			this.UpdateValue(value);
			UIStateSystem.LockOfClickInterval(0u);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.Target != null)
			{
				this.SetFlags(this.Target.get_isOn());
			}
		}

		protected override void OnDisable()
		{
			if (this.OffWhenDisable && this.Target != null)
			{
				this.Target.set_isOn(false);
			}
		}

		private void UpdateValue(object o)
		{
			this.Target.set_isOn((bool)o);
			if (this.Target.get_targetGraphic() != null)
			{
				this.Target.get_targetGraphic().set_enabled(!this.Target.get_isOn());
			}
			this.SetFlags(this.Target.get_isOn());
			if (this.Target.get_isOn())
			{
				base.get_transform().set_localScale(new Vector3(this.ScaleOn, this.ScaleOn, 1f));
			}
			else
			{
				base.get_transform().set_localScale(new Vector3(this.ScaleOff, this.ScaleOff, 1f));
			}
		}

		private void UpdateState(object arg)
		{
			this.Target.set_interactable((bool)arg);
		}

		private void SetFlags(bool on)
		{
			if (this.myHideFlags != null)
			{
				for (int i = 0; i < this.myHideFlags.Length; i++)
				{
					bool flag = false;
					ToggleBinderHideFlag toggleBinderHideFlag = this.myHideFlags[i];
					if (toggleBinderHideFlag != null)
					{
						if (toggleBinderHideFlag.HideWhenOn && on)
						{
							flag = false;
						}
						else if (toggleBinderHideFlag.HideWhenOn && !on)
						{
							flag = true;
						}
						else if (!toggleBinderHideFlag.HideWhenOn && on)
						{
							flag = true;
						}
						else if (!toggleBinderHideFlag.HideWhenOn && !on)
						{
							flag = false;
						}
						if (toggleBinderHideFlag.get_gameObject() != null)
						{
							toggleBinderHideFlag.get_gameObject().SetActive(flag);
						}
						if (flag)
						{
							Image[] componentsInChildren = toggleBinderHideFlag.GetComponentsInChildren<Image>();
							if (componentsInChildren != null)
							{
								for (int j = 0; j < componentsInChildren.Length; j++)
								{
									if (componentsInChildren[j] != null)
									{
										componentsInChildren[j].set_enabled(true);
									}
								}
							}
						}
					}
				}
			}
		}

		private void Sound()
		{
			if (!(this.Target is ToggleCustom))
			{
				SoundManager.PlayUI(base.get_gameObject());
			}
		}
	}
}
