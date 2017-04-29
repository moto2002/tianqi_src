using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3803), ForSend(3803), ProtoContract(Name = "MapTransportReq")]
	[Serializable]
	public class MapTransportReq : IExtensible
	{
		public static readonly short OP = 3803;

		private int _transportId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "transportId", DataFormat = DataFormat.TwosComplement)]
		public int transportId
		{
			get
			{
				return this._transportId;
			}
			set
			{
				this._transportId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
