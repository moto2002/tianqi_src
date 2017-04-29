using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ActiveCenterInfo")]
	[Serializable]
	public class ActiveCenterInfo : IExtensible
	{
		[ProtoContract(Name = "ActiveStatus")]
		[Serializable]
		public class ActiveStatus : IExtensible
		{
			[ProtoContract(Name = "AS")]
			public enum AS
			{
				[ProtoEnum(Name = "Wait", Value = 1)]
				Wait = 1,
				[ProtoEnum(Name = "Start", Value = 2)]
				Start,
				[ProtoEnum(Name = "Close", Value = 3)]
				Close,
				[ProtoEnum(Name = "NotOpen", Value = 4)]
				NotOpen,
				[ProtoEnum(Name = "PrepareOpen", Value = 5)]
				PrepareOpen
			}

			private IExtension extensionObject;

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _id;

		private ActiveCenterInfo.ActiveStatus.AS _status;

		private int _remainTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "status", DataFormat = DataFormat.TwosComplement)]
		public ActiveCenterInfo.ActiveStatus.AS status
		{
			get
			{
				return this._status;
			}
			set
			{
				this._status = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "remainTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainTimes
		{
			get
			{
				return this._remainTimes;
			}
			set
			{
				this._remainTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
