using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1457), ForSend(1457), ProtoContract(Name = "UpGodWeaponLvReq")]
	[Serializable]
	public class UpGodWeaponLvReq : IExtensible
	{
		public static readonly short OP = 1457;

		private int _Type;

		private int _material;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "Type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(2, IsRequired = false, Name = "material", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int material
		{
			get
			{
				return this._material;
			}
			set
			{
				this._material = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
