using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(574), ForSend(574), ProtoContract(Name = "CancelLoginRes")]
	[Serializable]
	public class CancelLoginRes : IExtensible
	{
		public static readonly short OP = 574;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
