using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YueQia")]
	[Serializable]
	public class YueQia : IExtensible
	{
		private int _ID;

		private int _name;

		private int _rmb;

		private int _diamonds;

		private int _desc;

		private int _everydayDiamonds;

		private int _dayAmount;

		private int _mailId;

		private int _activeTime;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "name", DataFormat = DataFormat.TwosComplement)]
		public int name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "rmb", DataFormat = DataFormat.TwosComplement)]
		public int rmb
		{
			get
			{
				return this._rmb;
			}
			set
			{
				this._rmb = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "diamonds", DataFormat = DataFormat.TwosComplement)]
		public int diamonds
		{
			get
			{
				return this._diamonds;
			}
			set
			{
				this._diamonds = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "desc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "everydayDiamonds", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int everydayDiamonds
		{
			get
			{
				return this._everydayDiamonds;
			}
			set
			{
				this._everydayDiamonds = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "dayAmount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dayAmount
		{
			get
			{
				return this._dayAmount;
			}
			set
			{
				this._dayAmount = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "mailId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mailId
		{
			get
			{
				return this._mailId;
			}
			set
			{
				this._mailId = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "activeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activeTime
		{
			get
			{
				return this._activeTime;
			}
			set
			{
				this._activeTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
