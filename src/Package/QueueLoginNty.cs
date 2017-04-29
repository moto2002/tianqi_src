using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(171), ForSend(171), ProtoContract(Name = "QueueLoginNty")]
	[Serializable]
	public class QueueLoginNty : IExtensible
	{
		public static readonly short OP = 171;

		private int _sequent;

		private int _enterPerMinute;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "sequent", DataFormat = DataFormat.TwosComplement)]
		public int sequent
		{
			get
			{
				return this._sequent;
			}
			set
			{
				this._sequent = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "enterPerMinute", DataFormat = DataFormat.TwosComplement)]
		public int enterPerMinute
		{
			get
			{
				return this._enterPerMinute;
			}
			set
			{
				this._enterPerMinute = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
