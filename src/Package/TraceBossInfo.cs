using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "TraceBossInfo")]
	[Serializable]
	public class TraceBossInfo : IExtensible
	{
		private int _labelId;

		private int _nextTime = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "labelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int labelId
		{
			get
			{
				return this._labelId;
			}
			set
			{
				this._labelId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "nextTime", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int nextTime
		{
			get
			{
				return this._nextTime;
			}
			set
			{
				this._nextTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
