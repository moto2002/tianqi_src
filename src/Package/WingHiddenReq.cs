using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(537), ForSend(537), ProtoContract(Name = "WingHiddenReq")]
	[Serializable]
	public class WingHiddenReq : IExtensible
	{
		public static readonly short OP = 537;

		private bool _hidden;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "hidden", DataFormat = DataFormat.Default)]
		public bool hidden
		{
			get
			{
				return this._hidden;
			}
			set
			{
				this._hidden = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
