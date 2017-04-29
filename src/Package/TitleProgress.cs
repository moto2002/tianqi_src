using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "TitleProgress")]
	[Serializable]
	public class TitleProgress : IExtensible
	{
		private int _titleId;

		private int _curProgress = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "titleId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "curProgress", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int curProgress
		{
			get
			{
				return this._curProgress;
			}
			set
			{
				this._curProgress = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
