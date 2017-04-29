using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5152), ForSend(5152), ProtoContract(Name = "RankInfoReq")]
	[Serializable]
	public class RankInfoReq : IExtensible
	{
		public static readonly short OP = 5152;

		private int _rankingType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "rankingType", DataFormat = DataFormat.TwosComplement)]
		public int rankingType
		{
			get
			{
				return this._rankingType;
			}
			set
			{
				this._rankingType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
