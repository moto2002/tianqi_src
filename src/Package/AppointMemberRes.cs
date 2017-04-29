using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(3663), ForSend(3663), ProtoContract(Name = "AppointMemberRes")]
	[Serializable]
	public class AppointMemberRes : IExtensible
	{
		public static readonly short OP = 3663;

		private long _roleId;

		private readonly List<MemberTitleType.MTT> _titles = new List<MemberTitleType.MTT>();

		private readonly List<MemberTitleType.MTT> _myTitles = new List<MemberTitleType.MTT>();

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

		[ProtoMember(2, Name = "titles", DataFormat = DataFormat.TwosComplement)]
		public List<MemberTitleType.MTT> titles
		{
			get
			{
				return this._titles;
			}
		}

		[ProtoMember(3, Name = "myTitles", DataFormat = DataFormat.TwosComplement)]
		public List<MemberTitleType.MTT> myTitles
		{
			get
			{
				return this._myTitles;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
