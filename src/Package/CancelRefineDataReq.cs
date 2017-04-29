using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1888), ForSend(1888), ProtoContract(Name = "CancelRefineDataReq")]
	[Serializable]
	public class CancelRefineDataReq : IExtensible
	{
		public static readonly short OP = 1888;

		private int _libPosition;

		private long _equipId;

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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
