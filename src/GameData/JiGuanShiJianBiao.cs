using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JiGuanShiJianBiao")]
	[Serializable]
	public class JiGuanShiJianBiao : IExtensible
	{
		[ProtoContract(Name = "ActivePair")]
		[Serializable]
		public class ActivePair : IExtensible
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

		[ProtoContract(Name = "DeactivePair")]
		[Serializable]
		public class DeactivePair : IExtensible
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

		[ProtoContract(Name = "StateupPair")]
		[Serializable]
		public class StateupPair : IExtensible
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

		[ProtoContract(Name = "StatedownPair")]
		[Serializable]
		public class StatedownPair : IExtensible
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

		private int _ID;

		private readonly List<JiGuanShiJianBiao.ActivePair> _active = new List<JiGuanShiJianBiao.ActivePair>();

		private readonly List<JiGuanShiJianBiao.DeactivePair> _deactive = new List<JiGuanShiJianBiao.DeactivePair>();

		private readonly List<JiGuanShiJianBiao.StateupPair> _stateUp = new List<JiGuanShiJianBiao.StateupPair>();

		private readonly List<JiGuanShiJianBiao.StatedownPair> _stateDown = new List<JiGuanShiJianBiao.StatedownPair>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "ID", DataFormat = DataFormat.TwosComplement)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		[ProtoMember(2, Name = "active", DataFormat = DataFormat.Default)]
		public List<JiGuanShiJianBiao.ActivePair> active
		{
			get
			{
				return this._active;
			}
		}

		[ProtoMember(3, Name = "deactive", DataFormat = DataFormat.Default)]
		public List<JiGuanShiJianBiao.DeactivePair> deactive
		{
			get
			{
				return this._deactive;
			}
		}

		[ProtoMember(4, Name = "stateUp", DataFormat = DataFormat.Default)]
		public List<JiGuanShiJianBiao.StateupPair> stateUp
		{
			get
			{
				return this._stateUp;
			}
		}

		[ProtoMember(5, Name = "stateDown", DataFormat = DataFormat.Default)]
		public List<JiGuanShiJianBiao.StatedownPair> stateDown
		{
			get
			{
				return this._stateDown;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
