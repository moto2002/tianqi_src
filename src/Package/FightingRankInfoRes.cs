using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4279), ForSend(4279), ProtoContract(Name = "FightingRankInfoRes")]
	[Serializable]
	public class FightingRankInfoRes : IExtensible
	{
		public static readonly short OP = 4279;

		private readonly List<FightingRankInfo> _items = new List<FightingRankInfo>();

		private int _remainTime;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<FightingRankInfo> items
		{
			get
			{
				return this._items;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "remainTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int remainTime
		{
			get
			{
				return this._remainTime;
			}
			set
			{
				this._remainTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
