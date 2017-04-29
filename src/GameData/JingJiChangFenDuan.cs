using ProtoBuf;
using System;

namespace GameData
{
	[ProtoContract(Name = "JingJiChangFenDuan")]
	[Serializable]
	public class JingJiChangFenDuan : IExtensible
	{
		private int _id;

		private int _min;

		private int _max;

		private int _name;

		private int _stageReward;

		private int _stage;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "min", DataFormat = DataFormat.TwosComplement)]
		public int min
		{
			get
			{
				return this._min;
			}
			set
			{
				this._min = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "max", DataFormat = DataFormat.TwosComplement)]
		public int max
		{
			get
			{
				return this._max;
			}
			set
			{
				this._max = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "name", DataFormat = DataFormat.TwosComplement)]
		public int name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "stageReward", DataFormat = DataFormat.TwosComplement)]
		public int stageReward
		{
			get
			{
				return this._stageReward;
			}
			set
			{
				this._stageReward = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "stage", DataFormat = DataFormat.TwosComplement)]
		public int stage
		{
			get
			{
				return this._stage;
			}
			set
			{
				this._stage = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
