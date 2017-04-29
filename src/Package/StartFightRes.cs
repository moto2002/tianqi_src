using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(218), ForSend(218), ProtoContract(Name = "StartFightRes")]
	[Serializable]
	public class StartFightRes : IExtensible
	{
		public static readonly short OP = 218;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
