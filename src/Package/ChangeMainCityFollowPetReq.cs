using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1096), ForSend(1096), ProtoContract(Name = "ChangeMainCityFollowPetReq")]
	[Serializable]
	public class ChangeMainCityFollowPetReq : IExtensible
	{
		public static readonly short OP = 1096;

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
