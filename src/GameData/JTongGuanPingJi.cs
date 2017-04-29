using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JTongGuanPingJi")]
	[Serializable]
	public class JTongGuanPingJi : IExtensible
	{
		private int _rank;

		private int _time;

		private float _ratio;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "rank", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "ratio", DataFormat = DataFormat.FixedSize), DefaultValue(0f)]
		public float ratio
		{
			get
			{
				return this._ratio;
			}
			set
			{
				this._ratio = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
