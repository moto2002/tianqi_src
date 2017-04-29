using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(695), ForSend(695), ProtoContract(Name = "DrawMailAttachReq")]
	[Serializable]
	public class DrawMailAttachReq : IExtensible
	{
		public static readonly short OP = 695;

		private long _id;

		private int _pos = -1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "pos", DataFormat = DataFormat.TwosComplement), DefaultValue(-1)]
		public int pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
