using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7713), ForSend(7713), ProtoContract(Name = "WingInfoChangeNty")]
	[Serializable]
	public class WingInfoChangeNty : IExtensible
	{
		public static readonly short OP = 7713;

		private WingInfo _wingInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "wingInfo", DataFormat = DataFormat.Default)]
		public WingInfo wingInfo
		{
			get
			{
				return this._wingInfo;
			}
			set
			{
				this._wingInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
