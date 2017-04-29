using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "LuanRuGuaiWuKu")]
	[Serializable]
	public class LuanRuGuaiWuKu : IExtensible
	{
		private int _monsterLibrary;

		private int _monster;

		private int _probability;

		private int _ruleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "monsterLibrary", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterLibrary
		{
			get
			{
				return this._monsterLibrary;
			}
			set
			{
				this._monsterLibrary = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "monster", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monster
		{
			get
			{
				return this._monster;
			}
			set
			{
				this._monster = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "probability", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				this._probability = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "ruleId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ruleId
		{
			get
			{
				return this._ruleId;
			}
			set
			{
				this._ruleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
