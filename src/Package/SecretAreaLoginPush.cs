using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(911), ForSend(911), ProtoContract(Name = "SecretAreaLoginPush")]
	[Serializable]
	public class SecretAreaLoginPush : IExtensible
	{
		public static readonly short OP = 911;

		private int _challengeTime;

		private int _challengeTimeBuyNum;

		private int _currClearBatch;

		private int _historyMaxClearBatch;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "challengeTime", DataFormat = DataFormat.TwosComplement)]
		public int challengeTime
		{
			get
			{
				return this._challengeTime;
			}
			set
			{
				this._challengeTime = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "challengeTimeBuyNum", DataFormat = DataFormat.TwosComplement)]
		public int challengeTimeBuyNum
		{
			get
			{
				return this._challengeTimeBuyNum;
			}
			set
			{
				this._challengeTimeBuyNum = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "currClearBatch", DataFormat = DataFormat.TwosComplement)]
		public int currClearBatch
		{
			get
			{
				return this._currClearBatch;
			}
			set
			{
				this._currClearBatch = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "historyMaxClearBatch", DataFormat = DataFormat.TwosComplement)]
		public int historyMaxClearBatch
		{
			get
			{
				return this._historyMaxClearBatch;
			}
			set
			{
				this._historyMaxClearBatch = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
