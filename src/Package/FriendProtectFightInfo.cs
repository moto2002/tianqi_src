using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "FriendProtectFightInfo")]
	[Serializable]
	public class FriendProtectFightInfo : IExtensible
	{
		private long _roleId;

		private int _todayHelpProtectTimes;

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

		[ProtoMember(2, IsRequired = false, Name = "todayHelpProtectTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int todayHelpProtectTimes
		{
			get
			{
				return this._todayHelpProtectTimes;
			}
			set
			{
				this._todayHelpProtectTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
