using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2882), ForSend(2882), ProtoContract(Name = "EnterMapUiReq")]
	[Serializable]
	public class EnterMapUiReq : IExtensible
	{
		public static readonly short OP = 2882;

		private int _mapId;

		private int _areaId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
