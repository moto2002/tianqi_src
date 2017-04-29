using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(7211), ForSend(7211), ProtoContract(Name = "TimeLimitedSalesReq")]
	[Serializable]
	public class TimeLimitedSalesReq : IExtensible
	{
		public static readonly short OP = 7211;

		private int _nPage = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "nPage", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
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
