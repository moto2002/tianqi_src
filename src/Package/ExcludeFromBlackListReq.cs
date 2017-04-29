using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(971), ForSend(971), ProtoContract(Name = "ExcludeFromBlackListReq")]
	[Serializable]
	public class ExcludeFromBlackListReq : IExtensible
	{
		public static readonly short OP = 971;

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
