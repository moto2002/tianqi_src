using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3918), ForSend(3918), ProtoContract(Name = "EnterGrabReq")]
	[Serializable]
	public class EnterGrabReq : IExtensible
	{
		public static readonly short OP = 3918;

		private int _mapId;

		private long _roleId;

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

		[ProtoMember(2, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
