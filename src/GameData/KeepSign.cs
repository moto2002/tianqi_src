using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"keeptime"
	}), ProtoContract(Name = "KeepSign")]
	[Serializable]
	public class KeepSign : IExtensible
	{
		private int _keeptime;

		private int _rewardId_v0;

		private int _rewardId_v1;

		private int _rewardId_v2;

		private int _rewardId_v3;

		private int _rewardId_v4;

		private int _rewardId_v5;

		private int _rewardId_v6;

		private int _rewardId_v7;

		private int _rewardId_v8;

		private int _rewardId_v9;

		private int _rewardId_v10;

		private int _rewardId_v11;

		private int _rewardId_v12;

		private int _rewardId_v13;

		private int _rewardId_v14;

		private int _rewardId_v15;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "keeptime", DataFormat = DataFormat.TwosComplement)]
		public int keeptime
		{
			get
			{
				return this._keeptime;
			}
			set
			{
				this._keeptime = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "rewardId_v0", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v0
		{
			get
			{
				return this._rewardId_v0;
			}
			set
			{
				this._rewardId_v0 = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "rewardId_v1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v1
		{
			get
			{
				return this._rewardId_v1;
			}
			set
			{
				this._rewardId_v1 = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "rewardId_v2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v2
		{
			get
			{
				return this._rewardId_v2;
			}
			set
			{
				this._rewardId_v2 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "rewardId_v3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v3
		{
			get
			{
				return this._rewardId_v3;
			}
			set
			{
				this._rewardId_v3 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "rewardId_v4", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v4
		{
			get
			{
				return this._rewardId_v4;
			}
			set
			{
				this._rewardId_v4 = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "rewardId_v5", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v5
		{
			get
			{
				return this._rewardId_v5;
			}
			set
			{
				this._rewardId_v5 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "rewardId_v6", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v6
		{
			get
			{
				return this._rewardId_v6;
			}
			set
			{
				this._rewardId_v6 = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "rewardId_v7", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v7
		{
			get
			{
				return this._rewardId_v7;
			}
			set
			{
				this._rewardId_v7 = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "rewardId_v8", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v8
		{
			get
			{
				return this._rewardId_v8;
			}
			set
			{
				this._rewardId_v8 = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "rewardId_v9", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v9
		{
			get
			{
				return this._rewardId_v9;
			}
			set
			{
				this._rewardId_v9 = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "rewardId_v10", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v10
		{
			get
			{
				return this._rewardId_v10;
			}
			set
			{
				this._rewardId_v10 = value;
			}
		}

		[ProtoMember(14, IsRequired = false, Name = "rewardId_v11", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v11
		{
			get
			{
				return this._rewardId_v11;
			}
			set
			{
				this._rewardId_v11 = value;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "rewardId_v12", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v12
		{
			get
			{
				return this._rewardId_v12;
			}
			set
			{
				this._rewardId_v12 = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "rewardId_v13", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v13
		{
			get
			{
				return this._rewardId_v13;
			}
			set
			{
				this._rewardId_v13 = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "rewardId_v14", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v14
		{
			get
			{
				return this._rewardId_v14;
			}
			set
			{
				this._rewardId_v14 = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "rewardId_v15", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId_v15
		{
			get
			{
				return this._rewardId_v15;
			}
			set
			{
				this._rewardId_v15 = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
