using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1299), ForSend(1299), ProtoContract(Name = "FirstOpenReq")]
	[Serializable]
	public class FirstOpenReq : IExtensible
	{
		public static readonly short OP = 1299;

		private int _typeId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
