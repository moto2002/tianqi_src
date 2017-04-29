using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(914), ForSend(914), ProtoContract(Name = "GuildQuestionOpenNty")]
	[Serializable]
	public class GuildQuestionOpenNty : IExtensible
	{
		public static readonly short OP = 914;

		private bool _isOpen;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "isOpen", DataFormat = DataFormat.Default)]
		public bool isOpen
		{
			get
			{
				return this._isOpen;
			}
			set
			{
				this._isOpen = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
