using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(205), ForSend(205), ProtoContract(Name = "TeamInfoNty")]
	[Serializable]
	public class TeamInfoNty : IExtensible
	{
		public static readonly short OP = 205;

		private int _sectionId;

		private string _sectionName;

		private long _leaderId;

		private readonly List<MemberResume> _members = new List<MemberResume>();

		private ulong _teamId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "sectionId", DataFormat = DataFormat.TwosComplement)]
		public int sectionId
		{
			get
			{
				return this._sectionId;
			}
			set
			{
				this._sectionId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "sectionName", DataFormat = DataFormat.Default)]
		public string sectionName
		{
			get
			{
				return this._sectionName;
			}
			set
			{
				this._sectionName = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "leaderId", DataFormat = DataFormat.TwosComplement)]
		public long leaderId
		{
			get
			{
				return this._leaderId;
			}
			set
			{
				this._leaderId = value;
			}
		}

		[ProtoMember(4, Name = "members", DataFormat = DataFormat.Default)]
		public List<MemberResume> members
		{
			get
			{
				return this._members;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "teamId", DataFormat = DataFormat.TwosComplement), DefaultValue(0f)]
		public ulong teamId
		{
			get
			{
				return this._teamId;
			}
			set
			{
				this._teamId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
