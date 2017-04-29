using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(118), ForSend(118), ProtoContract(Name = "FirstBattleEndRes")]
	[Serializable]
	public class FirstBattleEndRes : IExtensible
	{
		public static readonly short OP = 118;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
