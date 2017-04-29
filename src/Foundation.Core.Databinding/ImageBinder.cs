using System;
using UnityEngine;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	[RequireComponent(typeof(Image))]
	public class ImageBinder : BindingBase
	{
		public bool SetNativeSize;

		public bool SetLayoutIgnoreWhenEmpty;

		public bool RefreshLayout;

		private Image Target;

		[HideInInspector]
		public BindingBase.BindingInfo SpriteBinding = new BindingBase.BindingInfo
		{
			BindingName = "Sprite"
		};

		[HideInInspector]
		public BindingBase.BindingInfo ColorBinding = new BindingBase.BindingInfo
		{
			BindingName = "Color"
		};

		[HideInInspector]
		public BindingBase.BindingInfo HSVBinding = new BindingBase.BindingInfo
		{
			BindingName = "HSVid"
		};

		protected override void InitBinding()
		{
			this.Target = base.GetComponent<Image>();
			this.SpriteBinding.Action = new Action<object>(this.UpdateSprite);
			this.SpriteBinding.Filters = BindingBase.BindingFilter.Properties;
			this.SpriteBinding.FilterTypes = new Type[]
			{
				typeof(Texture2D)
			};
			this.ColorBinding.Action = new Action<object>(this.UpdateColor);
			this.ColorBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ColorBinding.FilterTypes = new Type[]
			{
				typeof(Color)
			};
			this.HSVBinding.Action = new Action<object>(this.UpdateHSV);
			this.HSVBinding.Filters = BindingBase.BindingFilter.Properties;
			this.HSVBinding.FilterTypes = new Type[]
			{
				typeof(int)
			};
		}

		private void OnDestroy()
		{
			this.Target = null;
		}

		private void UpdateSprite(object arg)
		{
			if (this.Target != null && arg != null)
			{
				if (arg is SpriteRenderer)
				{
					SpriteRenderer spriteRenderer = arg as SpriteRenderer;
					ResourceManager.SetSprite(this.Target, (SpriteRenderer)arg);
					if (this.SetNativeSize)
					{
						this.Target.SetNativeSize();
					}
					if (this.RefreshLayout)
					{
						LayoutElement component = base.get_transform().GetComponent<LayoutElement>();
						if (component != null)
						{
							component.set_preferredWidth(-1f);
						}
					}
					if (this.SetLayoutIgnoreWhenEmpty)
					{
						LayoutElement component2 = base.get_transform().GetComponent<LayoutElement>();
						if (component2 != null)
						{
							if (spriteRenderer != null && spriteRenderer.get_sprite() != null && spriteRenderer.get_sprite().get_name().Equals("Empty"))
							{
								component2.set_ignoreLayout(true);
							}
							else if ((Sprite)arg == null)
							{
								component2.set_ignoreLayout(true);
							}
							else
							{
								component2.set_ignoreLayout(false);
							}
						}
					}
				}
				else
				{
					Debuger.Error("===arg invalid, it need arg is sprite.", new object[0]);
				}
			}
		}

		private void UpdateColor(object arg)
		{
			if (this.Target != null && arg != null)
			{
				this.Target.set_color((Color)arg);
			}
		}

		private void UpdateHSV(object arg)
		{
			if (this.Target != null && arg != null)
			{
				if ((int)arg == 6)
				{
					ImageColorMgr.SetImageColor(this.Target, true);
				}
				else
				{
					ImageColorMgr.SetImageColor(this.Target, false);
				}
			}
		}
	}
}
