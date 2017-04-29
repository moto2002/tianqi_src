using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(206), ForSend(206), ProtoContract(Name = "KickMsgNty")]
	[Serializable]
	public class KickMsgNty : IExtensible
	{
		public static readonly short OP = 206;

		private int _sectionId;

		private ulong _teamId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "sectionId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sectionId
		{
			get
			{
				return this._sectionId;
			}
			set
			{
				this._sectionId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0f)]
		public ulong teamId
		{
			get
			{
				return this._teamId;
			}
			set
			{
				this._teamId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
