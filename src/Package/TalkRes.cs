using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(291), ForSend(291), ProtoContract(Name = "TalkRes")]
	[Serializable]
	public class TalkRes : IExtensible
	{
		public static readonly short OP = 291;

		private int _end_time;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "end_time", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int end_time
		{
			get
			{
				return this._end_time;
			}
			set
			{
				this._end_time = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
