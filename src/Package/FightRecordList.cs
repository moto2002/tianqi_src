using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(647), ForSend(647), ProtoContract(Name = "FightRecordList")]
	[Serializable]
	public class FightRecordList : IExtensible
	{
		public static readonly short OP = 647;

		private readonly List<FightRecord> _records = new List<FightRecord>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "records", DataFormat = DataFormat.Default)]
		public List<FightRecord> records
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
