using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GongHuiShangPinKu")]
	[Serializable]
	public class GongHuiShangPinKu : IExtensible
	{
		private int _commodityPool;

		private int _commodityId;

		private int _weight;

		private int _startLv;

		private int _endLv;

		private int _job;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "commodityPool", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int commodityPool
		{
			get
			{
				return this._commodityPool;
			}
			set
			{
				this._commodityPool = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "commodityId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int commodityId
		{
			get
			{
				return this._commodityId;
			}
			set
			{
				this._commodityId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "weight", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "startLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int startLv
		{
			get
			{
				return this._startLv;
			}
			set
			{
				this._startLv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "endLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int endLv
		{
			get
			{
				return this._endLv;
			}
			set
			{
				this._endLv = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "job", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int job
		{
			get
			{
				return this._job;
			}
			set
			{
				this._job = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
