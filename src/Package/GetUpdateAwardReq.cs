using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(5521), ForSend(5521), ProtoContract(Name = "GetUpdateAwardReq")]
	[Serializable]
	public class GetUpdateAwardReq : IExtensible
	{
		public static readonly short OP = 5521;

		private int _acId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "acId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int acId
		{
			get
			{
				return this._acId;
			}
			set
			{
				this._acId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
