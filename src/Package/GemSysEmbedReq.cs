using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(2131), ForSend(2131), ProtoContract(Name = "GemSysEmbedReq")]
	[Serializable]
	public class GemSysEmbedReq : IExtensible
	{
		public static readonly short OP = 2131;

		private EquipLibType.ELT _type;

		private long _gemId;

		private int _hole;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public EquipLibType.ELT type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "gemId", DataFormat = DataFormat.TwosComplement)]
		public long gemId
		{
			get
			{
				return this._gemId;
			}
			set
			{
				this._gemId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "hole", DataFormat = DataFormat.TwosComplement)]
		public int hole
		{
			get
			{
				return this._hole;
			}
			set
			{
				this._hole = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
