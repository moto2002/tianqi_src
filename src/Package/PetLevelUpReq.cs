using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(550), ForSend(550), ProtoContract(Name = "PetLevelUpReq")]
	[Serializable]
	public class PetLevelUpReq : IExtensible
	{
		public static readonly short OP = 550;

		private long _petUUId;

		private int _itemId;

		private int _levelTimes;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "petUUId", DataFormat = DataFormat.TwosComplement)]
		public long petUUId
		{
			get
			{
				return this._petUUId;
			}
			set
			{
				this._petUUId = value;
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

		[ProtoMember(3, IsRequired = false, Name = "levelTimes", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int levelTimes
		{
			get
			{
				return this._levelTimes;
			}
			set
			{
				this._levelTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
