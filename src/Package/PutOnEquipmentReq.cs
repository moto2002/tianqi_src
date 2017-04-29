using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(767), ForSend(767), ProtoContract(Name = "PutOnEquipmentReq")]
	[Serializable]
	public class PutOnEquipmentReq : IExtensible
	{
		public static readonly short OP = 767;

		private int _position;

		private long _equipId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public int position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "equipId", DataFormat = DataFormat.TwosComplement)]
		public long equipId
		{
			get
			{
				return this._equipId;
			}
			set
			{
				this._equipId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
