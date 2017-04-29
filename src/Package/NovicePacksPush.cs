using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(656), ForSend(656), ProtoContract(Name = "NovicePacksPush")]
	[Serializable]
	public class NovicePacksPush : IExtensible
	{
		public static readonly short OP = 656;

		private NovicePacksInfo _pack;

		private int _time;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "pack", DataFormat = DataFormat.Default)]
		public NovicePacksInfo pack
		{
			get
			{
				return this._pack;
			}
			set
			{
				this._pack = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
