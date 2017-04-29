using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XuanShangJiFenDuan")]
	[Serializable]
	public class XuanShangJiFenDuan : IExtensible
	{
		private string _dropNum;

		private int _low;

		private int _high;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "dropNum", DataFormat = DataFormat.Default)]
		public string dropNum
		{
			get
			{
				return this._dropNum;
			}
			set
			{
				this._dropNum = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "low", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int low
		{
			get
			{
				return this._low;
			}
			set
			{
				this._low = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "high", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int high
		{
			get
			{
				return this._high;
			}
			set
			{
				this._high = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
