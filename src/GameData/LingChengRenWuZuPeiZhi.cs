using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "LingChengRenWuZuPeiZhi")]
	[Serializable]
	public class LingChengRenWuZuPeiZhi : IExtensible
	{
		[ProtoContract(Name = "RewardPair")]
		[Serializable]
		public class RewardPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _groupId;

		private int _color;

		private int _weight;

		private readonly List<int> _taskLibrary = new List<int>();

		private readonly List<int> _rewardId = new List<int>();

		private readonly List<LingChengRenWuZuPeiZhi.RewardPair> _reward = new List<LingChengRenWuZuPeiZhi.RewardPair>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "groupId", DataFormat = DataFormat.TwosComplement)]
		public int groupId
		{
			get
			{
				return this._groupId;
			}
			set
			{
				this._groupId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "color", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "weight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				this._weight = value;
			}
		}

		[ProtoMember(5, Name = "taskLibrary", DataFormat = DataFormat.TwosComplement)]
		public List<int> taskLibrary
		{
			get
			{
				return this._taskLibrary;
			}
		}

		[ProtoMember(6, Name = "rewardId", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardId
		{
			get
			{
				return this._rewardId;
			}
		}

		[ProtoMember(7, Name = "reward", DataFormat = DataFormat.Default)]
		public List<LingChengRenWuZuPeiZhi.RewardPair> reward
		{
			get
			{
				return this._reward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
