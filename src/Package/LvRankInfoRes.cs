using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4223), ForSend(4223), ProtoContract(Name = "LvRankInfoRes")]
	[Serializable]
	public class LvRankInfoRes : IExtensible
	{
		public static readonly short OP = 4223;

		private readonly List<LvRankInfo> _items = new List<LvRankInfo>();

		private int _remainTime;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<LvRankInfo> items
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
