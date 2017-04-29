using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "BattleAction_RemoveEffect")]
	[Serializable]
	public class BattleAction_RemoveEffect : IExtensible
	{
		private int _effectId;

		private long _uniqueId;

		private long _casterId;

		private readonly List<long> _targetIds = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "effectId", DataFormat = DataFormat.TwosComplement)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "uniqueId", DataFormat = DataFormat.TwosComplement)]
		public long uniqueId
		{
			get
			{
				return this._uniqueId;
			}
			set
			{
				this._uniqueId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "casterId", DataFormat = DataFormat.TwosComplement)]
		public long casterId
		{
			get
			{
				return this._casterId;
			}
			set
			{
				this._casterId = value;
			}
		}

		[ProtoMember(4, Name = "targetIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> targetIds
		{
			get
			{
				return this._targetIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
