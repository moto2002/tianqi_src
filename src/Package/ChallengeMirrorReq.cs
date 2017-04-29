using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(580), ForSend(580), ProtoContract(Name = "ChallengeMirrorReq")]
	[Serializable]
	public class ChallengeMirrorReq : IExtensible
	{
		public static readonly short OP = 580;

		private int _taskId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "taskId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int taskId
		{
			get
			{
				return this._taskId;
			}
			set
			{
				this._taskId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
