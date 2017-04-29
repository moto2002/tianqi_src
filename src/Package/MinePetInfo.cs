using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "MinePetInfo")]
	[Serializable]
	public class MinePetInfo : IExtensible
	{
		private string _blockId;

		private long _petId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "blockId", DataFormat = DataFormat.Default)]
		public string blockId
		{
			get
			{
				return this._blockId;
			}
			set
			{
				this._blockId = value;
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
