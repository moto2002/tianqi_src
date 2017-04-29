using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChongWuLianLianKan")]
	[Serializable]
	public class ChongWuLianLianKan : IExtensible
	{
		private int _id;

		private int _picture;

		private int _weight;

		private int _rewardDrop;

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

		[ProtoMember(4, IsRequired = false, Name = "picture", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int picture
		{
			get
			{
				return this._picture;
			}
			set
			{
				this._picture = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "weight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "rewardDrop", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardDrop
		{
			get
			{
				return this._rewardDrop;
			}
			set
			{
				this._rewardDrop = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
