using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "AwardRecordInfo")]
	[Serializable]
	public class AwardRecordInfo : IExtensible
	{
		private long _roleId;

		private int _lastAwardTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "lastAwardTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastAwardTime
		{
			get
			{
				return this._lastAwardTime;
			}
			set
			{
				this._lastAwardTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
