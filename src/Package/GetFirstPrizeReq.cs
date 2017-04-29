using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3345), ForSend(3345), ProtoContract(Name = "GetFirstPrizeReq")]
	[Serializable]
	public class GetFirstPrizeReq : IExtensible
	{
		public static readonly short OP = 3345;

		private int _id;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
