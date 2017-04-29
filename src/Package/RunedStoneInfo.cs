using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "RunedStoneInfo")]
	[Serializable]
	public class RunedStoneInfo : IExtensible
	{
		private int _skillId;

		private readonly List<RunedStone> _runedStones = new List<RunedStone>();

		private int _embedGroupId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillId", DataFormat = DataFormat.TwosComplement)]
		public int skillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				this._skillId = value;
			}
		}

		[ProtoMember(2, Name = "runedStones", DataFormat = DataFormat.Default)]
		public List<RunedStone> runedStones
		{
			get
			{
				return this._runedStones;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "embedGroupId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int embedGroupId
		{
			get
			{
				return this._embedGroupId;
			}
			set
			{
				this._embedGroupId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
