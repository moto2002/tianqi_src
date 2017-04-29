using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ProductionInfo")]
	[Serializable]
	public class ProductionInfo : IExtensible
	{
		private ulong _uId;

		private int _typeId;

		private int _countDown;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "uId", DataFormat = DataFormat.TwosComplement)]
		public ulong uId
		{
			get
			{
				return this._uId;
			}
			set
			{
				this._uId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "countDown", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int countDown
		{
			get
			{
				return this._countDown;
			}
			set
			{
				this._countDown = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
