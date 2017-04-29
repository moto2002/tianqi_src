using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ScDungeonInfo")]
	[Serializable]
	public class ScDungeonInfo : IExtensible
	{
		private int _difficulty;

		private int _dungeonId;

		private int _bossId;

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

		[ProtoMember(3, IsRequired = true, Name = "bossId", DataFormat = DataFormat.TwosComplement)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
