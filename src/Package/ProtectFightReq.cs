using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(8821), ForSend(8821), ProtoContract(Name = "ProtectFightReq")]
	[Serializable]
	public class ProtectFightReq : IExtensible
	{
		public static readonly short OP = 8821;

		private int _mapId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "mapId", DataFormat = DataFormat.TwosComplement)]
		public int mapId
		{
			get
			{
				return this._mapId;
			}
			set
			{
				this._mapId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
