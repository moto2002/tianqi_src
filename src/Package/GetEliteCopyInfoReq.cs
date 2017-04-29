using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2232), ForSend(2232), ProtoContract(Name = "GetEliteCopyInfoReq")]
	[Serializable]
	public class GetEliteCopyInfoReq : IExtensible
	{
		public static readonly short OP = 2232;

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
