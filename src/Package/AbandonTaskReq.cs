using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(5734), ForSend(5734), ProtoContract(Name = "AbandonTaskReq")]
	[Serializable]
	public class AbandonTaskReq : IExtensible
	{
		public static readonly short OP = 5734;

		private int _number;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "number", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int number
		{
			get
			{
				return this._number;
			}
			set
			{
				this._number = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
