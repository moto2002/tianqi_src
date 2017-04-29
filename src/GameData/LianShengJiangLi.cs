using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "LianShengJiangLi")]
	[Serializable]
	public class LianShengJiangLi : IExtensible
	{
		private int _ranking;

		private readonly List<int> _itemId = new List<int>();

		private readonly List<int> _itemNum = new List<int>();

		private readonly List<int> _breakItemId = new List<int>();

		private readonly List<int> _breakItemNum = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "ranking", DataFormat = DataFormat.TwosComplement)]
		public int ranking
		{
			get
			{
				return this._ranking;
			}
			set
			{
				this._ranking = value;
			}
		}

		[ProtoMember(3, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemId
		{
			get
			{
				return this._itemId;
			}
		}

		[ProtoMember(4, Name = "itemNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> itemNum
		{
			get
			{
				return this._itemNum;
			}
		}

		[ProtoMember(5, Name = "breakItemId", DataFormat = DataFormat.TwosComplement)]
		public List<int> breakItemId
		{
			get
			{
				return this._breakItemId;
			}
		}

		[ProtoMember(6, Name = "breakItemNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> breakItemNum
		{
			get
			{
				return this._breakItemNum;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
