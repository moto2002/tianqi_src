using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3358), ForSend(3358), ProtoContract(Name = "GuildDataChangeNty")]
	[Serializable]
	public class GuildDataChangeNty : IExtensible
	{
		[ProtoContract(Name = "ChangeType")]
		public enum ChangeType
		{
			[ProtoEnum(Name = "InviteList", Value = 1)]
			InviteList = 1,
			[ProtoEnum(Name = "ApplicantList", Value = 2)]
			ApplicantList
		}

		public static readonly short OP = 3358;

		private GuildDataChangeNty.ChangeType _changeType;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "changeType", DataFormat = DataFormat.TwosComplement)]
		public GuildDataChangeNty.ChangeType changeType
		{
			get
			{
				return this._changeType;
			}
			set
			{
				this._changeType = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
