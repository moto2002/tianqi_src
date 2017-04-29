using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "UpdateAcInfo")]
	[Serializable]
	public class UpdateAcInfo : IExtensible
	{
		[ProtoContract(Name = "AcStep")]
		[Serializable]
		public class AcStep : IExtensible
		{
			[ProtoContract(Name = "STEP")]
			public enum STEP
			{
				[ProtoEnum(Name = "Ready", Value = 1)]
				Ready = 1,
				[ProtoEnum(Name = "Start", Value = 2)]
				Start,
				[ProtoEnum(Name = "Finish", Value = 3)]
				Finish,
				[ProtoEnum(Name = "Close", Value = 4)]
				Close
			}

			private IExtension extensionObject;

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 478;

		private int _acId;

		private UpdateAcInfo.AcStep.STEP _status;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "acId", DataFormat = DataFormat.TwosComplement)]
		public int acId
		{
			get
			{
				return this._acId;
			}
			set
			{
				this._acId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "status", DataFormat = DataFormat.TwosComplement)]
		public UpdateAcInfo.AcStep.STEP status
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
