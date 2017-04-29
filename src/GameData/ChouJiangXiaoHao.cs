using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChouJiangXiaoHao")]
	[Serializable]
	public class ChouJiangXiaoHao : IExtensible
	{
		private int _id;

		private int _openLv;

		private int _itemId;

		private int _amount;

		private int _lotteryId;

		private int _lotteryAmount;

		private int _times;

		private int _group;

		private readonly List<int> _firstDropId = new List<int>();

		private readonly List<int> _dropId = new List<int>();

		private int _freetime;

		private int _music;

		private int _pictureId;

		private int _titleId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "openLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openLv
		{
			get
			{
				return this._openLv;
			}
			set
			{
				this._openLv = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "amount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				this._amount = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "lotteryId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lotteryId
		{
			get
			{
				return this._lotteryId;
			}
			set
			{
				this._lotteryId = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "lotteryAmount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lotteryAmount
		{
			get
			{
				return this._lotteryAmount;
			}
			set
			{
				this._lotteryAmount = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "times", DataFormat = DataFormat.TwosComplement)]
		public int times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "group", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int group
		{
			get
			{
				return this._group;
			}
			set
			{
				this._group = value;
			}
		}

		[ProtoMember(10, Name = "firstDropId", DataFormat = DataFormat.TwosComplement)]
		public List<int> firstDropId
		{
			get
			{
				return this._firstDropId;
			}
		}

		[ProtoMember(11, Name = "dropId", DataFormat = DataFormat.TwosComplement)]
		public List<int> dropId
		{
			get
			{
				return this._dropId;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "freetime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int freetime
		{
			get
			{
				return this._freetime;
			}
			set
			{
				this._freetime = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "music", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int music
		{
			get
			{
				return this._music;
			}
			set
			{
				this._music = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "pictureId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pictureId
		{
			get
			{
				return this._pictureId;
			}
			set
			{
				this._pictureId = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "titleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int titleId
		{
			get
			{
				return this._titleId;
			}
			set
			{
				this._titleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
