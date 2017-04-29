using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(680), ForSend(680), ProtoContract(Name = "EnterMainCityReq")]
	[Serializable]
	public class EnterMainCityReq : IExtensible
	{
		public static readonly short OP = 680;

		private int _cityId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "cityId", DataFormat = DataFormat.TwosComplement)]
		public int cityId
		{
			get
			{
				return this._cityId;
			}
			set
			{
				this._cityId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
