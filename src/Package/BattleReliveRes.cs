using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(161), ForSend(161), ProtoContract(Name = "BattleReliveRes")]
	[Serializable]
	public class BattleReliveRes : IExtensible
	{
		public static readonly short OP = 161;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
