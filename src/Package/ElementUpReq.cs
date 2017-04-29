using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(514), ForSend(514), ProtoContract(Name = "ElementUpReq")]
	[Serializable]
	public class ElementUpReq : IExtensible
	{
		public static readonly short OP = 514;

		private int _elemId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
