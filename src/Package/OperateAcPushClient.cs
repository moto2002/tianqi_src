using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(7641), ForSend(7641), ProtoContract(Name = "OperateAcPushClient")]
	[Serializable]
	public class OperateAcPushClient : IExtensible
	{
		public static readonly short OP = 7641;

		private readonly List<Ac> _ac = new List<Ac>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "ac", DataFormat = DataFormat.Default)]
		public List<Ac> ac
		{
			get
			{
				return this._ac;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
