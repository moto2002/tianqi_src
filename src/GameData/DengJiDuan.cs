using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DengJiDuan")]
	[Serializable]
	public class DengJiDuan : IExtensible
	{
		private int _lvId;

		private int _minLv;

		private int _maxLv;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "lvId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lvId
		{
			get
			{
				return this._lvId;
			}
			set
			{
				this._lvId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "minLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "maxLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxLv
		{
			get
			{
				return this._maxLv;
			}
			set
			{
				this._maxLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
