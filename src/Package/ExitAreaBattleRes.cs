using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(890), ForSend(890), ProtoContract(Name = "ExitAreaBattleRes")]
	[Serializable]
	public class ExitAreaBattleRes : IExtensible
	{
		public static readonly short OP = 890;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
