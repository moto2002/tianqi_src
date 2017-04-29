using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3773), ForSend(3773), ProtoContract(Name = "CheckMailReq")]
	[Serializable]
	public class CheckMailReq : IExtensible
	{
		public static readonly short OP = 3773;

		private long _id;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public long id
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
