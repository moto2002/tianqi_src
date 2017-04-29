using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TuiJianHaoYou")]
	[Serializable]
	public class TuiJianHaoYou : IExtensible
	{
		private readonly List<int> _recommendLv = new List<int>();

		private int _recommendNumber;

		private int _displayNumber;

		private IExtension extensionObject;

		[ProtoMember(2, Name = "recommendLv", DataFormat = DataFormat.TwosComplement)]
		public List<int> recommendLv
		{
			get
			{
				return this._recommendLv;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "recommendNumber", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int recommendNumber
		{
			get
			{
				return this._recommendNumber;
			}
			set
			{
				this._recommendNumber = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "displayNumber", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int displayNumber
		{
			get
			{
				return this._displayNumber;
			}
			set
			{
				this._displayNumber = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
