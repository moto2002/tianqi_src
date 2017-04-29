using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"profession"
	}), ProtoContract(Name = "RoleCreate")]
	[Serializable]
	public class RoleCreate : IExtensible
	{
		[ProtoContract(Name = "NormalattackPair")]
		[Serializable]
		public class NormalattackPair : IExtensible
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

		[ProtoContract(Name = "Attack1Pair")]
		[Serializable]
		public class Attack1Pair : IExtensible
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

		[ProtoContract(Name = "Attack2Pair")]
		[Serializable]
		public class Attack2Pair : IExtensible
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

		[ProtoContract(Name = "Attack3Pair")]
		[Serializable]
		public class Attack3Pair : IExtensible
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

		[ProtoContract(Name = "Attack4Pair")]
		[Serializable]
		public class Attack4Pair : IExtensible
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

		[ProtoContract(Name = "Skill1Pair")]
		[Serializable]
		public class Skill1Pair : IExtensible
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

		[ProtoContract(Name = "Skill2Pair")]
		[Serializable]
		public class Skill2Pair : IExtensible
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

		[ProtoContract(Name = "Skill3Pair")]
		[Serializable]
		public class Skill3Pair : IExtensible
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

		[ProtoContract(Name = "RestoreskillPair")]
		[Serializable]
		public class RestoreskillPair : IExtensible
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

		[ProtoContract(Name = "Skill4Pair")]
		[Serializable]
		public class Skill4Pair : IExtensible
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

		[ProtoContract(Name = "ExchangePair")]
		[Serializable]
		public class ExchangePair : IExtensible
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

		[ProtoContract(Name = "FusePair")]
		[Serializable]
		public class FusePair : IExtensible
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

		[ProtoContract(Name = "RollPair")]
		[Serializable]
		public class RollPair : IExtensible
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

		[ProtoContract(Name = "Roll2Pair")]
		[Serializable]
		public class Roll2Pair : IExtensible
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

		[ProtoContract(Name = "AidelayPair")]
		[Serializable]
		public class AidelayPair : IExtensible
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

		private int _profession;

		private int _modle;

		private readonly List<string> _point = new List<string>();

		private readonly List<RoleCreate.NormalattackPair> _normalAttack = new List<RoleCreate.NormalattackPair>();

		private readonly List<RoleCreate.Attack1Pair> _attack1 = new List<RoleCreate.Attack1Pair>();

		private readonly List<RoleCreate.Attack2Pair> _attack2 = new List<RoleCreate.Attack2Pair>();

		private readonly List<RoleCreate.Attack3Pair> _attack3 = new List<RoleCreate.Attack3Pair>();

		private readonly List<RoleCreate.Attack4Pair> _attack4 = new List<RoleCreate.Attack4Pair>();

		private readonly List<RoleCreate.Skill1Pair> _skill1 = new List<RoleCreate.Skill1Pair>();

		private readonly List<RoleCreate.Skill2Pair> _skill2 = new List<RoleCreate.Skill2Pair>();

		private readonly List<RoleCreate.Skill3Pair> _skill3 = new List<RoleCreate.Skill3Pair>();

		private readonly List<RoleCreate.RestoreskillPair> _restoreSkill = new List<RoleCreate.RestoreskillPair>();

		private readonly List<RoleCreate.Skill4Pair> _skill4 = new List<RoleCreate.Skill4Pair>();

		private readonly List<RoleCreate.ExchangePair> _exchange = new List<RoleCreate.ExchangePair>();

		private readonly List<RoleCreate.FusePair> _fuse = new List<RoleCreate.FusePair>();

		private readonly List<RoleCreate.RollPair> _roll = new List<RoleCreate.RollPair>();

		private readonly List<RoleCreate.Roll2Pair> _roll2 = new List<RoleCreate.Roll2Pair>();

		private readonly List<int> _pet = new List<int>();

		private readonly List<int> _item = new List<int>();

		private readonly List<int> _num = new List<int>();

		private int _mission;

		private readonly List<int> _equipment = new List<int>();

		private readonly List<RoleCreate.AidelayPair> _aiDelay = new List<RoleCreate.AidelayPair>();

		private int _cameraLock;

		private int _picId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "profession", DataFormat = DataFormat.TwosComplement)]
		public int profession
		{
			get
			{
				return this._profession;
			}
			set
			{
				this._profession = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "modle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int modle
		{
			get
			{
				return this._modle;
			}
			set
			{
				this._modle = value;
			}
		}

		[ProtoMember(4, Name = "point", DataFormat = DataFormat.Default)]
		public List<string> point
		{
			get
			{
				return this._point;
			}
		}

		[ProtoMember(5, Name = "normalAttack", DataFormat = DataFormat.Default)]
		public List<RoleCreate.NormalattackPair> normalAttack
		{
			get
			{
				return this._normalAttack;
			}
		}

		[ProtoMember(6, Name = "attack1", DataFormat = DataFormat.Default)]
		public List<RoleCreate.Attack1Pair> attack1
		{
			get
			{
				return this._attack1;
			}
		}

		[ProtoMember(7, Name = "attack2", DataFormat = DataFormat.Default)]
		public List<RoleCreate.Attack2Pair> attack2
		{
			get
			{
				return this._attack2;
			}
		}

		[ProtoMember(8, Name = "attack3", DataFormat = DataFormat.Default)]
		public List<RoleCreate.Attack3Pair> attack3
		{
			get
			{
				return this._attack3;
			}
		}

		[ProtoMember(9, Name = "attack4", DataFormat = DataFormat.Default)]
		public List<RoleCreate.Attack4Pair> attack4
		{
			get
			{
				return this._attack4;
			}
		}

		[ProtoMember(10, Name = "skill1", DataFormat = DataFormat.Default)]
		public List<RoleCreate.Skill1Pair> skill1
		{
			get
			{
				return this._skill1;
			}
		}

		[ProtoMember(11, Name = "skill2", DataFormat = DataFormat.Default)]
		public List<RoleCreate.Skill2Pair> skill2
		{
			get
			{
				return this._skill2;
			}
		}

		[ProtoMember(12, Name = "skill3", DataFormat = DataFormat.Default)]
		public List<RoleCreate.Skill3Pair> skill3
		{
			get
			{
				return this._skill3;
			}
		}

		[ProtoMember(13, Name = "restoreSkill", DataFormat = DataFormat.Default)]
		public List<RoleCreate.RestoreskillPair> restoreSkill
		{
			get
			{
				return this._restoreSkill;
			}
		}

		[ProtoMember(14, Name = "skill4", DataFormat = DataFormat.Default)]
		public List<RoleCreate.Skill4Pair> skill4
		{
			get
			{
				return this._skill4;
			}
		}

		[ProtoMember(15, Name = "exchange", DataFormat = DataFormat.Default)]
		public List<RoleCreate.ExchangePair> exchange
		{
			get
			{
				return this._exchange;
			}
		}

		[ProtoMember(16, Name = "fuse", DataFormat = DataFormat.Default)]
		public List<RoleCreate.FusePair> fuse
		{
			get
			{
				return this._fuse;
			}
		}

		[ProtoMember(17, Name = "roll", DataFormat = DataFormat.Default)]
		public List<RoleCreate.RollPair> roll
		{
			get
			{
				return this._roll;
			}
		}

		[ProtoMember(18, Name = "roll2", DataFormat = DataFormat.Default)]
		public List<RoleCreate.Roll2Pair> roll2
		{
			get
			{
				return this._roll2;
			}
		}

		[ProtoMember(19, Name = "pet", DataFormat = DataFormat.TwosComplement)]
		public List<int> pet
		{
			get
			{
				return this._pet;
			}
		}

		[ProtoMember(20, Name = "item", DataFormat = DataFormat.TwosComplement)]
		public List<int> item
		{
			get
			{
				return this._item;
			}
		}

		[ProtoMember(21, Name = "num", DataFormat = DataFormat.TwosComplement)]
		public List<int> num
		{
			get
			{
				return this._num;
			}
		}

		[ProtoMember(22, IsRequired = true, Name = "mission", DataFormat = DataFormat.TwosComplement)]
		public int mission
		{
			get
			{
				return this._mission;
			}
			set
			{
				this._mission = value;
			}
		}

		[ProtoMember(23, Name = "equipment", DataFormat = DataFormat.TwosComplement)]
		public List<int> equipment
		{
			get
			{
				return this._equipment;
			}
		}

		[ProtoMember(24, Name = "aiDelay", DataFormat = DataFormat.Default)]
		public List<RoleCreate.AidelayPair> aiDelay
		{
			get
			{
				return this._aiDelay;
			}
		}

		[ProtoMember(25, IsRequired = false, Name = "cameraLock", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int cameraLock
		{
			get
			{
				return this._cameraLock;
			}
			set
			{
				this._cameraLock = value;
			}
		}

		[ProtoMember(26, IsRequired = false, Name = "picId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int picId
		{
			get
			{
				return this._picId;
			}
			set
			{
				this._picId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
