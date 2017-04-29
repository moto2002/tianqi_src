using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(702), ForSend(702), ProtoContract(Name = "MapObjEnterMapNty")]
	[Serializable]
	public class MapObjEnterMapNty : IExtensible
	{
		public static readonly short OP = 702;

		private int _mapId;

		private readonly List<MapObjInfo> _mapObjs = new List<MapObjInfo>();

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

		[ProtoMember(2, Name = "mapObjs", DataFormat = DataFormat.Default)]
		public List<MapObjInfo> mapObjs
		{
			get
			{
				return this._mapObjs;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
