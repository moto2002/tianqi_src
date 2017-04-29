using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JiNengPeiZhiJieSuo")]
	[Serializable]
	public class JiNengPeiZhiJieSuo : IExtensible
	{
		private int _id;

		private int _deblockingCostType;

		private int _deblockingCost;

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

		[ProtoMember(3, IsRequired = false, Name = "deblockingCostType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deblockingCostType
		{
			get
			{
				return this._deblockingCostType;
			}
			set
			{
				this._deblockingCostType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "deblockingCost", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deblockingCost
		{
			get
			{
				return this._deblockingCost;
			}
			set
			{
				this._deblockingCost = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
