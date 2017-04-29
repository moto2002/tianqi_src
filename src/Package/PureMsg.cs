using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "PureMsg")]
	[Serializable]
	public class PureMsg : IExtensible
	{
		private string _msg;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "msg", DataFormat = DataFormat.Default)]
		public string msg
		{
			get
			{
				return this._msg;
			}
			set
			{
				this._msg = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
