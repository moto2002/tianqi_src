using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1105), ForSend(1105), ProtoContract(Name = "DefendFightMonsterTipsNty")]
	[Serializable]
	public class DefendFightMonsterTipsNty : IExtensible
	{
		public static readonly short OP = 1105;

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
