using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3702), ForSend(3702), ProtoContract(Name = "PetUpStarReq")]
	[Serializable]
	public class PetUpStarReq : IExtensible
	{
		public static readonly short OP = 3702;

		private long _petUUId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
