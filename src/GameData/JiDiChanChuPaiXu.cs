using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JiDiChanChuPaiXu")]
	[Serializable]
	public class JiDiChanChuPaiXu : IExtensible
	{
		private int _winNum;

		private int _baseType;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "winNum", DataFormat = DataFormat.TwosComplement)]
		public int winNum
		{
			get
			{
				return this._winNum;
			}
			set
			{
				this._winNum = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "baseType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int baseType
		{
			get
			{
				return this._baseType;
			}
			set
			{
				this._baseType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
