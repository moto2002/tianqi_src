using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"Lv"
	}), ProtoContract(Name = "Golds")]
	[Serializable]
	public class Golds : IExtensible
	{
		private int _Lv;

		private int _gainGold;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Lv", DataFormat = DataFormat.TwosComplement)]
		public int Lv
		{
			get
			{
				return this._Lv;
			}
			set
			{
				this._Lv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "gainGold", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gainGold
		{
			get
			{
				return this._gainGold;
			}
			set
			{
				this._gainGold = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
