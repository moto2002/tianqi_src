using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1068), ForSend(1068), ProtoContract(Name = "DefendFightCarReachTipsNty")]
	[Serializable]
	public class DefendFightCarReachTipsNty : IExtensible
	{
		public static readonly short OP = 1068;

		private int _wave;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "wave", DataFormat = DataFormat.TwosComplement)]
		public int wave
		{
			get
			{
				return this._wave;
			}
			set
			{
				this._wave = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
