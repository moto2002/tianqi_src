using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "LeiXingXiaoHaoBiao")]
	[Serializable]
	public class LeiXingXiaoHaoBiao : IExtensible
	{
		private int _ID;

		private int _resetTime;

		private int _needGoodtype;

		private int _needGoodNum;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "resetTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int resetTime
		{
			get
			{
				return this._resetTime;
			}
			set
			{
				this._resetTime = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "needGoodtype", DataFormat = DataFormat.TwosComplement)]
		public int needGoodtype
		{
			get
			{
				return this._needGoodtype;
			}
			set
			{
				this._needGoodtype = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "needGoodNum", DataFormat = DataFormat.TwosComplement)]
		public int needGoodNum
		{
			get
			{
				return this._needGoodNum;
			}
			set
			{
				this._needGoodNum = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
