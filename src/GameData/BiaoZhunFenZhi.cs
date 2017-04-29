using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "BiaoZhunFenZhi")]
	[Serializable]
	public class BiaoZhunFenZhi : IExtensible
	{
		private int _Lv;

		private int _point;

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

		[ProtoMember(3, IsRequired = false, Name = "point", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int point
		{
			get
			{
				return this._point;
			}
			set
			{
				this._point = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
