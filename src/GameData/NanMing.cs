using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "NanMing")]
	[Serializable]
	public class NanMing : IExtensible
	{
		private int _boyName;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "boyName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int boyName
		{
			get
			{
				return this._boyName;
			}
			set
			{
				this._boyName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
