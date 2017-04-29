using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(623), ForSend(623), ProtoContract(Name = "ChallengeWildBossReq")]
	[Serializable]
	public class ChallengeWildBossReq : IExtensible
	{
		public static readonly short OP = 623;

		private int _idx;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "idx", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
