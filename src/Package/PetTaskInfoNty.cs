using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(202), ForSend(202), ProtoContract(Name = "PetTaskInfoNty")]
	[Serializable]
	public class PetTaskInfoNty : IExtensible
	{
		public static readonly short OP = 202;

		private readonly List<PetTaskInfo> _tasks = new List<PetTaskInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "tasks", DataFormat = DataFormat.Default)]
		public List<PetTaskInfo> tasks
		{
			get
			{
				return this._tasks;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
