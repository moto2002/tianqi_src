using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"menuId"
	}), ProtoContract(Name = "Menu")]
	[Serializable]
	public class Menu : IExtensible
	{
		private int _menuId;

		private int _opportunity;

		private int _num;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "menuId", DataFormat = DataFormat.TwosComplement)]
		public int menuId
		{
			get
			{
				return this._menuId;
			}
			set
			{
				this._menuId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "opportunity", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int opportunity
		{
			get
			{
				return this._opportunity;
			}
			set
			{
				this._opportunity = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
