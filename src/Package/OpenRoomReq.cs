using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2771), ForSend(2771), ProtoContract(Name = "OpenRoomReq")]
	[Serializable]
	public class OpenRoomReq : IExtensible
	{
		public static readonly short OP = 2771;

		private int _mapId;

		private int _areaId;

		private int _roomId;

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

		[ProtoMember(2, IsRequired = true, Name = "areaId", DataFormat = DataFormat.TwosComplement)]
		public int areaId
		{
			get
			{
				return this._areaId;
			}
			set
			{
				this._areaId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "roomId", DataFormat = DataFormat.TwosComplement)]
		public int roomId
		{
			get
			{
				return this._roomId;
			}
			set
			{
				this._roomId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
