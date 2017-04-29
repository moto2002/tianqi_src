using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "SecretAreaRankInfo")]
	[Serializable]
	public class SecretAreaRankInfo : IExtensible
	{
		private long _roleId;

		private string _name;

		private int _lv;

		private long _fighting;

		private int _maxClearBatch;

		private int _clearCostTime;

		private int _rank;

		private int _modelId;

		private int _freeze;

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

		[ProtoMember(5, IsRequired = true, Name = "maxClearBatch", DataFormat = DataFormat.TwosComplement)]
		public int maxClearBatch
		{
			get
			{
				return this._maxClearBatch;
			}
			set
			{
				this._maxClearBatch = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "clearCostTime", DataFormat = DataFormat.TwosComplement)]
		public int clearCostTime
		{
			get
			{
				return this._clearCostTime;
			}
			set
			{
				this._clearCostTime = value;
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

		[ProtoMember(9, IsRequired = false, Name = "freeze", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int freeze
		{
			get
			{
				return this._freeze;
			}
			set
			{
				this._freeze = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
