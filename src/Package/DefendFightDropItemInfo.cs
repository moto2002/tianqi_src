using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "DefendFightDropItemInfo")]
	[Serializable]
	public class DefendFightDropItemInfo : IExtensible
	{
		private int _typeId;

		private long _count;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "count", DataFormat = DataFormat.TwosComplement)]
		public long count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
