using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleFieldT")]
	[Serializable]
	public class BattleFieldT : IExtensible
	{
		[ProtoContract(Name = "BFT")]
		public enum BFT
		{
			[ProtoEnum(Name = "FrontEnd", Value = 0)]
			FrontEnd,
			[ProtoEnum(Name = "BackEnd", Value = 1)]
			BackEnd,
			[ProtoEnum(Name = "Mixed", Value = 2)]
			Mixed
		}

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
