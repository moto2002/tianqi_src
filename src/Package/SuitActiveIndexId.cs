using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "SuitActiveIndexId")]
	[Serializable]
	public class SuitActiveIndexId : IExtensible
	{
		private readonly List<int> _indexId = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "indexId", DataFormat = DataFormat.TwosComplement)]
		public List<int> indexId
		{
			get
			{
				return this._indexId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
