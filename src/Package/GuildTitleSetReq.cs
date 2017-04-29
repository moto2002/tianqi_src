using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3678), ForSend(3678), ProtoContract(Name = "GuildTitleSetReq")]
	[Serializable]
	public class GuildTitleSetReq : IExtensible
	{
		public static readonly short OP = 3678;

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
