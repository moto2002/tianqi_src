using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7213), ForSend(7213), ProtoContract(Name = "TimeLimitedSalesNty")]
	[Serializable]
	public class TimeLimitedSalesNty : IExtensible
	{
		public static readonly short OP = 7213;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
