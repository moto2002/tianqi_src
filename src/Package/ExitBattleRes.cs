using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(560), ForSend(560), ProtoContract(Name = "ExitBattleRes")]
	[Serializable]
	public class ExitBattleRes : IExtensible
	{
		public static readonly short OP = 560;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
