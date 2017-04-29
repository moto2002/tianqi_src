using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(127), ForSend(127), ProtoContract(Name = "GuildWarSceneRoleInfoNty")]
	[Serializable]
	public class GuildWarSceneRoleInfoNty : IExtensible
	{
		public static readonly short OP = 127;

		private long _hp = -1L;

		private int _reliveCD = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "hp", DataFormat = DataFormat.TwosComplement), DefaultValue(-1L)]
		public long hp
		{
			get
			{
				return this._hp;
			}
			set
			{
				this._hp = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "reliveCD", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int reliveCD
		{
			get
			{
				return this._reliveCD;
			}
			set
			{
				this._reliveCD = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
