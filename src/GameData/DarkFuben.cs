using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"FubenId"
	}), ProtoContract(Name = "DarkFuben")]
	[Serializable]
	public class DarkFuben : IExtensible
	{
		private int _FubenId;

		private int _mapID;

		private readonly List<int> _coordinate = new List<int>();

		private readonly List<int> _rewardID = new List<int>();

		private readonly List<int> _batchID = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "FubenId", DataFormat = DataFormat.TwosComplement)]
		public int FubenId
		{
			get
			{
				return this._FubenId;
			}
			set
			{
				this._FubenId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "mapID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mapID
		{
			get
			{
				return this._mapID;
			}
			set
			{
				this._mapID = value;
			}
		}

		[ProtoMember(4, Name = "coordinate", DataFormat = DataFormat.TwosComplement)]
		public List<int> coordinate
		{
			get
			{
				return this._coordinate;
			}
		}

		[ProtoMember(5, Name = "rewardID", DataFormat = DataFormat.TwosComplement)]
		public List<int> rewardID
		{
			get
			{
				return this._rewardID;
			}
		}

		[ProtoMember(6, Name = "batchID", DataFormat = DataFormat.TwosComplement)]
		public List<int> batchID
		{
			get
			{
				return this._batchID;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
