using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(542), ForSend(542), ProtoContract(Name = "CreateRoleReq")]
	[Serializable]
	public class CreateRoleReq : IExtensible
	{
		public static readonly short OP = 542;

		private string _roleName;

		private int _typeId;

		private int _sdkType;

		private readonly List<ClickRoleTime> _clickTimes = new List<ClickRoleTime>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "roleName", DataFormat = DataFormat.Default)]
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

		[ProtoMember(2, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "sdkType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int sdkType
		{
			get
			{
				return this._sdkType;
			}
			set
			{
				this._sdkType = value;
			}
		}

		[ProtoMember(4, Name = "clickTimes", DataFormat = DataFormat.Default)]
		public List<ClickRoleTime> clickTimes
		{
			get
			{
				return this._clickTimes;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
