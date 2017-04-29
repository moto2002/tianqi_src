using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(142), ForSend(142), ProtoContract(Name = "MonthCardTimeRes")]
	[Serializable]
	public class MonthCardTimeRes : IExtensible
	{
		public static readonly short OP = 142;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
