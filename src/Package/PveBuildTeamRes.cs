using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(436), ForSend(436), ProtoContract(Name = "PveBuildTeamRes")]
	[Serializable]
	public class PveBuildTeamRes : IExtensible
	{
		public static readonly short OP = 436;

		private readonly List<TeamDetailReason> _reasons = new List<TeamDetailReason>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "reasons", DataFormat = DataFormat.Default)]
		public List<TeamDetailReason> reasons
		{
			get
			{
				return this._reasons;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
