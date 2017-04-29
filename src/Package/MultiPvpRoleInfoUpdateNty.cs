using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(166), ForSend(166), ProtoContract(Name = "MultiPvpRoleInfoUpdateNty")]
	[Serializable]
	public class MultiPvpRoleInfoUpdateNty : IExtensible
	{
		public static readonly short OP = 166;

		private int _killCount;

		private int _deathCount;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "killCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int killCount
		{
			get
			{
				return this._killCount;
			}
			set
			{
				this._killCount = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "deathCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int deathCount
		{
			get
			{
				return this._deathCount;
			}
			set
			{
				this._deathCount = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
