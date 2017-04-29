using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(6833), ForSend(6833), ProtoContract(Name = "BountyTaskExitBtlRes")]
	[Serializable]
	public class BountyTaskExitBtlRes : IExtensible
	{
		public static readonly short OP = 6833;

		private int _totalScore;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "totalScore", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int totalScore
		{
			get
			{
				return this._totalScore;
			}
			set
			{
				this._totalScore = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
