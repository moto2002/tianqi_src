using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "FXueCaiDianFengFuBenBoCi")]
	[Serializable]
	public class FXueCaiDianFengFuBenBoCi : IExtensible
	{
		private int _id;

		private int _enemyNum;

		private readonly List<int> _level = new List<int>();

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

		[ProtoMember(3, IsRequired = false, Name = "enemyNum", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int enemyNum
		{
			get
			{
				return this._enemyNum;
			}
			set
			{
				this._enemyNum = value;
			}
		}

		[ProtoMember(4, Name = "level", DataFormat = DataFormat.TwosComplement)]
		public List<int> level
		{
			get
			{
				return this._level;
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
