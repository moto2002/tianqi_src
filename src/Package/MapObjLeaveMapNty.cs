using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(685), ForSend(685), ProtoContract(Name = "MapObjLeaveMapNty")]
	[Serializable]
	public class MapObjLeaveMapNty : IExtensible
	{
		public static readonly short OP = 685;

		private int _mapId;

		private readonly List<long> _objIds = new List<long>();

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

		[ProtoMember(2, Name = "objIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> objIds
		{
			get
			{
				return this._objIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
