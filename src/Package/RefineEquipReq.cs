using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(597), ForSend(597), ProtoContract(Name = "RefineEquipReq")]
	[Serializable]
	public class RefineEquipReq : IExtensible
	{
		public static readonly short OP = 597;

		private int _libPosition;

		private long _equipId;

		private int _position;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "libPosition", DataFormat = DataFormat.TwosComplement)]
		public int libPosition
		{
			get
			{
				return this._libPosition;
			}
			set
			{
				this._libPosition = value;
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

		[ProtoMember(3, IsRequired = true, Name = "position", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
