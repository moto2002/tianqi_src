using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "SCaiLiaoTiGongJingYanZhi")]
	[Serializable]
	public class SCaiLiaoTiGongJingYanZhi : IExtensible
	{
		private int _id;

		private int _EXP;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "EXP", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int EXP
		{
			get
			{
				return this._EXP;
			}
			set
			{
				this._EXP = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
