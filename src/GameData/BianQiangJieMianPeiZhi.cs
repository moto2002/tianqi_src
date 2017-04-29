using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "BianQiangJieMianPeiZhi")]
	[Serializable]
	public class BianQiangJieMianPeiZhi : IExtensible
	{
		private int _type;

		private int _pathId;

		private int _name;

		private int _name2;

		private int _name3;

		private int _icon;

		private int _icon2;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "pathId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pathId
		{
			get
			{
				return this._pathId;
			}
			set
			{
				this._pathId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "name2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name2
		{
			get
			{
				return this._name2;
			}
			set
			{
				this._name2 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "name3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name3
		{
			get
			{
				return this._name3;
			}
			set
			{
				this._name3 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "icon2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon2
		{
			get
			{
				return this._icon2;
			}
			set
			{
				this._icon2 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
