using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(907), ForSend(907), ProtoContract(Name = "DefendFightModeInfoNty")]
	[Serializable]
	public class DefendFightModeInfoNty : IExtensible
	{
		public static readonly short OP = 907;

		private DefendFightModeInfo _modeInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "modeInfo", DataFormat = DataFormat.Default)]
		public DefendFightModeInfo modeInfo
		{
			get
			{
				return this._modeInfo;
			}
			set
			{
				this._modeInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
