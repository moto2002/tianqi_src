using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "RoleAi")]
	[Serializable]
	public class RoleAi : IExtensible
	{
		[ProtoContract(Name = "AiPair")]
		[Serializable]
		public class AiPair : IExtensible
		{
			private string _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.Default)]
			public string key
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

		[ProtoContract(Name = "OtherplayeraiPair")]
		[Serializable]
		public class OtherplayeraiPair : IExtensible
		{
			private string _key;

			private int _value;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = true, Name = "key", DataFormat = DataFormat.Default)]
			public string key
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

		private uint _id;

		private int _profession;

		private int _fusePet;

		private readonly List<RoleAi.AiPair> _ai = new List<RoleAi.AiPair>();

		private readonly List<RoleAi.OtherplayeraiPair> _otherPlayerAi = new List<RoleAi.OtherplayeraiPair>();

		private int _hitTips;

		private int _dieTips;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public uint id
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

		[ProtoMember(3, IsRequired = false, Name = "profession", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "fusePet", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int fusePet
		{
			get
			{
				return this._fusePet;
			}
			set
			{
				this._fusePet = value;
			}
		}

		[ProtoMember(5, Name = "ai", DataFormat = DataFormat.Default)]
		public List<RoleAi.AiPair> ai
		{
			get
			{
				return this._ai;
			}
		}

		[ProtoMember(6, Name = "otherPlayerAi", DataFormat = DataFormat.Default)]
		public List<RoleAi.OtherplayeraiPair> otherPlayerAi
		{
			get
			{
				return this._otherPlayerAi;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "hitTips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int hitTips
		{
			get
			{
				return this._hitTips;
			}
			set
			{
				this._hitTips = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "dieTips", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int dieTips
		{
			get
			{
				return this._dieTips;
			}
			set
			{
				this._dieTips = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
