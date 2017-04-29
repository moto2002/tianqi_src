using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Runes_basic")]
	[Serializable]
	public class Runes_basic : IExtensible
	{
		[ProtoContract(Name = "ConditionPair")]
		[Serializable]
		public class ConditionPair : IExtensible
		{
			private int _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.TwosComplement)]
			public int key
			{
				get
				{
					return this._key;
				}
				set
				{
					this._key = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "value", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private int _id;

		private int _nextId;

		private int _runesGroup;

		private int _unlockLv;

		private readonly List<Runes_basic.ConditionPair> _condition = new List<Runes_basic.ConditionPair>();

		private int _icon;

		private int _name;

		private int _skillId;

		private int _artifactId;

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

		[ProtoMember(3, IsRequired = false, Name = "nextId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nextId
		{
			get
			{
				return this._nextId;
			}
			set
			{
				this._nextId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "runesGroup", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int runesGroup
		{
			get
			{
				return this._runesGroup;
			}
			set
			{
				this._runesGroup = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "unlockLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int unlockLv
		{
			get
			{
				return this._unlockLv;
			}
			set
			{
				this._unlockLv = value;
			}
		}

		[ProtoMember(6, Name = "condition", DataFormat = DataFormat.Default)]
		public List<Runes_basic.ConditionPair> condition
		{
			get
			{
				return this._condition;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(9, IsRequired = false, Name = "skillId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "artifactId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int artifactId
		{
			get
			{
				return this._artifactId;
			}
			set
			{
				this._artifactId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
