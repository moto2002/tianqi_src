using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "SCPersonalRankingInfo")]
	[Serializable]
	public class SCPersonalRankingInfo : IExtensible
	{
		private long _roleId;

		private string _name;

		private int _lv;

		private long _fighting;

		private int _difficulty;

		private int _btlCostTime;

		private int _rank;

		private int _modelId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
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

		[ProtoMember(3, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "fighting", DataFormat = DataFormat.TwosComplement)]
		public long fighting
		{
			get
			{
				return this._fighting;
			}
			set
			{
				this._fighting = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "difficulty", DataFormat = DataFormat.TwosComplement)]
		public int difficulty
		{
			get
			{
				return this._difficulty;
			}
			set
			{
				this._difficulty = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "btlCostTime", DataFormat = DataFormat.TwosComplement)]
		public int btlCostTime
		{
			get
			{
				return this._btlCostTime;
			}
			set
			{
				this._btlCostTime = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "rank", DataFormat = DataFormat.TwosComplement)]
		public int rank
		{
			get
			{
				return this._rank;
			}
			set
			{
				this._rank = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "modelId", DataFormat = DataFormat.TwosComplement)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
