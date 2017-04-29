using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "YuanSuFuBenPeiZhi")]
	[Serializable]
	public class YuanSuFuBenPeiZhi : IExtensible
	{
		private int _id;

		private int _ruleId;

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

		[ProtoMember(3, IsRequired = false, Name = "ruleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ruleId
		{
			get
			{
				return this._ruleId;
			}
			set
			{
				this._ruleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
