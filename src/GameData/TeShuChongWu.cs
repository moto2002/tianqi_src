using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "TeShuChongWu")]
	[Serializable]
	public class TeShuChongWu : IExtensible
	{
		private int _petId;

		private int _effectId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "petId", DataFormat = DataFormat.TwosComplement)]
		public int petId
		{
			get
			{
				return this._petId;
			}
			set
			{
				this._petId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "effectId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
