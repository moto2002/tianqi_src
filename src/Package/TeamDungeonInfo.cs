using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "TeamDungeonInfo")]
	[Serializable]
	public class TeamDungeonInfo : IExtensible
	{
		private DungeonType.ENUM _dungeonType;

		private readonly List<int> _dungeonParams = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dungeonType", DataFormat = DataFormat.TwosComplement)]
		public DungeonType.ENUM dungeonType
		{
			get
			{
				return this._dungeonType;
			}
			set
			{
				this._dungeonType = value;
			}
		}

		[ProtoMember(2, Name = "dungeonParams", DataFormat = DataFormat.TwosComplement)]
		public List<int> dungeonParams
		{
			get
			{
				return this._dungeonParams;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
