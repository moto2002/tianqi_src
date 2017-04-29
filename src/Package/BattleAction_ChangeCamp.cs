using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_ChangeCamp")]
	[Serializable]
	public class BattleAction_ChangeCamp : IExtensible
	{
		private long _soldierId;

		private int _camp;

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

		[ProtoMember(2, IsRequired = true, Name = "camp", DataFormat = DataFormat.TwosComplement)]
		public int camp
		{
			get
			{
				return this._camp;
			}
			set
			{
				this._camp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
