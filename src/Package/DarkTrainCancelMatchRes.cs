using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2106), ForSend(2106), ProtoContract(Name = "DarkTrainCancelMatchRes")]
	[Serializable]
	public class DarkTrainCancelMatchRes : IExtensible
	{
		public static readonly short OP = 2106;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
