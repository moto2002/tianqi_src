using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DiaoLuoZu")]
	[Serializable]
	public class DiaoLuoZu : IExtensible
	{
		private int _id;

		private int _groupId;

		private int _itemId;

		private int _minNum;

		private int _maxNum;

		private int _weigh;

		private int _minLv;

		private int _maxLv;

		private int _profession;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "groupId", DataFormat = DataFormat.TwosComplement)]
		public int groupId
		{
			get
			{
				return this._groupId;
			}
			set
			{
				this._groupId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "minNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minNum
		{
			get
			{
				return this._minNum;
			}
			set
			{
				this._minNum = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "maxNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxNum
		{
			get
			{
				return this._maxNum;
			}
			set
			{
				this._maxNum = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "weigh", DataFormat = DataFormat.TwosComplement)]
		public int weigh
		{
			get
			{
				return this._weigh;
			}
			set
			{
				this._weigh = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "maxLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxLv
		{
			get
			{
				return this._maxLv;
			}
			set
			{
				this._maxLv = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "profession", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int profession
		{
			get
			{
				return this._profession;
			}
			set
			{
				this._profession = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
