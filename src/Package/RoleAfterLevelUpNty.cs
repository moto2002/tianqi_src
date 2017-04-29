using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(820), ForSend(820), ProtoContract(Name = "RoleAfterLevelUpNty")]
	[Serializable]
	public class RoleAfterLevelUpNty : IExtensible
	{
		public static readonly short OP = 820;

		private int _oldLv;

		private int _newLv;

		private int _oldFighting;

		private int _newFighting;

		private int _oldEnergy;

		private int _newEnergy;

		private int _oldAttack;

		private int _newAttack;

		private int _oldDefence;

		private int _newDefence;

		private int _oldHp;

		private int _newHp;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "oldLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int oldLv
		{
			get
			{
				return this._oldLv;
			}
			set
			{
				this._oldLv = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "newLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int newLv
		{
			get
			{
				return this._newLv;
			}
			set
			{
				this._newLv = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "oldFighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int oldFighting
		{
			get
			{
				return this._oldFighting;
			}
			set
			{
				this._oldFighting = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "newFighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int newFighting
		{
			get
			{
				return this._newFighting;
			}
			set
			{
				this._newFighting = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "oldEnergy", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int oldEnergy
		{
			get
			{
				return this._oldEnergy;
			}
			set
			{
				this._oldEnergy = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "newEnergy", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int newEnergy
		{
			get
			{
				return this._newEnergy;
			}
			set
			{
				this._newEnergy = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "oldAttack", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int oldAttack
		{
			get
			{
				return this._oldAttack;
			}
			set
			{
				this._oldAttack = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "newAttack", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int newAttack
		{
			get
			{
				return this._newAttack;
			}
			set
			{
				this._newAttack = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "oldDefence", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int oldDefence
		{
			get
			{
				return this._oldDefence;
			}
			set
			{
				this._oldDefence = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "newDefence", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int newDefence
		{
			get
			{
				return this._newDefence;
			}
			set
			{
				this._newDefence = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "oldHp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int oldHp
		{
			get
			{
				return this._oldHp;
			}
			set
			{
				this._oldHp = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "newHp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int newHp
		{
			get
			{
				return this._newHp;
			}
			set
			{
				this._newHp = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
