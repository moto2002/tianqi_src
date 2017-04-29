using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(98), ForSend(98), ProtoContract(Name = "ReliveInGuildWarRes")]
	[Serializable]
	public class ReliveInGuildWarRes : IExtensible
	{
		public static readonly short OP = 98;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
