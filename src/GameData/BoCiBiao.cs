using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "BoCiBiao")]
	[Serializable]
	public class BoCiBiao : IExtensible
	{
		private int _refreshId;

		private int _minMass;

		private int _cameraType;

		private int _pathPoint;

		private int _pointArea;

		private readonly List<int> _batchTrigger = new List<int>();

		private readonly List<int> _monsterRefreshId = new List<int>();

		private int _monsterFloor;

		private readonly List<float> _pointB = new List<float>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "refreshId", DataFormat = DataFormat.TwosComplement)]
		public int refreshId
		{
			get
			{
				return this._refreshId;
			}
			set
			{
				this._refreshId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "minMass", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minMass
		{
			get
			{
				return this._minMass;
			}
			set
			{
				this._minMass = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "cameraType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cameraType
		{
			get
			{
				return this._cameraType;
			}
			set
			{
				this._cameraType = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "pathPoint", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pathPoint
		{
			get
			{
				return this._pathPoint;
			}
			set
			{
				this._pathPoint = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "pointArea", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int pointArea
		{
			get
			{
				return this._pointArea;
			}
			set
			{
				this._pointArea = value;
			}
		}

		[ProtoMember(9, Name = "batchTrigger", DataFormat = DataFormat.TwosComplement)]
		public List<int> batchTrigger
		{
			get
			{
				return this._batchTrigger;
			}
		}

		[ProtoMember(10, Name = "monsterRefreshId", DataFormat = DataFormat.TwosComplement)]
		public List<int> monsterRefreshId
		{
			get
			{
				return this._monsterRefreshId;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "monsterFloor", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterFloor
		{
			get
			{
				return this._monsterFloor;
			}
			set
			{
				this._monsterFloor = value;
			}
		}

		[ProtoMember(12, Name = "pointB", DataFormat = DataFormat.FixedSize)]
		public List<float> pointB
		{
			get
			{
				return this._pointB;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
