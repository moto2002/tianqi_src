using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Artifact")]
	[Serializable]
	public class Artifact : IExtensible
	{
		[ProtoContract(Name = "SystemparameterPair")]
		[Serializable]
		public class SystemparameterPair : IExtensible
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

		private int _type;

		private int _name;

		private int _priority;

		private int _frontArtifact;

		private int _explain;

		private int _model;

		private int _activation;

		private int _activationParameter;

		private int _acquisitionMode;

		private int _battle;

		private int _system;

		private readonly List<Artifact.SystemparameterPair> _systemParameter = new List<Artifact.SystemparameterPair>();

		private int _skillName;

		private int _skillExplain;

		private int _access;

		private int _icon;

		private int _areaIndex;

		private int _widgetId;

		private int _lv;

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

		[ProtoMember(3, IsRequired = false, Name = "type", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "priority", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				this._priority = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "frontArtifact", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int frontArtifact
		{
			get
			{
				return this._frontArtifact;
			}
			set
			{
				this._frontArtifact = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "explain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int explain
		{
			get
			{
				return this._explain;
			}
			set
			{
				this._explain = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int model
		{
			get
			{
				return this._model;
			}
			set
			{
				this._model = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "activation", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activation
		{
			get
			{
				return this._activation;
			}
			set
			{
				this._activation = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "activationParameter", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activationParameter
		{
			get
			{
				return this._activationParameter;
			}
			set
			{
				this._activationParameter = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "acquisitionMode", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int acquisitionMode
		{
			get
			{
				return this._acquisitionMode;
			}
			set
			{
				this._acquisitionMode = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "battle", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int battle
		{
			get
			{
				return this._battle;
			}
			set
			{
				this._battle = value;
			}
		}

		[ProtoMember(13, IsRequired = false, Name = "system", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int system
		{
			get
			{
				return this._system;
			}
			set
			{
				this._system = value;
			}
		}

		[ProtoMember(14, Name = "systemParameter", DataFormat = DataFormat.Default)]
		public List<Artifact.SystemparameterPair> systemParameter
		{
			get
			{
				return this._systemParameter;
			}
		}

		[ProtoMember(15, IsRequired = false, Name = "skillName", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillName
		{
			get
			{
				return this._skillName;
			}
			set
			{
				this._skillName = value;
			}
		}

		[ProtoMember(16, IsRequired = false, Name = "skillExplain", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int skillExplain
		{
			get
			{
				return this._skillExplain;
			}
			set
			{
				this._skillExplain = value;
			}
		}

		[ProtoMember(17, IsRequired = false, Name = "access", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int access
		{
			get
			{
				return this._access;
			}
			set
			{
				this._access = value;
			}
		}

		[ProtoMember(18, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(19, IsRequired = false, Name = "areaIndex", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int areaIndex
		{
			get
			{
				return this._areaIndex;
			}
			set
			{
				this._areaIndex = value;
			}
		}

		[ProtoMember(20, IsRequired = false, Name = "widgetId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int widgetId
		{
			get
			{
				return this._widgetId;
			}
			set
			{
				this._widgetId = value;
			}
		}

		[ProtoMember(21, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
