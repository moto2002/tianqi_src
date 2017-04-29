using System;
using UnityEngine;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	public class UtilsBinder : BindingBase
	{
		[HideInInspector]
		public BindingBase.BindingInfo Set2ParentBinding = new BindingBase.BindingInfo
		{
			BindingName = "Set2Parent"
		};

		[HideInInspector]
		public BindingBase.BindingInfo Set2ParentPositionBinding = new BindingBase.BindingInfo
		{
			BindingName = "Set2ParentPosition"
		};

		[HideInInspector]
		public BindingBase.BindingInfo SetAlignmentBinding = new BindingBase.BindingInfo
		{
			BindingName = "SetAlignmentBinding"
		};

		[HideInInspector]
		public BindingBase.BindingInfo SetSizeWidthBinding = new BindingBase.BindingInfo
		{
			BindingName = "SetSizeWidth"
		};

		[HideInInspector]
		public BindingBase.BindingInfo SetSizeHeightBinding = new BindingBase.BindingInfo
		{
			BindingName = "SetSizeHeight"
		};

		[HideInInspector]
		public BindingBase.BindingInfo IgnoreLayoutBinding = new BindingBase.BindingInfo
		{
			BindingName = "IgnoreLayout"
		};

		private RectTransform thisRTransform
		{
			get
			{
				return base.get_transform() as RectTransform;
			}
		}

		protected override void InitBinding()
		{
			this.Set2ParentBinding.Action = new Action<object>(this.AttachToParent);
			this.Set2ParentBinding.Filters = BindingBase.BindingFilter.Properties;
			this.Set2ParentBinding.FilterTypes = new Type[]
			{
				typeof(Transform)
			};
			this.Set2ParentPositionBinding.Action = new Action<object>(this.Set2ParentPosition);
			this.Set2ParentPositionBinding.Filters = BindingBase.BindingFilter.Properties;
			this.Set2ParentPositionBinding.FilterTypes = new Type[]
			{
				typeof(Transform)
			};
			this.SetAlignmentBinding.Action = new Action<object>(this.SetAlignment);
			this.SetAlignmentBinding.Filters = BindingBase.BindingFilter.Properties;
			this.SetAlignmentBinding.FilterTypes = new Type[]
			{
				typeof(Vector3)
			};
			this.SetSizeWidthBinding.Action = new Action<object>(this.SetSizeWidth);
			this.SetSizeWidthBinding.Filters = BindingBase.BindingFilter.Properties;
			this.SetSizeWidthBinding.FilterTypes = new Type[]
			{
				typeof(Vector2)
			};
			this.SetSizeHeightBinding.Action = new Action<object>(this.SetSizeHeight);
			this.SetSizeHeightBinding.Filters = BindingBase.BindingFilter.Properties;
			this.SetSizeHeightBinding.FilterTypes = new Type[]
			{
				typeof(Vector2)
			};
			this.IgnoreLayoutBinding.Action = new Action<object>(this.SetIgnoreLayout);
			this.IgnoreLayoutBinding.Filters = BindingBase.BindingFilter.Properties;
			this.IgnoreLayoutBinding.FilterTypes = new Type[]
			{
				typeof(bool)
			};
		}

		private void AttachToParent(object arg)
		{
			if (arg != null)
			{
				Transform transform = arg as Transform;
				if (transform != null)
				{
					UGUITools.ResetTransform(transform, base.get_transform());
				}
			}
		}

		private void Set2ParentPosition(object arg)
		{
			if (arg != null)
			{
				Transform transform = arg as Transform;
				if (transform != null)
				{
					transform.set_position(base.get_transform().get_position());
				}
			}
		}

		private void SetAlignment(object arg)
		{
			if (arg != null)
			{
				Vector3 vector = (Vector3)arg;
				Text component = base.get_gameObject().GetComponent<Text>();
				int num = (int)vector.z;
				switch (num)
				{
				case 1:
					this.thisRTransform.set_anchorMin(new Vector2(0.5f, 1f));
					this.thisRTransform.set_anchorMax(new Vector2(0.5f, 1f));
					this.thisRTransform.set_pivot(new Vector2(0.5f, 1f));
					goto IL_13F;
				case 2:
				case 3:
					IL_3A:
					if (num != 7)
					{
						goto IL_13F;
					}
					this.thisRTransform.set_anchorMin(new Vector2(0.5f, 0f));
					this.thisRTransform.set_anchorMax(new Vector2(0.5f, 0f));
					this.thisRTransform.set_pivot(new Vector2(0.5f, 0f));
					goto IL_13F;
				case 4:
					this.thisRTransform.set_anchorMin(new Vector2(0.5f, 0.5f));
					this.thisRTransform.set_anchorMax(new Vector2(0.5f, 0.5f));
					this.thisRTransform.set_pivot(new Vector2(0.5f, 0.5f));
					goto IL_13F;
				}
				goto IL_3A;
				IL_13F:
				if (component != null)
				{
					component.set_alignment(7);
				}
				this.thisRTransform.set_anchoredPosition(new Vector2(vector.x, vector.y));
			}
		}

		private void SetSizeWidth(object arg)
		{
			if (arg != null)
			{
				float num = (float)arg;
				this.thisRTransform.set_sizeDelta(new Vector2(num, this.thisRTransform.get_sizeDelta().y));
			}
		}

		private void SetSizeHeight(object arg)
		{
			if (arg != null)
			{
				float num = (float)arg;
				this.thisRTransform.set_sizeDelta(new Vector2(this.thisRTransform.get_sizeDelta().x, num));
			}
		}

		private void SetIgnoreLayout(object arg)
		{
			if (arg != null)
			{
				bool ignoreLayout = (bool)arg;
				LayoutElement component = base.GetComponent<LayoutElement>();
				component.set_ignoreLayout(ignoreLayout);
			}
		}
	}
}
