using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "SShenBingDengJi")]
	[Serializable]
	public class SShenBingDengJi : IExtensible
	{
		private int _id;

		private int _level;

		private int _EXP;

		private int _attrID;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "EXP", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int EXP
		{
			get
			{
				return this._EXP;
			}
			set
			{
				this._EXP = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "attrID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attrID
		{
			get
			{
				return this._attrID;
			}
			set
			{
				this._attrID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
