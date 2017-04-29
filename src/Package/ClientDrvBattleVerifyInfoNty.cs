using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(1229), ForSend(1229), ProtoContract(Name = "ClientDrvBattleVerifyInfoNty")]
	[Serializable]
	public class ClientDrvBattleVerifyInfoNty : IExtensible
	{
		public static readonly short OP = 1229;

		private readonly List<int> _loseRandIdxSeq = new List<int>();

		private int _curRandCount;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "loseRandIdxSeq", DataFormat = DataFormat.TwosComplement)]
		public List<int> loseRandIdxSeq
		{
			get
			{
				return this._loseRandIdxSeq;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "curRandCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int curRandCount
		{
			get
			{
				return this._curRandCount;
			}
			set
			{
				this._curRandCount = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
