using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1002), ForSend(1002), ProtoContract(Name = "RefuseGuildApplicantReq")]
	[Serializable]
	public class RefuseGuildApplicantReq : IExtensible
	{
		public static readonly short OP = 1002;

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
