using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6808), ForSend(6808), ProtoContract(Name = "SelectItemReq")]
	[Serializable]
	public class SelectItemReq : IExtensible
	{
		public static readonly short OP = 6808;

		private int _id;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
