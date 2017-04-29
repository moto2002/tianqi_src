using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "TitleId")]
	[Serializable]
	public class TitleId : IExtensible
	{
		private int _titleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "titleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int titleId
		{
			get
			{
				return this._titleId;
			}
			set
			{
				this._titleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
