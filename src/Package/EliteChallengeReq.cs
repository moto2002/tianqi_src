using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2279), ForSend(2279), ProtoContract(Name = "EliteChallengeReq")]
	[Serializable]
	public class EliteChallengeReq : IExtensible
	{
		public static readonly short OP = 2279;

		private int _copyId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "copyId", DataFormat = DataFormat.TwosComplement)]
		public int copyId
		{
			get
			{
				return this._copyId;
			}
			set
			{
				this._copyId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
