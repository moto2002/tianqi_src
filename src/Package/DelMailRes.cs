using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3776), ForSend(3776), ProtoContract(Name = "DelMailRes")]
	[Serializable]
	public class DelMailRes : IExtensible
	{
		public static readonly short OP = 3776;

		private long _id;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "id", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
