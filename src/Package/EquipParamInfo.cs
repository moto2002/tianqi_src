using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "EquipParamInfo")]
	[Serializable]
	public class EquipParamInfo : IExtensible
	{
		private int _step;

		private int _quality;

		private int _position;

		private int _betterQuality;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "step", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "quality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int quality
		{
			get
			{
				return this._quality;
			}
			set
			{
				this._quality = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "position", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "betterQuality", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int betterQuality
		{
			get
			{
				return this._betterQuality;
			}
			set
			{
				this._betterQuality = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
