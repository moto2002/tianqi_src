using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2105), ForSend(2105), ProtoContract(Name = "DarkTrainAutoMatchRes")]
	[Serializable]
	public class DarkTrainAutoMatchRes : IExtensible
	{
		public static readonly short OP = 2105;

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
