using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(105), ForSend(105), ProtoContract(Name = "EliteCancelMatchRes")]
	[Serializable]
	public class EliteCancelMatchRes : IExtensible
	{
		public static readonly short OP = 105;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
