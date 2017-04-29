using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(230), ForSend(230), ProtoContract(Name = "GuildWarEnterDungeonNty")]
	[Serializable]
	public class GuildWarEnterDungeonNty : IExtensible
	{
		public static readonly short OP = 230;

		private int _buffId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "buffId", DataFormat = DataFormat.TwosComplement)]
		public int buffId
		{
			get
			{
				return this._buffId;
			}
			set
			{
				this._buffId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
