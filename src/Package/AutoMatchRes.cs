using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(208), ForSend(208), ProtoContract(Name = "AutoMatchRes")]
	[Serializable]
	public class AutoMatchRes : IExtensible
	{
		public static readonly short OP = 208;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
