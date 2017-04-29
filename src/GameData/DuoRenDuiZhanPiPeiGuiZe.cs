using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "DuoRenDuiZhanPiPeiGuiZe")]
	[Serializable]
	public class DuoRenDuiZhanPiPeiGuiZe : IExtensible
	{
		private int _id;

		private readonly List<int> _matchPriority = new List<int>();

		private int _powerRange;

		private int _lvRange;

		private int _randomMatching;

		private int _botLvRange;

		private readonly List<int> _botLv = new List<int>();

		private int _botId;

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

		[ProtoMember(3, Name = "matchPriority", DataFormat = DataFormat.TwosComplement)]
		public List<int> matchPriority
		{
			get
			{
				return this._matchPriority;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "powerRange", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int powerRange
		{
			get
			{
				return this._powerRange;
			}
			set
			{
				this._powerRange = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "lvRange", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lvRange
		{
			get
			{
				return this._lvRange;
			}
			set
			{
				this._lvRange = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "randomMatching", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int randomMatching
		{
			get
			{
				return this._randomMatching;
			}
			set
			{
				this._randomMatching = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "botLvRange", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int botLvRange
		{
			get
			{
				return this._botLvRange;
			}
			set
			{
				this._botLvRange = value;
			}
		}

		[ProtoMember(8, Name = "botLv", DataFormat = DataFormat.TwosComplement)]
		public List<int> botLv
		{
			get
			{
				return this._botLv;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "botId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int botId
		{
			get
			{
				return this._botId;
			}
			set
			{
				this._botId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
