using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ExtraData")]
	[Serializable]
	public class ExtraData : IExtensible
	{
		public static readonly short OP = 373;

		private int _typeId;

		private int _lastOpenTime;

		private int _lastCloseTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "typeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "lastOpenTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastOpenTime
		{
			get
			{
				return this._lastOpenTime;
			}
			set
			{
				this._lastOpenTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "lastCloseTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastCloseTime
		{
			get
			{
				return this._lastCloseTime;
			}
			set
			{
				this._lastCloseTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
