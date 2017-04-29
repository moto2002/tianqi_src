using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3731), ForSend(3731), ProtoContract(Name = "ObtainPetNty")]
	[Serializable]
	public class ObtainPetNty : IExtensible
	{
		public static readonly short OP = 3731;

		private PetInfo _petInfo;

		private int _itemId;

		private int _getStar;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "petInfo", DataFormat = DataFormat.Default)]
		public PetInfo petInfo
		{
			get
			{
				return this._petInfo;
			}
			set
			{
				this._petInfo = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "itemId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int itemId
		{
			get
			{
				return this._itemId;
			}
			set
			{
				this._itemId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "getStar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int getStar
		{
			get
			{
				return this._getStar;
			}
			set
			{
				this._getStar = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
