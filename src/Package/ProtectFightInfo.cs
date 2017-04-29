using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ProtectFightInfo")]
	[Serializable]
	public class ProtectFightInfo : IExtensible
	{
		private int _todayProtectTimes;

		private int _todayGrabTimes;

		private int _todayHelpProtectTimes;

		private int _lastGrabTime;

		private long _helpRoleId;

		private int _curQuality;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "todayProtectTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int todayProtectTimes
		{
			get
			{
				return this._todayProtectTimes;
			}
			set
			{
				this._todayProtectTimes = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "todayGrabTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int todayGrabTimes
		{
			get
			{
				return this._todayGrabTimes;
			}
			set
			{
				this._todayGrabTimes = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "todayHelpProtectTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "lastGrabTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lastGrabTime
		{
			get
			{
				return this._lastGrabTime;
			}
			set
			{
				this._lastGrabTime = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "helpRoleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long helpRoleId
		{
			get
			{
				return this._helpRoleId;
			}
			set
			{
				this._helpRoleId = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "curQuality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int curQuality
		{
			get
			{
				return this._curQuality;
			}
			set
			{
				this._curQuality = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
