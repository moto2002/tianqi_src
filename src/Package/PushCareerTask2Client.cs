using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(5224), ForSend(5224), ProtoContract(Name = "PushCareerTask2Client")]
	[Serializable]
	public class PushCareerTask2Client : IExtensible
	{
		public static readonly short OP = 5224;

		private readonly List<OneCareer> _tasks = new List<OneCareer>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "tasks", DataFormat = DataFormat.Default)]
		public List<OneCareer> tasks
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
