using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_AddFilter")]
	[Serializable]
	public class BattleAction_AddFilter : IExtensible
	{
		private long _soldierId;

		private int _filterType;

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

		[ProtoMember(2, IsRequired = true, Name = "filterType", DataFormat = DataFormat.TwosComplement)]
		public int filterType
		{
			get
			{
				return this._filterType;
			}
			set
			{
				this._filterType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
