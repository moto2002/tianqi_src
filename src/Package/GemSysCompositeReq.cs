using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(231), ForSend(231), ProtoContract(Name = "GemSysCompositeReq")]
	[Serializable]
	public class GemSysCompositeReq : IExtensible
	{
		public static readonly short OP = 231;

		private int _typeId;

		private int _method = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "method", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int method
		{
			get
			{
				return this._method;
			}
			set
			{
				this._method = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
