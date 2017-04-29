using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(443), ForSend(443), ProtoContract(Name = "DelBuddyReq")]
	[Serializable]
	public class DelBuddyReq : IExtensible
	{
		public static readonly short OP = 443;

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
