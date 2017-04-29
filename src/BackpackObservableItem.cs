using Foundation.Core;
using System;
using UnityEngine;

public class BackpackObservableItem : ObservableObject
{
	public class Names
	{
		public const string ItemRootNullOn = "ItemRootNullOn";

		public const string ItemRootOn = "ItemRootOn";

		public const string ItemFrame = "ItemFrame";

		public const string ItemIcon = "ItemIcon";

		public const string ItemNum = "ItemNum";

		public const string ItemFlag = "ItemFlag";

		public const string ItemStepOn = "ItemStepOn";

		public const string ItemStepNum = "ItemStepNum";

		public const string IsSelected01 = "IsSelected01";

		public const string IsSelected02 = "IsSelected02";

		public const string IsSelected02ModeOn = "IsSelected02ModeOn";

		public const string ExcellentAttrVisibility = "ExcellentAttrVisibility";

		public const string ExcellentImage1 = "ExcellentImage1";

		public const string ExcellentImage2 = "ExcellentImage2";

		public const string ExcellentImage3 = "ExcellentImage3";

		public const string EquipIsBinding = "EquipIsBinding";

		public const string RedPointOn = "RedPointOn";

		public const string OnClickItem = "OnClickItem";
	}

	public long id;

	private int itemId;

	public Action<BackpackObservableItem> OnClickItemAction;

	private bool _ItemRootNullOn;

	private bool _ItemRootOn;

	private SpriteRenderer _ItemFrame;

	private SpriteRenderer _ItemIcon;

	private string _ItemNum;

	private bool _ItemFlag;

	private bool _ItemStepOn;

	private string _ItemStepNum;

	private bool _IsSelected01;

	private bool _IsSelected02;

	private bool _IsSelected02ModeOn;

	private bool _ExcellentImage1;

	private bool _ExcellentImage2;

	private bool _ExcellentImage3;

	private bool _ExcellentAttrVisibility;

	private int _ExcellentCount;

	private bool _EquipIsBinding;

	private bool _RedPointOn;

	private int mSelectedMode = 1;

	public int ItemId
	{
		get
		{
			return this.itemId;
		}
		set
		{
			this.itemId = value;
			this.ItemFrame = GameDataUtils.GetItemFrame(this.itemId);
		}
	}

	public bool ItemRootNullOn
	{
		get
		{
			return this._ItemRootNullOn;
		}
		set
		{
			if (this._ItemRootNullOn == value)
			{
				return;
			}
			this._ItemRootNullOn = value;
		}
	}

	public bool ItemRootOn
	{
		get
		{
			return this._ItemRootOn;
		}
		set
		{
			if (this._ItemRootOn == value)
			{
				return;
			}
			this._ItemRootOn = value;
		}
	}

	public SpriteRenderer ItemFrame
	{
		get
		{
			return this._ItemFrame;
		}
		set
		{
			this._ItemFrame = value;
			base.NotifyProperty("ItemFrame", value);
		}
	}

	public SpriteRenderer ItemIcon
	{
		get
		{
			return this._ItemIcon;
		}
		set
		{
			this._ItemIcon = value;
			base.NotifyProperty("ItemIcon", value);
		}
	}

	public string ItemNum
	{
		get
		{
			return this._ItemNum;
		}
		set
		{
			if (this._ItemNum == value)
			{
				return;
			}
			this._ItemNum = value;
			base.NotifyProperty("ItemNum", value);
		}
	}

	public bool ItemFlag
	{
		get
		{
			return this._ItemFlag;
		}
		set
		{
			if (this._ItemFlag == value)
			{
				return;
			}
			this._ItemFlag = value;
		}
	}

	public bool ItemStepOn
	{
		get
		{
			return this._ItemStepOn;
		}
		set
		{
			if (this._ItemStepOn == value)
			{
				return;
			}
			this._ItemStepOn = value;
			base.NotifyProperty("ItemStepOn", value);
		}
	}

	public string ItemStepNum
	{
		get
		{
			return this._ItemStepNum;
		}
		set
		{
			this._ItemStepNum = value;
			base.NotifyProperty("ItemStepNum", value);
		}
	}

	public bool IsSelected01
	{
		get
		{
			return this._IsSelected01;
		}
		set
		{
			if (this._IsSelected01 == value)
			{
				return;
			}
			this._IsSelected01 = value;
			base.NotifyProperty("IsSelected01", value);
		}
	}

	public bool IsSelected02
	{
		get
		{
			return this._IsSelected02;
		}
		set
		{
			if (this._IsSelected02 == value)
			{
				return;
			}
			this._IsSelected02 = value;
			base.NotifyProperty("IsSelected02", value);
		}
	}

	public bool IsSelected02ModeOn
	{
		get
		{
			return this._IsSelected02ModeOn;
		}
		set
		{
			if (this._IsSelected02ModeOn == value)
			{
				return;
			}
			this._IsSelected02ModeOn = value;
			base.NotifyProperty("IsSelected02ModeOn", value);
		}
	}

	public bool ExcellentImage1
	{
		get
		{
			return this._ExcellentImage1;
		}
		set
		{
			if (this._ExcellentImage1 != value)
			{
				this._ExcellentImage1 = value;
				base.NotifyProperty("ExcellentImage1", value);
			}
		}
	}

	public bool ExcellentImage2
	{
		get
		{
			return this._ExcellentImage2;
		}
		set
		{
			if (this._ExcellentImage2 != value)
			{
				this._ExcellentImage2 = value;
				base.NotifyProperty("ExcellentImage2", value);
			}
		}
	}

	public bool ExcellentImage3
	{
		get
		{
			return this._ExcellentImage3;
		}
		set
		{
			if (this._ExcellentImage3 != value)
			{
				this._ExcellentImage3 = value;
				base.NotifyProperty("ExcellentImage3", value);
			}
		}
	}

	public bool ExcellentAttrVisibility
	{
		get
		{
			return this._ExcellentAttrVisibility;
		}
		set
		{
			this._ExcellentAttrVisibility = value;
			base.NotifyProperty("ExcellentAttrVisibility", value);
		}
	}

	public int ExcellentCount
	{
		get
		{
			return this._ExcellentCount;
		}
		set
		{
			this._ExcellentCount = value;
			if (this._ExcellentCount > 0)
			{
				this.ExcellentAttrVisibility = true;
				this.ExcellentImage1 = (this._ExcellentCount >= 1);
				this.ExcellentImage2 = (this._ExcellentCount >= 2);
				this.ExcellentImage3 = (this._ExcellentCount >= 3);
			}
			else
			{
				this.ExcellentAttrVisibility = false;
			}
		}
	}

	public bool EquipIsBinding
	{
		get
		{
			return this._EquipIsBinding;
		}
		set
		{
			this._EquipIsBinding = value;
			base.NotifyProperty("EquipIsBinding", value);
		}
	}

	public bool RedPointOn
	{
		get
		{
			return this._RedPointOn;
		}
		set
		{
			if (this._RedPointOn != value)
			{
				this._RedPointOn = value;
				base.NotifyProperty("RedPointOn", value);
			}
		}
	}

	public void OnClickItem(BackpackObservableItem item)
	{
		if (this.OnClickItemAction != null)
		{
			this.OnClickItemAction.Invoke(item);
		}
	}

	public void SetSelectedMode(int mode)
	{
		this.mSelectedMode = mode;
		this.IsSelected02ModeOn = (mode == 2);
	}

	public void SetIsSelected(bool isOn)
	{
		if (this.mSelectedMode == 1)
		{
			this.IsSelected01 = isOn;
		}
		else if (this.mSelectedMode == 2)
		{
			this.IsSelected02 = isOn;
		}
	}

	public bool GetIsSelected()
	{
		if (this.mSelectedMode == 1)
		{
			return this.IsSelected01;
		}
		return this.mSelectedMode == 2 && this.IsSelected02;
	}
}
