using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(492), ForSend(492), ProtoContract(Name = "FindBuddyReq")]
	[Serializable]
	public class FindBuddyReq : IExtensible
	{
		public static readonly short OP = 492;

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
