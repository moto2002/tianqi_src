using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(61), ForSend(61), ProtoContract(Name = "CondMainCityEnterReq")]
	[Serializable]
	public class CondMainCityEnterReq : IExtensible
	{
		public static readonly short OP = 61;

		private int _condCityType;

		private int _mapId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "condCityType", DataFormat = DataFormat.TwosComplement)]
		public int condCityType
		{
			get
			{
				return this._condCityType;
			}
			set
			{
				this._condCityType = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "mapId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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
