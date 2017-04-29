using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "ArenaMirrorPet")]
	[Serializable]
	public class ArenaMirrorPet : IExtensible
	{
		private long _petId;

		private int _lv;

		private int _typeId;

		private int _star;

		private readonly List<KeyAndValue> _skills = new List<KeyAndValue>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "petId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = true, Name = "star", DataFormat = DataFormat.TwosComplement)]
		public int star
		{
			get
			{
				return this._star;
			}
			set
			{
				this._star = value;
			}
		}

		[ProtoMember(5, Name = "skills", DataFormat = DataFormat.Default)]
		public List<KeyAndValue> skills
		{
			get
			{
				return this._skills;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
