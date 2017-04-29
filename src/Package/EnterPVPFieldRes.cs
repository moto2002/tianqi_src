using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(624), ForSend(624), ProtoContract(Name = "EnterPVPFieldRes")]
	[Serializable]
	public class EnterPVPFieldRes : IExtensible
	{
		public static readonly short OP = 624;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
