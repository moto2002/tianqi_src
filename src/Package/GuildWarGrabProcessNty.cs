using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(225), ForSend(225), ProtoContract(Name = "GuildWarGrabProcessNty")]
	[Serializable]
	public class GuildWarGrabProcessNty : IExtensible
	{
		public static readonly short OP = 225;

		private long _grabGuildId;

		private int _grabProcess;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "grabGuildId", DataFormat = DataFormat.TwosComplement)]
		public long grabGuildId
		{
			get
			{
				return this._grabGuildId;
			}
			set
			{
				this._grabGuildId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "grabProcess", DataFormat = DataFormat.TwosComplement)]
		public int grabProcess
		{
			get
			{
				return this._grabProcess;
			}
			set
			{
				this._grabProcess = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
