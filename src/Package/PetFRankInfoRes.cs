using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(4149), ForSend(4149), ProtoContract(Name = "PetFRankInfoRes")]
	[Serializable]
	public class PetFRankInfoRes : IExtensible
	{
		public static readonly short OP = 4149;

		private readonly List<PetFRankInfo> _items = new List<PetFRankInfo>();

		private int _remainTime;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "items", DataFormat = DataFormat.Default)]
		public List<PetFRankInfo> items
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
