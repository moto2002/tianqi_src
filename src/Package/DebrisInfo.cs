using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "DebrisInfo")]
	[Serializable]
	public class DebrisInfo : IExtensible
	{
		private int _debrisId;

		private int _debrisNum;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "debrisId", DataFormat = DataFormat.TwosComplement)]
		public int debrisId
		{
			get
			{
				return this._debrisId;
			}
			set
			{
				this._debrisId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "debrisNum", DataFormat = DataFormat.TwosComplement)]
		public int debrisNum
		{
			get
			{
				return this._debrisNum;
			}
			set
			{
				this._debrisNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
