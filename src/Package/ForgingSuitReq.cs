using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4869), ForSend(4869), ProtoContract(Name = "ForgingSuitReq")]
	[Serializable]
	public class ForgingSuitReq : IExtensible
	{
		public static readonly short OP = 4869;

		private long _equipId;

		private int _libPosition;

		private int _suitId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "equipId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "libPosition", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = true, Name = "suitId", DataFormat = DataFormat.TwosComplement)]
		public int suitId
		{
			get
			{
				return this._suitId;
			}
			set
			{
				this._suitId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
