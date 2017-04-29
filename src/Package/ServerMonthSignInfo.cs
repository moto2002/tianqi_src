using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ServerMonthSignInfo")]
	[Serializable]
	public class ServerMonthSignInfo : IExtensible
	{
		private int _signMonth;

		private float _lastSignTime;

		private int _totalSignNum;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "signMonth", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int signMonth
		{
			get
			{
				return this._signMonth;
			}
			set
			{
				this._signMonth = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "lastSignTime", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float lastSignTime
		{
			get
			{
				return this._lastSignTime;
			}
			set
			{
				this._lastSignTime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "totalSignNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int totalSignNum
		{
			get
			{
				return this._totalSignNum;
			}
			set
			{
				this._totalSignNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
