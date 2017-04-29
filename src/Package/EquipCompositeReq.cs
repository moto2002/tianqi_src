using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(771), ForSend(771), ProtoContract(Name = "EquipCompositeReq")]
	[Serializable]
	public class EquipCompositeReq : IExtensible
	{
		public static readonly short OP = 771;

		private int _toEquipId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "toEquipId", DataFormat = DataFormat.TwosComplement)]
		public int toEquipId
		{
			get
			{
				return this._toEquipId;
			}
			set
			{
				this._toEquipId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
