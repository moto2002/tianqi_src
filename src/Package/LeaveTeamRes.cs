using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(489), ForSend(489), ProtoContract(Name = "LeaveTeamRes")]
	[Serializable]
	public class LeaveTeamRes : IExtensible
	{
		public static readonly short OP = 489;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
