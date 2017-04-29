using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(2004), ForSend(2004), ProtoContract(Name = "MemoryFlopInfoNty")]
	[Serializable]
	public class MemoryFlopInfoNty : IExtensible
	{
		public static readonly short OP = 2004;

		private int _todayRestTimes;

		private int _todayExtendTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "todayRestTimes", DataFormat = DataFormat.TwosComplement)]
		public int todayRestTimes
		{
			get
			{
				return this._todayRestTimes;
			}
			set
			{
				this._todayRestTimes = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "todayExtendTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int todayExtendTimes
		{
			get
			{
				return this._todayExtendTimes;
			}
			set
			{
				this._todayExtendTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
