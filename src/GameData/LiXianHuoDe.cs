using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "LiXianHuoDe")]
	[Serializable]
	public class LiXianHuoDe : IExtensible
	{
		private int _level;

		private long _exp;

		private readonly List<int> _reward = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "level", DataFormat = DataFormat.TwosComplement)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "exp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long exp
		{
			get
			{
				return this._exp;
			}
			set
			{
				this._exp = value;
			}
		}

		[ProtoMember(4, Name = "reward", DataFormat = DataFormat.TwosComplement)]
		public List<int> reward
		{
			get
			{
				return this._reward;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
