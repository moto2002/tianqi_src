using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(757), ForSend(757), ProtoContract(Name = "DefendFightInitNty")]
	[Serializable]
	public class DefendFightInitNty : IExtensible
	{
		public static readonly short OP = 757;

		private readonly List<DefendFightModeInfo> _modeInfo = new List<DefendFightModeInfo>();

		private int _bossRandomRate;

		private int _usePropsTimes;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "modeInfo", DataFormat = DataFormat.Default)]
		public List<DefendFightModeInfo> modeInfo
		{
			get
			{
				return this._modeInfo;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "bossRandomRate", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int bossRandomRate
		{
			get
			{
				return this._bossRandomRate;
			}
			set
			{
				this._bossRandomRate = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "usePropsTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int usePropsTimes
		{
			get
			{
				return this._usePropsTimes;
			}
			set
			{
				this._usePropsTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
