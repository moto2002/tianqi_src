using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"id"
	}), ProtoContract(Name = "Chongzhi")]
	[Serializable]
	public class Chongzhi : IExtensible
	{
		private int _id;

		private int _name;

		private int _chance;

		private int _rule;

		private int _give;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "chance", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int chance
		{
			get
			{
				return this._chance;
			}
			set
			{
				this._chance = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "rule", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rule
		{
			get
			{
				return this._rule;
			}
			set
			{
				this._rule = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "give", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int give
		{
			get
			{
				return this._give;
			}
			set
			{
				this._give = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
