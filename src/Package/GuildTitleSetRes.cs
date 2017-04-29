using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(3679), ForSend(3679), ProtoContract(Name = "GuildTitleSetRes")]
	[Serializable]
	public class GuildTitleSetRes : IExtensible
	{
		public static readonly short OP = 3679;

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
