using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3699), ForSend(3699), ProtoContract(Name = "PresPetUpStarReq")]
	[Serializable]
	public class PresPetUpStarReq : IExtensible
	{
		public static readonly short OP = 3699;

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
