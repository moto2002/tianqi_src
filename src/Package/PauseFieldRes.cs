using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(540), ForSend(540), ProtoContract(Name = "PauseFieldRes")]
	[Serializable]
	public class PauseFieldRes : IExtensible
	{
		public static readonly short OP = 540;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
