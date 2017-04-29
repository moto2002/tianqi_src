using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4519), ForSend(4519), ProtoContract(Name = "KickNty")]
	[Serializable]
	public class KickNty : IExtensible
	{
		public static readonly short OP = 4519;

		private int _typeId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
