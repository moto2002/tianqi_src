using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(617), ForSend(617), ProtoContract(Name = "WildBossCityDropNty")]
	[Serializable]
	public class WildBossCityDropNty : IExtensible
	{
		public static readonly short OP = 617;

		private int _dropCount;

		private Pos _pos;

		private int _bossCode;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dropCount", DataFormat = DataFormat.TwosComplement)]
		public int dropCount
		{
			get
			{
				return this._dropCount;
			}
			set
			{
				this._dropCount = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
		public Pos pos
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

		[ProtoMember(3, IsRequired = true, Name = "bossCode", DataFormat = DataFormat.TwosComplement)]
		public int bossCode
		{
			get
			{
				return this._bossCode;
			}
			set
			{
				this._bossCode = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
