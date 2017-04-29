using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ShouDongShuaXinFeiYong")]
	[Serializable]
	public class ShouDongShuaXinFeiYong : IExtensible
	{
		private int _refreshPriceId;

		private int _refreshTime;

		private int _num;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "refreshPriceId", DataFormat = DataFormat.TwosComplement)]
		public int refreshPriceId
		{
			get
			{
				return this._refreshPriceId;
			}
			set
			{
				this._refreshPriceId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "refreshTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int refreshTime
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

		[ProtoMember(4, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
