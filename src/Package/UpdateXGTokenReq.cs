using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(660), ForSend(660), ProtoContract(Name = "UpdateXGTokenReq")]
	[Serializable]
	public class UpdateXGTokenReq : IExtensible
	{
		public static readonly short OP = 660;

		private string _xgToken;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "xgToken", DataFormat = DataFormat.Default)]
		public string xgToken
		{
			get
			{
				return this._xgToken;
			}
			set
			{
				this._xgToken = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
