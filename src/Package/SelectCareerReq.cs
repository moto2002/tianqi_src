using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5339), ForSend(5339), ProtoContract(Name = "SelectCareerReq")]
	[Serializable]
	public class SelectCareerReq : IExtensible
	{
		public static readonly short OP = 5339;

		private CareerType.CT _career;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "career", DataFormat = DataFormat.TwosComplement)]
		public CareerType.CT career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
