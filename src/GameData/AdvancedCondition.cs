using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "AdvancedCondition")]
	[Serializable]
	public class AdvancedCondition : IExtensible
	{
		private int _id;

		private readonly List<int> _level = new List<int>();

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

		[ProtoMember(3, Name = "level", DataFormat = DataFormat.TwosComplement)]
		public List<int> level
		{
			get
			{
				return this._level;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
