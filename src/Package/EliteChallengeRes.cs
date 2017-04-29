using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(3389), ForSend(3389), ProtoContract(Name = "EliteChallengeRes")]
	[Serializable]
	public class EliteChallengeRes : IExtensible
	{
		public static readonly short OP = 3389;

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
