using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(882), ForSend(882), ProtoContract(Name = "SecretAreaRankListReq")]
	[Serializable]
	public class SecretAreaRankListReq : IExtensible
	{
		public static readonly short OP = 882;

		private int _page = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "page", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int page
		{
			get
			{
				return this._page;
			}
			set
			{
				this._page = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
