using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4312), ForSend(4312), ProtoContract(Name = "RechargeDiamondReq")]
	[Serializable]
	public class RechargeDiamondReq : IExtensible
	{
		public static readonly short OP = 4312;

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
