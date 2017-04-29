using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(561), ForSend(561), ProtoContract(Name = "PresResetPetLvRes")]
	[Serializable]
	public class PresResetPetLvRes : IExtensible
	{
		public static readonly short OP = 561;

		private long _petUUId;

		private int _needDiamond;

		private readonly List<ItemBriefInfo> _items = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "petUUId", DataFormat = DataFormat.TwosComplement)]
		public long petUUId
		{
			get
			{
				return this._petUUId;
			}
			set
			{
				this._petUUId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "needDiamond", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int needDiamond
		{
			get
			{
				return this._needDiamond;
			}
			set
			{
				this._needDiamond = value;
			}
		}

		[ProtoMember(3, Name = "items", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> items
		{
			get
			{
				return this._items;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
