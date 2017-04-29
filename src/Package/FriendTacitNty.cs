using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(219), ForSend(219), ProtoContract(Name = "FriendTacitNty")]
	[Serializable]
	public class FriendTacitNty : IExtensible
	{
		public static readonly short OP = 219;

		private int _level;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "level", DataFormat = DataFormat.TwosComplement)]
		public int level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
