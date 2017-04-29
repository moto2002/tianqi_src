using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(553), ForSend(553), ProtoContract(Name = "OpenVipBoxRes")]
	[Serializable]
	public class OpenVipBoxRes : IExtensible
	{
		public static readonly short OP = 553;

		private int _effectId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "effectId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int effectId
		{
			get
			{
				return this._effectId;
			}
			set
			{
				this._effectId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
