using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(214), ForSend(214), ProtoContract(Name = "CancelAutoMatchRes")]
	[Serializable]
	public class CancelAutoMatchRes : IExtensible
	{
		public static readonly short OP = 214;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
