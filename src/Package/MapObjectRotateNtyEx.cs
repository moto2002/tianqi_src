using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(857), ForSend(857), ProtoContract(Name = "MapObjectRotateNtyEx")]
	[Serializable]
	public class MapObjectRotateNtyEx : IExtensible
	{
		public static readonly short OP = 857;

		private readonly List<MapRotateInfo> _infos = new List<MapRotateInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infos", DataFormat = DataFormat.Default)]
		public List<MapRotateInfo> infos
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
