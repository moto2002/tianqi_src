using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3807), ForSend(3807), ProtoContract(Name = "NewMainCityNty")]
	[Serializable]
	public class NewMainCityNty : IExtensible
	{
		public static readonly short OP = 3807;

		private int _id;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
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
