using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(821), ForSend(821), ProtoContract(Name = "ReplaceCurrTitleReq")]
	[Serializable]
	public class ReplaceCurrTitleReq : IExtensible
	{
		public static readonly short OP = 821;

		private int _titleId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "titleId", DataFormat = DataFormat.TwosComplement)]
		public int titleId
		{
			get
			{
				return this._titleId;
			}
			set
			{
				this._titleId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
