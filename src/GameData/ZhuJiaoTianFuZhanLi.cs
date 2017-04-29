using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ZhuJiaoTianFuZhanLi")]
	[Serializable]
	public class ZhuJiaoTianFuZhanLi : IExtensible
	{
		private int _id;

		private int _lv;

		private int _talentUnitFightPower;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "talentUnitFightPower", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int talentUnitFightPower
		{
			get
			{
				return this._talentUnitFightPower;
			}
			set
			{
				this._talentUnitFightPower = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
