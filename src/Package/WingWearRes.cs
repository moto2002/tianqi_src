using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(468), ForSend(468), ProtoContract(Name = "WingWearRes")]
	[Serializable]
	public class WingWearRes : IExtensible
	{
		public static readonly short OP = 468;

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
