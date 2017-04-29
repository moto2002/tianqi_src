using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "ItemExtendInfo")]
	[Serializable]
	public class ItemExtendInfo : IExtensible
	{
		private int _advancedExp;

		private int _currentExp;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "advancedExp", DataFormat = DataFormat.ZigZag), DefaultValue(0)]
		public int advancedExp
		{
			get
			{
				return this._advancedExp;
			}
			set
			{
				this._advancedExp = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "currentExp", DataFormat = DataFormat.ZigZag), DefaultValue(0)]
		public int currentExp
		{
			get
			{
				return this._currentExp;
			}
			set
			{
				this._currentExp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
