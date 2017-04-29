using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JuQingZhiYinDuiHua")]
	[Serializable]
	public class JuQingZhiYinDuiHua : IExtensible
	{
		private int _speakLibrary;

		private int _monsterId;

		private int _speak;

		private int _weight;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "speakLibrary", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int speakLibrary
		{
			get
			{
				return this._speakLibrary;
			}
			set
			{
				this._speakLibrary = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "monsterId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterId
		{
			get
			{
				return this._monsterId;
			}
			set
			{
				this._monsterId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "speak", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int speak
		{
			get
			{
				return this._speak;
			}
			set
			{
				this._speak = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "weight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
