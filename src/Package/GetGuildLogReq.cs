using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3349), ForSend(3349), ProtoContract(Name = "GetGuildLogReq")]
	[Serializable]
	public class GetGuildLogReq : IExtensible
	{
		public static readonly short OP = 3349;

		private int _nPage;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "nPage", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nPage
		{
			get
			{
				return this._nPage;
			}
			set
			{
				this._nPage = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
