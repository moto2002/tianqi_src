using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3806), ForSend(3806), ProtoContract(Name = "EnterMainCityRes")]
	[Serializable]
	public class EnterMainCityRes : IExtensible
	{
		public static readonly short OP = 3806;

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
