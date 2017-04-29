using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Sort")]
	[Serializable]
	public class Sort : IExtensible
	{
		private int _id;

		private readonly List<string> _order = new List<string>();

		private readonly List<int> _type = new List<int>();

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

		[ProtoMember(3, Name = "order", DataFormat = DataFormat.Default)]
		public List<string> order
		{
			get
			{
				return this._order;
			}
		}

		[ProtoMember(4, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public List<int> type
		{
			get
			{
				return this._type;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
