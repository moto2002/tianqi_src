using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(209), ForSend(209), ProtoContract(Name = "AutoMatchNty")]
	[Serializable]
	public class AutoMatchNty : IExtensible
	{
		public static readonly short OP = 209;

		private int _retCode;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "retCode", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int retCode
		{
			get
			{
				return this._retCode;
			}
			set
			{
				this._retCode = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
