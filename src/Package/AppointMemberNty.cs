using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(3662), ForSend(3662), ProtoContract(Name = "AppointMemberNty")]
	[Serializable]
	public class AppointMemberNty : IExtensible
	{
		public static readonly short OP = 3662;

		private long _roleId;

		private string _roleName;

		private readonly List<MemberTitleType.MTT> _titles = new List<MemberTitleType.MTT>();

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

		[ProtoMember(3, Name = "titles", DataFormat = DataFormat.TwosComplement)]
		public List<MemberTitleType.MTT> titles
		{
			get
			{
				return this._titles;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
