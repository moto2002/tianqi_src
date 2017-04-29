using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(153), ForSend(153), ProtoContract(Name = "GetSumRechargeRes")]
	[Serializable]
	public class GetSumRechargeRes : IExtensible
	{
		public static readonly short OP = 153;

		private int _rechargeNum;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "rechargeNum", DataFormat = DataFormat.TwosComplement)]
		public int rechargeNum
		{
			get
			{
				return this._rechargeNum;
			}
			set
			{
				this._rechargeNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
