using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_EndWeak")]
	[Serializable]
	public class BattleAction_EndWeak : IExtensible
	{
		private long _soldierId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "soldierId", DataFormat = DataFormat.TwosComplement)]
		public long soldierId
		{
			get
			{
				return this._soldierId;
			}
			set
			{
				this._soldierId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
