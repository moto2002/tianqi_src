using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "DBScDungeonInfo")]
	[Serializable]
	public class DBScDungeonInfo : IExtensible
	{
		private int _difficulty;

		private int _dungeonId;

		private int _sceneId;

		private int _bossId;

		private readonly List<int> _monsterIds = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "difficulty", DataFormat = DataFormat.TwosComplement)]
		public int difficulty
		{
			get
			{
				return this._difficulty;
			}
			set
			{
				this._difficulty = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "dungeonId", DataFormat = DataFormat.TwosComplement)]
		public int dungeonId
		{
			get
			{
				return this._dungeonId;
			}
			set
			{
				this._dungeonId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "sceneId", DataFormat = DataFormat.TwosComplement)]
		public int sceneId
		{
			get
			{
				return this._sceneId;
			}
			set
			{
				this._sceneId = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "bossId", DataFormat = DataFormat.TwosComplement)]
		public int bossId
		{
			get
			{
				return this._bossId;
			}
			set
			{
				this._bossId = value;
			}
		}

		[ProtoMember(5, Name = "monsterIds", DataFormat = DataFormat.TwosComplement)]
		public List<int> monsterIds
		{
			get
			{
				return this._monsterIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
