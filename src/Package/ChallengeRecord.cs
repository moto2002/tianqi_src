using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "ChallengeRecord")]
	[Serializable]
	public class ChallengeRecord : IExtensible
	{
		private long _fromId;

		private string _fromName;

		private long _toId;

		private string _toName;

		private long _winnerId;

		private int _score;

		private long _date;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "fromId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = true, Name = "fromName", DataFormat = DataFormat.Default)]
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

		[ProtoMember(3, IsRequired = true, Name = "toId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(4, IsRequired = true, Name = "toName", DataFormat = DataFormat.Default)]
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

		[ProtoMember(5, IsRequired = true, Name = "winnerId", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(6, IsRequired = true, Name = "score", DataFormat = DataFormat.TwosComplement)]
		public int score
		{
			get
			{
				return this._score;
			}
			set
			{
				this._score = value;
			}
		}

		[ProtoMember(7, IsRequired = true, Name = "date", DataFormat = DataFormat.TwosComplement)]
		public long date
		{
			get
			{
				return this._date;
			}
			set
			{
				this._date = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
