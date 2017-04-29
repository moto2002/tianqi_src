using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(116), ForSend(116), ProtoContract(Name = "GetFundRewardRes")]
	[Serializable]
	public class GetFundRewardRes : IExtensible
	{
		public static readonly short OP = 116;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
