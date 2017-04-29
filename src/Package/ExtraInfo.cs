using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ExtraInfo")]
	[Serializable]
	public class ExtraInfo : IExtensible
	{
		private int _endTime;

		private int _transformId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "endTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "transformId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int transformId
		{
			get
			{
				return this._transformId;
			}
			set
			{
				this._transformId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
