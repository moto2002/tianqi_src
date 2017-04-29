using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3781), ForSend(3781), ProtoContract(Name = "TaskGroupAcceptRes")]
	[Serializable]
	public class TaskGroupAcceptRes : IExtensible
	{
		public static readonly short OP = 3781;

		private int _num;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
