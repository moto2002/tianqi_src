using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(167), ForSend(167), ProtoContract(Name = "MultiPvpBeginMatchRes")]
	[Serializable]
	public class MultiPvpBeginMatchRes : IExtensible
	{
		public static readonly short OP = 167;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
