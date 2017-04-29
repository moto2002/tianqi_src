using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(792), ForSend(792), ProtoContract(Name = "GangFightExitBattle")]
	[Serializable]
	public class GangFightExitBattle : IExtensible
	{
		public static readonly short OP = 792;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
