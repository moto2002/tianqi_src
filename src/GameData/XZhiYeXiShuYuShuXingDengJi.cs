using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XZhiYeXiShuYuShuXingDengJi")]
	[Serializable]
	public class XZhiYeXiShuYuShuXingDengJi : IExtensible
	{
		private int _attrId;

		private int _level;

		private float _kuangzhanshiJobValue;

		private float _jixieshiJobValue;

		private float _jianhaoJobValue;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "attrId", DataFormat = DataFormat.TwosComplement)]
		public int attrId
		{
			get
			{
				return this._attrId;
			}
			set
			{
				this._attrId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "kuangzhanshiJobValue", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float kuangzhanshiJobValue
		{
			get
			{
				return this._kuangzhanshiJobValue;
			}
			set
			{
				this._kuangzhanshiJobValue = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "jixieshiJobValue", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float jixieshiJobValue
		{
			get
			{
				return this._jixieshiJobValue;
			}
			set
			{
				this._jixieshiJobValue = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "jianhaoJobValue", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float jianhaoJobValue
		{
			get
			{
				return this._jianhaoJobValue;
			}
			set
			{
				this._jianhaoJobValue = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
