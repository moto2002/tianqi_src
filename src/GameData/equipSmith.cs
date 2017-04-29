using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "equipSmith")]
	[Serializable]
	public class equipSmith : IExtensible
	{
		private int _id;

		private int _minLevel;

		private int _maxLevel;

		private int _maxEquipStep;

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

		[ProtoMember(3, IsRequired = false, Name = "minLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int minLevel
		{
			get
			{
				return this._minLevel;
			}
			set
			{
				this._minLevel = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "maxLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxLevel
		{
			get
			{
				return this._maxLevel;
			}
			set
			{
				this._maxLevel = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "maxEquipStep", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int maxEquipStep
		{
			get
			{
				return this._maxEquipStep;
			}
			set
			{
				this._maxEquipStep = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
