using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(759), ForSend(759), ProtoContract(Name = "MapObjectMoveNtyEx")]
	[Serializable]
	public class MapObjectMoveNtyEx : IExtensible
	{
		public static readonly short OP = 759;

		private readonly List<MapMoveInfo> _infos = new List<MapMoveInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infos", DataFormat = DataFormat.Default)]
		public List<MapMoveInfo> infos
		{
			get
			{
				return this._infos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
