using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "FightRecord")]
	[Serializable]
	public class FightRecord : IExtensible
	{
		private string _time;

		private long _fromId;

		private string _fromName;

		private int _fromWinCount;

		private long _toId;

		private string _toName;

		private int _toWinCount;

		private long _winnerId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "time", DataFormat = DataFormat.Default)]
		public string time
		{
			get
			{
				return this._time;
			}
			set
			{
				this._time = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "fromId", DataFormat = DataFormat.TwosComplement)]
		public long fromId
		{
			get
			{
				return this._fromId;
			}
			set
			{
				this._fromId = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "fromName", DataFormat = DataFormat.Default)]
		public string fromName
		{
			get
			{
				return this._fromName;
			}
			set
			{
				this._fromName = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "fromWinCount", DataFormat = DataFormat.TwosComplement)]
		public int fromWinCount
		{
			get
			{
				return this._fromWinCount;
			}
			set
			{
				this._fromWinCount = value;
			}
		}

		[ProtoMember(5, IsRequired = true, Name = "toId", DataFormat = DataFormat.TwosComplement)]
		public long toId
		{
			get
			{
				return this._toId;
			}
			set
			{
				this._toId = value;
			}
		}

		[ProtoMember(6, IsRequired = true, Name = "toName", DataFormat = DataFormat.Default)]
		public string toName
		{
			get
			{
				return this._toName;
			}
			set
			{
				this._toName = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "toWinCount", DataFormat = DataFormat.TwosComplement)]
		public int toWinCount
		{
			get
			{
				return this._toWinCount;
			}
			set
			{
				this._toWinCount = value;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "winnerId", DataFormat = DataFormat.TwosComplement)]
		public long winnerId
		{
			get
			{
				return this._winnerId;
			}
			set
			{
				this._winnerId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
