using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "RiChangJiangLiMiaoShu")]
	[Serializable]
	public class RiChangJiangLiMiaoShu : IExtensible
	{
		private int _rewardId;

		private int _id;

		private int _minLv;

		private int _maxLv;

		private int _rewardChinese;

		private int _reward;

		private readonly List<int> _rewardShow = new List<int>();

		private readonly List<int> _rewardNum = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "rewardId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = true, Name = "minLv", DataFormat = DataFormat.TwosComplement)]
		public int minLv
		{
			get
			{
				return this._minLv;
			}
			set
			{
				this._minLv = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "maxLv", DataFormat = DataFormat.TwosComplement)]
		public int maxLv
		{
			get
			{
				return this._maxLv;
			}
			set
			{
				this._maxLv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "rewardChinese", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardChinese
		{
			get
			{
				return this._rewardChinese;
			}
			set
			{
				this._rewardChinese = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "reward", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int reward
		{
			get
			{
				return this._reward;
			}
			set
			{
				this._reward = value;
			}
		}

		[ProtoMember(7, Name = "rewardShow", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardShow
		{
			get
			{
				return this._rewardShow;
			}
		}

		[ProtoMember(8, Name = "rewardNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardNum
		{
			get
			{
				return this._rewardNum;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
