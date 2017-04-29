using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(222), ForSend(222), ProtoContract(Name = "BattleCollectItemAddNty")]
	[Serializable]
	public class BattleCollectItemAddNty : IExtensible
	{
		[ProtoContract(Name = "CollectionInfo")]
		[Serializable]
		public class CollectionInfo : IExtensible
		{
			private int _idx;

			private DropItem _dropItems;

			private Pos _pos;

			private IExtension extensionObject;

			[ProtoMember(2, IsRequired = true, Name = "idx", DataFormat = DataFormat.TwosComplement)]
			public int idx
			{
				get
				{
					return this._idx;
				}
				set
				{
					this._idx = value;
				}
			}

			[ProtoMember(1, IsRequired = true, Name = "dropItems", DataFormat = DataFormat.Default)]
			public DropItem dropItems
			{
				get
				{
					return this._dropItems;
				}
				set
				{
					this._dropItems = value;
				}
			}

			[ProtoMember(3, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
			public Pos pos
			{
				get
				{
					return this._pos;
				}
				set
				{
					this._pos = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 222;

		private readonly List<BattleCollectItemAddNty.CollectionInfo> _infoList = new List<BattleCollectItemAddNty.CollectionInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infoList", DataFormat = DataFormat.Default)]
		public List<BattleCollectItemAddNty.CollectionInfo> infoList
		{
			get
			{
				return this._infoList;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
