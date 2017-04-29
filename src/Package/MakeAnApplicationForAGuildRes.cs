using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1180), ForSend(1180), ProtoContract(Name = "MakeAnApplicationForAGuildRes")]
	[Serializable]
	public class MakeAnApplicationForAGuildRes : IExtensible
	{
		public static readonly short OP = 1180;

		private long _guildId;

		private int _coolDown;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "guildId", DataFormat = DataFormat.TwosComplement)]
		public long guildId
		{
			get
			{
				return this._guildId;
			}
			set
			{
				this._guildId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "coolDown", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int coolDown
		{
			get
			{
				return this._coolDown;
			}
			set
			{
				this._coolDown = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
