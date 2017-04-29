using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(533), ForSend(533), ProtoContract(Name = "MapObjInfoNty")]
	[Serializable]
	public class MapObjInfoNty : IExtensible
	{
		public static readonly short OP = 533;

		private readonly List<MapObjInfo> _mapObjs = new List<MapObjInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "mapObjs", DataFormat = DataFormat.Default)]
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
