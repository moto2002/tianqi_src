using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "PaiMingJiangLi")]
	[Serializable]
	public class PaiMingJiangLi : IExtensible
	{
		private int _rankingMax;

		private int _rankingMin;

		private int _reward;

		private readonly List<int> _itemId = new List<int>();

		private readonly List<int> _itemNum = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "rankingMax", DataFormat = DataFormat.TwosComplement)]
		public int rankingMax
		{
			get
			{
				return this._rankingMax;
			}
			set
			{
				this._rankingMax = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "rankingMin", DataFormat = DataFormat.TwosComplement)]
		public int rankingMin
		{
			get
			{
				return this._rankingMin;
			}
			set
			{
				this._rankingMin = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "reward", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemId
		{
			get
			{
				return this._itemId;
			}
		}

		[ProtoMember(6, Name = "itemNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemNum
		{
			get
			{
				return this._itemNum;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
