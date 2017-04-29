using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(713), ForSend(713), ProtoContract(Name = "MapLoadDoneReport")]
	[Serializable]
	public class MapLoadDoneReport : IExtensible
	{
		public static readonly short OP = 713;

		private int _mapId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
