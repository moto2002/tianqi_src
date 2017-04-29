using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Zu")]
	[Serializable]
	public class Zu : IExtensible
	{
		private int _groupId;

		private int _itemId;

		private long _Num;

		private int _weigh;

		private int _profession;

		private int _minLv;

		private int _maxLv;

		private IExtension extensionObject;

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

		[ProtoMember(4, IsRequired = false, Name = "Num", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long Num
		{
			get
			{
				return this._Num;
			}
			set
			{
				this._Num = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "weigh", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(6, IsRequired = false, Name = "profession", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
