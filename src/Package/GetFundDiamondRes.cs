using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(130), ForSend(130), ProtoContract(Name = "GetFundDiamondRes")]
	[Serializable]
	public class GetFundDiamondRes : IExtensible
	{
		public static readonly short OP = 130;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
