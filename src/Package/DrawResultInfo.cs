using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DrawResultInfo")]
	[Serializable]
	public class DrawResultInfo : IExtensible
	{
		private int _drawTime;

		private int _itemId;

		private int _itemCount = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "drawTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int drawTime
		{
			get
			{
				return this._drawTime;
			}
			set
			{
				this._drawTime = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "itemCount", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int itemCount
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
}
