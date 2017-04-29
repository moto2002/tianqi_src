using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "HolyWeaponInfo")]
	[Serializable]
	public class HolyWeaponInfo : IExtensible
	{
		private int _Id;

		private int _Type;

		private int _State;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "Type", DataFormat = DataFormat.TwosComplement)]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "State", DataFormat = DataFormat.TwosComplement)]
		public int State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
