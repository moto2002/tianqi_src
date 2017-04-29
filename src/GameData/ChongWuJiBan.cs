using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChongWuJiBan")]
	[Serializable]
	public class ChongWuJiBan : IExtensible
	{
		private int _linkedId;

		private readonly List<int> _linkedPetId = new List<int>();

		private int _linkedAttrId;

		private int _desc;

		private int _name;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "linkedId", DataFormat = DataFormat.TwosComplement)]
		public int linkedId
		{
			get
			{
				return this._linkedId;
			}
			set
			{
				this._linkedId = value;
			}
		}

		[ProtoMember(3, Name = "linkedPetId", DataFormat = DataFormat.TwosComplement)]
		public List<int> linkedPetId
		{
			get
			{
				return this._linkedPetId;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "linkedAttrId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int linkedAttrId
		{
			get
			{
				return this._linkedAttrId;
			}
			set
			{
				this._linkedAttrId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "desc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int desc
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
