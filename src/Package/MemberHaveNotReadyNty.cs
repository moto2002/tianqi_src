using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(220), ForSend(220), ProtoContract(Name = "MemberHaveNotReadyNty")]
	[Serializable]
	public class MemberHaveNotReadyNty : IExtensible
	{
		public static readonly short OP = 220;

		private readonly List<long> _roleIds = new List<long>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "roleIds", DataFormat = DataFormat.TwosComplement)]
		public List<long> roleIds
		{
			get
			{
				return this._roleIds;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
