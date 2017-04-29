using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6265), ForSend(6265), ProtoContract(Name = "UpdateAwardReq")]
	[Serializable]
	public class UpdateAwardReq : IExtensible
	{
		public static readonly short OP = 6265;

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
