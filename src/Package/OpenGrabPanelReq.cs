using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(5273), ForSend(5273), ProtoContract(Name = "OpenGrabPanelReq")]
	[Serializable]
	public class OpenGrabPanelReq : IExtensible
	{
		public static readonly short OP = 5273;

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
