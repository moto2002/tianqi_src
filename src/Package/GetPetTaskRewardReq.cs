using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(211), ForSend(211), ProtoContract(Name = "GetPetTaskRewardReq")]
	[Serializable]
	public class GetPetTaskRewardReq : IExtensible
	{
		public static readonly short OP = 211;

		private long _idx;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "idx", DataFormat = DataFormat.TwosComplement)]
		public long idx
		{
			get
			{
				return this._idx;
			}
			set
			{
				this._idx = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
