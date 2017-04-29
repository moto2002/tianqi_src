using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(456), ForSend(456), ProtoContract(Name = "AnswerMatchRes")]
	[Serializable]
	public class AnswerMatchRes : IExtensible
	{
		public static readonly short OP = 456;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
