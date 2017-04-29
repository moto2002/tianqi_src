using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(217), ForSend(217), ProtoContract(Name = "BattlePickCollectionRes")]
	[Serializable]
	public class BattlePickCollectionRes : IExtensible
	{
		public static readonly short OP = 217;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
