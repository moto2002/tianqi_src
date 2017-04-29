using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(740), ForSend(740), ProtoContract(Name = "ReliveInPVPFieldRes")]
	[Serializable]
	public class ReliveInPVPFieldRes : IExtensible
	{
		public static readonly short OP = 740;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
