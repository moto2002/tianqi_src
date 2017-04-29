using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "RoleBriefInfo")]
	[Serializable]
	public class RoleBriefInfo : IExtensible
	{
		private long _roleId;

		private string _roleName;

		private int _lv;

		private long _exp;

		private long _expLmt;

		private int _typeId;

		private int _modelId;

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

		[ProtoMember(2, IsRequired = true, Name = "roleName", DataFormat = DataFormat.Default)]
		public string roleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				this._roleName = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "lv", DataFormat = DataFormat.TwosComplement)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "exp", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(5, IsRequired = true, Name = "expLmt", DataFormat = DataFormat.TwosComplement)]
		public long expLmt
		{
			get
			{
				return this._expLmt;
			}
			set
			{
				this._expLmt = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "modelId", DataFormat = DataFormat.TwosComplement)]
		public int modelId
		{
			get
			{
				return this._modelId;
			}
			set
			{
				this._modelId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
