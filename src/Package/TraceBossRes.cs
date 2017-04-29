using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(4486), ForSend(4486), ProtoContract(Name = "TraceBossRes")]
	[Serializable]
	public class TraceBossRes : IExtensible
	{
		public static readonly short OP = 4486;

		private int _labelId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "labelId", DataFormat = DataFormat.TwosComplement)]
		public int labelId
		{
			get
			{
				return this._labelId;
			}
			set
			{
				this._labelId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
