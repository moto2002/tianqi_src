using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(933), ForSend(933), ProtoContract(Name = "QueryApplicantInfoRes")]
	[Serializable]
	public class QueryApplicantInfoRes : IExtensible
	{
		public static readonly short OP = 933;

		private readonly List<ApplicantInfo> _applicants = new List<ApplicantInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "applicants", DataFormat = DataFormat.Default)]
		public List<ApplicantInfo> applicants
		{
			get
			{
				return this._applicants;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
