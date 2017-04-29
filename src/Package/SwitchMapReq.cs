using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(515), ForSend(515), ProtoContract(Name = "SwitchMapReq")]
	[Serializable]
	public class SwitchMapReq : IExtensible
	{
		public static readonly short OP = 515;

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
