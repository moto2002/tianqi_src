using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "BossDropLog")]
	[Serializable]
	public class BossDropLog : IExtensible
	{
		private int _dateTimeSec;

		private string _roleName;

		private readonly List<ItemBriefInfo> _items = new List<ItemBriefInfo>();

		private int _labelId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dateTimeSec", DataFormat = DataFormat.TwosComplement)]
		public int dateTimeSec
		{
			get
			{
				return this._dateTimeSec;
			}
			set
			{
				this._dateTimeSec = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "roleName", DataFormat = DataFormat.Default)]
		public string roleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				this._roleName = value;
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

		[ProtoMember(4, IsRequired = false, Name = "labelId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int labelId
		{
			get
			{
				return this._labelId;
			}
			set
			{
				this._labelId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
