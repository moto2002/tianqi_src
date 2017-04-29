using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GoodsExtra")]
	[Serializable]
	public class GoodsExtra : IExtensible
	{
		private int _vipLvLmt;

		private readonly List<int> _discountIds = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "vipLvLmt", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vipLvLmt
		{
			get
			{
				return this._vipLvLmt;
			}
			set
			{
				this._vipLvLmt = value;
			}
		}

		[ProtoMember(2, Name = "discountIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> discountIds
		{
			get
			{
				return this._discountIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
