using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleTimeInfo")]
	[Serializable]
	public class BattleTimeInfo : IExtensible
	{
		private int _battleBeginTick;

		private int _battleEndTick;

		private int _serverTick;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "battleBeginTick", DataFormat = DataFormat.TwosComplement)]
		public int battleBeginTick
		{
			get
			{
				return this._battleBeginTick;
			}
			set
			{
				this._battleBeginTick = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "battleEndTick", DataFormat = DataFormat.TwosComplement)]
		public int battleEndTick
		{
			get
			{
				return this._battleEndTick;
			}
			set
			{
				this._battleEndTick = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "serverTick", DataFormat = DataFormat.TwosComplement)]
		public int serverTick
		{
			get
			{
				return this._serverTick;
			}
			set
			{
				this._serverTick = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
