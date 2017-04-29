using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_AllLoadDoneNty")]
	[Serializable]
	public class BattleAction_AllLoadDoneNty : IExtensible
	{
		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
