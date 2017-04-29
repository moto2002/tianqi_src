using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(114), ForSend(114), ProtoContract(Name = "GetFundRewardReq")]
	[Serializable]
	public class GetFundRewardReq : IExtensible
	{
		public static readonly short OP = 114;

		private int _Id;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
