using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(41), ForSend(41), ProtoContract(Name = "BuyExperienceCopyBuffReq")]
	[Serializable]
	public class BuyExperienceCopyBuffReq : IExtensible
	{
		public static readonly short OP = 41;

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
