using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_RemoveBuff")]
	[Serializable]
	public class BattleAction_RemoveBuff : IExtensible
	{
		private long _targetId;

		private int _buffId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "targetId", DataFormat = DataFormat.TwosComplement)]
		public long targetId
		{
			get
			{
				return this._targetId;
			}
			set
			{
				this._targetId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "buffId", DataFormat = DataFormat.TwosComplement)]
		public int buffId
		{
			get
			{
				return this._buffId;
			}
			set
			{
				this._buffId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
