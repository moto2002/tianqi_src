using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1036), ForSend(1036), ProtoContract(Name = "EnchantEquipResultAckReq")]
	[Serializable]
	public class EnchantEquipResultAckReq : IExtensible
	{
		public static readonly short OP = 1036;

		private int _libPosition;

		private long _equipId;

		private ExcellentAttr _excellentAttr;

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

		[ProtoMember(3, IsRequired = true, Name = "excellentAttr", DataFormat = DataFormat.Default)]
		public ExcellentAttr excellentAttr
		{
			get
			{
				return this._excellentAttr;
			}
			set
			{
				this._excellentAttr = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
