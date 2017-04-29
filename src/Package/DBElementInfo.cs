using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "DBElementInfo")]
	[Serializable]
	public class DBElementInfo : IExtensible
	{
		private int _elemId;

		private int _elemLv;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "elemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int elemId
		{
			get
			{
				return this._elemId;
			}
			set
			{
				this._elemId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "elemLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int elemLv
		{
			get
			{
				return this._elemLv;
			}
			set
			{
				this._elemLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
