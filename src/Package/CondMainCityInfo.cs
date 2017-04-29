using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "CondMainCityInfo")]
	[Serializable]
	public class CondMainCityInfo : IExtensible
	{
		private Pos _pos;

		private int _mapId;

		private long _roleId;

		private int _condMainCityType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
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

		[ProtoMember(2, IsRequired = true, Name = "mapId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "condMainCityType", DataFormat = DataFormat.TwosComplement)]
		public int condMainCityType
		{
			get
			{
				return this._condMainCityType;
			}
			set
			{
				this._condMainCityType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
