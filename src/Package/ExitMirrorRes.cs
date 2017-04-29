using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(586), ForSend(586), ProtoContract(Name = "ExitMirrorRes")]
	[Serializable]
	public class ExitMirrorRes : IExtensible
	{
		public static readonly short OP = 586;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
