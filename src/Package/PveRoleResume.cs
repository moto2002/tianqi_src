using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "PveRoleResume")]
	[Serializable]
	public class PveRoleResume : IExtensible
	{
		private long _roleId;

		private string _name;

		private int _level;

		private long _fighting;

		private CareerType.CT _career;

		private int _vipLv;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public long roleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "name", DataFormat = DataFormat.Default)]
		public string name
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

		[ProtoMember(3, IsRequired = false, Name = "level", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		[ProtoMember(4, IsRequired = false, Name = "fighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long fighting
		{
			get
			{
				return this._fighting;
			}
			set
			{
				this._fighting = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "career", DataFormat = DataFormat.TwosComplement)]
		public CareerType.CT career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "vipLv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int vipLv
		{
			get
			{
				return this._vipLv;
			}
			set
			{
				this._vipLv = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
