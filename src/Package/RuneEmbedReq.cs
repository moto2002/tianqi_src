using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3709), ForSend(3709), ProtoContract(Name = "RuneEmbedReq")]
	[Serializable]
	public class RuneEmbedReq : IExtensible
	{
		public static readonly short OP = 3709;

		private long _petUUId;

		private int _runePos;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "petUUId", DataFormat = DataFormat.TwosComplement)]
		public long petUUId
		{
			get
			{
				return this._petUUId;
			}
			set
			{
				this._petUUId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "runePos", DataFormat = DataFormat.TwosComplement)]
		public int runePos
		{
			get
			{
				return this._runePos;
			}
			set
			{
				this._runePos = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
