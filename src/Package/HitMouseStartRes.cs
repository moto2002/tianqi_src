using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4382), ForSend(4382), ProtoContract(Name = "HitMouseStartRes")]
	[Serializable]
	public class HitMouseStartRes : IExtensible
	{
		public static readonly short OP = 4382;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
