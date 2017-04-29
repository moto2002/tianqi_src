using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "NvMing")]
	[Serializable]
	public class NvMing : IExtensible
	{
		private int _girlName;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "girlName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int girlName
		{
			get
			{
				return this._girlName;
			}
			set
			{
				this._girlName = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
