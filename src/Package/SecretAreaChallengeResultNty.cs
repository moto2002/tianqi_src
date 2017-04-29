using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1217), ForSend(1217), ProtoContract(Name = "SecretAreaChallengeResultNty")]
	[Serializable]
	public class SecretAreaChallengeResultNty : IExtensible
	{
		public static readonly short OP = 1217;

		private int _startStage;

		private int _startBatch;

		private int _endStage;

		private int _endBatch;

		private readonly List<ItemBriefInfo> _copyRewards = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "startStage", DataFormat = DataFormat.TwosComplement)]
		public int startStage
		{
			get
			{
				return this._startStage;
			}
			set
			{
				this._startStage = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "startBatch", DataFormat = DataFormat.TwosComplement)]
		public int startBatch
		{
			get
			{
				return this._startBatch;
			}
			set
			{
				this._startBatch = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "endStage", DataFormat = DataFormat.TwosComplement)]
		public int endStage
		{
			get
			{
				return this._endStage;
			}
			set
			{
				this._endStage = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "endBatch", DataFormat = DataFormat.TwosComplement)]
		public int endBatch
		{
			get
			{
				return this._endBatch;
			}
			set
			{
				this._endBatch = value;
			}
		}

		[ProtoMember(5, Name = "copyRewards", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> copyRewards
		{
			get
			{
				return this._copyRewards;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
