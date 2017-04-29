using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(8776), ForSend(8776), ProtoContract(Name = "RoleChangeCareerNty")]
	[Serializable]
	public class RoleChangeCareerNty : IExtensible
	{
		public static readonly short OP = 8776;

		private long _oldFighting;

		private long _newFighting;

		private int _oldAttack;

		private int _newAttack;

		private int _oldDefence;

		private int _newDefence;

		private long _oldHp;

		private long _newHp;

		private int _oldParryRatio;

		private int _newParryRatio;

		private int _oldCritRatio;

		private int _newCritRatio;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "oldFighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long oldFighting
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

		[ProtoMember(2, IsRequired = false, Name = "newFighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long newFighting
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

		[ProtoMember(3, IsRequired = false, Name = "oldAttack", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "newAttack", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(5, IsRequired = false, Name = "oldDefence", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(6, IsRequired = false, Name = "newDefence", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(7, IsRequired = false, Name = "oldHp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long oldHp
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

		[ProtoMember(8, IsRequired = false, Name = "newHp", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long newHp
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

		[ProtoMember(9, IsRequired = false, Name = "oldParryRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int oldParryRatio
		{
			get
			{
				return this._oldParryRatio;
			}
			set
			{
				this._oldParryRatio = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "newParryRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int newParryRatio
		{
			get
			{
				return this._newParryRatio;
			}
			set
			{
				this._newParryRatio = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "oldCritRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int oldCritRatio
		{
			get
			{
				return this._oldCritRatio;
			}
			set
			{
				this._oldCritRatio = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "newCritRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int newCritRatio
		{
			get
			{
				return this._newCritRatio;
			}
			set
			{
				this._newCritRatio = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
