using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(195), ForSend(195), ProtoContract(Name = "MultiPveBestRecordInfoNty")]
	[Serializable]
	public class MultiPveBestRecordInfoNty : IExtensible
	{
		[ProtoContract(Name = "BestRecordInfo")]
		[Serializable]
		public class BestRecordInfo : IExtensible
		{
			private int _dungeonId;

			private int _passTime;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = false, Name = "dungeonId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int dungeonId
			{
				get
				{
					return this._dungeonId;
				}
				set
				{
					this._dungeonId = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "passTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int passTime
			{
				get
				{
					return this._passTime;
				}
				set
				{
					this._passTime = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 195;

		private readonly List<MultiPveBestRecordInfoNty.BestRecordInfo> _records = new List<MultiPveBestRecordInfoNty.BestRecordInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "records", DataFormat = DataFormat.Default)]
		public List<MultiPveBestRecordInfoNty.BestRecordInfo> records
		{
			get
			{
				return this._records;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
