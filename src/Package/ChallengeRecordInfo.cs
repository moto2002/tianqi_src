using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(796), ForSend(796), ProtoContract(Name = "ChallengeRecordInfo")]
	[Serializable]
	public class ChallengeRecordInfo : IExtensible
	{
		public static readonly short OP = 796;

		private readonly List<ChallengeRecord> _records = new List<ChallengeRecord>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "records", DataFormat = DataFormat.Default)]
		public List<ChallengeRecord> records
		{
			get
			{
				return this._records;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
