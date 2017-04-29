using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(429), ForSend(429), ProtoContract(Name = "EnchantEquipReq")]
	[Serializable]
	public class EnchantEquipReq : IExtensible
	{
		public static readonly short OP = 429;

		private int _libPosition;

		private long _equipId;

		private int _position;

		private int _enchantId;

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

		[ProtoMember(4, IsRequired = true, Name = "enchantId", DataFormat = DataFormat.TwosComplement)]
		public int enchantId
		{
			get
			{
				return this._enchantId;
			}
			set
			{
				this._enchantId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
