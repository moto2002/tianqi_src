using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(68), ForSend(68), ProtoContract(Name = "ChallengeGuildBossNty")]
	[Serializable]
	public class ChallengeGuildBossNty : IExtensible
	{
		[ProtoContract(Name = "PrizeItemInfo")]
		[Serializable]
		public class PrizeItemInfo : IExtensible
		{
			private int _itemId;

			private long _itemCount;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "itemId", DataFormat = DataFormat.TwosComplement)]
			public int itemId
			{
				get
				{
					return this._itemId;
				}
				set
				{
					this._itemId = value;
				}
			}

			[ProtoMember(2, IsRequired = true, Name = "itemCount", DataFormat = DataFormat.TwosComplement)]
			public long itemCount
			{
				get
				{
					return this._itemCount;
				}
				set
				{
					this._itemCount = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 68;

		private bool _isEnd;

		private long _hurtValue;

		private readonly List<ChallengeGuildBossNty.PrizeItemInfo> _prizeItems = new List<ChallengeGuildBossNty.PrizeItemInfo>();

		private long _fatal2BossRoleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "isEnd", DataFormat = DataFormat.Default)]
		public bool isEnd
		{
			get
			{
				return this._isEnd;
			}
			set
			{
				this._isEnd = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "hurtValue", DataFormat = DataFormat.TwosComplement)]
		public long hurtValue
		{
			get
			{
				return this._hurtValue;
			}
			set
			{
				this._hurtValue = value;
			}
		}

		[ProtoMember(3, Name = "prizeItems", DataFormat = DataFormat.Default)]
		public List<ChallengeGuildBossNty.PrizeItemInfo> prizeItems
		{
			get
			{
				return this._prizeItems;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "fatal2BossRoleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long fatal2BossRoleId
		{
			get
			{
				return this._fatal2BossRoleId;
			}
			set
			{
				this._fatal2BossRoleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
