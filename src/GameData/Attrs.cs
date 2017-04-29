using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "Attrs")]
	[Serializable]
	public class Attrs : IExtensible
	{
		private int _id;

		private readonly List<int> _attrs = new List<int>();

		private readonly List<int> _values = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
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

		[ProtoMember(4, Name = "attrs", DataFormat = DataFormat.TwosComplement)]
		public List<int> attrs
		{
			get
			{
				return this._attrs;
			}
		}

		[ProtoMember(6, Name = "values", DataFormat = DataFormat.TwosComplement)]
		public List<int> values
		{
			get
			{
				return this._values;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
