using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GuaiWuKu")]
	[Serializable]
	public class GuaiWuKu : IExtensible
	{
		private int _monsterLibrary;

		private int _monsterType;

		private int _monster;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "monsterLibrary", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(3, IsRequired = false, Name = "monsterType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterType
		{
			get
			{
				return this._monsterType;
			}
			set
			{
				this._monsterType = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "monster", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
