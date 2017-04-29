using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(526), ForSend(526), ProtoContract(Name = "EnterRoomReq")]
	[Serializable]
	public class EnterRoomReq : IExtensible
	{
		public static readonly short OP = 526;

		private int _area;

		private int _roomId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "area", DataFormat = DataFormat.TwosComplement)]
		public int area
		{
			get
			{
				return this._area;
			}
			set
			{
				this._area = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "roomId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int roomId
		{
			get
			{
				return this._roomId;
			}
			set
			{
				this._roomId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
