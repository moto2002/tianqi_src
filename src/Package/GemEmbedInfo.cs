using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "GemEmbedInfo")]
	[Serializable]
	public class GemEmbedInfo : IExtensible
	{
		private int _typeId;

		private long _id;

		private int _hole;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.ZigZag)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "hole", DataFormat = DataFormat.ZigZag)]
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
