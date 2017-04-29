using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(81), ForSend(81), ProtoContract(Name = "AssaultRes")]
	[Serializable]
	public class AssaultRes : IExtensible
	{
		public static readonly short OP = 81;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
