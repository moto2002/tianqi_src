using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "CostGem")]
	[Serializable]
	public class CostGem : IExtensible
	{
		private long _gemId;

		private uint _gemNum = 1u;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "gemId", DataFormat = DataFormat.TwosComplement)]
		public long gemId
		{
			get
			{
				return this._gemId;
			}
			set
			{
				this._gemId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "gemNum", DataFormat = DataFormat.TwosComplement), DefaultValue(1L)]
		public uint gemNum
		{
			get
			{
				return this._gemNum;
			}
			set
			{
				this._gemNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
