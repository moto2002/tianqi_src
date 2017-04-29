using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(750), ForSend(750), ProtoContract(Name = "MoveToBlackListReq")]
	[Serializable]
	public class MoveToBlackListReq : IExtensible
	{
		public static readonly short OP = 750;

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
