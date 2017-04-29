using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(363), ForSend(363), ProtoContract(Name = "OneCareer")]
	[Serializable]
	public class OneCareer : IExtensible
	{
		public static readonly short OP = 363;

		private int _dstCareer;

		private readonly List<CareerTask> _tasks = new List<CareerTask>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dstCareer", DataFormat = DataFormat.TwosComplement)]
		public int dstCareer
		{
			get
			{
				return this._dstCareer;
			}
			set
			{
				this._dstCareer = value;
			}
		}

		[ProtoMember(2, Name = "tasks", DataFormat = DataFormat.Default)]
		public List<CareerTask> tasks
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
