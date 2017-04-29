using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(163), ForSend(163), ProtoContract(Name = "MultiPvpReliveRes")]
	[Serializable]
	public class MultiPvpReliveRes : IExtensible
	{
		public static readonly short OP = 163;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
