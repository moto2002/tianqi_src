using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "Talent")]
	[Serializable]
	public class Talent : IExtensible
	{
		[ProtoContract(Name = "ActivationPair")]
		[Serializable]
		public class ActivationPair : IExtensible
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

		[ProtoContract(Name = "PretalentPair")]
		[Serializable]
		public class PretalentPair : IExtensible
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

		private int _job;

		private int _minRoleLv;

		private int _templateId;

		private readonly List<Talent.ActivationPair> _activation = new List<Talent.ActivationPair>();

		private int _lv;

		private int _nextLv;

		private readonly List<Talent.PretalentPair> _preTalent = new List<Talent.PretalentPair>();

		private int _name;

		private int _desc;

		private int _icon;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "job", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int job
		{
			get
			{
				return this._job;
			}
			set
			{
				this._job = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "minRoleLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minRoleLv
		{
			get
			{
				return this._minRoleLv;
			}
			set
			{
				this._minRoleLv = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "templateId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int templateId
		{
			get
			{
				return this._templateId;
			}
			set
			{
				this._templateId = value;
			}
		}

		[ProtoMember(5, Name = "activation", DataFormat = DataFormat.Default)]
		public List<Talent.ActivationPair> activation
		{
			get
			{
				return this._activation;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(7, IsRequired = false, Name = "nextLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nextLv
		{
			get
			{
				return this._nextLv;
			}
			set
			{
				this._nextLv = value;
			}
		}

		[ProtoMember(8, Name = "preTalent", DataFormat = DataFormat.Default)]
		public List<Talent.PretalentPair> preTalent
		{
			get
			{
				return this._preTalent;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(10, IsRequired = false, Name = "desc", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(11, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
