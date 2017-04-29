using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1751), ForSend(1751), ProtoContract(Name = "SelectPetToMiningReq")]
	[Serializable]
	public class SelectPetToMiningReq : IExtensible
	{
		public static readonly short OP = 1751;

		private string _mineBlockId;

		private long _petId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "mineBlockId", DataFormat = DataFormat.Default)]
		public string mineBlockId
		{
			get
			{
				return this._mineBlockId;
			}
			set
			{
				this._mineBlockId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "petId", DataFormat = DataFormat.TwosComplement)]
		public long petId
		{
			get
			{
				return this._petId;
			}
			set
			{
				this._petId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
