using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(6264), ForSend(6264), ProtoContract(Name = "UpdateAwardRes")]
	[Serializable]
	public class UpdateAwardRes : IExtensible
	{
		public static readonly short OP = 6264;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
