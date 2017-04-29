using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(976), ForSend(976), ProtoContract(Name = "AcceptGuildApplicantReq")]
	[Serializable]
	public class AcceptGuildApplicantReq : IExtensible
	{
		public static readonly short OP = 976;

		private readonly List<long> _roleId = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "roleId", DataFormat = DataFormat.TwosComplement)]
		public List<long> roleId
		{
			get
			{
				return this._roleId;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
