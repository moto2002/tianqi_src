using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(7632), ForSend(7632), ProtoContract(Name = "OpenTramcarPanelReq")]
	[Serializable]
	public class OpenTramcarPanelReq : IExtensible
	{
		public static readonly short OP = 7632;

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
