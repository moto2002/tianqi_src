using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(466), ForSend(466), ProtoContract(Name = "WingWearReq")]
	[Serializable]
	public class WingWearReq : IExtensible
	{
		public static readonly short OP = 466;

		private int _wingCfgId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "wingCfgId", DataFormat = DataFormat.TwosComplement)]
		public int wingCfgId
		{
			get
			{
				return this._wingCfgId;
			}
			set
			{
				this._wingCfgId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
