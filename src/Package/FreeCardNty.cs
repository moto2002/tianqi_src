using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(132), ForSend(132), ProtoContract(Name = "FreeCardNty")]
	[Serializable]
	public class FreeCardNty : IExtensible
	{
		public static readonly short OP = 132;

		private int _times;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "times", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
