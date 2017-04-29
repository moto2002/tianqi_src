using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "GemPartInfo")]
	[Serializable]
	public class GemPartInfo : IExtensible
	{
		private EquipLibType.ELT _type;

		private readonly List<GemEmbedInfo> _gemEmbedInfo = new List<GemEmbedInfo>();

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

		[ProtoMember(2, Name = "gemEmbedInfo", DataFormat = DataFormat.Default)]
		public List<GemEmbedInfo> gemEmbedInfo
		{
			get
			{
				return this._gemEmbedInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
