using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(785), ForSend(785), ProtoContract(Name = "IgnoreApplicantReq")]
	[Serializable]
	public class IgnoreApplicantReq : IExtensible
	{
		public static readonly short OP = 785;

		private long _id;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
