using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "MineInfo")]
	[Serializable]
	public class MineInfo : IExtensible
	{
		private string _blockId;

		private int _lastModifyTime;

		private long _petId;

		private int _mineType;

		private int _petStar;

		private readonly List<DebrisInfo> _debrisInfos = new List<DebrisInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "blockId", DataFormat = DataFormat.Default)]
		public string blockId
		{
			get
			{
				return this._blockId;
			}
			set
			{
				this._blockId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "lastModifyTime", DataFormat = DataFormat.TwosComplement)]
		public int lastModifyTime
		{
			get
			{
				return this._lastModifyTime;
			}
			set
			{
				this._lastModifyTime = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "petId", DataFormat = DataFormat.TwosComplement)]
		public long petId
		{
			get
			{
				return this._petId;
			}
			set
			{
				this._petId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "mineType", DataFormat = DataFormat.TwosComplement)]
		public int mineType
		{
			get
			{
				return this._mineType;
			}
			set
			{
				this._mineType = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "petStar", DataFormat = DataFormat.TwosComplement)]
		public int petStar
		{
			get
			{
				return this._petStar;
			}
			set
			{
				this._petStar = value;
			}
		}

		[ProtoMember(6, Name = "debrisInfos", DataFormat = DataFormat.Default)]
		public List<DebrisInfo> debrisInfos
		{
			get
			{
				return this._debrisInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
