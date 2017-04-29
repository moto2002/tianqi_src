using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "MonsterLibrary")]
	[Serializable]
	public class MonsterLibrary : IExtensible
	{
		private int _id;

		private int _librariesId;

		private int _unlockLv;

		private int _monsterID;

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

		[ProtoMember(3, IsRequired = false, Name = "librariesId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int librariesId
		{
			get
			{
				return this._librariesId;
			}
			set
			{
				this._librariesId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "unlockLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "monsterID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int monsterID
		{
			get
			{
				return this._monsterID;
			}
			set
			{
				this._monsterID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
