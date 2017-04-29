using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "BaoShiKongPeiZhi")]
	[Serializable]
	public class BaoShiKongPeiZhi : IExtensible
	{
		private int _position;

		private int _slotOpen;

		private int _openingCondition;

		private readonly List<int> _gemType = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "slotOpen", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int slotOpen
		{
			get
			{
				return this._slotOpen;
			}
			set
			{
				this._slotOpen = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "openingCondition", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openingCondition
		{
			get
			{
				return this._openingCondition;
			}
			set
			{
				this._openingCondition = value;
			}
		}

		[ProtoMember(5, Name = "gemType", DataFormat = DataFormat.TwosComplement)]
		public List<int> gemType
		{
			get
			{
				return this._gemType;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
