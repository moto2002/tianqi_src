using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1783), ForSend(1783), ProtoContract(Name = "BuyGrowthPlanRes")]
	[Serializable]
	public class BuyGrowthPlanRes : IExtensible
	{
		public static readonly short OP = 1783;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
