using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(649), ForSend(649), ProtoContract(Name = "PveReadyTimeNty")]
	[Serializable]
	public class PveReadyTimeNty : IExtensible
	{
		public static readonly short OP = 649;

		private int _time;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "time", DataFormat = DataFormat.TwosComplement)]
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
