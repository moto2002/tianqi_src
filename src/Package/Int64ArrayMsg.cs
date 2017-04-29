using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "Int64ArrayMsg")]
	[Serializable]
	public class Int64ArrayMsg : IExtensible
	{
		private readonly List<Int64IndexValue> _Int64Array = new List<Int64IndexValue>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "Int64Array", DataFormat = DataFormat.Default)]
		public List<Int64IndexValue> Int64Array
		{
			get
			{
				return this._Int64Array;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
