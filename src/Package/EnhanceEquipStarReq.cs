using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(425), ForSend(425), ProtoContract(Name = "EnhanceEquipStarReq")]
	[Serializable]
	public class EnhanceEquipStarReq : IExtensible
	{
		public static readonly short OP = 425;

		private int _libPosition;

		private long _equipId;

		private int _starMaterialId;

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

		[ProtoMember(3, IsRequired = true, Name = "starMaterialId", DataFormat = DataFormat.TwosComplement)]
		public int starMaterialId
		{
			get
			{
				return this._starMaterialId;
			}
			set
			{
				this._starMaterialId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
