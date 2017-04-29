using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2003), ForSend(2003), ProtoContract(Name = "EndMemoryFlopRes")]
	[Serializable]
	public class EndMemoryFlopRes : IExtensible
	{
		public static readonly short OP = 2003;

		private int _passTime;

		private readonly List<ItemBriefInfo> _rewards = new List<ItemBriefInfo>();

		private readonly List<ItemBriefInfo> _scoreRewards = new List<ItemBriefInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "passTime", DataFormat = DataFormat.TwosComplement)]
		public int passTime
		{
			get
			{
				return this._passTime;
			}
			set
			{
				this._passTime = value;
			}
		}

		[ProtoMember(2, Name = "rewards", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> rewards
		{
			get
			{
				return this._rewards;
			}
		}

		[ProtoMember(3, Name = "scoreRewards", DataFormat = DataFormat.Default)]
		public List<ItemBriefInfo> scoreRewards
		{
			get
			{
				return this._scoreRewards;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
