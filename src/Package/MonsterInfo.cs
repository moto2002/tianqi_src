using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "MonsterInfo")]
	[Serializable]
	public class MonsterInfo : IExtensible
	{
		private int _typeid;

		private long _id;

		private string _name;

		private readonly List<int> _skillIds = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeid", DataFormat = DataFormat.TwosComplement)]
		public int typeid
		{
			get
			{
				return this._typeid;
			}
			set
			{
				this._typeid = value;
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

		[ProtoMember(3, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(4, Name = "skillIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> skillIds
		{
			get
			{
				return this._skillIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
