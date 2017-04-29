using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DbAc")]
	[Serializable]
	public class DbAc : IExtensible
	{
		private int _typeId;

		private readonly List<DbAcItem> _acItemInfo = new List<DbAcItem>();

		private int _curData;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "typeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, Name = "acItemInfo", DataFormat = DataFormat.Default)]
		public List<DbAcItem> acItemInfo
		{
			get
			{
				return this._acItemInfo;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "curData", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int curData
		{
			get
			{
				return this._curData;
			}
			set
			{
				this._curData = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
