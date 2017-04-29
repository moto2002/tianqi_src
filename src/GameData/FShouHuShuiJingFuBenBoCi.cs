using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "FShouHuShuiJingFuBenBoCi")]
	[Serializable]
	public class FShouHuShuiJingFuBenBoCi : IExtensible
	{
		private int _id;

		private int _refreshId;

		private int _level;

		private int _rewardId;

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

		[ProtoMember(3, IsRequired = false, Name = "refreshId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshId
		{
			get
			{
				return this._refreshId;
			}
			set
			{
				this._refreshId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "rewardId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId
		{
			get
			{
				return this._rewardId;
			}
			set
			{
				this._rewardId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
