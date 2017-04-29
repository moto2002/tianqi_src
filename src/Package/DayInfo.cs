using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "DayInfo")]
	[Serializable]
	public class DayInfo : IExtensible
	{
		public static readonly short OP = 279;

		private int _startDay;

		private readonly List<RawInfo> _rawInfo = new List<RawInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "startDay", DataFormat = DataFormat.TwosComplement)]
		public int startDay
		{
			get
			{
				return this._startDay;
			}
			set
			{
				this._startDay = value;
			}
		}

		[ProtoMember(2, Name = "rawInfo", DataFormat = DataFormat.Default)]
		public List<RawInfo> rawInfo
		{
			get
			{
				return this._rawInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
