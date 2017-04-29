using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1004), ForSend(1004), ProtoContract(Name = "RefuseGuildApplicantRes")]
	[Serializable]
	public class RefuseGuildApplicantRes : IExtensible
	{
		public static readonly short OP = 1004;

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
