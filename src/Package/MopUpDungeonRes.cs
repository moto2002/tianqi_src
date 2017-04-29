using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(650), ForSend(650), ProtoContract(Name = "MopUpDungeonRes")]
	[Serializable]
	public class MopUpDungeonRes : IExtensible
	{
		public static readonly short OP = 650;

		private readonly List<ChallengeResult> _results = new List<ChallengeResult>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "results", DataFormat = DataFormat.Default)]
		public List<ChallengeResult> results
		{
			get
			{
				return this._results;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
