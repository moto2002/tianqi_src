using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(866), ForSend(866), ProtoContract(Name = "FlyShoeTransportReq")]
	[Serializable]
	public class FlyShoeTransportReq : IExtensible
	{
		public static readonly short OP = 866;

		private int _mapId;

		private Pos _pos;

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

		[ProtoMember(2, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
		public Pos pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
