using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GouMaiXiangGuan")]
	[Serializable]
	public class GouMaiXiangGuan : IExtensible
	{
		private int _type;

		private int _nameId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "nameId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nameId
		{
			get
			{
				return this._nameId;
			}
			set
			{
				this._nameId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
