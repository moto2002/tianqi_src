using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4041), ForSend(4041), ProtoContract(Name = "TeamStartFightAnswerRes")]
	[Serializable]
	public class TeamStartFightAnswerRes : IExtensible
	{
		public static readonly short OP = 4041;

		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
