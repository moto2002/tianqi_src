using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(401), ForSend(401), ProtoContract(Name = "CleanUpReq")]
	[Serializable]
	public class CleanUpReq : IExtensible
	{
		public static readonly short OP = 401;

		private int _bagType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "bagType", DataFormat = DataFormat.TwosComplement)]
		public int bagType
		{
			get
			{
				return this._bagType;
			}
			set
			{
				this._bagType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
