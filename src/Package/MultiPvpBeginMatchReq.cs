using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(169), ForSend(169), ProtoContract(Name = "MultiPvpBeginMatchReq")]
	[Serializable]
	public class MultiPvpBeginMatchReq : IExtensible
	{
		public static readonly short OP = 169;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
