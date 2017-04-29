using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3829), ForSend(3829), ProtoContract(Name = "DefendFightMatchStatusNty")]
	[Serializable]
	public class DefendFightMatchStatusNty : IExtensible
	{
		public static readonly short OP = 3829;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
