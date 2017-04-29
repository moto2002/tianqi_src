using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ItemSetting")]
	[Serializable]
	public class ItemSetting : IExtensible
	{
		private int _sType;

		private bool _bOpen;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "sType", DataFormat = DataFormat.TwosComplement)]
		public int sType
		{
			get
			{
				return this._sType;
			}
			set
			{
				this._sType = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "bOpen", DataFormat = DataFormat.Default)]
		public bool bOpen
		{
			get
			{
				return this._bOpen;
			}
			set
			{
				this._bOpen = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
