using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(102), ForSend(102), ProtoContract(Name = "HeartBeatRes")]
	[Serializable]
	public class HeartBeatRes : IExtensible
	{
		public static readonly short OP = 102;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
