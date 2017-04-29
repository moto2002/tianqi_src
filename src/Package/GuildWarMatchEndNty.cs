using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(143), ForSend(143), ProtoContract(Name = "GuildWarMatchEndNty")]
	[Serializable]
	public class GuildWarMatchEndNty : IExtensible
	{
		public static readonly short OP = 143;

		private int _endUtc;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "endUtc", DataFormat = DataFormat.TwosComplement)]
		public int endUtc
		{
			get
			{
				return this._endUtc;
			}
			set
			{
				this._endUtc = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
