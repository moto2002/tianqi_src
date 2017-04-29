using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "WingInfos")]
	[Serializable]
	public class WingInfos : IExtensible
	{
		private WingSeries.WS _wingSeries;

		private readonly List<WingInfo> _wingInfos = new List<WingInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "wingSeries", DataFormat = DataFormat.TwosComplement)]
		public WingSeries.WS wingSeries
		{
			get
			{
				return this._wingSeries;
			}
			set
			{
				this._wingSeries = value;
			}
		}

		[ProtoMember(2, Name = "wingInfos", DataFormat = DataFormat.Default)]
		public List<WingInfo> wingInfos
		{
			get
			{
				return this._wingInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
