using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "GoodsCountLimit")]
	[Serializable]
	public class GoodsCountLimit : IExtensible
	{
		private int _id;

		private long _count;

		private int _period;

		private long _refreshTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "count", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "period", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int period
		{
			get
			{
				return this._period;
			}
			set
			{
				this._period = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "refreshTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long refreshTime
		{
			get
			{
				return this._refreshTime;
			}
			set
			{
				this._refreshTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
