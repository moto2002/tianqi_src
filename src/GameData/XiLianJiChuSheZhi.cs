using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XiLianJiChuSheZhi")]
	[Serializable]
	public class XiLianJiChuSheZhi : IExtensible
	{
		private int _quality;

		private int _attrNum;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "quality", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "attrNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int attrNum
		{
			get
			{
				return this._attrNum;
			}
			set
			{
				this._attrNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
