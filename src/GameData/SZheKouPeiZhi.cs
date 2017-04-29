using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "SZheKouPeiZhi")]
	[Serializable]
	public class SZheKouPeiZhi : IExtensible
	{
		private int _Id;

		private float _discount;

		private int _icon;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "discount", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float discount
		{
			get
			{
				return this._discount;
			}
			set
			{
				this._discount = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
