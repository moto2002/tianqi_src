using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "RoomInfo")]
	[Serializable]
	public class RoomInfo : IExtensible
	{
		private int _roomId;

		private readonly List<PlayerInfo> _playerInfo = new List<PlayerInfo>();

		private int _areaId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roomId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, Name = "playerInfo", DataFormat = DataFormat.Default)]
		public List<PlayerInfo> playerInfo
		{
			get
			{
				return this._playerInfo;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "areaId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
