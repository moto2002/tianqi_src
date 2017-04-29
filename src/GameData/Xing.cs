using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Xing")]
	[Serializable]
	public class Xing : IExtensible
	{
		private int _familyName;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "familyName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int familyName
		{
			get
			{
				return this._familyName;
			}
			set
			{
				this._familyName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
